using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TourOperatorPerformanceReport
{
    public partial class TourOperatorReportForm : Form
    {
        // Configuration - Replace with your actual connection string
        private readonly string _connectionString = "Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

        // Data storage
        private DataTable _operatorData;

        public TourOperatorReportForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Tour Operator Performance Report";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9F);

            // Main layout panel
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(10)
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            // Header
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Fill
            };
            Label headerLabel = new Label
            {
                Text = "Tour Operator Performance Report",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold)
            };
            headerPanel.Controls.Add(headerLabel);

            // Chart layout
            TableLayoutPanel chartPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(5)
            };
            chartPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            chartPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            chartPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            chartPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            // Average Rating Chart
            Chart ratingChart = CreateChart("RatingChart", "Average Operator Rating (1-5 stars)");
            chartPanel.Controls.Add(ratingChart, 0, 0);

            // Revenue Chart
            Chart revenueChart = CreateChart("RevenueChart", "Revenue per Operator");
            chartPanel.Controls.Add(revenueChart, 1, 0);

            // Response Time Chart
            Chart responseTimeChart = CreateChart("ResponseTimeChart", "Average Response Time (hours)");
            chartPanel.Controls.Add(responseTimeChart, 0, 1);

            // Grid View for detailed data
            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Name = "operatorGridView"
            };
            chartPanel.Controls.Add(dataGridView, 1, 1);

            // Footer
            Panel footerPanel = new Panel
            {
                Dock = DockStyle.Fill
            };
            Button refreshButton = new Button
            {
                Text = "Refresh Data",
                Dock = DockStyle.Right,
                Width = 120,
                Height = 35,
                Margin = new Padding(0, 5, 10, 5)
            };
            refreshButton.Click += (sender, e) => LoadData();

            Label footerLabel = new Label
            {
                Text = "Data refreshed: " + DateTime.Now.ToString(),
                Name = "lastRefreshLabel",
                AutoSize = true,
                Dock = DockStyle.Left,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            footerPanel.Controls.Add(refreshButton);
            footerPanel.Controls.Add(footerLabel);

            // Add all panels to main layout
            mainPanel.Controls.Add(headerPanel, 0, 0);
            mainPanel.Controls.Add(chartPanel, 0, 1);
            mainPanel.Controls.Add(footerPanel, 0, 2);

            this.Controls.Add(mainPanel);
        }

        private Chart CreateChart(string name, string title)
        {
            Chart chart = new Chart
            {
                Name = name,
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.WhiteSmoke
            };

            // Configure chart area
            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.BackColor = Color.White;
            chart.ChartAreas.Add(chartArea);

            // Configure title
            Title chartTitle = new Title
            {
                Text = title,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = ContentAlignment.TopCenter
            };
            chart.Titles.Add(chartTitle);

            // Configure legend
            Legend legend = new Legend
            {
                Name = "MainLegend",
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 8F)
            };
            chart.Legends.Add(legend);

            return chart;
        }

        private void LoadData()
        {
            try
            {
                // Fetch operator performance data
                _operatorData = GetOperatorPerformanceData();

                // Update grid view
                var gridView = this.Controls.Find("operatorGridView", true).FirstOrDefault() as DataGridView;
                if (gridView != null)
                {
                    gridView.DataSource = _operatorData;
                    gridView.Refresh();
                }

                // Update charts
                UpdateRatingChart();
                UpdateRevenueChart();
                UpdateResponseTimeChart();

                // Update refresh timestamp
                var refreshLabel = this.Controls.Find("lastRefreshLabel", true).FirstOrDefault() as Label;
                if (refreshLabel != null)
                {
                    refreshLabel.Text = "Data refreshed: " + DateTime.Now.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Data Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetOperatorPerformanceData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Query that joins relevant tables to get operator performance metrics
                    string query = @"
                    SELECT 
                        TourOperator.operator_id,
                        TourOperator.company_name AS [Operator Name],
                        COALESCE(AVG(CAST(R.rating AS FLOAT)), 0) AS [Average Rating],
                        COALESCE(SUM(B.total_amount), 0) AS [Total Revenue],
                        COALESCE(AVG(
                            DATEDIFF(HOUR, OI.inquiry_timestamp, OI.response_timestamp)
                        ), 0) AS [Avg Response Time (hours)],
                        COUNT(DISTINCT B.booking_id) AS [Bookings Count],
                        COUNT(DISTINCT TR.trip_id) AS [Trips Count]
                    FROM 
                        TourOperator
                    LEFT JOIN 
                        Trip TR ON TourOperator.operator_id = TR.operator_id
                    LEFT JOIN 
                        Review R ON TR.trip_id = R.trip_id
                    LEFT JOIN 
                        Booking B ON TR.trip_id = B.trip_id
                    LEFT JOIN 
                        OperatorInquiry_ OI ON TourOperator.operator_id = OI.operator_id
                    GROUP BY 
                        TourOperator.operator_id, TourOperator.company_name
                    ORDER BY 
                        [Average Rating] DESC;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        private void UpdateRatingChart()
        {
            var chart = this.Controls.Find("RatingChart", true).FirstOrDefault() as Chart;
            if (chart == null || _operatorData == null || _operatorData.Rows.Count == 0)
                return;

            chart.Series.Clear();

            // Create bar chart for ratings
            Series series = new Series
            {
                Name = "Average Rating",
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                LabelFormat = "0.0"
            };

            // Add data points
            foreach (DataRow row in _operatorData.Rows)
            {
                string operatorName = row["Operator Name"].ToString();
                double rating = Convert.ToDouble(row["Average Rating"]);

                // Truncate long operator names for display
                if (operatorName.Length > 15)
                    operatorName = operatorName.Substring(0, 12) + "...";

                series.Points.AddXY(operatorName, rating);

                // Color code based on rating
                DataPoint point = series.Points.Last();
                if (rating >= 4.5)
                    point.Color = Color.FromArgb(0, 180, 0); // Excellent - Green
                else if (rating >= 4.0)
                    point.Color = Color.FromArgb(144, 238, 144); // Good - Light Green
                else if (rating >= 3.0)
                    point.Color = Color.FromArgb(255, 255, 0); // Average - Yellow
                else
                    point.Color = Color.FromArgb(255, 99, 71); // Poor - Red
            }

            chart.Series.Add(series);

            // Set Y-axis range for ratings
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 5;
            chart.ChartAreas[0].AxisY.Interval = 1;
        }

        private void UpdateRevenueChart()
        {
            var chart = this.Controls.Find("RevenueChart", true).FirstOrDefault() as Chart;
            if (chart == null || _operatorData == null || _operatorData.Rows.Count == 0)
                return;

            chart.Series.Clear();

            // Create pie chart for revenue distribution
            Series series = new Series
            {
                Name = "Revenue Share",
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                LabelFormat = "{0:P1}"
            };

            // Calculate total revenue
            double totalRevenue = 0;
            foreach (DataRow row in _operatorData.Rows)
            {
                totalRevenue += Convert.ToDouble(row["Total Revenue"]);
            }

            // Skip if no revenue data
            if (totalRevenue == 0)
                return;

            // Add data points for top operators (to avoid pie chart with too many slices)
            var topOperators = _operatorData.AsEnumerable()
                .OrderByDescending(r => Convert.ToDouble(r["Total Revenue"]))
                .Take(5)
                .ToList();

            // Add "Others" category if needed
            double othersRevenue = totalRevenue - topOperators.Sum(r => Convert.ToDouble(r["Total Revenue"]));

            foreach (DataRow row in topOperators)
            {
                string operatorName = row["Operator Name"].ToString();
                double revenue = Convert.ToDouble(row["Total Revenue"]);
                double percentage = revenue / totalRevenue;

                // Truncate long operator names
                if (operatorName.Length > 15)
                    operatorName = operatorName.Substring(0, 12) + "...";

                int dataPointIndex = series.Points.AddXY(operatorName, percentage);
                series.Points[dataPointIndex].LegendText = operatorName + " ($" + revenue.ToString("N0") + ")";
            }

            // Add "Others" slice if significant
            if (othersRevenue / totalRevenue > 0.01)
            {
                int dataPointIndex = series.Points.AddXY("Others", othersRevenue / totalRevenue);
                series.Points[dataPointIndex].LegendText = "Others ($" + othersRevenue.ToString("N0") + ")";
            }

            chart.Series.Add(series);

            // Configure pie chart appearance
            chart.ChartAreas[0].Area3DStyle.Enable3D = true;
            chart.ChartAreas[0].Area3DStyle.Inclination = 20;
        }

        private void UpdateResponseTimeChart()
        {
            var chart = this.Controls.Find("ResponseTimeChart", true).FirstOrDefault() as Chart;
            if (chart == null || _operatorData == null || _operatorData.Rows.Count == 0)
                return;

            chart.Series.Clear();

            // Create horizontal bar chart for response times
            Series series = new Series
            {
                Name = "Response Time",
                ChartType = SeriesChartType.Bar,
                IsValueShownAsLabel = true,
                LabelFormat = "0.0 hrs"
            };

            // Add data points (limit to top 10 for readability)
            var sortedData = _operatorData.AsEnumerable()
                .Where(r => Convert.ToDouble(r["Avg Response Time (hours)"]) > 0) // Only include operators with inquiries
                .OrderBy(r => Convert.ToDouble(r["Avg Response Time (hours)"]))
                .Take(10)
                .ToList();

            foreach (DataRow row in sortedData)
            {
                string operatorName = row["Operator Name"].ToString();
                double responseTime = Convert.ToDouble(row["Avg Response Time (hours)"]);

                // Truncate long operator names
                if (operatorName.Length > 15)
                    operatorName = operatorName.Substring(0, 12) + "...";

                series.Points.AddXY(operatorName, responseTime);

                // Color code based on response time
                DataPoint point = series.Points.Last();
                if (responseTime <= 1)
                    point.Color = Color.FromArgb(0, 180, 0); // Excellent - Green
                else if (responseTime <= 4)
                    point.Color = Color.FromArgb(144, 238, 144); // Good - Light Green
                else if (responseTime <= 12)
                    point.Color = Color.FromArgb(255, 255, 0); // Average - Yellow
                else if (responseTime <= 24)
                    point.Color = Color.FromArgb(255, 165, 0); // Slow - Orange
                else
                    point.Color = Color.FromArgb(255, 99, 71); // Poor - Red
            }

            chart.Series.Add(series);
        }
    }
}