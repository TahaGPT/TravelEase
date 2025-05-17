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
    public class PaymentTransactionandFraudReport : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-2I8F60O\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

        public PaymentTransactionandFraudReport()
        {
            // Initialize the form manually since we're not using InitializeComponent()
            this.Text = "Payment Transaction and Fraud Report";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = SystemColors.Window;
            this.Padding = new Padding(10);

            // Create and configure the tab control
            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(10, 10)
            };
            this.Controls.Add(tabControl);

            // Add the tabs
            AddPaymentSuccessTab(tabControl);
            AddChargebackTab(tabControl);
        }

        private void AddPaymentSuccessTab(TabControl tabControl)
        {
            // Create the tab page
            TabPage tab = new TabPage("Payment Success Rate");
            tabControl.TabPages.Add(tab);

            // Create a panel to hold the chart and button
            Panel panel = new Panel { Dock = DockStyle.Fill };
            tab.Controls.Add(panel);

            // Create and configure the chart
            Chart chart = new Chart
            {
                Dock = DockStyle.Fill,
                Height = panel.Height - 45  // Leave space for button
            };
            panel.Controls.Add(chart);

            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            // Add export button
            Button exportBtn = new Button
            {
                Text = "Export Data",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.LightGray
            };
            exportBtn.Click += (sender, e) => ExportData(chart, "Payment_Success_Rate");
            panel.Controls.Add(exportBtn);

            // Get and bind data
            DataTable data = GetPaymentSuccessData();
            BindChartData(chart, data,
                new[] {
                    ("Successful", "SuccessfulPayments", SeriesChartType.StackedColumn, Color.Green),
                    ("Failed", "FailedPayments", SeriesChartType.StackedColumn, Color.Red),
                    ("Success Rate %", "SuccessRate", SeriesChartType.Line, Color.DarkGreen),
                    ("Failure Rate %", "FailureRate", SeriesChartType.Line, Color.DarkRed)
                },
                "Payment Success/Failure Rate",
                "Month",
                "Transaction Count",
                "Percentage Rate");
        }

        private DataTable GetPaymentSuccessData()
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        FORMAT(payment_date, 'yyyy-MM') AS Month,
                        SUM(CASE WHEN payment_status = 'Completed' THEN 1 ELSE 0 END) AS SuccessfulPayments,
                        SUM(CASE WHEN payment_status = 'Failed' THEN 1 ELSE 0 END) AS FailedPayments,
                        COUNT(*) AS TotalPayments,
                        (SUM(CASE WHEN payment_status = 'Completed' THEN 1 ELSE 0 END) * 100.0 / NULLIF(COUNT(*), 0)) AS SuccessRate,
                        (SUM(CASE WHEN payment_status = 'Failed' THEN 1 ELSE 0 END) * 100.0 / NULLIF(COUNT(*), 0)) AS FailureRate
                    FROM Payment
                    GROUP BY FORMAT(payment_date, 'yyyy-MM')
                    ORDER BY Month";

                new SqlDataAdapter(query, conn).Fill(data);
            }
            return data;
        }

        private void AddChargebackTab(TabControl tabControl)
        {
            TabPage tab = new TabPage("Chargeback Rate");
            tabControl.TabPages.Add(tab);

            Panel panel = new Panel { Dock = DockStyle.Fill };
            tab.Controls.Add(panel);

            Chart chart = new Chart
            {
                Dock = DockStyle.Fill,
                Height = panel.Height - 45
            };
            panel.Controls.Add(chart);
            chart.ChartAreas.Add(new ChartArea());

            Button exportBtn = new Button
            {
                Text = "Export Data",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.LightGray
            };
            exportBtn.Click += (sender, e) => ExportData(chart, "Chargeback_Rate");
            panel.Controls.Add(exportBtn);

            DataTable data = GetChargebackData();
            BindChartData(chart, data,
                new[] {
                    ("Disputed Transactions", "DisputedTransactions", SeriesChartType.Column, Color.Orange),
                    ("Dispute Rate %", "DisputeRate", SeriesChartType.Line, Color.Red)
                },
                "Chargeback Rate (Disputed Transactions)",
                "Month",
                "Dispute Count",
                "Dispute Rate %");
        }

        private DataTable GetChargebackData()
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        FORMAT(payment_date, 'yyyy-MM') AS Month,
                        SUM(CASE WHEN is_disputed = 1 THEN 1 ELSE 0 END) AS DisputedTransactions,
                        COUNT(*) AS TotalTransactions,
                        (SUM(CASE WHEN is_disputed = 1 THEN 1 ELSE 0 END) * 100.0 / NULLIF(COUNT(*), 0)) AS DisputeRate
                    FROM Payment
                    GROUP BY FORMAT(payment_date, 'yyyy-MM')
                    ORDER BY Month";

                new SqlDataAdapter(query, conn).Fill(data);
            }
            return data;
        }

        private void BindChartData(Chart chart, DataTable data,
            (string name, string yField, SeriesChartType type, Color color)[] seriesDefinitions,
            string title, string xAxisTitle, string yAxisTitle, string y2AxisTitle)
        {
            // Clear any existing series
            chart.Series.Clear();

            // Add each series
            foreach (var seriesDef in seriesDefinitions)
            {
                Series series = new Series(seriesDef.name)
                {
                    ChartType = seriesDef.type,
                    XValueMember = "Month",
                    YValueMembers = seriesDef.yField,
                    Color = seriesDef.color,
                    BorderWidth = 2,
                    IsValueShownAsLabel = seriesDef.type == SeriesChartType.Line,
                    LabelFormat = seriesDef.type == SeriesChartType.Line ? "0.##'%'" : ""
                };

                if (seriesDef.type == SeriesChartType.Line)
                {
                    series.YAxisType = AxisType.Secondary;
                }

                chart.Series.Add(series);
            }

            // Set data source and bind
            chart.DataSource = data;
            chart.DataBind();

            // Configure chart appearance
            chart.Titles.Add(title);
            ChartArea area = chart.ChartAreas[0];
            area.AxisX.Title = xAxisTitle;
            area.AxisY.Title = yAxisTitle;
            area.AxisY2.Title = y2AxisTitle;
            area.AxisY2.Enabled = AxisEnabled.True;

            if (!chart.Legends.Any())
            {
                chart.Legends.Add(new Legend { Docking = Docking.Bottom });
            }
        }

        private void ExportData(Chart chart, string reportName)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|CSV Data|*.csv";
                saveDialog.Title = "Export Report";
                saveDialog.FileName = $"{reportName}_{DateTime.Now:yyyyMMdd}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        switch (saveDialog.FilterIndex)
                        {
                            case 1: // PNG
                                chart.SaveImage(saveDialog.FileName, ChartImageFormat.Png);
                                break;
                            case 2: // JPEG
                                chart.SaveImage(saveDialog.FileName, ChartImageFormat.Jpeg);
                                break;
                            case 3: // CSV
                                ExportToCsv(chart.DataSource as DataTable, saveDialog.FileName);
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
                    writer.WriteLine(string.Join(",", row.ItemArray.Select(field =>
                        field is DBNull ? "" : field.ToString())));
                }
            }
        }
    }
}