using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TravelBookingSystem
{
    public partial class AbandonedBookingAnalysisForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";
        private DataTable abandonedBookingsData;

        public AbandonedBookingAnalysisForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Abandoned Booking Analysis";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Create summary panel
            Panel summaryPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Summary labels
            Label lblTitle = new Label
            {
                Text = "Abandoned Booking Analysis Dashboard",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(500, 30)
            };
            summaryPanel.Controls.Add(lblTitle);

            // Key metrics labels
            lblAbandonmentRate = new Label
            {
                Text = "Abandonment Rate: Loading...",
                Font = new Font("Arial", 10),
                Location = new Point(10, 50),
                Size = new Size(300, 20)
            };
            summaryPanel.Controls.Add(lblAbandonmentRate);

            lblRecoveryRate = new Label
            {
                Text = "Recovery Rate: Loading...",
                Font = new Font("Arial", 10),
                Location = new Point(10, 75),
                Size = new Size(300, 20)
            };
            summaryPanel.Controls.Add(lblRecoveryRate);

            lblPotentialLoss = new Label
            {
                Text = "Potential Revenue Loss: Loading...",
                Font = new Font("Arial", 10),
                Location = new Point(400, 50),
                Size = new Size(300, 20)
            };
            summaryPanel.Controls.Add(lblPotentialLoss);

            // Date range controls
            Label lblDateRange = new Label
            {
                Text = "Date Range:",
                Font = new Font("Arial", 10),
                Location = new Point(400, 75),
                Size = new Size(100, 20)
            };
            summaryPanel.Controls.Add(lblDateRange);

            dtpStartDate = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(500, 75),
                Size = new Size(100, 20),
                Value = DateTime.Now.AddMonths(-1)
            };
            summaryPanel.Controls.Add(dtpStartDate);

            dtpEndDate = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(610, 75),
                Size = new Size(100, 20),
                Value = DateTime.Now
            };
            summaryPanel.Controls.Add(dtpEndDate);

            btnRefresh = new Button
            {
                Text = "Refresh",
                Location = new Point(720, 75),
                Size = new Size(80, 25)
            };
            btnRefresh.Click += BtnRefresh_Click;
            summaryPanel.Controls.Add(btnRefresh);

            // Add summary panel to form
            this.Controls.Add(summaryPanel);

            // Create tab control for charts
            TabControl tabCharts = new TabControl
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 120),
                Size = new Size(this.Width, this.Height - 120)
            };

            // Create tabs
            TabPage tabStages = new TabPage("Abandonment Stages");
            TabPage tabReasons = new TabPage("Abandonment Reasons");
            TabPage tabRevenue = new TabPage("Revenue Analysis");
            TabPage tabData = new TabPage("Data Grid");

            // Create charts
            chartStages = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };
            ConfigureChart(chartStages, "Abandonment by Stage", SeriesChartType.Pie);
            tabStages.Controls.Add(chartStages);

            chartReasons = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };
            ConfigureChart(chartReasons, "Abandonment by Reason", SeriesChartType.Bar);
            tabReasons.Controls.Add(chartReasons);

            chartRevenue = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };
            ConfigureChart(chartRevenue, "Potential Revenue Loss", SeriesChartType.Column);
            tabRevenue.Controls.Add(chartRevenue);

            // Create data grid
            dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            tabData.Controls.Add(dataGridView);

            // Add tabs to tab control
            tabCharts.TabPages.Add(tabStages);
            tabCharts.TabPages.Add(tabReasons);
            tabCharts.TabPages.Add(tabRevenue);
            tabCharts.TabPages.Add(tabData);

            // Add tab control to form
            this.Controls.Add(tabCharts);

            // Export button
            btnExport = new Button
            {
                Text = "Export Report",
                Dock = DockStyle.Bottom,
                Height = 30
            };
            btnExport.Click += BtnExport_Click;
            this.Controls.Add(btnExport);
        }

        // Form controls
        private Label lblAbandonmentRate;
        private Label lblRecoveryRate;
        private Label lblPotentialLoss;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnRefresh;
        private Button btnExport;
        private Chart chartStages;
        private Chart chartReasons;
        private Chart chartRevenue;
        private DataGridView dataGridView;

        private void ConfigureChart(Chart chart, string title, SeriesChartType chartType)
        {
            chart.Titles.Add(new Title(title, Docking.Top, new Font("Arial", 12, FontStyle.Bold), Color.Black));

            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.BackColor = Color.White;
            chart.ChartAreas.Add(chartArea);

            Series series = new Series(title);
            series.ChartType = chartType;
            if (chartType == SeriesChartType.Pie)
            {
                series.Label = "#PERCENT{P0}";
                series.LegendText = "#VALX";
                series["PieLabelStyle"] = "Outside";
                series["PieLineColor"] = "Black";
            }
            else
            {
                series.XValueType = ChartValueType.String;
                series.YValueType = ChartValueType.Int32;
            }
            chart.Series.Add(series);

            chart.Legends.Add(new Legend());
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT ab.abandoned_id, ab.abandonment_stage, ab.abandonment_reason, 
                           ab.abandonment_timestamp, ab.potential_revenue, ab.recovery_attempt_made, 
                           ab.recovered, b.booking_id, b.booking_date, b.total_amount
                    FROM AbandonedBooking ab
                    INNER JOIN Booking b ON ab.booking_id = b.booking_id
                    WHERE ab.abandonment_timestamp BETWEEN @StartDate AND @EndDate
                    ORDER BY ab.abandonment_timestamp DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StartDate", dtpStartDate.Value.Date);
                    command.Parameters.AddWithValue("@EndDate", dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1));

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    abandonedBookingsData = new DataTable();
                    adapter.Fill(abandonedBookingsData);

                    // Calculate metrics
                    CalculateMetrics();

                    // Update charts
                    UpdateCharts();

                    // Update data grid
                    dataGridView.DataSource = abandonedBookingsData;
                    FormatDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateMetrics()
        {
            if (abandonedBookingsData == null || abandonedBookingsData.Rows.Count == 0)
            {
                lblAbandonmentRate.Text = "Abandonment Rate: No data";
                lblRecoveryRate.Text = "Recovery Rate: No data";
                lblPotentialLoss.Text = "Potential Revenue Loss: No data";
                return;
            }

            // Get total bookings count (would need additional query in real implementation)
            // For demo purposes, assuming 100 total bookings
            int totalBookings = 100;
            int abandonedCount = abandonedBookingsData.Rows.Count;

            // Calculate abandonment rate
            decimal abandonmentRate = (decimal)abandonedCount / totalBookings * 100;
            lblAbandonmentRate.Text = $"Abandonment Rate: {abandonmentRate:0.00}%";

            // Calculate recovery rate
            int recoveredCount = abandonedBookingsData.AsEnumerable()
                .Count(row => Convert.ToInt32(row["recovered"]) == 1);
            decimal recoveryRate = (decimal)recoveredCount / abandonedCount * 100;
            lblRecoveryRate.Text = $"Recovery Rate: {recoveryRate:0.00}%";

            // Calculate potential revenue loss
            decimal potentialLoss = abandonedBookingsData.AsEnumerable()
                .Where(row => Convert.ToInt32(row["recovered"]) == 0)
                .Sum(row => Convert.ToDecimal(row["potential_revenue"]));
            lblPotentialLoss.Text = $"Potential Revenue Loss: ${potentialLoss:0.00}";
        }

        private void UpdateCharts()
        {
            if (abandonedBookingsData == null || abandonedBookingsData.Rows.Count == 0)
            {
                ClearCharts();
                return;
            }

            // Abandonment stages chart (Pie)
            chartStages.Series[0].Points.Clear();
            var stageGroups = abandonedBookingsData.AsEnumerable()
                .GroupBy(row => row["abandonment_stage"].ToString())
                .Select(g => new { Stage = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count);

            foreach (var group in stageGroups)
            {
                chartStages.Series[0].Points.AddXY(group.Stage, group.Count);
            }

            // Abandonment reasons chart (Bar)
            chartReasons.Series[0].Points.Clear();
            var reasonGroups = abandonedBookingsData.AsEnumerable()
                .GroupBy(row => row["abandonment_reason"].ToString())
                .Select(g => new { Reason = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(10); // Top 10 reasons

            foreach (var group in reasonGroups)
            {
                chartReasons.Series[0].Points.AddXY(group.Reason, group.Count);
            }

            // Revenue analysis chart (Column)
            chartRevenue.Series[0].Points.Clear();
            var revenueByStage = abandonedBookingsData.AsEnumerable()
                .GroupBy(row => row["abandonment_stage"].ToString())
                .Select(g => new
                {
                    Stage = g.Key,
                    PotentialRevenue = g.Sum(row => Convert.ToDecimal(row["potential_revenue"]))
                })
                .OrderByDescending(g => g.PotentialRevenue);

            foreach (var group in revenueByStage)
            {
                chartRevenue.Series[0].Points.AddXY(group.Stage, group.PotentialRevenue);
            }
        }

        private void ClearCharts()
        {
            chartStages.Series[0].Points.Clear();
            chartReasons.Series[0].Points.Clear();
            chartRevenue.Series[0].Points.Clear();
        }

        private void FormatDataGrid()
        {
            if (dataGridView.Columns.Count == 0)
                return;

            // Rename columns to be more user-friendly
            dataGridView.Columns["abandoned_id"].HeaderText = "ID";
            dataGridView.Columns["abandonment_stage"].HeaderText = "Stage";
            dataGridView.Columns["abandonment_reason"].HeaderText = "Reason";
            dataGridView.Columns["abandonment_timestamp"].HeaderText = "Timestamp";
            dataGridView.Columns["potential_revenue"].HeaderText = "Potential Revenue ($)";
            dataGridView.Columns["booking_id"].HeaderText = "Booking ID";
            dataGridView.Columns["booking_date"].HeaderText = "Booking Date";
            dataGridView.Columns["total_amount"].HeaderText = "Total Amount ($)";

            // Convert int columns to CheckBox columns
            DataGridViewCheckBoxColumn recoveryAttemptColumn = new DataGridViewCheckBoxColumn();
            recoveryAttemptColumn.HeaderText = "Recovery Attempted";
            recoveryAttemptColumn.Name = "recovery_attempt_made_bool";
            recoveryAttemptColumn.ReadOnly = true;
            dataGridView.Columns.Add(recoveryAttemptColumn);

            DataGridViewCheckBoxColumn recoveredColumn = new DataGridViewCheckBoxColumn();
            recoveredColumn.HeaderText = "Recovered";
            recoveredColumn.Name = "recovered_bool";
            recoveredColumn.ReadOnly = true;
            dataGridView.Columns.Add(recoveredColumn);

            // Hide original columns
            dataGridView.Columns["recovery_attempt_made"].Visible = false;
            dataGridView.Columns["recovered"].Visible = false;

            // Set checkbox values based on integer values
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["recovery_attempt_made"].Value != null)
                {
                    row.Cells["recovery_attempt_made_bool"].Value =
                        Convert.ToInt32(row.Cells["recovery_attempt_made"].Value) == 1;
                }

                if (row.Cells["recovered"].Value != null)
                {
                    row.Cells["recovered_bool"].Value =
                        Convert.ToInt32(row.Cells["recovered"].Value) == 1;
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx",
                    Title = "Export Abandoned Booking Report"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveDialog.FileName.EndsWith(".csv"))
                    {
                        // Export to CSV
                        ExportToCSV(saveDialog.FileName);
                    }
                    else if (saveDialog.FileName.EndsWith(".xlsx"))
                    {
                        // In a real application, you would use a library like EPPlus
                        // or NPOI to export to Excel
                        MessageBox.Show("Excel export would be implemented in a real application. CSV export is available.",
                            "Feature Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCSV(string filePath)
        {
            if (abandonedBookingsData == null || abandonedBookingsData.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath))
            {
                // Write header
                List<string> headers = new List<string>();
                foreach (DataColumn column in abandonedBookingsData.Columns)
                {
                    headers.Add(column.ColumnName);
                }
                sw.WriteLine(string.Join(",", headers));

                // Write data rows
                foreach (DataRow row in abandonedBookingsData.Rows)
                {
                    List<string> fields = new List<string>();
                    foreach (var item in row.ItemArray)
                    {
                        // Escape commas and quotes
                        string field = item.ToString().Replace("\"", "\"\"");
                        if (field.Contains(","))
                        {
                            field = "\"" + field + "\"";
                        }
                        fields.Add(field);
                    }
                    sw.WriteLine(string.Join(",", fields));
                }
            }

            MessageBox.Show("Export completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Sample data method - would not be needed in a real application
        public void LoadSampleData()
        {
            abandonedBookingsData = new DataTable();

            // Create columns
            abandonedBookingsData.Columns.Add("abandoned_id", typeof(int));
            abandonedBookingsData.Columns.Add("abandonment_stage", typeof(string));
            abandonedBookingsData.Columns.Add("abandonment_reason", typeof(string));
            abandonedBookingsData.Columns.Add("abandonment_timestamp", typeof(DateTime));
            abandonedBookingsData.Columns.Add("potential_revenue", typeof(decimal));
            abandonedBookingsData.Columns.Add("recovery_attempt_made", typeof(int));
            abandonedBookingsData.Columns.Add("recovered", typeof(int));
            abandonedBookingsData.Columns.Add("booking_id", typeof(int));
            abandonedBookingsData.Columns.Add("booking_date", typeof(DateTime));
            abandonedBookingsData.Columns.Add("total_amount", typeof(decimal));

            // Add sample data
            abandonedBookingsData.Rows.Add(1, "Payment", "Payment Declined", DateTime.Now.AddDays(-10), 499.99, 1, 0, 1001, DateTime.Now.AddDays(-10), 499.99);
            abandonedBookingsData.Rows.Add(2, "Review", "Changed Mind", DateTime.Now.AddDays(-9), 299.50, 1, 1, 1002, DateTime.Now.AddDays(-9), 299.50);
            abandonedBookingsData.Rows.Add(3, "Payment", "High Price", DateTime.Now.AddDays(-8), 799.99, 1, 0, 1003, DateTime.Now.AddDays(-8), 799.99);
            abandonedBookingsData.Rows.Add(4, "Personal Info", "Privacy Concerns", DateTime.Now.AddDays(-7), 399.99, 0, 0, 1004, DateTime.Now.AddDays(-7), 399.99);
            abandonedBookingsData.Rows.Add(5, "Payment", "Technical Error", DateTime.Now.AddDays(-6), 599.99, 1, 1, 1005, DateTime.Now.AddDays(-6), 599.99);
            abandonedBookingsData.Rows.Add(6, "Confirmation", "Unsure About Dates", DateTime.Now.AddDays(-5), 349.99, 1, 0, 1006, DateTime.Now.AddDays(-5), 349.99);
            abandonedBookingsData.Rows.Add(7, "Review", "Found Better Option", DateTime.Now.AddDays(-4), 449.99, 0, 0, 1007, DateTime.Now.AddDays(-4), 449.99);
            abandonedBookingsData.Rows.Add(8, "Payment", "Payment Declined", DateTime.Now.AddDays(-3), 699.99, 1, 0, 1008, DateTime.Now.AddDays(-3), 699.99);
            abandonedBookingsData.Rows.Add(9, "Personal Info", "Complex Process", DateTime.Now.AddDays(-2), 249.99, 1, 1, 1009, DateTime.Now.AddDays(-2), 249.99);
            abandonedBookingsData.Rows.Add(10, "Payment", "High Price", DateTime.Now.AddDays(-1), 599.99, 1, 0, 1010, DateTime.Now.AddDays(-1), 599.99);

            // Calculate metrics
            CalculateMetrics();

            // Update charts
            UpdateCharts();

            // Update data grid
            dataGridView.DataSource = abandonedBookingsData;
            FormatDataGrid();
        }

        
    }
}