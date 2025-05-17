using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TripBookingRevenueReport
{
    public partial class TripReportForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

        public TripReportForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Trip Booking & Revenue Report";
            this.Size = new Size(1000, 700);

            // Main TabControl
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Tab Pages
            TabPage bookingsTab = new TabPage("Bookings Overview");
            TabPage revenueTab = new TabPage("Revenue Analysis");
            TabPage cancellationsTab = new TabPage("Cancellations");
            TabPage seasonalityTab = new TabPage("Seasonal Trends");

            // Add charts to tabs
            bookingsTab.Controls.Add(CreateTotalBookingsChart());
            revenueTab.Controls.Add(CreateRevenueChart());
            cancellationsTab.Controls.Add(CreateCancellationChart());
            seasonalityTab.Controls.Add(CreateSeasonalityChart());

            // Add tabs to control
            tabControl.TabPages.Add(bookingsTab);
            tabControl.TabPages.Add(revenueTab);
            tabControl.TabPages.Add(cancellationsTab);
            tabControl.TabPages.Add(seasonalityTab);

            this.Controls.Add(tabControl);
        }

        private Chart CreateTotalBookingsChart()
        {
            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;
            chart.Titles.Add("Total Bookings by Trip Type");

            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            Series series = new Series
            {
                Name = "Bookings",
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            chart.Series.Add(series);

            // Data will be loaded in LoadData method
            return chart;
        }

        private Chart CreateRevenueChart()
        {
            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;

            // Split into two chart areas
            ChartArea categoryArea = new ChartArea("CategoryArea");
            ChartArea durationArea = new ChartArea("DurationArea");
            categoryArea.Position.X = 0;
            categoryArea.Position.Y = 0;
            categoryArea.Position.Width = 50;
            categoryArea.Position.Height = 100;
            durationArea.Position.X = 50;
            durationArea.Position.Y = 0;
            durationArea.Position.Width = 50;
            durationArea.Position.Height = 100;

            chart.ChartAreas.Add(categoryArea);
            chart.ChartAreas.Add(durationArea);

            chart.Titles.Add(new Title("Revenue by Category", Docking.Top));

            // Series for Trip Type
            Series typeSeries = new Series
            {
                Name = "TripType",
                ChartArea = "CategoryArea",
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };

            // Series for Duration
            Series durationSeries = new Series
            {
                Name = "Duration",
                ChartArea = "DurationArea",
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };

            chart.Series.Add(typeSeries);
            chart.Series.Add(durationSeries);

            // Legend for both pie charts
            Legend legend = new Legend("SharedLegend");
            chart.Legends.Add(legend);
            typeSeries.Legend = "SharedLegend";
            durationSeries.Legend = "SharedLegend";

            return chart;
        }

        private Chart CreateCancellationChart()
        {
            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;
            chart.Titles.Add("Cancellation Analysis");

            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            // Cancellation rate series
            Series cancellationRateSeries = new Series
            {
                Name = "Cancellation Rate",
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Color = Color.Firebrick,
                YAxisType = AxisType.Primary
            };

            // Cancellation reason series
            Series cancellationReasonSeries = new Series
            {
                Name = "Cancellation Reasons",
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };

            chart.Series.Add(cancellationRateSeries);
            chart.Series.Add(cancellationReasonSeries);

            // Position the pie chart in a panel
            Panel piePanel = new Panel();
            piePanel.Dock = DockStyle.Right;
            piePanel.Width = 300;
            Chart reasonsChart = new Chart();
            reasonsChart.Dock = DockStyle.Fill;
            reasonsChart.Series.Add(cancellationReasonSeries);
            piePanel.Controls.Add(reasonsChart);

            return chart;
        }

        private Chart CreateSeasonalityChart()
        {
            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;
            chart.Titles.Add("Peak Booking Periods & Average Booking Value");

            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            // Bookings by month series
            Series bookingsSeries = new Series
            {
                Name = "Monthly Bookings",
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Color = Color.SteelBlue,
                YAxisType = AxisType.Primary
            };

            // Average booking value series
            Series avgValueSeries = new Series
            {
                Name = "Avg. Booking Value",
                ChartType = SeriesChartType.Line,
                IsValueShownAsLabel = true,
                Color = Color.DarkGreen,
                YAxisType = AxisType.Secondary,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8
            };

            chart.Series.Add(bookingsSeries);
            chart.Series.Add(avgValueSeries);

            // Secondary Y-axis for average booking value
            chartArea.AxisY2.Enabled = AxisEnabled.True;
            chartArea.AxisY2.Title = "Average Booking Value ($)";
            chartArea.AxisY.Title = "Number of Bookings";
            chartArea.AxisX.Title = "Month";

            return chart;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Load Total Bookings by Trip Type
                    LoadTotalBookingsByTripType(connection);

                    // Load Revenue by Category
                    LoadRevenueByCategory(connection);

                    // Load Cancellation Data
                    LoadCancellationData(connection);

                    // Load Seasonality Data
                    LoadSeasonalityData(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTotalBookingsByTripType(SqlConnection connection)
        {
            string query = @"
                SELECT t.trip_type, COUNT(b.booking_id) AS total_bookings
                FROM Booking b
                JOIN Trip t ON b.trip_id = t.trip_id
                WHERE b.Booking_status = 1
                GROUP BY t.trip_type
                ORDER BY total_bookings DESC";

            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                Chart bookingsChart = (Chart)Controls[0].Controls[0].Controls[0];
                Series bookingsSeries = bookingsChart.Series[0];
                bookingsSeries.Points.Clear();

                while (reader.Read())
                {
                    string tripType = reader["trip_type"].ToString();
                    int bookingsCount = Convert.ToInt32(reader["total_bookings"]);
                    bookingsSeries.Points.AddXY(tripType, bookingsCount);
                }
            }
        }

        private void LoadRevenueByCategory(SqlConnection connection)
        {
            // Revenue by Trip Type
            string tripTypeQuery = @"
                SELECT t.trip_type, SUM(b.total_amount) AS total_revenue
                FROM Booking b
                JOIN Trip t ON b.trip_id = t.trip_id
                WHERE b.Booking_status = 1
                GROUP BY t.trip_type
                ORDER BY total_revenue DESC";

            Chart revenueChart = (Chart)Controls[0].Controls[1].Controls[0];
            Series typeSeries = revenueChart.Series[0];
            typeSeries.Points.Clear();

            using (SqlCommand command = new SqlCommand(tripTypeQuery, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tripType = reader["trip_type"].ToString();
                    decimal revenue = Convert.ToDecimal(reader["total_revenue"]);
                    int pointIndex = typeSeries.Points.AddXY(tripType, revenue);
                    typeSeries.Points[pointIndex].LegendText = $"{tripType} (${revenue:N0})";
                }
            }

            // Revenue by Duration
            string durationQuery = @"
                SELECT 
                    CASE 
                        WHEN t.duration = 1 THEN '1-Day'
                        WHEN t.duration <= 4 THEN '2-4 Days'
                        WHEN t.duration <= 7 THEN '5-7 Days'
                        ELSE '8+ Days'
                    END AS duration_category,
                    SUM(b.total_amount) AS total_revenue
                FROM Booking b
                JOIN Trip t ON b.trip_id = t.trip_id
                WHERE b.Booking_status = 1
                GROUP BY CASE 
                            WHEN t.duration = 1 THEN '1-Day'
                            WHEN t.duration <= 4 THEN '2-4 Days'
                            WHEN t.duration <= 7 THEN '5-7 Days'
                            ELSE '8+ Days'
                         END
                ORDER BY total_revenue DESC";

            Series durationSeries = revenueChart.Series[1];
            durationSeries.Points.Clear();

            using (SqlCommand command = new SqlCommand(durationQuery, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string duration = reader["duration_category"].ToString();
                    decimal revenue = Convert.ToDecimal(reader["total_revenue"]);
                    int pointIndex = durationSeries.Points.AddXY(duration, revenue);
                    durationSeries.Points[pointIndex].LegendText = $"{duration} (${revenue:N0})";
                }
            }
        }

        private void LoadCancellationData(SqlConnection connection)
        {
            // Cancellation rate by trip type
            string cancellationRateQuery = @"
                SELECT 
                    t.trip_type,
                    CAST(SUM(CASE WHEN b.Booking_status = 0 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(b.booking_id) * 100 AS cancellation_rate
                FROM Booking b
                JOIN Trip t ON b.trip_id = t.trip_id
                GROUP BY t.trip_type
                HAVING COUNT(b.booking_id) > 0";

            Chart cancellationChart = (Chart)Controls[0].Controls[2].Controls[0];
            Series cancellationRateSeries = cancellationChart.Series[0];
            cancellationRateSeries.Points.Clear();

            using (SqlCommand command = new SqlCommand(cancellationRateQuery, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tripType = reader["trip_type"].ToString();
                    double cancellationRate = Convert.ToDouble(reader["cancellation_rate"]);
                    cancellationRateSeries.Points.AddXY(tripType, cancellationRate);
                }
            }

            // Cancellation reasons
            string cancellationReasonQuery = @"
                SELECT 
                    cancellation_reason,
                    COUNT(*) AS reason_count
                FROM Booking
                WHERE Booking_status = 0 AND cancellation_reason IS NOT NULL
                GROUP BY cancellation_reason
                ORDER BY reason_count DESC";

            Series cancellationReasonSeries = cancellationChart.Series[1];
            cancellationReasonSeries.Points.Clear();

            using (SqlCommand command = new SqlCommand(cancellationReasonQuery, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string reason = reader["cancellation_reason"].ToString();
                    int count = Convert.ToInt32(reader["reason_count"]);
                    cancellationReasonSeries.Points.AddXY(reason, count);
                }
            }
        }

        private void LoadSeasonalityData(SqlConnection connection)
        {
            // Bookings by month
            string seasonalityQuery = @"
                SELECT 
                    MONTH(booking_timestamp) AS booking_month,
                    COUNT(*) AS booking_count,
                    AVG(total_amount) AS avg_booking_value
                FROM Booking
                WHERE Booking_status = 1
                GROUP BY MONTH(booking_timestamp)
                ORDER BY booking_month";

            Chart seasonalityChart = (Chart)Controls[0].Controls[3].Controls[0];
            Series bookingsSeries = seasonalityChart.Series[0];
            Series avgValueSeries = seasonalityChart.Series[1];
            bookingsSeries.Points.Clear();
            avgValueSeries.Points.Clear();

            using (SqlCommand command = new SqlCommand(seasonalityQuery, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int month = Convert.ToInt32(reader["booking_month"]);
                    string monthName = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                    int bookingCount = Convert.ToInt32(reader["booking_count"]);
                    double avgValue = Convert.ToDouble(reader["avg_booking_value"]);

                    bookingsSeries.Points.AddXY(monthName, bookingCount);
                    avgValueSeries.Points.AddXY(monthName, avgValue);
                }
            }
        }
    }
}