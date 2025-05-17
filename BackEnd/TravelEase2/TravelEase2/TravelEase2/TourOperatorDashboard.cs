using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TravelEase
{
    public partial class TourOperatorDashboard : Form
    {
        private int operatorId;
        private string connectionString = "Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";
        private int OperatorId = 1;

        public TourOperatorDashboard(int u = 1, string p = "")
        {
            OperatorId = u;
            InitializeComponent();

            LoadDashboardData();

        }

        private void LoadDashboardData()
        {
            LoadOperatorName();

            // Load active trips count
            LoadActiveTripsCount();

            // Load total revenue
            LoadTotalRevenue();
            LoadTripsData();
            LoadPendingBookingsData();
        }

        #region Trips Grid Methods

        /// <summary>
        /// Loads trip data for the current tour operator
        /// </summary>
        private void LoadTripsData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT t.trip_id, t.title, t.destination, t.price_per_person, 
                               t.capacity, t.start_date, t.end_date, t.duration,
                               t.min_group_size, t.max_group_size, t.views_count,
                               tc.category_name, COUNT(b.booking_id) AS booking_count
                        FROM Trip t 
                        LEFT JOIN TripCategory tc ON t.category_id = tc.category_id
                        LEFT JOIN Booking b ON t.trip_id = b.trip_id
                        WHERE t.operator_id = @OperatorId
                        GROUP BY t.trip_id, t.title, t.destination, t.price_per_person, 
                                 t.capacity, t.start_date, t.end_date, t.duration,
                                 t.min_group_size, t.max_group_size, t.views_count,
                                 tc.category_name
                        ORDER BY t.start_date";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OperatorId", OperatorId);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dgvTrips.DataSource = dataTable;

                        // Format the DataGridView
                        FormatTripsGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading trips data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Format the trips grid with appropriate column headers and styling
        /// </summary>
        private void FormatTripsGrid()
        {
            // Configure column headers and display format
            if (dgvTrips.Columns.Count > 0)
            {
                // Set column headers
                dgvTrips.Columns["trip_id"].HeaderText = "ID";
                dgvTrips.Columns["title"].HeaderText = "Trip Title";
                dgvTrips.Columns["destination"].HeaderText = "Destination";
                dgvTrips.Columns["price_per_person"].HeaderText = "Price per Person";
                dgvTrips.Columns["capacity"].HeaderText = "Capacity";
                dgvTrips.Columns["start_date"].HeaderText = "Start Date";
                dgvTrips.Columns["end_date"].HeaderText = "End Date";
                dgvTrips.Columns["duration"].HeaderText = "Days";
                dgvTrips.Columns["min_group_size"].HeaderText = "Min Group";
                dgvTrips.Columns["max_group_size"].HeaderText = "Max Group";
                dgvTrips.Columns["views_count"].HeaderText = "Views";
                dgvTrips.Columns["category_name"].HeaderText = "Category";
                dgvTrips.Columns["booking_count"].HeaderText = "Bookings";

                // Optional: Format date columns
                dgvTrips.Columns["start_date"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dgvTrips.Columns["end_date"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                // Optional: Format currency column
                dgvTrips.Columns["price_per_person"].DefaultCellStyle.Format = "C0";

                // Hide ID column if you prefer
                // dgvTrips.Columns["trip_id"].Visible = false;
            }
        }

        /// <summary>
        /// Trip grid cell click event handler
        /// </summary>
        private void dgvTrips_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Don't process clicks on header row or if no row is selected
            if (e.RowIndex < 0) return;

            // Get the selected trip ID
            if (dgvTrips.CurrentRow != null)
            {
                int tripId = Convert.ToInt32(dgvTrips.CurrentRow.Cells["trip_id"].Value);
                // You could show trip details in a separate panel or form
                LoadTripDetails(tripId);
            }
        }

        /// <summary>
        /// Trip grid cell content click event handler (could be used for buttons within cells)
        /// </summary>
        private void dgvTrips_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // This method is triggered when a cell's content is clicked
            // You can use this for handling button clicks if you add button columns
            if (e.RowIndex < 0) return;

            // Example: If you have a button column at index 5 to edit a trip
            // if (e.ColumnIndex == 5)
            // {
            //     int tripId = Convert.ToInt32(dgvTrips.Rows[e.RowIndex].Cells["trip_id"].Value);
            //     OpenTripEditForm(tripId);
            // }
        }

        /// <summary>
        /// Load details for a specific trip (example implementation)
        /// </summary>
        private void LoadTripDetails(int tripId)
        {
            // Implement to show additional details about the selected trip
            // This could update labels or controls on your form with trip details
            MessageBox.Show($"Load details for trip ID: {tripId}", "Trip Selected");

            // You might want to load the bookings for this specific trip
            LoadBookingsForTrip(tripId);
        }

        #endregion

        #region Bookings Grid Methods

        /// <summary>
        /// Loads pending bookings data for the current tour operator
        /// </summary>
        private void LoadPendingBookingsData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT b.booking_id, b.booking_date, b.status, b.total_amount,
                               b.Group_Size, t.title AS trip_name, t.destination,
                               tr.name AS traveler_name, p.payment_status
                        FROM Booking b
                        JOIN Trip t ON b.trip_id = t.trip_id
                        JOIN Traveler tr ON b.traveler_id = tr.traveler_id
                        JOIN Payment p ON b.payment_id = p.payment_id
                        WHERE t.operator_id = @OperatorId
                        AND b.status = 'Pending'
                        ORDER BY b.booking_date DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OperatorId", OperatorId);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dgvBookings.DataSource = dataTable;

                        // Format the DataGridView
                        FormatBookingsGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load bookings for a specific trip
        /// </summary>
        private void LoadBookingsForTrip(int tripId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT b.booking_id, b.booking_date, b.status, b.total_amount,
                               b.Group_Size, tr.name AS traveler_name, 
                               tr.email AS traveler_email, p.payment_status
                        FROM Booking b
                        JOIN Traveler tr ON b.traveler_id = tr.traveler_id
                        JOIN Payment p ON b.payment_id = p.payment_id
                        WHERE b.trip_id = @TripId
                        ORDER BY b.booking_date DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TripId", tripId);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dgvBookings.DataSource = dataTable;

                        // Format the DataGridView
                        FormatBookingsGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings for trip: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Format the bookings grid with appropriate column headers and styling
        /// </summary>
        private void FormatBookingsGrid()
        {
            // Configure column headers and display format
            if (dgvBookings.Columns.Count > 0)
            {
                // Set column headers
                dgvBookings.Columns["booking_id"].HeaderText = "ID";
                dgvBookings.Columns["booking_date"].HeaderText = "Booking Date";
                dgvBookings.Columns["status"].HeaderText = "Status";
                dgvBookings.Columns["total_amount"].HeaderText = "Total Amount";
                dgvBookings.Columns["Group_Size"].HeaderText = "Group Size";

                // These columns may or may not exist depending on which query was used
                if (dgvBookings.Columns.Contains("trip_name"))
                    dgvBookings.Columns["trip_name"].HeaderText = "Trip";

                if (dgvBookings.Columns.Contains("destination"))
                    dgvBookings.Columns["destination"].HeaderText = "Destination";

                dgvBookings.Columns["traveler_name"].HeaderText = "Traveler";

                if (dgvBookings.Columns.Contains("traveler_email"))
                    dgvBookings.Columns["traveler_email"].HeaderText = "Email";

                dgvBookings.Columns["payment_status"].HeaderText = "Payment Status";

                // Format date columns
                dgvBookings.Columns["booking_date"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                // Format currency column
                dgvBookings.Columns["total_amount"].DefaultCellStyle.Format = "C0";

                // Add color coding for payment status
                dgvBookings.CellFormatting += DgvBookings_CellFormatting;
            }
        }

        /// <summary>
        /// Format cells in the bookings grid based on their values
        /// </summary>
        private void DgvBookings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Color code payment status column
            if (dgvBookings.Columns[e.ColumnIndex].Name == "payment_status" && e.Value != null)
            {
                string status = e.Value.ToString();

                switch (status.ToLower())
                {
                    case "completed":
                        e.CellStyle.ForeColor = System.Drawing.Color.Green;
                        break;
                    case "pending":
                        e.CellStyle.ForeColor = System.Drawing.Color.Orange;
                        break;
                    case "failed":
                        e.CellStyle.ForeColor = System.Drawing.Color.Red;
                        break;
                }
            }

            // Color code booking status column
            if (dgvBookings.Columns[e.ColumnIndex].Name == "status" && e.Value != null)
            {
                string status = e.Value.ToString();

                switch (status.ToLower())
                {
                    case "confirmed":
                        e.CellStyle.ForeColor = System.Drawing.Color.Green;
                        break;
                    case "pending":
                        e.CellStyle.ForeColor = System.Drawing.Color.Blue;
                        break;
                    case "cancelled":
                        e.CellStyle.ForeColor = System.Drawing.Color.Red;
                        break;
                }
            }
        }

        /// <summary>
        /// Booking grid cell click event handler
        /// </summary>
        private void dgvBookings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Don't process clicks on header row
            if (e.RowIndex < 0) return;

            // Get the selected booking ID
            if (dgvBookings.CurrentRow != null)
            {
                int bookingId = Convert.ToInt32(dgvBookings.CurrentRow.Cells["booking_id"].Value);
                // Show booking details or open a form to manage the booking
                ShowBookingDetails(bookingId);
            }
        }

        /// <summary>
        /// Booking grid cell content click event handler
        /// </summary>
        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // This would be used for handling button clicks within cells
            if (e.RowIndex < 0) return;

            // Example: If you have added action buttons to the grid
            // if (e.ColumnIndex == dgvBookings.Columns["approveButton"].Index)
            // {
            //     int bookingId = Convert.ToInt32(dgvBookings.Rows[e.RowIndex].Cells["booking_id"].Value);
            //     ApproveBooking(bookingId);
            // }
        }

        /// <summary>
        /// Show details for a specific booking (example implementation)
        /// </summary>
        private void ShowBookingDetails(int bookingId)
        {
            // You would implement this to show details of the selected booking
            MessageBox.Show($"Show details for booking ID: {bookingId}", "Booking Selected");

            // You might open a new form or populate details in a panel
            // OpenBookingDetailsForm(bookingId);
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Approve a pending booking
        /// </summary>
        private void ApproveBooking(int bookingId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Booking SET status = 'Confirmed' WHERE booking_id = @BookingId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookingId", bookingId);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Booking has been confirmed!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the bookings grid
                            LoadPendingBookingsData();
                        }
                        else
                        {
                            MessageBox.Show("Failed to confirm booking.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving booking: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reject a pending booking
        /// </summary>
        private void RejectBooking(int bookingId, string reason)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Start a transaction since we need to update multiple tables
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // 1. Update the booking status
                        string updateBookingQuery = @"
                            UPDATE Booking 
                            SET status = 'Cancelled', 
                                cancellation_date = GETDATE(),
                                cancellation_reason = @Reason
                            WHERE booking_id = @BookingId";

                        using (SqlCommand command = new SqlCommand(updateBookingQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@BookingId", bookingId);
                            command.Parameters.AddWithValue("@Reason", reason);
                            command.ExecuteNonQuery();
                        }

                        // 2. Get the payment ID for this booking
                        int paymentId = 0;
                        string getPaymentQuery = "SELECT payment_id FROM Booking WHERE booking_id = @BookingId";

                        using (SqlCommand command = new SqlCommand(getPaymentQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@BookingId", bookingId);
                            object result = command.ExecuteScalar();

                            if (result != null && result != DBNull.Value)
                            {
                                paymentId = Convert.ToInt32(result);

                                // 3. Update the payment status to initiate refund
                                string updatePaymentQuery = @"
                                    UPDATE Payment 
                                    SET payment_status = 'Refund Initiated',
                                        is_refunded = 1
                                    WHERE payment_id = @PaymentId";

                                using (SqlCommand paymentCommand = new SqlCommand(updatePaymentQuery, connection, transaction))
                                {
                                    paymentCommand.Parameters.AddWithValue("@PaymentId", paymentId);
                                    paymentCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        // Commit the transaction
                        transaction.Commit();

                        MessageBox.Show("Booking has been rejected and refund initiated.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh the bookings grid
                        LoadPendingBookingsData();
                    }
                    catch (Exception ex)
                    {
                        // Roll back the transaction on error
                        transaction.Rollback();
                        throw new Exception($"Transaction failed: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rejecting booking: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Search and Filter Methods

        /// <summary>
        /// Search trips by title, destination, or category
        /// </summary>
        public void SearchTrips(string searchTerm)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT t.trip_id, t.title, t.destination, t.price_per_person, 
                               t.capacity, t.start_date, t.end_date, t.duration,
                               t.min_group_size, t.max_group_size, t.views_count,
                               tc.category_name, COUNT(b.booking_id) AS booking_count
                        FROM Trip t
                        LEFT JOIN TripCategory tc ON t.category_id = tc.category_id
                        LEFT JOIN Booking b ON t.trip_id = b.trip_id
                        WHERE t.operator_id = @OperatorId
                        AND (
                            t.title LIKE @SearchTerm
                            OR t.destination LIKE @SearchTerm
                            OR tc.category_name LIKE @SearchTerm
                        )
                        GROUP BY t.trip_id, t.title, t.destination, t.price_per_person, 
                                 t.capacity, t.start_date, t.end_date, t.duration,
                                 t.min_group_size, t.max_group_size, t.views_count,
                                 tc.category_name
                        ORDER BY t.start_date";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OperatorId", OperatorId);
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dgvTrips.DataSource = dataTable;
                        FormatTripsGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching trips: {ex.Message}", "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Filter trips by date range
        /// </summary>
        public void FilterTripsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT t.trip_id, t.title, t.destination, t.price_per_person, 
                               t.capacity, t.start_date, t.end_date, t.duration,
                               t.min_group_size, t.max_group_size, t.views_count,
                               tc.category_name, COUNT(b.booking_id) AS booking_count
                        FROM Trip t
                        LEFT JOIN TripCategory tc ON t.category_id = tc.category_id
                        LEFT JOIN Booking b ON t.trip_id = b.trip_id
                        WHERE t.operator_id = @OperatorId
                        AND t.start_date >= @StartDate
                        AND t.end_date <= @EndDate
                        GROUP BY t.trip_id, t.title, t.destination, t.price_per_person, 
                                 t.capacity, t.start_date, t.end_date, t.duration,
                                 t.min_group_size, t.max_group_size, t.views_count,
                                 tc.category_name
                        ORDER BY t.start_date";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OperatorId", OperatorId);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dgvTrips.DataSource = dataTable;
                        FormatTripsGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering trips: {ex.Message}", "Filter Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        private void LoadOperatorName()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT company_name FROM TourOperator WHERE operator_id = @OperatorId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OperatorId", OperatorId);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            lblOperatorName.Text = result.ToString();
                        }
                        else
                        {
                            lblOperatorName.Text = "Unknown Operator";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading operator name: " + ex.Message, "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblOperatorName.Text = "Error Loading";
            }
        }

        private void LoadActiveTripsCount()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to count active trips (trips where end_date is in the future)
                    string query = @"SELECT COUNT(*) 
                                    FROM Trip 
                                    WHERE operator_id = @OperatorId 
                                    AND end_date >= GETDATE()";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OperatorId", OperatorId);
                        int activeTrips = (int)command.ExecuteScalar();
                        lblActiveTrips.Text = activeTrips.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading active trips: " + ex.Message, "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblActiveTrips.Text = "Error";
            }
        }

        private void LoadTotalRevenue()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to calculate total revenue from all bookings for this operator's trips
                    // Removed status filter to show all bookings revenue
                    string query = @"SELECT COALESCE(SUM(CAST(b.total_amount AS decimal(18,2))), 0) AS TotalRevenue
                                    FROM Booking b
                                    JOIN Trip t ON b.trip_id = t.trip_id
                                    WHERE t.operator_id = @OperatorId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OperatorId", OperatorId);
                        object result = command.ExecuteScalar();

                        // Handle the result safely regardless of its type
                        decimal totalRevenue = 0;
                        if (result != null && result != DBNull.Value)
                        {
                            // Try to convert to decimal, if it fails, use 0
                            if (!decimal.TryParse(result.ToString(), out totalRevenue))
                            {
                                totalRevenue = 0;
                            }
                        }

                        lblTotalRevenue.Text = string.Format("${0:N0}", totalRevenue);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading total revenue: " + ex.Message, "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblTotalRevenue.Text = "Error";
            }
        }



        private void btnCreateTrip_Click(object sender, EventArgs e)
        {
            // Open create trip form
            CreateTripForm tripForm = new CreateTripForm(OperatorId,connectionString);
            if (tripForm.ShowDialog() == DialogResult.OK)
            {
                LoadDashboardData(); // Refresh data after creating new trip
            }
        }

        private void btnManageTrips_Click(object sender, EventArgs e)
        {
            // Open trip management form
            TripManagementForm tripManagement = new TripManagementForm(connectionString);
            tripManagement.Show();
            //if (tripManagement.ShowDialog() == DialogResult.OK)
            //{
            //    LoadDashboardData(); // Refresh data after managing trips
            //}
        }

        private void btnResourceCoordination_Click(object sender, EventArgs e)
        {
            // Open resource coordination form
            ResourceCoordinationForm resourceForm = new ResourceCoordinationForm();
            resourceForm.ShowDialog();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            // Open analytics form
            PerformanceAnalyticsForm analyticsForm = new PerformanceAnalyticsForm(connectionString, operatorId);
            analyticsForm.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            // Redirect to login form
            //LoginForm loginForm = new LoginForm();
            //loginForm.Show();
        }
        /*
        private void dgvTrips_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.ColumnIndex == dgvTrips.Columns["ViewDetails"].Index)
            //{
            //    string tripId = dgvTrips.Rows[e.RowIndex].Cells["TripID"].Value.ToString();
            //    // Open trip details form
            //    TripDetailsForm detailsForm = new TripDetailsForm(tripId);
            //    detailsForm.ShowDialog();
            //}
        }

        private void dgvBookings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.ColumnIndex == dgvBookings.Columns["ManageBooking"].Index)
            //{
            //    string bookingId = dgvBookings.Rows[e.RowIndex].Cells["BookingID"].Value.ToString();
            //    // Open booking management form
            //    BookingManagementForm bookingForm = new BookingManagementForm(bookingId);
            //    if (bookingForm.ShowDialog() == DialogResult.OK)
            //    {
            //        LoadDashboardData(); // Refresh data after managing booking
            //    }
            //}
        }
        */
        private void lblMinimize_Click(object sender, EventArgs e)
        {

        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void panelSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTripsTitle_Click(object sender, EventArgs e)
        {

        }
        /*
        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTrips_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        */
        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        private void panelActive_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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

        private void btnBookingMng_Click(object sender, EventArgs e)
        {
            TourOperatorBookingRequestsForm book = new TourOperatorBookingRequestsForm(connectionString, operatorId);
            book.Show();
            this.Hide();
        }

        private void lblActiveTrips_Click(object sender, EventArgs e)
        {
            // Show active trips in a separate form or panel
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT trip_id, title, destination, start_date, end_date, 
                                    price_per_person, capacity
                                    FROM Trip 
                                    WHERE operator_id = @OperatorId 
                                    AND end_date >= GETDATE()
                                    ORDER BY start_date";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@OperatorId", OperatorId);

                    DataTable tripsTable = new DataTable();
                    adapter.Fill(tripsTable);

                    // Display the trips in a new form or panel
                    DisplayTripsDetails(tripsTable, "Active Trips");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving active trips: " + ex.Message, "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblTotalRevenue_Click(object sender, EventArgs e)
        {
            // Show revenue breakdown in a separate form or panel
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Modified to include booking status and show all bookings
                    string query = @"SELECT t.title AS TripName, 
                                    COUNT(b.booking_id) AS BookingsCount,
                                    SUM(CAST(b.total_amount AS decimal(18,2))) AS Revenue,
                                    ISNULL(b.status, 'Unknown') AS BookingStatus
                                    FROM Trip t
                                    LEFT JOIN Booking b ON t.trip_id = b.trip_id
                                    WHERE t.operator_id = @OperatorId
                                    GROUP BY t.title, b.status
                                    ORDER BY Revenue DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@OperatorId", OperatorId);

                    DataTable revenueTable = new DataTable();
                    adapter.Fill(revenueTable);

                    if (revenueTable.Rows.Count == 0)
                    {
                        // If no bookings found, display trips without bookings
                        query = @"SELECT t.title AS TripName, 
                                0 AS BookingsCount,
                                CAST(0 AS decimal(18,2)) AS Revenue,
                                'No Bookings' AS BookingStatus
                                FROM Trip t
                                WHERE t.operator_id = @OperatorId
                                ORDER BY t.title";

                        adapter = new SqlDataAdapter(query, connection);
                        adapter.SelectCommand.Parameters.AddWithValue("@OperatorId", OperatorId);
                        adapter.Fill(revenueTable);

                        if (revenueTable.Rows.Count == 0)
                        {
                            MessageBox.Show("No trips found for this operator. Please create trips first.",
                                           "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    // Display the revenue breakdown in a new form or panel
                    DisplayRevenueDetails(revenueTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving revenue details: " + ex.Message, "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void lblOperatorName_Click(object sender, EventArgs e)
        {
            // Show operator details or profile in a separate form
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT company_name, email, phone, registration_date, last_login_date_
                                    FROM TourOperator
                                    WHERE operator_id = @OperatorId";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@OperatorId", OperatorId);

                    DataTable operatorTable = new DataTable();
                    adapter.Fill(operatorTable);

                    if (operatorTable.Rows.Count > 0)
                    {
                        // Display operator profile details or open profile editor
                        DisplayOperatorProfile(operatorTable.Rows[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving operator details: " + ex.Message, "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper methods to display detailed information

        private void DisplayTripsDetails(DataTable tripsTable, string title)
        {
            // This method would create and show a form with a DataGridView
            // showcasing the active trips
            Form tripsForm = new Form
            {
                Text = title,
                Size = new Size(800, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            DataGridView gridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = tripsTable,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            tripsForm.Controls.Add(gridView);
            tripsForm.ShowDialog();
        }

        private void DisplayRevenueDetails(DataTable revenueTable)
        {
            // This method creates and shows a form with revenue breakdown
            Form revenueForm = new Form
            {
                Text = "Revenue Breakdown by Trip",
                Size = new Size(700, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            // Calculate total revenue
            decimal totalRevenue = 0;
            foreach (DataRow row in revenueTable.Rows)
            {
                if (row["Revenue"] != DBNull.Value)
                {
                    decimal val;
                    if (decimal.TryParse(row["Revenue"].ToString(), out val))
                        totalRevenue += val;
                }
            }

            // Create a panel for the summary
            Panel summaryPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(10)
            };

            // Add summary labels
            Label lblTotalRevenue = new Label
            {
                Text = $"Total Revenue: {string.Format("${0:N0}", totalRevenue)}",
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            Label lblTotalBookings = new Label
            {
                Text = $"Total Bookings: {revenueTable.Compute("SUM(BookingsCount)", "")}",
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                Location = new Point(10, 40),
                AutoSize = true
            };

            // Create data grid for the details
            DataGridView gridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = revenueTable,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.AliceBlue }
            };

            // Format columns
            if (revenueTable.Columns.Contains("Revenue"))
            {
                gridView.Columns["Revenue"].DefaultCellStyle.Format = "C0";
                gridView.Columns["Revenue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (revenueTable.Columns.Contains("BookingsCount"))
            {
                gridView.Columns["BookingsCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Format status column if it exists
            if (revenueTable.Columns.Contains("BookingStatus"))
            {
                gridView.CellFormatting += (s, e) => {
                    if (e.ColumnIndex == gridView.Columns["BookingStatus"].Index && e.Value != null)
                    {
                        string status = e.Value.ToString();
                        if (status == "Confirmed")
                            e.CellStyle.ForeColor = Color.Green;
                        else if (status == "Cancelled" || status == "No Bookings")
                            e.CellStyle.ForeColor = Color.Red;
                        else if (status == "Pending")
                            e.CellStyle.ForeColor = Color.Orange;

                        e.CellStyle.Font = new Font(gridView.Font, FontStyle.Bold);
                    }
                };
            }

            // Add controls to the form
            summaryPanel.Controls.Add(lblTotalRevenue);
            summaryPanel.Controls.Add(lblTotalBookings);
            revenueForm.Controls.Add(gridView);
            revenueForm.Controls.Add(summaryPanel);

            // Show the form
            revenueForm.ShowDialog();
        }

        private void DisplayOperatorProfile(DataRow operatorData)
        {
            // This method would create and show a form with operator details
            Form profileForm = new Form
            {
                Text = "Operator Profile",
                Size = new Size(500, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            TableLayoutPanel panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(10)
            };

            // Add field labels and values
            AddProfileField(panel, 0, "Company Name:", operatorData["company_name"].ToString());
            AddProfileField(panel, 1, "Email:", operatorData["email"].ToString());
            AddProfileField(panel, 2, "Phone:", operatorData["phone"].ToString());
            AddProfileField(panel, 3, "Registration Date:",
                            Convert.ToDateTime(operatorData["registration_date"]).ToShortDateString());
            AddProfileField(panel, 4, "Last Login:",
                            Convert.ToDateTime(operatorData["last_login_date_"]).ToShortDateString());

            profileForm.Controls.Add(panel);
            profileForm.ShowDialog();
        }

        private void AddProfileField(TableLayoutPanel panel, int row, string fieldName, string fieldValue)
        {
            panel.Controls.Add(new Label
            {
                Text = fieldName,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                Dock = DockStyle.Fill
            }, 0, row);

            panel.Controls.Add(new Label
            {
                Text = fieldValue,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                Dock = DockStyle.Fill
            }, 1, row);
        }
    

        private void TourOperatorDashboard_Load(object sender, EventArgs e)
        {

        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}