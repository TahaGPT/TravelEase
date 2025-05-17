using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

namespace TravelEaseReporting
{
    public partial class TripBookingRevenueReportPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReportData();
            }
        }

        private void LoadReportData()
        {
            // Configure the ReportViewer control
            TripBookingRevenueReport.ProcessingMode = ProcessingMode.Local;
            TripBookingRevenueReport.LocalReport.ReportPath = Server.MapPath("~/TripBookingRevenueReport.rdlc");

            // Data source configurations
            string connectionString = ConfigurationManager.ConnectionStrings["TravelEaseConnection"].ConnectionString;

            // Prepare data sources
            ReportDataSource totalBookingsDataSource = PrepareDataSource(
                connectionString,
                @"SELECT
                    COUNT(booking_id) AS TotalBookings,
                    SUM(total_amount) AS TotalRevenue,
                    AVG(total_amount) AS AverageBookingValue,
                    (COUNT(CASE WHEN status = 'Canceled' THEN 1 END) * 100.0 / COUNT(booking_id)) AS CancellationRate,
                    DATEPART(month, booking_date) AS BookingMonth
                FROM Booking
                GROUP BY DATEPART(month, booking_date)",
                "TotalBookingsDataset");

            ReportDataSource revenueByCategoryDataSource = PrepareDataSource(
                connectionString,
                @"SELECT
                    tc.category_name AS TripCategory,
                    COUNT(b.booking_id) AS CategoryBookings,
                    SUM(b.total_amount) AS CategoryRevenue,
                    AVG(b.total_amount) AS CategoryAverageBooking
                FROM Trip t
                JOIN TripCategory tc ON t.category_id = tc.category_id
                JOIN Booking b ON t.trip_id = b.trip_id
                GROUP BY tc.category_name",
                "RevenueByTripCategoryDataset");

            ReportDataSource peakBookingPeriodsDataSource = PrepareDataSource(
                connectionString,
                @"SELECT
                    DATEPART(month, booking_date) AS BookingMonth,
                    COUNT(booking_id) AS MonthlyBookings,
                    SUM(total_amount) AS MonthlyRevenue
                FROM Booking
                GROUP BY DATEPART(month, booking_date)
                ORDER BY MonthlyBookings DESC",
                "PeakBookingPeriodsDataset");

            // Clear existing data sources and add new ones
            TripBookingRevenueReport.LocalReport.DataSources.Clear();
            TripBookingRevenueReport.LocalReport.DataSources.Add(totalBookingsDataSource);
            TripBookingRevenueReport.LocalReport.DataSources.Add(revenueByCategoryDataSource);
            TripBookingRevenueReport.LocalReport.DataSources.Add(peakBookingPeriodsDataSource);

            // Set report parameters
            ReportParameter[] reportParameters = new ReportParameter[]
            {
                new ReportParameter("StartDate", "2024-01-01"),
                new ReportParameter("EndDate", "2024-12-31")
            };
            TripBookingRevenueReport.LocalReport.SetParameters(reportParameters);

            // Refresh the report
            TripBookingRevenueReport.LocalReport.Refresh();
        }

        private ReportDataSource PrepareDataSource(string connectionString, string sqlQuery, string datasetName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        return new ReportDataSource(datasetName, dataTable);
                    }
                }
            }
        }
    }
}