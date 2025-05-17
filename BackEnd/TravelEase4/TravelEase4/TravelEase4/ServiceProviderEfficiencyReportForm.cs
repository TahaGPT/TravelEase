using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ServiceProviderEfficiencyReport
{
    public partial class ServiceProviderEfficiencyReportForm : Form
    {
        // Connection string - update with your actual connection details
        private string connectionString = @"Data Source=DESKTOP-2I8F60O\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

        public ServiceProviderEfficiencyReportForm()
        {
            InitializeComponent();
        }

        private void ServiceProviderEfficiencyReportForm_Load(object sender, EventArgs e)
        {
            // Initialize the date pickers with default values (last month)
            dtpStartDate.Value = DateTime.Now.AddMonths(-1);
            dtpEndDate.Value = DateTime.Now;

            // Load provider types for the combo box
            cboProviderType.Items.Add("All");
            cboProviderType.Items.Add("Hotel");
            cboProviderType.Items.Add("Guide");
            cboProviderType.Items.Add("Transport");
            cboProviderType.SelectedIndex = 0;

            // Initial load of data
            LoadData();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Clear previous data
                dgvServiceProviders.DataSource = null;
                chartPerformance.Series.Clear();

                string providerType = cboProviderType.SelectedItem.ToString();
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1); // End of selected day

                // Load appropriate data based on provider type
                switch (providerType)
                {
                    case "All":
                        LoadAllProvidersData(startDate, endDate);
                        break;
                    case "Hotel":
                        LoadHotelData(startDate, endDate);
                        break;
                    case "Guide":
                        LoadGuideData(startDate, endDate);
                        break;
                    case "Transport":
                        LoadTransportData(startDate, endDate);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadAllProvidersData(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a command to get all service providers with their performance metrics
                using (SqlCommand command = new SqlCommand(@"
                    SELECT 
                        sp.provider_id, 
                        sp.name AS ProviderName, 
                        sp.service_type AS ProviderType,
                        COUNT(bs.booking_service_id_) AS TotalBookings,
                        SUM(CASE WHEN bs.service_status = 'Completed' THEN 1 ELSE 0 END) AS CompletedServices,
                        CASE 
                            WHEN COUNT(bs.booking_service_id_) > 0 
                            THEN CAST(SUM(CASE WHEN bs.service_status = 'Completed' THEN 1 ELSE 0 END) AS FLOAT) / COUNT(bs.booking_service_id_) * 100 
                            ELSE 0 
                        END AS CompletionRate,
                        CASE 
                            WHEN sp.service_type = 'Hotel' THEN 'Hotel Occupancy'
                            WHEN sp.service_type = 'Guide' THEN 'Guide Rating'
                            WHEN sp.service_type = 'Transport' THEN 'On-Time Performance'
                            ELSE 'General Performance'
                        END AS MetricType
                    FROM 
                        ServiceProvider sp
                    LEFT JOIN 
                        BookingServices_ bs ON sp.provider_id = bs.provider_id
                    LEFT JOIN 
                        Booking b ON bs.booking_id = b.booking_id
                    WHERE 
                        b.booking_date BETWEEN @StartDate AND @EndDate
                        OR b.booking_date IS NULL
                    GROUP BY 
                        sp.provider_id, sp.name, sp.service_type
                    ORDER BY 
                        sp.service_type, CompletionRate DESC", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvServiceProviders.DataSource = dataTable;

                    // Create chart
                    CreateProviderTypeChart(dataTable);
                }
            }
        }

        private void LoadHotelData(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a command to get hotel occupancy data
                using (SqlCommand command = new SqlCommand(@"
                    SELECT 
                        h.hotel_id,
                        sp.name AS HotelName,
                        h.total_rooms AS TotalRooms,
                        COUNT(bs.booking_service_id_) AS BookedRooms,
                        CASE 
                            WHEN h.total_rooms > 0 
                            THEN CAST(COUNT(bs.booking_service_id_) AS FLOAT) / h.total_rooms * 100 
                            ELSE 0 
                        END AS OccupancyRate,
                        h.star_rating AS StarRating,
                        CASE WHEN h.wheelchair_accessible = 1 THEN 'Yes' ELSE 'No' END AS WheelchairAccessible
                    FROM 
                        Hotel h
                    JOIN 
                        ServiceProvider sp ON h.provider_id = sp.provider_id
                    LEFT JOIN 
                        BookingServices_ bs ON sp.provider_id = bs.provider_id
                    LEFT JOIN 
                        Booking b ON bs.booking_id = b.booking_id AND b.booking_date BETWEEN @StartDate AND @EndDate
                    GROUP BY 
                        h.hotel_id, sp.name, h.total_rooms, h.star_rating, h.wheelchair_accessible
                    ORDER BY 
                        OccupancyRate DESC", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvServiceProviders.DataSource = dataTable;

                    // Create hotel occupancy chart
                    CreateHotelOccupancyChart(dataTable);
                }
            }
        }

        private void LoadGuideData(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a command to get guide ratings
                using (SqlCommand command = new SqlCommand(@"
                    SELECT 
                        g.guide_id,
                        sp.name AS GuideName,
                        g.specialization,
                        g.experience_years AS ExperienceYears,
                        COUNT(DISTINCT gl.language) AS LanguagesCount,
                        COALESCE(AVG(CAST(r.rating AS FLOAT)), 0) AS AverageRating,
                        COUNT(DISTINCT bs.booking_service_id_) AS TotalBookings
                    FROM 
                        Guide_ g
                    JOIN 
                        ServiceProvider sp ON g.provider_id = sp.provider_id
                    LEFT JOIN 
                        GuideLanguages gl ON g.guide_id = gl.guide_id
                    LEFT JOIN 
                        BookingServices_ bs ON sp.provider_id = bs.provider_id
                    LEFT JOIN 
                        Booking b ON bs.booking_id = b.booking_id
                    LEFT JOIN 
                        Review r ON b.traveler_id = r.traveler_id AND b.trip_id = r.trip_id
                    WHERE 
                        b.booking_date BETWEEN @StartDate AND @EndDate
                        OR b.booking_date IS NULL
                    GROUP BY 
                        g.guide_id, sp.name, g.specialization, g.experience_years
                    ORDER BY 
                        AverageRating DESC", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvServiceProviders.DataSource = dataTable;

                    // Create guide ratings chart
                    CreateGuideRatingsChart(dataTable);
                }
            }
        }

        private void LoadTransportData(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a command to get transport on-time performance
                using (SqlCommand command = new SqlCommand(@"
                    SELECT 
                        t.transport_id,
                        sp.name AS TransportProviderName,
                        t.transport_type,
                        t.fleet_size,
                        t.capacity_per_vehicle,
                        COUNT(bs.booking_service_id_) AS TotalTrips,
                        SUM(CASE WHEN bs.service_status = 'Completed' THEN 1 ELSE 0 END) AS CompletedTrips,
                        CASE 
                            WHEN COUNT(bs.booking_service_id_) > 0 
                            THEN CAST(SUM(CASE WHEN bs.service_status = 'Completed' THEN 1 ELSE 0 END) AS FLOAT) / COUNT(bs.booking_service_id_) * 100 
                            ELSE 0 
                        END AS OnTimePerformance
                    FROM 
                        TransportProvider t
                    JOIN 
                        ServiceProvider sp ON t.provider_id = sp.provider_id
                    LEFT JOIN 
                        BookingServices_ bs ON sp.provider_id = bs.provider_id
                    LEFT JOIN 
                        Booking b ON bs.booking_id = b.booking_id
                    WHERE 
                        b.booking_date BETWEEN @StartDate AND @EndDate
                        OR b.booking_date IS NULL
                    GROUP BY 
                        t.transport_id, sp.name, t.transport_type, t.fleet_size, t.capacity_per_vehicle
                    ORDER BY 
                        OnTimePerformance DESC", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvServiceProviders.DataSource = dataTable;

                    // Create transport performance chart
                    CreateTransportPerformanceChart(dataTable);
                }
            }
        }

        private void CreateProviderTypeChart(DataTable dataTable)
        {
            chartPerformance.Series.Clear();
            chartPerformance.Titles.Clear();

            // Add a title
            chartPerformance.Titles.Add("Service Provider Performance by Type");

            // Create a pie chart for provider types
            Series series = new Series("ProviderTypes");
            series.ChartType = SeriesChartType.Pie;

            // Group by provider type and calculate average completion rate
            var providerGroups = dataTable.AsEnumerable()
                .GroupBy(row => row.Field<string>("ProviderType"))
                .Select(g => new {
                    ProviderType = g.Key,
                    AverageRate = g.Average(r => Convert.ToDouble(r["CompletionRate"]))
                });

            foreach (var group in providerGroups)
            {
                if (!string.IsNullOrEmpty(group.ProviderType))
                {
                    series.Points.AddXY(group.ProviderType, group.AverageRate);
                }
            }

            // Format the pie chart
            series.IsValueShownAsLabel = true;
            series.Label = "#VALX: #PERCENT{P1}";

            chartPerformance.Series.Add(series);
        }

        private void CreateHotelOccupancyChart(DataTable dataTable)
        {
            chartPerformance.Series.Clear();
            chartPerformance.Titles.Clear();

            // Add a title
            chartPerformance.Titles.Add("Hotel Occupancy Rates");

            // Create a bar chart for hotel occupancy
            Series series = new Series("Occupancy");
            series.ChartType = SeriesChartType.Bar;

            foreach (DataRow row in dataTable.Rows)
            {
                string hotelName = row["HotelName"].ToString();
                double occupancyRate = Convert.ToDouble(row["OccupancyRate"]);

                series.Points.AddXY(hotelName, occupancyRate);
            }

            // Format the chart
            series.IsValueShownAsLabel = true;
            series.Label = "#VAL{N1}%";

            chartPerformance.Series.Add(series);
            chartPerformance.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartPerformance.ChartAreas[0].AxisX.Interval = 1;
            chartPerformance.ChartAreas[0].AxisY.Title = "Occupancy Rate (%)";
            chartPerformance.ChartAreas[0].AxisY.Maximum = 100;
        }

        private void CreateGuideRatingsChart(DataTable dataTable)
        {
            chartPerformance.Series.Clear();
            chartPerformance.Titles.Clear();

            // Add a title
            chartPerformance.Titles.Add("Guide Ratings");

            // Create a column chart for guide ratings
            Series series = new Series("Ratings");
            series.ChartType = SeriesChartType.Column;

            foreach (DataRow row in dataTable.Rows)
            {
                string guideName = row["GuideName"].ToString();
                double rating = Convert.ToDouble(row["AverageRating"]);

                series.Points.AddXY(guideName, rating);
            }

            // Format the chart
            series.IsValueShownAsLabel = true;
            series.Label = "#VAL{N1}";

            chartPerformance.Series.Add(series);
            chartPerformance.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartPerformance.ChartAreas[0].AxisX.Interval = 1;
            chartPerformance.ChartAreas[0].AxisY.Title = "Average Rating";
            chartPerformance.ChartAreas[0].AxisY.Maximum = 5;
        }

        private void CreateTransportPerformanceChart(DataTable dataTable)
        {
            chartPerformance.Series.Clear();
            chartPerformance.Titles.Clear();

            // Add a title
            chartPerformance.Titles.Add("Transport On-Time Performance");

            // Create a column chart for transport performance
            Series series = new Series("OnTimePerformance");
            series.ChartType = SeriesChartType.Column;

            foreach (DataRow row in dataTable.Rows)
            {
                string providerName = row["TransportProviderName"].ToString();
                double performance = Convert.ToDouble(row["OnTimePerformance"]);

                series.Points.AddXY(providerName, performance);
            }

            // Format the chart
            series.IsValueShownAsLabel = true;
            series.Label = "#VAL{N1}%";

            chartPerformance.Series.Add(series);
            chartPerformance.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartPerformance.ChartAreas[0].AxisX.Interval = 1;
            chartPerformance.ChartAreas[0].AxisY.Title = "On-Time Performance (%)";
            chartPerformance.ChartAreas[0].AxisY.Maximum = 100;
        }

        private void cboProviderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblProviderType = new System.Windows.Forms.Label();
            this.cboProviderType = new System.Windows.Forms.ComboBox();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dgvServiceProviders = new System.Windows.Forms.DataGridView();
            this.chartPerformance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceProviders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1084, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Service Provider Efficiency Report";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProviderType
            // 
            this.lblProviderType.AutoSize = true;
            this.lblProviderType.Location = new System.Drawing.Point(12, 14);
            this.lblProviderType.Name = "lblProviderType";
            this.lblProviderType.Size = new System.Drawing.Size(80, 13);
            this.lblProviderType.TabIndex = 1;
            this.lblProviderType.Text = "Provider Type:";
            // 
            // cboProviderType
            // 
            this.cboProviderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProviderType.FormattingEnabled = true;
            this.cboProviderType.Location = new System.Drawing.Point(98, 11);
            this.cboProviderType.Name = "cboProviderType";
            this.cboProviderType.Size = new System.Drawing.Size(121, 21);
            this.cboProviderType.TabIndex = 2;
            this.cboProviderType.SelectedIndexChanged += new System.EventHandler(this.cboProviderType_SelectedIndexChanged);
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(277, 14);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(70, 13);
            this.lblDateRange.TabIndex = 3;
            this.lblDateRange.Text = "Date Range:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(353, 11);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(100, 20);
            this.dtpStartDate.TabIndex = 4;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(459, 14);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 13);
            this.lblTo.TabIndex = 5;
            this.lblTo.Text = "to:";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(485, 11);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(100, 20);
            this.dtpEndDate.TabIndex = 6;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(615, 9);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(100, 23);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate Report";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // dgvServiceProviders
            // 
            this.dgvServiceProviders.AllowUserToAddRows = false;
            this.dgvServiceProviders.AllowUserToDeleteRows = false;
            this.dgvServiceProviders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServiceProviders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServiceProviders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServiceProviders.Location = new System.Drawing.Point(0, 0);
            this.dgvServiceProviders.Name = "dgvServiceProviders";
            this.dgvServiceProviders.ReadOnly = true;
            this.dgvServiceProviders.Size = new System.Drawing.Size(1084, 224);
            this.dgvServiceProviders.TabIndex = 8;
            // 
            // chartPerformance
            // 
            chartArea.Name = "ChartArea1";
            this.chartPerformance.ChartAreas.Add(chartArea);
            this.chartPerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            legend.Name = "Legend1";
            this.chartPerformance.Legends.Add(legend);
            this.chartPerformance.Location = new System.Drawing.Point(0, 0);
            this.chartPerformance.Name = "chartPerformance";
            this.chartPerformance.Size = new System.Drawing.Size(1084, 307);
            this.chartPerformance.TabIndex = 9;
            this.chartPerformance.Text = "Performance Chart";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblProviderType);
            this.panel1.Controls.Add(this.cboProviderType);
            this.panel1.Controls.Add(this.lblDateRange);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Controls.Add(this.lblTo);
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1084, 40);
            this.panel1.TabIndex = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 70);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvServiceProviders);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chartPerformance);
            this.splitContainer1.Size = new System.Drawing.Size(1084, 541);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 11;
            // 
            // ServiceProviderEfficiencyReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 611);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Name = "ServiceProviderEfficiencyReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Provider Efficiency Report";
            this.Load += new System.EventHandler(this.ServiceProviderEfficiencyReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceProviders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblProviderType;
        private System.Windows.Forms.ComboBox cboProviderType;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dgvServiceProviders;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPerformance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}