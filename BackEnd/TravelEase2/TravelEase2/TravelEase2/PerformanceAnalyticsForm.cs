
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TravelEase
{
    public partial class PerformanceAnalyticsForm : Form
    {
        private Chart bookperT;
        private Chart RevperT;
        private Button button1;
        private Chart AvgperT;

        private string connectionString;
        private int operatorId = 18;

        public PerformanceAnalyticsForm(string con, int operatorId)
        {
            connectionString = con;
            //this.operatorId = operatorId;
            InitializeComponent();
        }
        
        private void PerformanceAnalyticsForm_Load(object sender, EventArgs e)
        {
            // Use BeginInvoke to ensure the form is fully loaded before loading data
            BeginInvoke(new Action(LoadPerformanceData));
        }

        private void LoadPerformanceData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch Performance Data
                    string query = @"
                    SELECT 
                        t.title AS TripTitle,
                        COUNT(b.booking_id) AS BookingCount,
                        SUM(COALESCE(b.total_amount, 0)) AS TotalRevenue,
                        AVG(COALESCE(r.rating, 0)) AS AverageRating
                    FROM 
                        Trip t
                    LEFT JOIN 
                        Booking b ON t.trip_id = b.trip_id
                    LEFT JOIN 
                        Review r ON t.trip_id = r.trip_id
                    WHERE 
                        t.operator_id = @OperatorId
                    GROUP BY 
                        t.title
                    ORDER BY 
                        BookingCount DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OperatorId", operatorId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Clear and reset charts to prevent layout conflicts
                            ResetCharts();

                            // Populate Charts
                            PopulateBookingsChart(dt);
                            PopulateRevenueChart(dt);
                            PopulateRatingsChart(dt);

                            // Add Summary Labels
                            AddSummaryLabels(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Use Invoke to show message box from UI thread
                Invoke(new Action(() =>
                    MessageBox.Show($"Error loading performance data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ));
            }
        }

        private void ResetCharts()
        {
            // Clear existing series and reset chart areas
            foreach (var chart in new[] { bookperT, RevperT, AvgperT })
            {
                chart.Series[0].Points.Clear();
                chart.ChartAreas[0].RecalculateAxesScale();
            }
        }

        private void PopulateBookingsChart(DataTable dt)
        {
            bookperT.Series[0].ChartType = SeriesChartType.Column;
            bookperT.Series[0].Color = Color.DodgerBlue;

            foreach (DataRow row in dt.Rows)
            {
                string tripTitle = row["TripTitle"].ToString();
                int bookingCount = Convert.ToInt32(row["BookingCount"]);
                bookperT.Series[0].Points.AddXY(tripTitle, bookingCount);
            }

            CustomizeChart(bookperT, "Bookings per Trip");
        }

        private void PopulateRevenueChart(DataTable dt)
        {
            RevperT.Series[0].ChartType = SeriesChartType.Pie;

            foreach (DataRow row in dt.Rows)
            {
                string tripTitle = row["TripTitle"].ToString();
                decimal totalRevenue = Convert.ToDecimal(row["TotalRevenue"]);
                RevperT.Series[0].Points.AddXY(tripTitle, (double)totalRevenue);
            }

            CustomizeChart(RevperT, "Revenue per Trip", SeriesChartType.Pie);
        }

        private void PopulateRatingsChart(DataTable dt)
        {
            AvgperT.Series[0].ChartType = SeriesChartType.Bar;

            foreach (DataRow row in dt.Rows)
            {
                string tripTitle = row["TripTitle"].ToString();
                double averageRating = Convert.ToDouble(row["AverageRating"]);
                AvgperT.Series[0].Points.AddXY(tripTitle, averageRating);
            }

            CustomizeChart(AvgperT, "Average Rating per Trip");
        }

        private void CustomizeChart(Chart chart, string title, SeriesChartType chartType = SeriesChartType.Column)
        {
            // Clear existing customizations
            chart.Titles.Clear();
            chart.ChartAreas[0].AxisX.Title = "Trips";
            chart.ChartAreas[0].AxisY.Title = GetYAxisTitle(title);

            // Add Title
            Title chartTitle = new Title(title);
            chartTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            chart.Titles.Add(chartTitle);

            // Customize Series
            chart.Series[0].ChartType = chartType;

            // Color and Style
            if (chartType == SeriesChartType.Pie)
            {
                chart.Series[0].Color = Color.DarkBlue;
                chart.Series[0]["PieLabelStyle"] = "Outside";
            }
            else
            {
                chart.Series[0].Color = Color.DodgerBlue;
            }

            // Enable/Disable 3D
            chart.ChartAreas[0].Area3DStyle.Enable3D = false;
        }

        private string GetYAxisTitle(string chartTitle)
        {
            switch (chartTitle)
            {
                case "Bookings per Trip":
                    return "Number of Bookings";
                case "Revenue per Trip":
                    return "Total Revenue";
                case "Average Rating per Trip":
                    return "Average Rating";
                default:
                    return "Value";
            }
        }





        private void AddSummaryLabels(DataTable performanceData)
        {
            // Create a panel for summary
            Panel summaryPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 100,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            // Calculate Summary Metrics
            int totalBookings = 0;
            decimal totalRevenue = 0;
            double averageRating = 0;
            int tripCount = performanceData.Rows.Count;

            foreach (DataRow row in performanceData.Rows)
            {
                totalBookings += Convert.ToInt32(row["BookingCount"]);
                totalRevenue += Convert.ToDecimal(row["TotalRevenue"]);
                averageRating += Convert.ToDouble(row["AverageRating"]);
            }

            averageRating /= tripCount;

            // Create Labels
            Label lblTotalBookings = CreateSummaryLabel($"Total Bookings: {totalBookings}", 20);
            Label lblTotalRevenue = CreateSummaryLabel($"Total Revenue: ${totalRevenue:N2}", 200);
            Label lblAverageRating = CreateSummaryLabel($"Average Rating: {averageRating:F2}", 380);

            // Add Labels to Panel
            summaryPanel.Controls.Add(lblTotalBookings);
            summaryPanel.Controls.Add(lblTotalRevenue);
            summaryPanel.Controls.Add(lblAverageRating);

            // Add Panel to Form
            this.Controls.Add(summaryPanel);
            summaryPanel.BringToFront();
        }

        private Label CreateSummaryLabel(string text, int xPosition)
        {
            return new Label
            {
                Text = text,
                Location = new Point(xPosition, 30),
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RevperT_Click(object sender, EventArgs e)
        {
            // Optional: Add interactivity if needed
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 28D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 19D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 35D);
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 350000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 220000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 400000D);
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 4.6D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 4.1D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 4.8D);
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.bookperT = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.RevperT = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.AvgperT = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bookperT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RevperT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AvgperT)).BeginInit();
            this.SuspendLayout();
            // 
            // bookperT
            // 
            chartArea1.Name = "ChartArea1";
            this.bookperT.ChartAreas.Add(chartArea1);
            this.bookperT.Location = new System.Drawing.Point(20, 20);
            this.bookperT.Name = "bookperT";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            dataPoint1.AxisLabel = "Hunza";
            dataPoint2.AxisLabel = "Swat";
            dataPoint3.AxisLabel = "Murree";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            this.bookperT.Series.Add(series1);
            this.bookperT.Size = new System.Drawing.Size(300, 250);
            this.bookperT.TabIndex = 0;
            title1.Name = "Title1";
            title1.Text = "Bookings per Trip";
            this.bookperT.Titles.Add(title1);
            // 
            // RevperT
            // 
            chartArea2.Name = "ChartArea1";
            this.RevperT.ChartAreas.Add(chartArea2);
            this.RevperT.Location = new System.Drawing.Point(340, 20);
            this.RevperT.Name = "RevperT";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Name = "Series1";
            dataPoint4.AxisLabel = "Hunza";
            dataPoint5.AxisLabel = "Swat";
            dataPoint6.AxisLabel = "Murree";
            series2.Points.Add(dataPoint4);
            series2.Points.Add(dataPoint5);
            series2.Points.Add(dataPoint6);
            this.RevperT.Series.Add(series2);
            this.RevperT.Size = new System.Drawing.Size(300, 250);
            this.RevperT.TabIndex = 1;
            title2.Name = "Title1";
            title2.Text = "Revenue per Trip";
            this.RevperT.Titles.Add(title2);
            this.RevperT.Click += new System.EventHandler(this.RevperT_Click);
            // 
            // AvgperT
            // 
            chartArea3.Name = "ChartArea1";
            this.AvgperT.ChartAreas.Add(chartArea3);
            this.AvgperT.Location = new System.Drawing.Point(660, 20);
            this.AvgperT.Name = "AvgperT";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series3.Name = "Series1";
            dataPoint7.AxisLabel = "Hunza";
            dataPoint8.AxisLabel = "Swat";
            dataPoint9.AxisLabel = "Murree";
            series3.Points.Add(dataPoint7);
            series3.Points.Add(dataPoint8);
            series3.Points.Add(dataPoint9);
            this.AvgperT.Series.Add(series3);
            this.AvgperT.Size = new System.Drawing.Size(300, 250);
            this.AvgperT.TabIndex = 2;
            title3.Name = "Title1";
            title3.Text = "Average Rating per Trip";
            this.AvgperT.Titles.Add(title3);
            this.AvgperT.Click += new System.EventHandler(this.AvgperT_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.button1.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.button1.Location = new System.Drawing.Point(422, 320);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 51);
            this.button1.TabIndex = 3;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PerformanceAnalyticsForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1000, 419);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bookperT);
            this.Controls.Add(this.RevperT);
            this.Controls.Add(this.AvgperT);
            this.Name = "PerformanceAnalyticsForm";
            this.Text = "Performance Analytics";
            this.Load += new System.EventHandler(this.PerformanceAnalyticsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bookperT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RevperT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AvgperT)).EndInit();
            this.ResumeLayout(false);

        }

        private void AvgperT_Click(object sender, EventArgs e)
        {

        }


    }
}
