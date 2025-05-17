using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TravelEaseDashboard
{
    public class PlatformGrowthReport : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-2I8F60O\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

        public PlatformGrowthReport()
        {
            this.Text = "TravelEase Analytics Dashboard";
            this.WindowState = FormWindowState.Maximized;

            TabControl tabControl = new TabControl { Dock = DockStyle.Fill };
            this.Controls.Add(tabControl);

            AddRegistrationTab(tabControl);
            AddActiveUsersTab(tabControl);
            AddPartnershipTab(tabControl);
            AddRegionalTab(tabControl);
        }

        private void AddRegistrationTab(TabControl tabControl)
        {
            TabPage tab = new TabPage("New User Registrations");
            tabControl.TabPages.Add(tab);

            Panel panel = new Panel { Dock = DockStyle.Fill };
            tab.Controls.Add(panel);

            Chart chart = new Chart { Dock = DockStyle.Fill, Height = panel.Height - 40 };
            panel.Controls.Add(chart);
            chart.ChartAreas.Add(new ChartArea());

            Button exportBtn = new Button { Text = "Export Data", Dock = DockStyle.Bottom, Height = 40 };
            exportBtn.Click += (sender, e) => ExportData(chart, "User_Registrations");
            panel.Controls.Add(exportBtn);

            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        FORMAT(T.registration_date, 'yyyy-MM') AS Month,
                        (SELECT COUNT(*) FROM Traveler WHERE FORMAT(registration_date, 'yyyy-MM') = FORMAT(T.registration_date, 'yyyy-MM')) AS Travelers,
                        (SELECT COUNT(*) FROM TourOperator WHERE FORMAT(registration_date, 'yyyy-MM') = FORMAT(T.registration_date, 'yyyy-MM')) AS Operators,
                        (SELECT COUNT(*) FROM ServiceProvider WHERE FORMAT(registration_date, 'yyyy-MM') = FORMAT(T.registration_date, 'yyyy-MM')) AS Providers
                    FROM (
                        SELECT registration_date FROM Traveler
                        UNION SELECT registration_date FROM TourOperator
                        UNION SELECT registration_date FROM ServiceProvider
                    ) AS T
                    GROUP BY FORMAT(T.registration_date, 'yyyy-MM'), T.registration_date
                    ORDER BY Month";

                new SqlDataAdapter(query, conn).Fill(data);
            }

            AddSeries(chart, "Travelers", SeriesChartType.Column, "Month", "Travelers", Color.Blue);
            AddSeries(chart, "Operators", SeriesChartType.Column, "Month", "Operators", Color.Green);
            AddSeries(chart, "Providers", SeriesChartType.Column, "Month", "Providers", Color.Orange);

            chart.DataSource = data;
            chart.DataBind();
            chart.Titles.Add("New User Registrations by Month");
            chart.ChartAreas[0].AxisX.Title = "Month";
            chart.ChartAreas[0].AxisY.Title = "Count";
            chart.Legends.Add(new Legend());
        }

        private void AddActiveUsersTab(TabControl tabControl)
        {
            TabPage tab = new TabPage("Active Users");
            tabControl.TabPages.Add(tab);

            Panel panel = new Panel { Dock = DockStyle.Fill };
            tab.Controls.Add(panel);

            Chart chart = new Chart { Dock = DockStyle.Fill, Height = panel.Height - 40 };
            panel.Controls.Add(chart);
            chart.ChartAreas.Add(new ChartArea());

            // Debug Label
            Label debugLabel = new Label { Dock = DockStyle.Bottom, Height = 60, BorderStyle = BorderStyle.FixedSingle };
            panel.Controls.Add(debugLabel);

            Button exportBtn = new Button { Text = "Export Data", Dock = DockStyle.Bottom, Height = 40 };
            exportBtn.Click += (sender, e) => ExportData(chart, "Active_Users");
            panel.Controls.Add(exportBtn);

            // Get and verify data
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                -- Get active travelers (last 6 months)
                SELECT 
                    FORMAT(last_login_date, 'yyyy-MM') AS Month,
                    COUNT(*) AS Travelers,
                    0 AS Operators  -- Initialize Operators column
                FROM Traveler
                WHERE last_login_date >= DATEADD(MONTH, -6, GETDATE())
                GROUP BY FORMAT(last_login_date, 'yyyy-MM')
                
                UNION ALL
                
                -- Get active operators (last 6 months)
                SELECT 
                    FORMAT(last_login_date_, 'yyyy-MM') AS Month,
                    0 AS Travelers,  -- Initialize Travelers column
                    COUNT(*) AS Operators
                FROM TourOperator
                WHERE last_login_date_ >= DATEADD(MONTH, -6, GETDATE())
                GROUP BY FORMAT(last_login_date_, 'yyyy-MM')
                
                ORDER BY Month";

                    new SqlDataAdapter(query, conn).Fill(data);

                    // Debug output
                    debugLabel.Text = $"Data loaded: {data.Rows.Count} rows\n";
                    if (data.Rows.Count > 0)
                    {
                        debugLabel.Text += $"First month: {data.Rows[0]["Month"]} - " +
                                         $"Travelers: {data.Rows[0]["Travelers"]}, " +
                                         $"Operators: {data.Rows[0]["Operators"]}";
                    }
                }

                // Create series if we have data
                if (data.Rows.Count > 0)
                {
                    // Combine months (since we have separate rows for travelers/operators)
                    DataTable combinedData = data.Clone();
                    var months = data.AsEnumerable()
                                   .Select(row => row.Field<string>("Month"))
                                   .Distinct()
                                   .OrderBy(m => m);

                    foreach (string month in months)
                    {
                        DataRow newRow = combinedData.NewRow();
                        newRow["Month"] = month;

                        var travelerRow = data.Select($"Month = '{month}' AND Travelers > 0");
                        newRow["Travelers"] = travelerRow.Length > 0 ? travelerRow[0]["Travelers"] : 0;

                        var operatorRow = data.Select($"Month = '{month}' AND Operators > 0");
                        newRow["Operators"] = operatorRow.Length > 0 ? operatorRow[0]["Operators"] : 0;

                        combinedData.Rows.Add(newRow);
                    }

                    // Add series
                    AddSeries(chart, "Travelers", SeriesChartType.Column, "Month", "Travelers", Color.Red);
                    AddSeries(chart, "Operators", SeriesChartType.Column, "Month", "Operators", Color.Blue);

                    chart.DataSource = combinedData;
                    chart.DataBind();
                }
                else
                {
                    debugLabel.Text += "\nNO DATA FOUND - Check if last_login_date fields have values";
                }
            }
            catch (Exception ex)
            {
                debugLabel.Text = $"ERROR: {ex.Message}";
            }

            chart.Titles.Add("Monthly Active Users (Last 6 Months)");
            chart.ChartAreas[0].AxisX.Title = "Month";
            chart.ChartAreas[0].AxisY.Title = "Active Users";
            chart.Legends.Add(new Legend());
        }

        private void AddPartnershipTab(TabControl tabControl)
        {
            TabPage tab = new TabPage("Partnership Growth");
            tabControl.TabPages.Add(tab);

            Panel panel = new Panel { Dock = DockStyle.Fill };
            tab.Controls.Add(panel);

            Chart chart = new Chart { Dock = DockStyle.Fill, Height = panel.Height - 40 };
            panel.Controls.Add(chart);
            chart.ChartAreas.Add(new ChartArea());

            Button exportBtn = new Button { Text = "Export Data", Dock = DockStyle.Bottom, Height = 40 };
            exportBtn.Click += (sender, e) => ExportData(chart, "Partnership_Growth");
            panel.Controls.Add(exportBtn);

            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        FORMAT(H.registration_date, 'yyyy-MM') AS Month,
                        COUNT(DISTINCT H.hotel_id) AS Hotels,
                        COUNT(DISTINCT O.operator_id) AS Operators
                    FROM (
                        SELECT H.hotel_id, SP.registration_date 
                        FROM Hotel H
                        JOIN ServiceProvider SP ON H.provider_id = SP.provider_id
                    ) AS H
                    FULL OUTER JOIN (
                        SELECT operator_id, registration_date 
                        FROM TourOperator
                    ) AS O ON FORMAT(H.registration_date, 'yyyy-MM') = FORMAT(O.registration_date, 'yyyy-MM')
                    GROUP BY FORMAT(H.registration_date, 'yyyy-MM')
                    ORDER BY Month";

                new SqlDataAdapter(query, conn).Fill(data);
            }

            AddSeries(chart, "Hotels", SeriesChartType.StackedColumn, "Month", "Hotels", Color.Teal);
            AddSeries(chart, "Operators", SeriesChartType.StackedColumn, "Month", "Operators", Color.SteelBlue);

            chart.DataSource = data;
            chart.DataBind();
            chart.Titles.Add("New Partnerships by Month");
            chart.ChartAreas[0].AxisX.Title = "Month";
            chart.ChartAreas[0].AxisY.Title = "New Partners";
            chart.Legends.Add(new Legend());
        }

        private void AddRegionalTab(TabControl tabControl)
        {
            TabPage tab = new TabPage("Regional Expansion");
            tabControl.TabPages.Add(tab);

            Panel panel = new Panel { Dock = DockStyle.Fill };
            tab.Controls.Add(panel);

            Chart chart = new Chart { Dock = DockStyle.Fill, Height = panel.Height - 40 };
            panel.Controls.Add(chart);
            chart.ChartAreas.Add(new ChartArea());

            Button exportBtn = new Button { Text = "Export Data", Dock = DockStyle.Bottom, Height = 40 };
            exportBtn.Click += (sender, e) => ExportData(chart, "Regional_Expansion");
            panel.Controls.Add(exportBtn);

            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        FORMAT(start_date, 'yyyy-MM') AS Month,
                        COUNT(DISTINCT destination) AS NewDestinations
                    FROM Trip
                    GROUP BY FORMAT(start_date, 'yyyy-MM')
                    ORDER BY Month";

                new SqlDataAdapter(query, conn).Fill(data);
            }

            AddSeries(chart, "New Destinations", SeriesChartType.Area, "Month", "NewDestinations", Color.FromArgb(100, Color.Green));

            chart.DataSource = data;
            chart.DataBind();
            chart.Titles.Add("New Destinations by Month");
            chart.ChartAreas[0].AxisX.Title = "Month";
            chart.ChartAreas[0].AxisY.Title = "Destinations Added";
        }

        private void AddSeries(Chart chart, string name, SeriesChartType type, string xField, string yField, Color color)
        {
            Series series = new Series(name)
            {
                ChartType = type,
                XValueMember = xField,
                YValueMembers = yField,
                BorderWidth = 2,
                Color = color
            };
            chart.Series.Add(series);
        }

        private void ExportData(Chart chart, string reportName)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV File|*.csv|PNG Image|*.png|JPEG Image|*.jpg",
                Title = "Export Data",
                FileName = $"{reportName}_{DateTime.Now:yyyyMMdd}"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    switch (saveDialog.FilterIndex)
                    {
                        case 1: // CSV
                            ExportToCsv(chart.DataSource as DataTable, saveDialog.FileName);
                            break;
                        case 2: // PNG
                            chart.SaveImage(saveDialog.FileName, ChartImageFormat.Png);
                            break;
                        case 3: // JPEG
                            chart.SaveImage(saveDialog.FileName, ChartImageFormat.Jpeg);
                            break;
                    }
                    MessageBox.Show("Export successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Export failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToCsv(DataTable data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write headers
                writer.WriteLine(string.Join(",", data.Columns.Cast<DataColumn>().Select(col => col.ColumnName)));

                // Write data
                foreach (DataRow row in data.Rows)
                {
                    writer.WriteLine(string.Join(",", row.ItemArray.Select(field => field.ToString())));
                }
            }
        }
    }
}