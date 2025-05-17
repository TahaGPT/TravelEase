using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelEase1;

namespace TravelEase
{
    public partial class TravelerDashboard : Form
    {
        string username;
        string password;
        int tID;
        SqlConnection conn;
        SqlCommand cm;
        public TravelerDashboard(string u = "", string p = "", int id = 1)
        {
            
            InitializeComponent();
            // Load user data
            LoadUserData();
            // Load trips data
            username = u;
            password = p;
            LoadTripsData();


        }

        private void LoadUserData()
        {
            // This would be replaced with actual database calls

        }

        /*private void LoadTripsData()
        {
            // This would be replaced with actual database calls
            // Create sample data for the trips grid
            DataTable dt = new DataTable();
            dt.Columns.Add("Trip ID", typeof(string));
            dt.Columns.Add("Destination", typeof(string));
            dt.Columns.Add("Dates", typeof(string));
            dt.Columns.Add("Status", typeof(string));

            dt.Rows.Add("TR-1001", "Paris, France", "May 15 - May 22", "Confirmed");
            dt.Rows.Add("TR-1002", "Tokyo, Japan", "June 10 - June 17", "Pending");
            dt.Rows.Add("TR-1003", "Bali, Indonesia", "July 5 - July 12", "Confirmed");

            dgvRecentTrips.DataSource = dt;
            SetupActionColumn();
        }

        private void SetupActionColumn()
        {
            // Add Actions column with buttons
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Actions";
            btnColumn.Text = "View Details";
            btnColumn.UseColumnTextForButtonValue = true;
            btnColumn.FlatStyle = FlatStyle.Flat;
            dgvRecentTrips.Columns.Add(btnColumn);

            // Adjust column widths for better visibility
            dgvRecentTrips.Columns["Trip ID"].Width = 80;
            dgvRecentTrips.Columns["Destination"].Width = 150;
            dgvRecentTrips.Columns["Dates"].Width = 150;
            dgvRecentTrips.Columns["Status"].Width = 100;
            if (dgvRecentTrips.Columns["Actions"] != null)
            {
                dgvRecentTrips.Columns["Actions"].Width = 100;
            }
        }*/


        private void SetupActionColumn()
        {
            // Add Actions column with buttons
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Actions";
            btnColumn.Text = "View Details";
            btnColumn.UseColumnTextForButtonValue = true;
            btnColumn.FlatStyle = FlatStyle.Flat;
            dgvRecentTrips.Columns.Add(btnColumn);

            // Adjust column widths for better visibility
            dgvRecentTrips.Columns["Trip ID"].Width = 80;
            dgvRecentTrips.Columns["Destination"].Width = 150;
            dgvRecentTrips.Columns["Dates"].Width = 150;
            dgvRecentTrips.Columns["Status"].Width = 100;
            if (dgvRecentTrips.Columns["Actions"] != null)
            {
                dgvRecentTrips.Columns["Actions"].Width = 100;
            }
        }


        private void LoadTripsData()
        {

            string connectionString = "Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // extract travler id
                string travelerIdQuery = "SELECT traveler_id FROM Traveler WHERE email = @TravelerId";
                using (SqlCommand cm = new SqlCommand(travelerIdQuery, conn))
                {
                    cm.Parameters.AddWithValue("@TravelerId", username);
                    SqlDataReader reader = cm.ExecuteReader();

                    if (reader.Read())
                    {
                        tID = (int)reader["traveler_id"];
                    }
                    reader.Close();
                }
                string query = @"
            SELECT 
                'TR-' + CAST(t.trip_id AS VARCHAR) AS [Trip ID],
                t.destination AS [Destination],
                FORMAT(t.start_date, 'MMM dd') + ' - ' + FORMAT(t.end_date, 'MMM dd') AS [Dates],
                b.status AS [Status]
            FROM Booking b
            INNER JOIN Trip t ON b.trip_id = t.trip_id
            WHERE b.traveler_id = @TravelerId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TravelerId", tID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            dgvRecentTrips.DataSource = dt;

            // Ensure "Actions" column is not duplicated
            if (!dgvRecentTrips.Columns.Contains("Actions"))
                SetupActionColumn();

            // Resize columns for neat layout
            dgvRecentTrips.Columns["Trip ID"].Width = 80;
            dgvRecentTrips.Columns["Destination"].Width = 150;
            dgvRecentTrips.Columns["Dates"].Width = 150;
            dgvRecentTrips.Columns["Status"].Width = 100;
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

        private void btnBookNewTrip_Click(object sender, EventArgs e)
        {
            // Logic to open trip booking form
            MessageBox.Show("Opening the trip booking interface", "Book New Trip");
            // TripBookingForm bookingForm = new TripBookingForm();
            // bookingForm.ShowDialog();
        }

        private void dgvRecentTrips_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the button column and not the header row
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvRecentTrips.Columns.Count - 1)
            {
                string tripId = dgvRecentTrips.Rows[e.RowIndex].Cells["Trip ID"].Value.ToString();
                MessageBox.Show($"Opening details for Trip {tripId}", "Trip Details");
                // TripDetailsForm detailsForm = new TripDetailsForm(tripId);
                // detailsForm.ShowDialog();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Already on dashboard, no action needed
        }



        private void btnSearchTrips_Click(object sender, EventArgs e)
        {
            // Logic to navigate to Search Trips page
            MessageBox.Show("Navigating to Search Trips", "Navigation");
            search searchForm = new search(username);
            searchForm.Show();
        }

        private void btnTravelPass_Click(object sender, EventArgs e)
        {
            DigitalTravelPassForm passForm = new DigitalTravelPassForm();
            passForm.Show();
            //this.Hide();
        }

        private void btnReviews_Click(object sender, EventArgs e)
        {
            // Logic to navigate to Reviews page
            MessageBox.Show("Navigating to Reviews", "Navigation");
            Feedback reviewsForm = new Feedback();
            reviewsForm.Show();
        }

        private void btnProfileSettings_Click(object sender, EventArgs e)
        {

            ProfileSetting settingsForm = new ProfileSetting(username, password);
            settingsForm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblUpcomingTrips_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {

        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {

        }

        private void panelNavigation_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotalSpentTitle_Click(object sender, EventArgs e)
        {

        }

        private void dgvRecentTrips_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblRecentBookings_Click(object sender, EventArgs e)
        {

        }

        private void panelTotalSpent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotalSpent_Click(object sender, EventArgs e)
        {

        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelUpcomingTrips_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblUpcomingTripsTitle_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click_1(object sender, EventArgs e)
        {
                
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string query = @"
                SELECT 
                    COUNT(b.booking_id) AS TripCount
                FROM 
                    Traveler t
                    INNER JOIN Booking b ON t.traveler_id = b.traveler_id
                WHERE 
                    t.email LIKE @Email";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@Email",username);
                object result = command.ExecuteScalar();
                int tripCount = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                // Update a label with the trip count
                textBox1.Text = $"Total Trips: {tripCount}";
                textBox1.Visible = true;
            }

        }
    }
}