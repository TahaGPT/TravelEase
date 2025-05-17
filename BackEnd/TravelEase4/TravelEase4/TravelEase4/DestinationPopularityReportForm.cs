using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DestinationPopularityReport
{
    public partial class DestinationPopularityReportForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

        public DestinationPopularityReportForm()
        {
            InitializeComponent();
        }

        private void DestinationPopularityReportForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Get most booked destinations
                DataTable mostBookedDestinations = GetMostBookedDestinations();
                PopulateMostBookedDestinationsChart(mostBookedDestinations);

                // Get seasonal trends
                DataTable seasonalTrends = GetSeasonalTrends();
                PopulateSeasonalTrendsChart(seasonalTrends);

                // Get satisfaction scores
                DataTable satisfactionScores = GetDestinationSatisfactionScores();
                PopulateSatisfactionScoresChart(satisfactionScores);

                // Get emerging destinations
                DataTable emergingDestinations = GetEmergingDestinations();
                PopulateEmergingDestinationsChart(emergingDestinations);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetMostBookedDestinations()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT TOP 10 
                        t.destination, 
                        COUNT(b.booking_id) AS total_bookings
                    FROM 
                        Trip t
                    INNER JOIN 
                        Booking b ON t.trip_id = b.trip_id
                    GROUP BY 
                        t.destination
                    ORDER BY 
                        total_bookings DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        private DataTable GetSeasonalTrends()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        MONTH(t.start_date) AS month,
                        COUNT(b.booking_id) AS total_bookings
                    FROM 
                        Trip t
                    INNER JOIN 
                        Booking b ON t.trip_id = b.trip_id
                    GROUP BY 
                        MONTH(t.start_date)
                    ORDER BY 
                        month";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        private DataTable GetDestinationSatisfactionScores()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        t.destination, 
                        AVG(CAST(r.rating AS FLOAT)) AS average_rating
                    FROM 
                        Trip t
                    INNER JOIN 
                        Review r ON t.trip_id = r.trip_id
                    GROUP BY 
                        t.destination
                    HAVING 
                        COUNT(r.review_id) > 5
                    ORDER BY 
                        average_rating DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        private DataTable GetEmergingDestinations()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Emerging destinations - measured by growth in wishlist additions 
                // compared to actual bookings ratio
                string query = @"
                    SELECT TOP 5
                        t.destination,
                        COUNT(DISTINCT w.wishlist_entry_id) AS wishlist_count,
                        COUNT(DISTINCT b.booking_id) AS booking_count,
                        CASE 
                            WHEN COUNT(DISTINCT b.booking_id) = 0 THEN 0
                            ELSE CAST(COUNT(DISTINCT w.wishlist_entry_id) AS FLOAT) / COUNT(DISTINCT b.booking_id)
                        END AS interest_ratio
                    FROM 
                        Trip t
                    LEFT JOIN 
                        Wishlist w ON t.trip_id = w.trip_id
                    LEFT JOIN 
                        Booking b ON t.trip_id = b.trip_id
                    GROUP BY 
                        t.destination
                    HAVING 
                        COUNT(DISTINCT w.wishlist_entry_id) > 10
                    ORDER BY 
                        interest_ratio DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        private void PopulateMostBookedDestinationsChart(DataTable data)
        {
            chartMostBooked.Series.Clear();
            chartMostBooked.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartMostBooked.ChartAreas[0].AxisX.Interval = 1;

            Series series = new Series
            {
                Name = "Bookings",
                ChartType = SeriesChartType.Column,
                Color = Color.SteelBlue
            };

            foreach (DataRow row in data.Rows)
            {
                series.Points.AddXY(row["destination"].ToString(), Convert.ToInt32(row["total_bookings"]));
            }

            chartMostBooked.Series.Add(series);
            chartMostBooked.Titles.Add("Most Booked Destinations");
            chartMostBooked.ChartAreas[0].AxisY.Title = "Number of Bookings";
            chartMostBooked.ChartAreas[0].AxisX.Title = "Destination";
        }

        private void PopulateSeasonalTrendsChart(DataTable data)
        {
            chartSeasonalTrends.Series.Clear();

            Series series = new Series
            {
                Name = "Monthly Bookings",
                ChartType = SeriesChartType.Line,
                Color = Color.ForestGreen,
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8
            };

            string[] monthNames = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            foreach (DataRow row in data.Rows)
            {
                int month = Convert.ToInt32(row["month"]);
                string monthName = monthNames[month - 1];
                series.Points.AddXY(monthName, Convert.ToInt32(row["total_bookings"]));
            }

            chartSeasonalTrends.Series.Add(series);
            chartSeasonalTrends.Titles.Add("Seasonal Booking Trends");
            chartSeasonalTrends.ChartAreas[0].AxisY.Title = "Number of Bookings";
            chartSeasonalTrends.ChartAreas[0].AxisX.Title = "Month";
        }

        private void PopulateSatisfactionScoresChart(DataTable data)
        {
            chartSatisfaction.Series.Clear();
            chartSatisfaction.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartSatisfaction.ChartAreas[0].AxisX.Interval = 1;

            Series series = new Series
            {
                Name = "Average Rating",
                ChartType = SeriesChartType.Bar,
                Color = Color.Orange
            };

            foreach (DataRow row in data.Rows)
            {
                double rating = Convert.ToDouble(row["average_rating"]);
                series.Points.AddXY(row["destination"].ToString(), rating);
            }

            chartSatisfaction.Series.Add(series);
            chartSatisfaction.Titles.Add("Destination Satisfaction Scores");
            chartSatisfaction.ChartAreas[0].AxisY.Title = "Average Rating (1-5)";
            chartSatisfaction.ChartAreas[0].AxisX.Title = "Destination";
            chartSatisfaction.ChartAreas[0].AxisY.Maximum = 5;
            chartSatisfaction.ChartAreas[0].AxisY.Minimum = 0;
        }

        private void PopulateEmergingDestinationsChart(DataTable data)
        {
            chartEmerging.Series.Clear();

            Series series = new Series
            {
                Name = "Interest Ratio",
                ChartType = SeriesChartType.Pie
            };

            foreach (DataRow row in data.Rows)
            {
                string destination = row["destination"].ToString();
                double ratio = Convert.ToDouble(row["interest_ratio"]);
                int wishlistCount = Convert.ToInt32(row["wishlist_count"]);

                DataPoint point = new DataPoint();
                point.SetValueXY(destination, ratio);
                point.Label = $"{destination}: {ratio:F2}";
                point.LegendText = destination;
                point.ToolTip = $"{destination}\nWishlist count: {wishlistCount}\nInterest ratio: {ratio:F2}";
                series.Points.Add(point);
            }

            chartEmerging.Series.Add(series);
            chartEmerging.Titles.Add("Emerging Destinations (Wishlist to Booking Ratio)");
            chartEmerging.Series[0]["PieLabelStyle"] = "Outside";
            chartEmerging.Series[0]["PieLineColor"] = "Black";
            chartEmerging.Legends[0].Enabled = true;
        }

        // Designer-generated code
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();

            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMostBooked = new System.Windows.Forms.TabPage();
            this.chartMostBooked = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabSeasonalTrends = new System.Windows.Forms.TabPage();
            this.chartSeasonalTrends = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabSatisfaction = new System.Windows.Forms.TabPage();
            this.chartSatisfaction = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabEmerging = new System.Windows.Forms.TabPage();
            this.chartEmerging = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();

            this.tabControl.SuspendLayout();
            this.tabMostBooked.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMostBooked)).BeginInit();
            this.tabSeasonalTrends.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSeasonalTrends)).BeginInit();
            this.tabSatisfaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSatisfaction)).BeginInit();
            this.tabEmerging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEmerging)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(303, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Destination Popularity Report";

            // tabControl
            this.tabControl.Controls.Add(this.tabMostBooked);
            this.tabControl.Controls.Add(this.tabSeasonalTrends);
            this.tabControl.Controls.Add(this.tabSatisfaction);
            this.tabControl.Controls.Add(this.tabEmerging);
            this.tabControl.Location = new System.Drawing.Point(12, 42);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(960, 500);
            this.tabControl.TabIndex = 1;

            // tabMostBooked
            this.tabMostBooked.Controls.Add(this.chartMostBooked);
            this.tabMostBooked.Location = new System.Drawing.Point(4, 22);
            this.tabMostBooked.Name = "tabMostBooked";
            this.tabMostBooked.Padding = new System.Windows.Forms.Padding(3);
            this.tabMostBooked.Size = new System.Drawing.Size(952, 474);
            this.tabMostBooked.TabIndex = 0;
            this.tabMostBooked.Text = "Most Booked Destinations";
            this.tabMostBooked.UseVisualStyleBackColor = true;

            // chartMostBooked
            this.chartMostBooked.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartMostBooked.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartMostBooked.Legends.Add(legend1);
            this.chartMostBooked.Location = new System.Drawing.Point(6, 6);
            this.chartMostBooked.Name = "chartMostBooked";
            this.chartMostBooked.Size = new System.Drawing.Size(940, 462);
            this.chartMostBooked.TabIndex = 0;
            this.chartMostBooked.Text = "Most Booked Destinations";

            // tabSeasonalTrends
            this.tabSeasonalTrends.Controls.Add(this.chartSeasonalTrends);
            this.tabSeasonalTrends.Location = new System.Drawing.Point(4, 22);
            this.tabSeasonalTrends.Name = "tabSeasonalTrends";
            this.tabSeasonalTrends.Padding = new System.Windows.Forms.Padding(3);
            this.tabSeasonalTrends.Size = new System.Drawing.Size(952, 474);
            this.tabSeasonalTrends.TabIndex = 1;
            this.tabSeasonalTrends.Text = "Seasonal Trends";
            this.tabSeasonalTrends.UseVisualStyleBackColor = true;

            // chartSeasonalTrends
            this.chartSeasonalTrends.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chartSeasonalTrends.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartSeasonalTrends.Legends.Add(legend2);
            this.chartSeasonalTrends.Location = new System.Drawing.Point(6, 6);
            this.chartSeasonalTrends.Name = "chartSeasonalTrends";
            this.chartSeasonalTrends.Size = new System.Drawing.Size(940, 462);
            this.chartSeasonalTrends.TabIndex = 0;
            this.chartSeasonalTrends.Text = "Seasonal Trends";

            // tabSatisfaction
            this.tabSatisfaction.Controls.Add(this.chartSatisfaction);
            this.tabSatisfaction.Location = new System.Drawing.Point(4, 22);
            this.tabSatisfaction.Name = "tabSatisfaction";
            this.tabSatisfaction.Size = new System.Drawing.Size(952, 474);
            this.tabSatisfaction.TabIndex = 2;
            this.tabSatisfaction.Text = "Satisfaction Scores";
            this.tabSatisfaction.UseVisualStyleBackColor = true;

            // chartSatisfaction
            this.chartSatisfaction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.Name = "ChartArea1";
            this.chartSatisfaction.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartSatisfaction.Legends.Add(legend3);
            this.chartSatisfaction.Location = new System.Drawing.Point(6, 6);
            this.chartSatisfaction.Name = "chartSatisfaction";
            this.chartSatisfaction.Size = new System.Drawing.Size(940, 462);
            this.chartSatisfaction.TabIndex = 0;
            this.chartSatisfaction.Text = "Satisfaction Scores";

            // tabEmerging
            this.tabEmerging.Controls.Add(this.chartEmerging);
            this.tabEmerging.Location = new System.Drawing.Point(4, 22);
            this.tabEmerging.Name = "tabEmerging";
            this.tabEmerging.Size = new System.Drawing.Size(952, 474);
            this.tabEmerging.TabIndex = 3;
            this.tabEmerging.Text = "Emerging Destinations";
            this.tabEmerging.UseVisualStyleBackColor = true;

            // chartEmerging
            this.chartEmerging.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            chartArea4.Name = "ChartArea1";
            this.chartEmerging.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartEmerging.Legends.Add(legend4);
            this.chartEmerging.Location = new System.Drawing.Point(6, 6);
            this.chartEmerging.Name = "chartEmerging";
            this.chartEmerging.Size = new System.Drawing.Size(940, 462);
            this.chartEmerging.TabIndex = 0;
            this.chartEmerging.Text = "Emerging Destinations";

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(816, 548);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // btnExport
            this.btnExport.Location = new System.Drawing.Point(897, 548);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            // DestinationPopularityReportForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 581);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblTitle);
            this.Name = "DestinationPopularityReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Destination Popularity Report";
            this.Load += new System.EventHandler(this.DestinationPopularityReportForm_Load);

            this.tabControl.ResumeLayout(false);
            this.tabMostBooked.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMostBooked)).EndInit();
            this.tabSeasonalTrends.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartSeasonalTrends)).EndInit();
            this.tabSatisfaction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartSatisfaction)).EndInit();
            this.tabEmerging.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartEmerging)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMostBooked;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMostBooked;
        private System.Windows.Forms.TabPage tabSeasonalTrends;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSeasonalTrends;
        private System.Windows.Forms.TabPage tabSatisfaction;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSatisfaction;
        private System.Windows.Forms.TabPage tabEmerging;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEmerging;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Data refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Simple export functionality - save current chart as image
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png|JPEG Image|*.jpg|BMP Image|*.bmp",
                Title = "Export Chart"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                Chart currentChart = null;

                switch (tabControl.SelectedIndex)
                {
                    case 0:
                        currentChart = chartMostBooked;
                        break;
                    case 1:
                        currentChart = chartSeasonalTrends;
                        break;
                    case 2:
                        currentChart = chartSatisfaction;
                        break;
                    case 3:
                        currentChart = chartEmerging;
                        break;
                }

                if (currentChart != null)
                {
                    try
                    {
                        currentChart.SaveImage(saveDialog.FileName, ChartImageFormat.Png);
                        MessageBox.Show("Chart exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }

   
}
