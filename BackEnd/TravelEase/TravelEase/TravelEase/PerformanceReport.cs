using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TravelEase
{
    public partial class PerformanceReportForm : Form
    {
        private string connectionString;
        int providerId;
        public PerformanceReportForm(int providerId, string conns)
        {
            InitializeComponent();
            this.providerId = providerId;
            this.Load += PerformanceReportForm_Load;
            this.connectionString = conns;
        }

        private void PerformanceReportForm_Load(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void LoadReportData()
        {
            try
            {
                LoadOccupancyRateChart();
                LoadServicePerformanceChart();
                LoadMonthlyRevenueChart();
                LoadRevenueByTripTypeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Implementation 1: Occupancy Rate Chart
        private void LoadOccupancyRateChart()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // First, let's check if we have any bookings for this provider at all
                string testQuery = @"
            SELECT COUNT(*) 
            FROM ServiceProvider 
            WHERE provider_id = @ProviderId";

                using (SqlCommand testCommand = new SqlCommand(testQuery, connection))
                {
                    testCommand.Parameters.AddWithValue("@ProviderId", providerId);
                    int providerExists = (int)testCommand.ExecuteScalar();

                    if (providerExists == 0)
                    {
                        // Provider doesn't exist, show message and return
                        MessageBox.Show("No data found for this provider ID.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // Simpler query with fewer joins, focusing only on bookings and provider connection
                string query = @"
            SELECT 
                MONTH(b.booking_date) AS month_num,
                DATENAME(MONTH, b.booking_date) AS month_name,
                COUNT(b.booking_id) AS total_bookings
            FROM Booking b
            JOIN BookingServices_ bs ON b.booking_id = bs.booking_id
            WHERE bs.provider_id = @ProviderId
            GROUP BY MONTH(b.booking_date), DATENAME(MONTH, b.booking_date)
            ORDER BY month_num";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProviderId", providerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        // No bookings found for this provider
                        MessageBox.Show("No booking data found for this provider ID.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Create empty chart with months of the year
                        CreateEmptyChart();
                        return;
                    }

                    // Clear existing series
                    chartOccupancy.Series.Clear();
                    chartOccupancy.Titles.Clear();

                    // Create new series
                    Series bookingSeries = new Series
                    {
                        Name = "Monthly Bookings",
                        ChartType = SeriesChartType.Column,
                        Color = Color.FromArgb(52, 152, 219)
                    };

                    // Add data points from the query results
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string month = row["month_name"].ToString();
                        int bookingCount = Convert.ToInt32(row["total_bookings"]);
                        bookingSeries.Points.AddXY(month, bookingCount);
                    }

                    // Add series to chart
                    chartOccupancy.Series.Add(bookingSeries);

                    // Configure chart appearance
                    chartOccupancy.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                    chartOccupancy.ChartAreas[0].AxisX.Title = "Month";
                    chartOccupancy.ChartAreas[0].AxisY.Title = "Number of Bookings";
                    chartOccupancy.ChartAreas[0].AxisY.Minimum = 0;

                    // Set appropriate Y-axis maximum with some headroom
                    int maxBookings = dataTable.AsEnumerable()
                        .Max(row => Convert.ToInt32(row["total_bookings"]));
                    chartOccupancy.ChartAreas[0].AxisY.Maximum = Math.Max(5, Math.Ceiling(maxBookings * 1.2)); // At least 5, or 20% headroom

                    // Add title
                    Title title = new Title(
                        "Monthly Booking Volume for Provider",
                        Docking.Top,
                        new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                        System.Drawing.Color.Black
                    );
                    chartOccupancy.Titles.Add(title);
                }
            }
        }

    // Helper method to create an empty chart with all months
    private void CreateEmptyChart()
        {
            // Clear existing series
            chartOccupancy.Series.Clear();
            chartOccupancy.Titles.Clear();

            // Create new series
            Series emptySeries = new Series
            {
                Name = "Monthly Bookings",
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(200, 200, 200) // Light gray for empty data
            };

            // Add months of the year
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            foreach (string month in months)
            {
                emptySeries.Points.AddXY(month, 0);
            }

            // Add series to chart
            chartOccupancy.Series.Add(emptySeries);

            // Configure chart appearance
            chartOccupancy.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartOccupancy.ChartAreas[0].AxisX.Title = "Month";
            chartOccupancy.ChartAreas[0].AxisY.Title = "Number of Bookings";
            chartOccupancy.ChartAreas[0].AxisY.Minimum = 0;
            chartOccupancy.ChartAreas[0].AxisY.Maximum = 5; // Small max value since there's no data

            // Add title
            Title title = new Title(
                "No Booking Data Available for Provider",
                Docking.Top,
                new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                System.Drawing.Color.Black
            );
            chartOccupancy.Titles.Add(title);
        }


        // Implementation 2: Service Performance Chart
        private void LoadServicePerformanceChart()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        sp.service_type,
                        AVG(CAST(r.rating AS FLOAT)) AS AverageRating,
                        COUNT(r.review_id) AS ReviewCount
                    FROM ServiceProvider sp
                    INNER JOIN ProviderAssignment pa ON sp.provider_id = pa.provider_id
                    INNER JOIN ServiceAssignment sa ON pa.assignment_id = sa.assignment_id
                    INNER JOIN is_assigned ia ON sa.assignment_id = ia.assignment_id
                    INNER JOIN Trip t ON ia.trip_id = t.trip_id
                    INNER JOIN Review r ON t.trip_id = r.trip_id
                    WHERE sp.provider_id = @ProviderId
                    GROUP BY sp.service_type";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProviderId", providerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Clear existing series
                    chartServices.Series.Clear();

                    // Primary series for rating
                    Series ratingSeries = new Series
                    {
                        Name = "Average Rating",
                        ChartType = SeriesChartType.Column,
                        Color = Color.FromArgb(52, 152, 219),
                        YAxisType = AxisType.Primary
                    };

                    // Secondary series for review count
                    Series countSeries = new Series
                    {
                        Name = "Review Count",
                        ChartType = SeriesChartType.Line,
                        Color = Color.FromArgb(231, 76, 60),
                        YAxisType = AxisType.Secondary,
                        MarkerStyle = MarkerStyle.Circle,
                        MarkerSize = 8
                    };

                    // Add data points
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string serviceType = row["service_type"].ToString();
                        double avgRating = Convert.ToDouble(row["AverageRating"]);
                        int reviewCount = Convert.ToInt32(row["ReviewCount"]);

                        ratingSeries.Points.AddXY(serviceType, avgRating);
                        countSeries.Points.AddXY(serviceType, reviewCount);
                    }

                    // Configure chart
                    chartServices.Series.Add(ratingSeries);
                    chartServices.Series.Add(countSeries);

                    // Set up secondary Y-axis
                    chartServices.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;

                    // Configure axes
                    chartServices.ChartAreas[0].AxisX.Title = "Service Type";
                    chartServices.ChartAreas[0].AxisY.Title = "Average Rating";
                    chartServices.ChartAreas[0].AxisY.Maximum = 5;
                    chartServices.ChartAreas[0].AxisY.Minimum = 0;
                    chartServices.ChartAreas[0].AxisY2.Title = "Number of Reviews";


                    // Add chart title
                    // Add chart title
                    System.Windows.Forms.DataVisualization.Charting.Title title = new System.Windows.Forms.DataVisualization.Charting.Title(
                        "Service Performance Ratings",
                        System.Windows.Forms.DataVisualization.Charting.Docking.Top,
                        new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                        System.Drawing.Color.Black
                    );
                    chartServices.Titles.Add(title);

                    // Add legend
                    chartServices.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend("ServicesLegend"));
                }
            }
        }

        // Implementation 3: Monthly Revenue Chart
        private void LoadMonthlyRevenueChart()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                    FORMAT(b.booking_date, 'MMM yyyy') AS Month,
                    SUM(p.amount) AS Revenue
                FROM Booking b
                INNER JOIN Payment p ON b.payment_id = p.payment_id
                INNER JOIN BookingServices_ bs ON b.booking_id = bs.booking_id
                WHERE bs.provider_id = @ProviderId
                  AND p.payment_status = 'Completed'
                GROUP BY FORMAT(b.booking_date, 'MMM yyyy')
                ORDER BY MIN(b.booking_date)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProviderId", providerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Clear existing series
                    chartRevenue.Series.Clear();

                    // Create new series
                    Series revenueSeries = new Series
                    {
                        Name = "Monthly Revenue",
                        ChartType = SeriesChartType.Spline,
                        Color = Color.FromArgb(46, 204, 113),
                        BorderWidth = 3,
                        MarkerStyle = MarkerStyle.Circle,
                        MarkerSize = 8
                    };

                    // Add data points
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string month = row["Month"].ToString();
                        decimal revenue = Convert.ToDecimal(row["Revenue"]);

                        revenueSeries.Points.AddXY(month, revenue);
                    }

                    // Add series to chart
                    chartRevenue.Series.Add(revenueSeries);

                    // Configure chart
                    chartRevenue.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                    chartRevenue.ChartAreas[0].AxisX.Title = "Month";
                    chartRevenue.ChartAreas[0].AxisY.Title = "Revenue ($)";
                    chartRevenue.ChartAreas[0].AxisY.LabelStyle.Format = "${0:#,##0}";

                    // Add chart title
                    System.Windows.Forms.DataVisualization.Charting.Title title = new System.Windows.Forms.DataVisualization.Charting.Title(
                        "Monthly Revenue",
                        System.Windows.Forms.DataVisualization.Charting.Docking.Top,
                        new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                        System.Drawing.Color.Black
                    );
                    chartRevenue.Titles.Add(title);


                    // Add trendline
                    Series trendSeries = chartRevenue.Series.Add("Trend");
                    trendSeries.ChartType = SeriesChartType.Line;
                    trendSeries.Color = Color.FromArgb(155, 89, 182);

                    // Calculate and add trendline if there are enough data points
                    if (dataTable.Rows.Count >= 2)
                    {
                        double[] xValues = new double[dataTable.Rows.Count];
                        double[] yValues = new double[dataTable.Rows.Count];

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            xValues[i] = i;
                            yValues[i] = Convert.ToDouble(dataTable.Rows[i]["Revenue"]);
                        }

                        // Simple linear regression
                        double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;
                        for (int i = 0; i < xValues.Length; i++)
                        {
                            sumX += xValues[i];
                            sumY += yValues[i];
                            sumXY += xValues[i] * yValues[i];
                            sumX2 += xValues[i] * xValues[i];
                        }

                        double n = xValues.Length;
                        double slope = (n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX);
                        double intercept = (sumY - slope * sumX) / n;

                        // Add trendline points
                        for (int i = 0; i < xValues.Length; i++)
                        {
                            double y = slope * xValues[i] + intercept;
                            trendSeries.Points.AddXY(dataTable.Rows[i]["Month"], y);
                        }
                    }
                }
            }
        }

        // Implementation 4: Revenue by Trip Type Chart
        private void LoadRevenueByTripTypeData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                    t.trip_type,
                    SUM(p.amount) AS Revenue,
                    COUNT(b.booking_id) AS BookingCount
                FROM Booking b
                INNER JOIN Payment p ON b.payment_id = p.payment_id
                INNER JOIN Trip t ON b.trip_id = t.trip_id
                INNER JOIN BookingServices_ bs ON b.booking_id = bs.booking_id
                WHERE bs.provider_id = @ProviderId 
                  AND p.payment_status = 'Completed'
                GROUP BY t.trip_type";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProviderId", providerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Clear existing series
                    chartTripTypes.Series.Clear();

                    // Create pie chart series
                    // Create pie chart series
                    Series revenueSeries = new Series
                    {
                        Name = "Revenue by Trip Type",
                        ChartType = SeriesChartType.Pie,
                        IsValueShownAsLabel = true,
                        LabelFormat = "${0:#,##0}",
                        Font = new System.Drawing.Font("Segoe UI", 10)
                    };


                    // Add data points and custom colors
                    Color[] customColors = new Color[] {
                        Color.FromArgb(52, 152, 219),  // Blue
                        Color.FromArgb(46, 204, 113),  // Green
                        Color.FromArgb(155, 89, 182),  // Purple
                        Color.FromArgb(230, 126, 34),  // Orange
                        Color.FromArgb(231, 76, 60),   // Red
                        Color.FromArgb(241, 196, 15)   // Yellow
                    };

                    int colorIndex = 0;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        string tripType = row["trip_type"].ToString();
                        decimal revenue = Convert.ToDecimal(row["Revenue"]);

                        DataPoint point = new DataPoint();
                        point.AxisLabel = tripType;
                        point.YValues = new double[] { Convert.ToDouble(revenue) };
                        point.Label = $"{tripType}: ${revenue:#,##0}";

                        // Set custom color
                        point.Color = customColors[colorIndex % customColors.Length];
                        colorIndex++;

                        // Add to series
                        revenueSeries.Points.Add(point);
                    }

                    // Add series to chart
                    chartTripTypes.Series.Add(revenueSeries);

                    // Configure chart
                    chartTripTypes.ChartAreas[0].Area3DStyle.Enable3D = true;
                    chartTripTypes.ChartAreas[0].Area3DStyle.Inclination = 30;

                    // Add chart title
                    Title title = new Title("Revenue by Trip Type", Docking.Top, new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold), Color.Black);
                    chartTripTypes.Titles.Add(title);


                    // Add legend
                    chartTripTypes.Legends.Add(new Legend("TripTypesLegend"));
                    chartTripTypes.Series[0].Legend = "TripTypesLegend";
                }
            }
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControlReports = new System.Windows.Forms.TabControl();
            this.tabOccupancy = new System.Windows.Forms.TabPage();
            this.chartOccupancy = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabServices = new System.Windows.Forms.TabPage();
            this.chartServices = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabRevenue = new System.Windows.Forms.TabPage();
            this.chartRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabTripTypes = new System.Windows.Forms.TabPage();
            this.chartTripTypes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.tabControlReports.SuspendLayout();
            this.tabOccupancy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartOccupancy)).BeginInit();
            this.tabServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartServices)).BeginInit();
            this.tabRevenue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).BeginInit();
            this.tabTripTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTripTypes)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 60);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(246, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Performance Report";
            // 
            // tabControlReports
            // 
            this.tabControlReports.Controls.Add(this.tabOccupancy);
            this.tabControlReports.Controls.Add(this.tabServices);
            this.tabControlReports.Controls.Add(this.tabRevenue);
            this.tabControlReports.Controls.Add(this.tabTripTypes);
            this.tabControlReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlReports.Location = new System.Drawing.Point(0, 60);
            this.tabControlReports.Name = "tabControlReports";
            this.tabControlReports.SelectedIndex = 0;
            this.tabControlReports.Size = new System.Drawing.Size(800, 420);
            this.tabControlReports.TabIndex = 0;
            // 
            // tabOccupancy
            // 
            this.tabOccupancy.Controls.Add(this.chartOccupancy);
            this.tabOccupancy.Location = new System.Drawing.Point(4, 25);
            this.tabOccupancy.Name = "tabOccupancy";
            this.tabOccupancy.Padding = new System.Windows.Forms.Padding(3);
            this.tabOccupancy.Size = new System.Drawing.Size(792, 391);
            this.tabOccupancy.TabIndex = 0;
            this.tabOccupancy.Text = "Occupancy Rate";
            this.tabOccupancy.UseVisualStyleBackColor = true;
            // 
            // chartOccupancy
            // 
            chartArea1.Name = "ChartArea1";
            this.chartOccupancy.ChartAreas.Add(chartArea1);
            this.chartOccupancy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartOccupancy.Location = new System.Drawing.Point(3, 3);
            this.chartOccupancy.Name = "chartOccupancy";
            this.chartOccupancy.Size = new System.Drawing.Size(786, 385);
            this.chartOccupancy.TabIndex = 0;
            this.chartOccupancy.Click += new System.EventHandler(this.chartOccupancy_Click);
            // 
            // tabServices
            // 
            this.tabServices.Controls.Add(this.chartServices);
            this.tabServices.Location = new System.Drawing.Point(4, 25);
            this.tabServices.Name = "tabServices";
            this.tabServices.Padding = new System.Windows.Forms.Padding(3);
            this.tabServices.Size = new System.Drawing.Size(792, 391);
            this.tabServices.TabIndex = 1;
            this.tabServices.Text = "Service Performance";
            this.tabServices.UseVisualStyleBackColor = true;
            // 
            // chartServices
            // 
            chartArea2.Name = "ChartArea1";
            this.chartServices.ChartAreas.Add(chartArea2);
            this.chartServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartServices.Location = new System.Drawing.Point(3, 3);
            this.chartServices.Name = "chartServices";
            this.chartServices.Size = new System.Drawing.Size(786, 385);
            this.chartServices.TabIndex = 0;
            this.chartServices.Click += new System.EventHandler(this.chartServices_Click);
            // 
            // tabRevenue
            // 
            this.tabRevenue.Controls.Add(this.chartRevenue);
            this.tabRevenue.Location = new System.Drawing.Point(4, 25);
            this.tabRevenue.Name = "tabRevenue";
            this.tabRevenue.Padding = new System.Windows.Forms.Padding(3);
            this.tabRevenue.Size = new System.Drawing.Size(792, 391);
            this.tabRevenue.TabIndex = 2;
            this.tabRevenue.Text = "Monthly Revenue";
            this.tabRevenue.UseVisualStyleBackColor = true;
            // 
            // chartRevenue
            // 
            chartArea3.Name = "ChartArea1";
            this.chartRevenue.ChartAreas.Add(chartArea3);
            this.chartRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartRevenue.Location = new System.Drawing.Point(3, 3);
            this.chartRevenue.Name = "chartRevenue";
            this.chartRevenue.Size = new System.Drawing.Size(786, 385);
            this.chartRevenue.TabIndex = 0;
            // 
            // tabTripTypes
            // 
            this.tabTripTypes.Controls.Add(this.chartTripTypes);
            this.tabTripTypes.Location = new System.Drawing.Point(4, 25);
            this.tabTripTypes.Name = "tabTripTypes";
            this.tabTripTypes.Padding = new System.Windows.Forms.Padding(3);
            this.tabTripTypes.Size = new System.Drawing.Size(792, 391);
            this.tabTripTypes.TabIndex = 3;
            this.tabTripTypes.Text = "Revenue by Trip Type";
            this.tabTripTypes.UseVisualStyleBackColor = true;
            // 
            // chartTripTypes
            // 
            chartArea4.Name = "ChartArea1";
            this.chartTripTypes.ChartAreas.Add(chartArea4);
            this.chartTripTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTripTypes.Location = new System.Drawing.Point(3, 3);
            this.chartTripTypes.Name = "chartTripTypes";
            this.chartTripTypes.Size = new System.Drawing.Size(786, 385);
            this.chartTripTypes.TabIndex = 0;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlFooter.Controls.Add(this.btnExport);
            this.pnlFooter.Controls.Add(this.btnClose);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 480);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(800, 50);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(20, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 30);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export Report";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(680, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PerformanceReportForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 530);
            this.Controls.Add(this.tabControlReports);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PerformanceReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Performance Report";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tabControlReports.ResumeLayout(false);
            this.tabOccupancy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartOccupancy)).EndInit();
            this.tabServices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartServices)).EndInit();
            this.tabRevenue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).EndInit();
            this.tabTripTypes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTripTypes)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        // Export to PDF functionality
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    Title = "Export Performance Report to PDF",
                    FileName = $"PerformanceReport_{DateTime.Now.ToString("yyyyMMdd")}.pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Create PDF document
                    using (FileStream fs = new FileStream(saveDialog.FileName, FileMode.Create))
                    {
                        // Create document with A4 size
                        Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                        PdfWriter writer = PdfWriter.GetInstance(document, fs);
                        document.Open();

                        // Add title
                        Paragraph title = new Paragraph("Performance Report",
                            new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD));
                        title.Alignment = Element.ALIGN_CENTER;
                        title.SpacingAfter = 20;
                        document.Add(title);

                        // Add date
                        Paragraph date = new Paragraph($"Generated on: {DateTime.Now.ToString("MMMM dd, yyyy")}",
                            new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12));
                        date.Alignment = Element.ALIGN_CENTER;
                        date.SpacingAfter = 20;
                        document.Add(date);

                        // Add each chart image
                        ExportChartToPdf(document, chartOccupancy, "Occupancy Rate");
                        ExportChartToPdf(document, chartServices, "Service Performance");
                        ExportChartToPdf(document, chartRevenue, "Monthly Revenue");
                        ExportChartToPdf(document, chartTripTypes, "Revenue by Trip Type");

                        document.Close();
                        MessageBox.Show("Report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to export chart to PDF
        private void ExportChartToPdf(Document document, Chart chart, string sectionTitle)
        {
            // Add section title
            Paragraph section = new Paragraph(sectionTitle,
                new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD));
            section.SpacingBefore = 20;
            section.SpacingAfter = 10;
            document.Add(section);

            // Create memory stream for chart image
            using (MemoryStream ms = new MemoryStream())
            {
                // Save chart as image
                chart.SaveImage(ms, ChartImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Create iTextSharp image and scale it to fit page width
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(imageBytes);
                float maxWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                float scale = maxWidth / chartImage.Width;
                chartImage.ScalePercent(scale * 100);

                document.Add(chartImage);
            }

            // Get data from chart and add as table if needed
            if (chart.Series.Count > 0 && chart.Series[0].Points.Count > 0)
            {
                // Create table with headers
                PdfPTable dataTable = new PdfPTable(2);
                dataTable.SpacingBefore = 10;
                dataTable.WidthPercentage = 100;

                // Add headers
                PdfPCell cell1 = new PdfPCell(new Phrase("Category",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD)));
                PdfPCell cell2 = new PdfPCell(new Phrase("Value",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD)));

                cell1.BackgroundColor = new BaseColor(240, 240, 240);
                cell2.BackgroundColor = new BaseColor(240, 240, 240);

                dataTable.AddCell(cell1);
                dataTable.AddCell(cell2);

                // Add data rows
                foreach (DataPoint point in chart.Series[0].Points)
                {
                    string category = point.AxisLabel;
                    string value = point.YValues[0].ToString("C2");

                    dataTable.AddCell(new Phrase(category, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
                    dataTable.AddCell(new Phrase(value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
                }

                document.Add(dataTable);
            }

            // Add page break after each section except the last one
            if (sectionTitle != "Revenue by Trip Type")
            {
                document.NewPage();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Form fields
        private Panel pnlHeader;
        private Label lblTitle;
        private TabControl tabControlReports;
        private TabPage tabOccupancy;
        private Chart chartOccupancy;
        private TabPage tabServices;
        private Chart chartServices;
        private TabPage tabRevenue;
        private Chart chartRevenue;
        private TabPage tabTripTypes;
        private Chart chartTripTypes;
        private Panel pnlFooter;
        private Button btnClose;
        private Button btnExport;

        private void chartOccupancy_Click(object sender, EventArgs e)
        {

        }

        private void chartServices_Click(object sender, EventArgs e)
        {

        }
    }
}