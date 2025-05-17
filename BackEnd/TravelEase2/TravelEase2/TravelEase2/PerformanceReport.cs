using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace TravelEase
{
    public partial class PerformanceReportForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";
        private int providerId;

        public PerformanceReportForm(int providerId)
        {
            InitializeComponent();
            this.providerId = providerId;
            this.Load += PerformanceReportForm_Load;
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

        private void LoadOccupancyRateChart()
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    string query = @"SELECT 
            //                    DATENAME(MONTH, b.CheckInDate) as Month,
            //                    COUNT(*) as BookingsCount,
            //                    (SELECT COUNT(*) FROM Services WHERE ProviderID = @ProviderID 
            //                     AND ServiceType = 'Room') as TotalRooms,
            //                    (COUNT(*) * 100.0 / 
            //                        CASE 
            //                            WHEN (SELECT COUNT(*) FROM Services WHERE ProviderID = @ProviderID 
            //                                  AND ServiceType = 'Room') = 0 THEN 1
            //                            ELSE (SELECT COUNT(*) FROM Services WHERE ProviderID = @ProviderID 
            //                                  AND ServiceType = 'Room')
            //                        END) as OccupancyRate
            //                    FROM Bookings b
            //                    JOIN Services s ON b.ServiceID = s.ServiceID
            //                    WHERE s.ProviderID = @ProviderID
            //                    AND s.ServiceType = 'Room'
            //                    AND YEAR(b.CheckInDate) = YEAR(GETDATE())
            //                    GROUP BY DATENAME(MONTH, b.CheckInDate), MONTH(b.CheckInDate)
            //                    ORDER BY MONTH(b.CheckInDate)";

            //    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            //    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", providerId);

            //    DataTable occupancyTable = new DataTable();
            //    adapter.Fill(occupancyTable);

            //    // Configure chart
            //    chartOccupancy.Series.Clear();
            //    Series occupancySeries = new Series("Occupancy Rate");
            //    occupancySeries.ChartType = SeriesChartType.Column;
            //    occupancySeries.Color = Color.FromArgb(52, 152, 219); // Blue

            //    foreach (DataRow row in occupancyTable.Rows)
            //    {
            //        occupancySeries.Points.AddXY(row["Month"].ToString(), Convert.ToDouble(row["OccupancyRate"]));
            //    }

            //    chartOccupancy.Series.Add(occupancySeries);
            //    chartOccupancy.ChartAreas[0].AxisY.Title = "Occupancy Rate (%)";
            //    chartOccupancy.ChartAreas[0].AxisX.Title = "Month";
            //    chartOccupancy.ChartAreas[0].AxisY.Maximum = 100;
            //    chartOccupancy.Titles.Clear();
            //    chartOccupancy.Titles.Add("Monthly Room Occupancy Rate");
            //}
        }

        private void LoadServicePerformanceChart()
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    string query = @"SELECT 
            //                    s.ServiceType,
            //                    COUNT(b.BookingID) as BookingsCount,
            //                    AVG(ISNULL(b.Rating, 0)) as AverageRating
            //                    FROM Services s
            //                    LEFT JOIN Bookings b ON s.ServiceID = b.ServiceID
            //                    WHERE s.ProviderID = @ProviderID
            //                    GROUP BY s.ServiceType
            //                    ORDER BY COUNT(b.BookingID) DESC";

            //    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            //    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", providerId);

            //    DataTable serviceTable = new DataTable();
            //    adapter.Fill(serviceTable);

            //    // Configure chart
            //    chartServices.Series.Clear();
            //    Series bookingsSeries = new Series("Bookings");
            //    bookingsSeries.ChartType = SeriesChartType.Column;
            //    bookingsSeries.Color = Color.FromArgb(46, 204, 113); // Green

            //    Series ratingSeries = new Series("Average Rating");
            //    ratingSeries.ChartType = SeriesChartType.Line;
            //    ratingSeries.Color = Color.FromArgb(231, 76, 60); // Red
            //    ratingSeries.YAxisType = AxisType.Secondary;

            //    foreach (DataRow row in serviceTable.Rows)
            //    {
            //        bookingsSeries.Points.AddXY(row["ServiceType"].ToString(), Convert.ToInt32(row["BookingsCount"]));
            //        ratingSeries.Points.AddXY(row["ServiceType"].ToString(), Convert.ToDouble(row["AverageRating"]));
            //    }

            //    chartServices.Series.Add(bookingsSeries);
            //    chartServices.Series.Add(ratingSeries);
            //    chartServices.ChartAreas[0].AxisY.Title = "Bookings Count";
            //    chartServices.ChartAreas[0].AxisY2.Title = "Average Rating";
            //    chartServices.ChartAreas[0].AxisY2.Minimum = 0;
            //    chartServices.ChartAreas[0].AxisY2.Maximum = 5;
            //    chartServices.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            //    chartServices.Titles.Clear();
            //    chartServices.Titles.Add("Service Performance");
            //}
        }

        private void LoadMonthlyRevenueChart()
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    string query = @"SELECT 
            //                    DATENAME(MONTH, b.CheckInDate) as Month,
            //                    SUM(b.TotalAmount) as Revenue
            //                    FROM Bookings b
            //                    JOIN Services s ON b.ServiceID = s.ServiceID
            //                    WHERE s.ProviderID = @ProviderID
            //                    AND YEAR(b.CheckInDate) = YEAR(GETDATE())
            //                    GROUP BY DATENAME(MONTH, b.CheckInDate), MONTH(b.CheckInDate)
            //                    ORDER BY MONTH(b.CheckInDate)";

            //    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            //    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", providerId);

            //    DataTable revenueTable = new DataTable();
            //    adapter.Fill(revenueTable);

            //    // Configure chart
            //    chartRevenue.Series.Clear();
            //    Series revenueSeries = new Series("Monthly Revenue");
            //    revenueSeries.ChartType = SeriesChartType.Line;
            //    revenueSeries.Color = Color.FromArgb(155, 89, 182); // Purple
            //    revenueSeries.BorderWidth = 3;
            //    revenueSeries.MarkerStyle = MarkerStyle.Circle;
            //    revenueSeries.MarkerSize = 8;

            //    foreach (DataRow row in revenueTable.Rows)
            //    {
            //        revenueSeries.Points.AddXY(row["Month"].ToString(), Convert.ToDouble(row["Revenue"]));
            //    }

            //    chartRevenue.Series.Add(revenueSeries);
            //    chartRevenue.ChartAreas[0].AxisY.Title = "Revenue ($)";
            //    chartRevenue.ChartAreas[0].AxisX.Title = "Month";
            //    chartRevenue.ChartAreas[0].AxisY.LabelStyle.Format = "${0:#,##0}";
            //    chartRevenue.Titles.Clear();
            //    chartRevenue.Titles.Add("Monthly Revenue");
            //}
        }

        private void LoadRevenueByTripTypeData()
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    string query = @"SELECT 
            //                    tr.TripType,
            //                    SUM(b.TotalAmount) as Revenue
            //                    FROM Bookings b
            //                    JOIN Services s ON b.ServiceID = s.ServiceID
            //                    JOIN Trips tr ON b.TripID = tr.TripID
            //                    WHERE s.ProviderID = @ProviderID
            //                    GROUP BY tr.TripType";

            //    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            //    adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", providerId);

            //    DataTable tripTypeTable = new DataTable();
            //    adapter.Fill(tripTypeTable);

            //    // Configure chart
            //    chartTripTypes.Series.Clear();
            //    Series tripTypeSeries = new Series("Revenue by Trip Type");
            //    tripTypeSeries.ChartType = SeriesChartType.Pie;

            //    foreach (DataRow row in tripTypeTable.Rows)
            //    {
            //        tripTypeSeries.Points.AddXY(row["TripType"].ToString(), Convert.ToDouble(row["Revenue"]));
            //    }

            //    // Set datalabels
            //    tripTypeSeries["PieLabelStyle"] = "Outside";
            //    tripTypeSeries.Label = "#VALX: $#VALY{#,##0}";
            //    tripTypeSeries.IsValueShownAsLabel = true;

            //    chartTripTypes.Series.Add(tripTypeSeries);
            //    chartTripTypes.Titles.Clear();
            //    chartTripTypes.Titles.Add("Revenue by Trip Type");
            //}
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
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(282, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Performance Report";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
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
            this.tabControlReports.SelectedIndexChanged += new System.EventHandler(this.tabControlReports_SelectedIndexChanged);
            // 
            // tabOccupancy
            // 
            this.tabOccupancy.Controls.Add(this.chartOccupancy);
            this.tabOccupancy.Location = new System.Drawing.Point(4, 29);
            this.tabOccupancy.Name = "tabOccupancy";
            this.tabOccupancy.Padding = new System.Windows.Forms.Padding(3);
            this.tabOccupancy.Size = new System.Drawing.Size(792, 387);
            this.tabOccupancy.TabIndex = 0;
            this.tabOccupancy.Text = "Occupancy Rate";
            this.tabOccupancy.UseVisualStyleBackColor = true;
            this.tabOccupancy.Click += new System.EventHandler(this.tabOccupancy_Click);
            // 
            // chartOccupancy
            // 
            chartArea1.Name = "ChartArea1";
            this.chartOccupancy.ChartAreas.Add(chartArea1);
            this.chartOccupancy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartOccupancy.Location = new System.Drawing.Point(3, 3);
            this.chartOccupancy.Name = "chartOccupancy";
            this.chartOccupancy.Size = new System.Drawing.Size(786, 381);
            this.chartOccupancy.TabIndex = 0;
            this.chartOccupancy.Click += new System.EventHandler(this.chartOccupancy_Click);
            // 
            // tabServices
            // 
            this.tabServices.Controls.Add(this.chartServices);
            this.tabServices.Location = new System.Drawing.Point(4, 29);
            this.tabServices.Name = "tabServices";
            this.tabServices.Padding = new System.Windows.Forms.Padding(3);
            this.tabServices.Size = new System.Drawing.Size(792, 0);
            this.tabServices.TabIndex = 1;
            this.tabServices.Text = "Service Performance";
            this.tabServices.UseVisualStyleBackColor = true;
            this.tabServices.Click += new System.EventHandler(this.tabServices_Click);
            // 
            // chartServices
            // 
            chartArea2.Name = "ChartArea1";
            this.chartServices.ChartAreas.Add(chartArea2);
            this.chartServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartServices.Location = new System.Drawing.Point(3, 3);
            this.chartServices.Name = "chartServices";
            this.chartServices.Size = new System.Drawing.Size(786, 0);
            this.chartServices.TabIndex = 0;
            this.chartServices.Click += new System.EventHandler(this.chartServices_Click);
            // 
            // tabRevenue
            // 
            this.tabRevenue.Controls.Add(this.chartRevenue);
            this.tabRevenue.Location = new System.Drawing.Point(4, 29);
            this.tabRevenue.Name = "tabRevenue";
            this.tabRevenue.Padding = new System.Windows.Forms.Padding(3);
            this.tabRevenue.Size = new System.Drawing.Size(792, 0);
            this.tabRevenue.TabIndex = 2;
            this.tabRevenue.Text = "Monthly Revenue";
            this.tabRevenue.UseVisualStyleBackColor = true;
            this.tabRevenue.Click += new System.EventHandler(this.tabRevenue_Click);
            // 
            // chartRevenue
            // 
            chartArea3.Name = "ChartArea1";
            this.chartRevenue.ChartAreas.Add(chartArea3);
            this.chartRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartRevenue.Location = new System.Drawing.Point(3, 3);
            this.chartRevenue.Name = "chartRevenue";
            this.chartRevenue.Size = new System.Drawing.Size(786, 0);
            this.chartRevenue.TabIndex = 0;
            this.chartRevenue.Click += new System.EventHandler(this.chartRevenue_Click);
            // 
            // tabTripTypes
            // 
            this.tabTripTypes.Controls.Add(this.chartTripTypes);
            this.tabTripTypes.Location = new System.Drawing.Point(4, 29);
            this.tabTripTypes.Name = "tabTripTypes";
            this.tabTripTypes.Padding = new System.Windows.Forms.Padding(3);
            this.tabTripTypes.Size = new System.Drawing.Size(792, 0);
            this.tabTripTypes.TabIndex = 3;
            this.tabTripTypes.Text = "Revenue by Trip Type";
            this.tabTripTypes.UseVisualStyleBackColor = true;
            this.tabTripTypes.Click += new System.EventHandler(this.tabTripTypes_Click);
            // 
            // chartTripTypes
            // 
            chartArea4.Name = "ChartArea1";
            this.chartTripTypes.ChartAreas.Add(chartArea4);
            this.chartTripTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTripTypes.Location = new System.Drawing.Point(3, 3);
            this.chartTripTypes.Name = "chartTripTypes";
            this.chartTripTypes.Size = new System.Drawing.Size(786, 0);
            this.chartTripTypes.TabIndex = 0;
            this.chartTripTypes.Click += new System.EventHandler(this.chartTripTypes_Click);
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
            this.pnlFooter.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlFooter_Paint);
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Export functionality - for now just a placeholder
            MessageBox.Show("Report export functionality will be implemented.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Go back to previous form
            TourOperatorDashboard dash = new TourOperatorDashboard();
            dash.Show();
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

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void tabControlReports_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabOccupancy_Click(object sender, EventArgs e)
        {

        }

        private void tabServices_Click(object sender, EventArgs e)
        {

        }

        private void chartServices_Click(object sender, EventArgs e)
        {

        }

        private void tabRevenue_Click(object sender, EventArgs e)
        {

        }

        private void chartRevenue_Click(object sender, EventArgs e)
        {

        }

        private void tabTripTypes_Click(object sender, EventArgs e)
        {

        }

        private void chartTripTypes_Click(object sender, EventArgs e)
        {

        }

        private void pnlFooter_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}