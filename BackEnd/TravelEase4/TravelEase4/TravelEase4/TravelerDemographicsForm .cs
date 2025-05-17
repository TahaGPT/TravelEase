using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TravelerDemographicsApp
{
    public partial class TravelerDemographicsForm : Form
    {
        private string connectionString = @"Data Source=DESKTOP-2I8F60O\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";
        private Chart ageChart;
        private Chart nationalityChart;
        private Chart tripTypesChart;
        private Chart budgetChart;
        private Label overallStatsLabel;

        public TravelerDemographicsForm()
        {
            InitializeComponent();
            InitializeCharts();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Traveler Demographics and Preferences Report";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Create a panel for charts
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 2,
                Padding = new Padding(10)
            };

            // Set row and column percentages
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));   // Header
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 45));   // Top charts
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 45));   // Bottom charts

            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            // Add header panel that spans both columns
            Panel headerPanel = new Panel { Dock = DockStyle.Fill };
            overallStatsLabel = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            headerPanel.Controls.Add(overallStatsLabel);
            mainPanel.Controls.Add(headerPanel, 0, 0);
            mainPanel.SetColumnSpan(headerPanel, 2);

            this.Controls.Add(mainPanel);
        }

        private void InitializeCharts()
        {
            // Create the four charts
            ageChart = CreateChart("Age Distribution");
            nationalityChart = CreateChart("Nationality Distribution");
            tripTypesChart = CreateChart("Preferred Trip Types");
            budgetChart = CreateChart("Spending Habits (Budget)");

            // Find the TableLayoutPanel to add the charts
            TableLayoutPanel mainPanel = (TableLayoutPanel)this.Controls[0];

            // Add charts to panel
            mainPanel.Controls.Add(ageChart, 0, 1);
            mainPanel.Controls.Add(nationalityChart, 1, 1);
            mainPanel.Controls.Add(tripTypesChart, 0, 2);
            mainPanel.Controls.Add(budgetChart, 1, 2);
        }

        private Chart CreateChart(string title)
        {
            Chart chart = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                BorderlineColor = Color.LightGray,
                BorderlineDashStyle = ChartDashStyle.Solid,
                BorderlineWidth = 1
            };

            // Add chart area
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.BackColor = Color.White;
            chart.ChartAreas.Add(chartArea);

            // Add legend
            Legend legend = new Legend();
            chart.Legends.Add(legend);

            // Add title
            Title chartTitle = new Title(title);
            chartTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            chart.Titles.Add(chartTitle);

            return chart;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    LoadAgeDistribution(connection);
                    LoadNationalityDistribution(connection);
                    LoadTripTypePreferences(connection);
                    LoadBudgetDistribution(connection);
                    LoadOverallStats(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Data Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAgeDistribution(SqlConnection connection)
        {
            string query = @"
                SELECT 
                    CASE 
                        WHEN age < 18 THEN 'Under 18'
                        WHEN age BETWEEN 18 AND 24 THEN '18-24'
                        WHEN age BETWEEN 25 AND 34 THEN '25-34'
                        WHEN age BETWEEN 35 AND 44 THEN '35-44'
                        WHEN age BETWEEN 45 AND 54 THEN '45-54'
                        WHEN age BETWEEN 55 AND 64 THEN '55-64'
                        ELSE '65+' 
                    END as AgeGroup,
                    COUNT(*) as Count
                FROM Traveler
                GROUP BY 
                    CASE 
                        WHEN age < 18 THEN 'Under 18'
                        WHEN age BETWEEN 18 AND 24 THEN '18-24'
                        WHEN age BETWEEN 25 AND 34 THEN '25-34'
                        WHEN age BETWEEN 35 AND 44 THEN '35-44'
                        WHEN age BETWEEN 45 AND 54 THEN '45-54'
                        WHEN age BETWEEN 55 AND 64 THEN '55-64'
                        ELSE '65+' 
                    END
                ORDER BY AgeGroup";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Series series = new Series("Age Groups");
                    series.ChartType = SeriesChartType.Column;

                    while (reader.Read())
                    {
                        string ageGroup = reader["AgeGroup"].ToString();
                        int count = Convert.ToInt32(reader["Count"]);
                        series.Points.AddXY(ageGroup, count);
                    }

                    ageChart.Series.Add(series);
                }
            }
        }

        private void LoadNationalityDistribution(SqlConnection connection)
        {
            string query = @"
                SELECT TOP 10 
                    nationality, 
                    COUNT(*) as Count
                FROM Traveler
                GROUP BY nationality
                ORDER BY COUNT(*) DESC";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Series series = new Series("Top Nationalities");
                    series.ChartType = SeriesChartType.Pie;

                    while (reader.Read())
                    {
                        string nationality = reader["nationality"].ToString();
                        int count = Convert.ToInt32(reader["Count"]);
                        DataPoint point = new DataPoint();
                        point.SetValueXY(nationality, count);
                        point.LegendText = nationality;
                        point.Label = nationality + ": #PERCENT{P0}";
                        series.Points.Add(point);
                    }

                    nationalityChart.Series.Add(series);
                }
            }
        }

        private void LoadTripTypePreferences(SqlConnection connection)
        {
            string query = @"
                SELECT TOP 10
                    tc.category_name,
                    COUNT(b.booking_id) as BookingCount
                FROM TripCategory tc
                JOIN Trip t ON tc.category_id = t.category_id
                JOIN Booking b ON t.trip_id = b.trip_id
                WHERE b.status = 'Confirmed'
                GROUP BY tc.category_name
                ORDER BY BookingCount DESC";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Series series = new Series("Trip Types");
                    series.ChartType = SeriesChartType.Doughnut;

                    while (reader.Read())
                    {
                        string categoryName = reader["category_name"].ToString();
                        int count = Convert.ToInt32(reader["BookingCount"]);
                        DataPoint point = new DataPoint();
                        point.SetValueXY(categoryName, count);
                        point.LegendText = categoryName;
                        point.Label = "#PERCENT{P0}";
                        series.Points.Add(point);
                    }

                    tripTypesChart.Series.Add(series);
                }
            }
        }

        private void LoadBudgetDistribution(SqlConnection connection)
        {
            string query = @"
                SELECT 
                    CASE 
                        WHEN tp.budget_max < 500 THEN 'Under $500'
                        WHEN tp.budget_max BETWEEN 500 AND 1000 THEN '$500-1000'
                        WHEN tp.budget_max BETWEEN 1001 AND 2000 THEN '$1001-2000'
                        WHEN tp.budget_max BETWEEN 2001 AND 3000 THEN '$2001-3000'
                        WHEN tp.budget_max BETWEEN 3001 AND 5000 THEN '$3001-5000'
                        ELSE 'Over $5000' 
                    END as BudgetRange,
                    COUNT(*) as Count
                FROM Traveler_preferences tp
                GROUP BY 
                    CASE 
                        WHEN tp.budget_max < 500 THEN 'Under $500'
                        WHEN tp.budget_max BETWEEN 500 AND 1000 THEN '$500-1000'
                        WHEN tp.budget_max BETWEEN 1001 AND 2000 THEN '$1001-2000'
                        WHEN tp.budget_max BETWEEN 2001 AND 3000 THEN '$2001-3000'
                        WHEN tp.budget_max BETWEEN 3001 AND 5000 THEN '$3001-5000'
                        ELSE 'Over $5000' 
                    END
                ORDER BY 
                    CASE 
                        WHEN CASE 
                            WHEN tp.budget_max < 500 THEN 'Under $500'
                            WHEN tp.budget_max BETWEEN 500 AND 1000 THEN '$500-1000'
                            WHEN tp.budget_max BETWEEN 1001 AND 2000 THEN '$1001-2000'
                            WHEN tp.budget_max BETWEEN 2001 AND 3000 THEN '$2001-3000'
                            WHEN tp.budget_max BETWEEN 3001 AND 5000 THEN '$3001-5000'
                            ELSE 'Over $5000' 
                        END = 'Under $500' THEN 1
                        WHEN CASE 
                            WHEN tp.budget_max < 500 THEN 'Under $500'
                            WHEN tp.budget_max BETWEEN 500 AND 1000 THEN '$500-1000'
                            WHEN tp.budget_max BETWEEN 1001 AND 2000 THEN '$1001-2000'
                            WHEN tp.budget_max BETWEEN 2001 AND 3000 THEN '$2001-3000'
                            WHEN tp.budget_max BETWEEN 3001 AND 5000 THEN '$3001-5000'
                            ELSE 'Over $5000' 
                        END = '$500-1000' THEN 2
                        WHEN CASE 
                            WHEN tp.budget_max < 500 THEN 'Under $500'
                            WHEN tp.budget_max BETWEEN 500 AND 1000 THEN '$500-1000'
                            WHEN tp.budget_max BETWEEN 1001 AND 2000 THEN '$1001-2000'
                            WHEN tp.budget_max BETWEEN 2001 AND 3000 THEN '$2001-3000'
                            WHEN tp.budget_max BETWEEN 3001 AND 5000 THEN '$3001-5000'
                            ELSE 'Over $5000' 
                        END = '$1001-2000' THEN 3
                        WHEN CASE 
                            WHEN tp.budget_max < 500 THEN 'Under $500'
                            WHEN tp.budget_max BETWEEN 500 AND 1000 THEN '$500-1000'
                            WHEN tp.budget_max BETWEEN 1001 AND 2000 THEN '$1001-2000'
                            WHEN tp.budget_max BETWEEN 2001 AND 3000 THEN '$2001-3000'
                            WHEN tp.budget_max BETWEEN 3001 AND 5000 THEN '$3001-5000'
                            ELSE 'Over $5000' 
                        END = '$2001-3000' THEN 4
                        WHEN CASE 
                            WHEN tp.budget_max < 500 THEN 'Under $500'
                            WHEN tp.budget_max BETWEEN 500 AND 1000 THEN '$500-1000'
                            WHEN tp.budget_max BETWEEN 1001 AND 2000 THEN '$1001-2000'
                            WHEN tp.budget_max BETWEEN 2001 AND 3000 THEN '$2001-3000'
                            WHEN tp.budget_max BETWEEN 3001 AND 5000 THEN '$3001-5000'
                            ELSE 'Over $5000' 
                        END = '$3001-5000' THEN 5
                        ELSE 6
                    END";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Series series = new Series("Budget Distribution");
                    series.ChartType = SeriesChartType.Bar;

                    while (reader.Read())
                    {
                        string budgetRange = reader["BudgetRange"].ToString();
                        int count = Convert.ToInt32(reader["Count"]);
                        series.Points.AddXY(budgetRange, count);
                    }

                    budgetChart.Series.Add(series);
                }
            }
        }

        private void LoadOverallStats(SqlConnection connection)
        {
            string query = @"
                SELECT 
                    (SELECT COUNT(*) FROM Traveler) AS TotalTravelers,
                    (SELECT COUNT(DISTINCT nationality) FROM Traveler) AS UniqueNationalities,
                    (SELECT AVG(total_amount) FROM Booking) AS AvgBookingAmount,
                    (SELECT COUNT(*) FROM Booking WHERE status = 'Confirmed') AS ConfirmedBookings,
                    (SELECT AVG(tp.budget_max - tp.budget_min) FROM Traveler_preferences tp) AS AvgBudgetRange";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int totalTravelers = Convert.ToInt32(reader["TotalTravelers"]);
                        int uniqueNationalities = Convert.ToInt32(reader["UniqueNationalities"]);
                        decimal avgBookingAmount = Convert.ToDecimal(reader["AvgBookingAmount"]);
                        int confirmedBookings = Convert.ToInt32(reader["ConfirmedBookings"]);
                        decimal avgBudgetRange = Convert.ToDecimal(reader["AvgBudgetRange"]);

                        overallStatsLabel.Text = $"Total Travelers: {totalTravelers} | " +
                                               $"Unique Nationalities: {uniqueNationalities} | " +
                                               $"Avg. Booking: ${avgBookingAmount:N2} | " +
                                               $"Confirmed Bookings: {confirmedBookings} | " +
                                               $"Avg. Budget Range: ${avgBudgetRange:N2}";
                    }
                }
            }
        }
    }
}