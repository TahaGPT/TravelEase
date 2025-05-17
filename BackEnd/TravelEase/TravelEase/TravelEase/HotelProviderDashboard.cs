using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelEase
{
        public partial class HotelProviderDashboard : Form
    {
        private string connstr = @"Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

        private int providerId = 13;
        public HotelProviderDashboard(int u)
        {
            providerId = u;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnbookReq_Click(object sender, EventArgs e)
        {
            // Get the current service provider ID
            int providerID = providerId;

            // Create the manager
            BookingRequestsForm manager = new BookingRequestsForm(providerID, connstr);

            // Show the booking requests form
            manager.Show();
        }

        private void btnAddNewService_Click(object sender, EventArgs e)
        {

            //i want to open new form i just created ServiceListing.cs

            ServiceListingForm serviceListingForm = new ServiceListingForm(0);

            serviceListingForm.ShowDialog();






        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        private void my_Service_Button_Click(object sender, EventArgs e)
        {
            MyServicesForm service = new MyServicesForm(providerId, connstr);
            service.ShowDialog();
        }

        private void lblMoneyCount_Click(object sender, EventArgs e)
        {
            // Show "refreshing" indicator
            lblMoneyCount.Text = "...";

            // Update the revenue
            UpdateMonthlyRevenue();
        }

        private void UpdateMonthlyRevenue()
        {
            try
            {
                // Get the first and last day of the current month
                DateTime today = DateTime.Today;
                DateTime firstDayOfYear = new DateTime(today.Year, 1, 1);
                DateTime lastDayOfYear = new DateTime(today.Year, 12, 31);

                string query = @"
                    SELECT COALESCE(SUM(b.total_amount), 0)
                    FROM Booking b
                    JOIN BookingServices_ bs ON b.booking_id = bs.booking_id
                    WHERE bs.provider_id = @providerId
                    AND b.booking_date BETWEEN @startDate AND @endDate
                    AND b.status = 'Confirmed'";

                using (SqlConnection con = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Get the actual provider ID (assuming you have it stored somewhere)
                    //int providerId = 1; // Replace with your actual provider ID retrieval
                    cmd.Parameters.AddWithValue("@providerId", providerId);
                    cmd.Parameters.AddWithValue("@startDate", firstDayOfYear);
                    cmd.Parameters.AddWithValue("@endDate", lastDayOfYear);

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    // Update the label on the UI thread
                    this.Invoke((MethodInvoker)delegate {
                        if (result != DBNull.Value)
                        {
                            decimal revenue = Convert.ToDecimal(result);
                            lblMoneyCount.Text = "$" + revenue.ToString("N0"); // Format as currency without cents
                        }
                        else
                        {
                            lblMoneyCount.Text = "$0";
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in UpdateMonthlyRevenue: {ex.Message}");

                // Update UI with error
                this.Invoke((MethodInvoker)delegate {
                    lblMoneyCount.Text = "Error";
                });

                MessageBox.Show($"Error calculating monthly revenue: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblCounting_Click(object sender, EventArgs e)
        {
            lblCounting.Text = "...";

            // Update the count
            UpdatePendingBookingsCount();
        }


        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelNavigation_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblHotelName_Click(object sender, EventArgs e)
        {

        }

        private void labelPendingTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Logic to handle logout
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                this.Close();
                /*                Login log = new Login();
                                log.Show();*/

                // Here you would typically show the login form
                // LoginForm loginForm = new LoginForm();
                // loginForm.Show();
            }
        }

        private void panelMainContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnProfileSet_Click(object sender, EventArgs e)
        {
            //ProfileSetting prof = new ProfileSetting();
            //prof.Show();
        }

        private void HotelProviderDashboard_Load(object sender, EventArgs e)
        {
            lblCounting.Text = "...";

            // Update the count right away
            UpdatePendingBookingsCount();
            UpdateMonthlyRevenue();
            InitializeDataGridView();
            LoadRecentBookings(providerId);
        }

        private void UpdatePendingBookingsCount()
        {
            try
            {
                string query = @"SELECT COUNT(*) 
                        FROM Booking b
                        JOIN BookingServices_ bs ON b.booking_id = bs.booking_id
                        WHERE b.status = 'Confirmed' 
                        AND bs.provider_id = @providerId";

                using (SqlConnection con = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Get the actual provider ID (assuming you have it stored somewhere)
                     // Replace with your actual provider ID retrieval
                    cmd.Parameters.AddWithValue("@providerId", providerId);

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    // Update the label on the UI thread
                    this.Invoke((MethodInvoker)delegate {
                        if (result != null)
                        {
                            int count = Convert.ToInt32(result);
                            lblCounting.Text = count.ToString();
                        }
                        else
                        {
                            lblCounting.Text = "0";
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in UpdatePendingBookingsCount: {ex.Message}");

                // Update UI with error
                this.Invoke((MethodInvoker)delegate {
                    lblCounting.Text = "Error";
                });

                MessageBox.Show($"Error counting pending bookings: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void panelPendingBookings_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvRecentBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the Actions column and is not a header cell
            if (e.ColumnIndex == dgvRecentBookings.Columns["Actions"].Index && e.RowIndex >= 0)
            {
                try
                {
                    // Get the booking ID from the clicked row
                    int bookingId = Convert.ToInt32(dgvRecentBookings.Rows[e.RowIndex].Cells["BookingID"].Value);

                    // Show booking details form or dialog
                    ShowBookingDetails(bookingId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error viewing booking details: " + ex.Message, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InitializeDataGridView()
        {
            // Configure the Actions column as a button column
            DataGridViewButtonColumn actionColumn = dgvRecentBookings.Columns["Actions"] as DataGridViewButtonColumn;
            if (actionColumn == null)
            {
                actionColumn = new DataGridViewButtonColumn();
                actionColumn.Name = "Actions";
                actionColumn.HeaderText = "Actions";
                actionColumn.Text = "View Details";
                actionColumn.UseColumnTextForButtonValue = true;
                dgvRecentBookings.Columns.Add(actionColumn);
            }

            // Set other column properties for better appearance
            dgvRecentBookings.Columns["BookingID"].Width = 80;
            dgvRecentBookings.Columns["TourOperator"].Width = 200;
            dgvRecentBookings.Columns["CheckInDate"].Width = 120;
            dgvRecentBookings.Columns["Status"].Width = 100;
            dgvRecentBookings.Columns["Actions"].Width = 120;

            // Apply some styling
            dgvRecentBookings.DefaultCellStyle.ForeColor = Color.White;
            dgvRecentBookings.DefaultCellStyle.BackColor = Color.FromArgb(0, 30, 60);
            dgvRecentBookings.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 60, 120);
            dgvRecentBookings.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 20, 40);
            dgvRecentBookings.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRecentBookings.EnableHeadersVisualStyles = false;
        }

        private void ShowBookingDetails(int bookingId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connstr))
                {
                    connection.Open();

                    // Updated query with a different alias for TourOperator
                    string query = @"
                SELECT b.booking_id, b.booking_date, b.status, b.total_amount, 
                       b.cancellation_policy, b.Group_Size, b.booking_timestamp,
                       t.title AS TripName, t.description, t.destination, t.start_date, t.end_date,
                       tr.name AS TravelerName, tr.email, tr.nationality,
                       op.company_name AS TourOperator, op.phone AS OperatorPhone
                FROM Booking b
                INNER JOIN Trip t ON b.trip_id = t.trip_id
                INNER JOIN Traveler tr ON b.traveler_id = tr.traveler_id
                INNER JOIN TourOperator op ON t.operator_id = op.operator_id
                WHERE b.booking_id = @BookingId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookingId", bookingId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Create and show booking details form
                                string details = $"Booking #{reader["booking_id"]}\n\n" +
                                                $"Trip: {reader["TripName"]}\n" +
                                                $"Dates: {((DateTime)reader["start_date"]).ToString("yyyy-MM-dd")} to {((DateTime)reader["end_date"]).ToString("yyyy-MM-dd")}\n" +
                                                $"Status: {reader["status"]}\n" +
                                                $"Group Size: {reader["Group_Size"]}\n\n" +
                                                $"Traveler: {reader["TravelerName"]}\n" +
                                                $"Email: {reader["email"]}\n" +
                                                $"Nationality: {reader["nationality"]}\n\n" +
                                                $"Tour Operator: {reader["TourOperator"]}\n" +
                                                $"Operator Phone: {reader["OperatorPhone"]}\n\n" +
                                                $"Total Amount: ${reader["total_amount"]}\n" +
                                                $"Booking Date: {((DateTime)reader["booking_date"]).ToString("yyyy-MM-dd")}";

                                MessageBox.Show(details, "Booking Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Booking details not found.", "Not Found",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving booking details: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecentBookings(int providerId)
        {
            try
            {
                dgvRecentBookings.Rows.Clear();

                using (SqlConnection connection = new SqlConnection(connstr))
                {
                    connection.Open();

                    // Simplified query that gets bookings directly related to service providers
                    string query = @"
                SELECT b.booking_id, t.title AS TripName, op.company_name AS TourOperator, 
                       t.start_date AS CheckInDate, b.status, b.booking_date
                FROM Booking b
                INNER JOIN Trip t ON b.trip_id = t.trip_id
                INNER JOIN TourOperator op ON t.operator_id = op.operator_id
                INNER JOIN BookingServices_ bs ON b.booking_id = bs.booking_id
                WHERE bs.provider_id = @ProviderId
                ORDER BY b.booking_date DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProviderId", providerId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int rowIndex = dgvRecentBookings.Rows.Add();
                                DataGridViewRow row = dgvRecentBookings.Rows[rowIndex];

                                row.Cells["BookingID"].Value = reader["booking_id"].ToString();
                                row.Cells["TourOperator"].Value = reader["TourOperator"].ToString();

                                // Format the check-in date
                                if (reader["CheckInDate"] != DBNull.Value)
                                {
                                    DateTime checkInDate = (DateTime)reader["CheckInDate"];
                                    row.Cells["CheckInDate"].Value = checkInDate.ToString("yyyy-MM-dd");
                                }

                                row.Cells["Status"].Value = reader["status"].ToString();

                                // Add "View" button to Actions column
                                DataGridViewButtonCell buttonCell = row.Cells["Actions"] as DataGridViewButtonCell;
                                if (buttonCell != null)
                                {
                                    buttonCell.Value = "View Details";
                                }

                                // Color-code the status cell based on booking status
                                string status = reader["status"].ToString().ToLower();
                                if (status == "confirmed")
                                {
                                    row.Cells["Status"].Style.ForeColor = Color.Green;
                                }
                                else if (status == "pending")
                                {
                                    row.Cells["Status"].Style.ForeColor = Color.Orange;
                                }
                                else if (status == "cancelled")
                                {
                                    row.Cells["Status"].Style.ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading recent bookings: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPerformRep_Click(object sender, EventArgs e)
        {
            PerformanceReportForm form = new PerformanceReportForm(providerId, connstr);

            form.ShowDialog();
        }
    }
}
