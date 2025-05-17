
using DestinationPopularityReport;
using ServiceProviderEfficiencyReport;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using TourOperatorPerformanceReport;
using TravelBookingSystem;
using TravelEaseDashboard;
using TravelEaseDataVisualization;
using TravelerDemographicsApp;
using TripBookingRevenueReport;
//using TravelEase.AdminDashboard;

namespace TravelEase
{
    public partial class AdminDashboard : Form
    {
        private string connectionString = @"Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

        // Fields for form dragging
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        // Admin info
        private int adminId;
        string username;
        string password;
        private string adminName;

        public AdminDashboard(string u = "", string p = "")
        {
            InitializeComponent();
            username = u;
            password = p;

            // Set admin name in header
            lblAdminName.Text = adminName;
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            // Load dashboard data
            LoadDashboardStatistics();
            LoadRecentReviews();
            LoadPendingApprovals();
            string query = "SELECT COUNT(trip_id) FROM Trip";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    lblTripsValue.Text = $"{count}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            //doing the same for travellers
            string query2 = "SELECT COUNT(traveler_id) FROM Traveler";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query2, conn))
            {
                try
                {
                    conn.Open();
                    int count2 = (int)cmd.ExecuteScalar();
                    lblTravellersValue.Text = $"{count2}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            //doing same for operators
            string query3 = "SELECT COUNT(operator_id) FROM TourOperator";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query3, conn))
            {
                try
                {
                    conn.Open();
                    int count3 = (int)cmd.ExecuteScalar();
                    lblOperatorsValue.Text = $"{count3}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            //doing same for total revenue
            string query4 = "SELECT SUM(total_amount) FROM Booking";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query4, conn))
            {
                try
                {
                    conn.Open();
                    int count4 = (int)cmd.ExecuteScalar();
                    lblRevenueValue.Text = $"{"$"+count4}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            showingrr();
            showpapp();
            // Highlight the dashboard button as selected
            SetActiveButton(btnDashboard);
        }

        #region UI Control Methods

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            // Allow form to be dragged by header
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Show login form and close this form
            //Login loginForm = new Login();
            //loginForm.Show();
            //this.Close();
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

        // Method to highlight active button
        private void SetActiveButton(Button button)
        {
            // Reset all button backgrounds
            foreach (Control ctrl in pnlSidebar.Controls)
            {
                if (ctrl is Button)
                {
                    ctrl.BackColor = Color.FromArgb(33, 62, 85);
                }
            }

            // Set active button to highlight color
            button.BackColor = Color.FromArgb(41, 128, 185);
        }

        #endregion

        #region Navigation Methods

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDashboard);
            // Already on dashboard - refresh data
            LoadDashboardStatistics();
            LoadRecentReviews();
            LoadPendingApprovals();
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnUserManagement);
            //// Open User Management Form
            UserManagement userMgmtForm = new UserManagement();
            userMgmtForm.Show();
            //this.Hide();
        }

        private void btnTourCategories_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnTourCategories);
            //// Open Tour Categories Form
            TourCategories tourCategoriesForm = new TourCategories();
            tourCategoriesForm.Show();
            //this.Hide();
        }

        private void btnReviewModeration_Click(object sender, EventArgs e)
        {
            //SetActiveButton(btnReviewModeration);
            //// Open Review Moderation Form
            ReviewModeration reviewModerationForm = new ReviewModeration();
            reviewModerationForm.Show();
            //this.Hide();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnAnalytics);
            //// Open Analytics Form
            PaymentTransactionandFraudReport analyticsForm = new PaymentTransactionandFraudReport();
            PlatformGrowthReport analyticsForm2 = new PlatformGrowthReport();
            analyticsForm.Show();
            analyticsForm2.Show();
            //this.Hide();
        }

        #endregion

        #region Data Loading Methods

        private void LoadDashboardStatistics()
        {
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();

            //        // Get total trips count
            //        string tripsSql = "SELECT COUNT(*) FROM Trips";
            //        using (SqlCommand cmd = new SqlCommand(tripsSql, conn))
            //        {
            //            int tripsCount = (int)cmd.ExecuteScalar();
            //            lblTripsValue.Text = tripsCount.ToString();
            //        }

            //        // Get total travellers count
            //        string travellersSql = "SELECT COUNT(*) FROM Users WHERE UserTypeID = (SELECT UserTypeID FROM UserTypes WHERE TypeName = 'Traveller')";
            //        using (SqlCommand cmd = new SqlCommand(travellersSql, conn))
            //        {
            //            int travellersCount = (int)cmd.ExecuteScalar();
            //            lblTravellersValue.Text = travellersCount.ToString();
            //        }

            //        // Get total operators count
            //        string operatorsSql = "SELECT COUNT(*) FROM Users WHERE UserTypeID = (SELECT UserTypeID FROM UserTypes WHERE TypeName = 'Tour Operator')";
            //        using (SqlCommand cmd = new SqlCommand(operatorsSql, conn))
            //        {
            //            int operatorsCount = (int)cmd.ExecuteScalar();
            //            lblOperatorsValue.Text = operatorsCount.ToString();
            //        }

            //        // Get total revenue
            //        string revenueSql = "SELECT SUM(TotalAmount) FROM Bookings WHERE Status = 'Confirmed'";
            //        using (SqlCommand cmd = new SqlCommand(revenueSql, conn))
            //        {
            //            object result = cmd.ExecuteScalar();
            //            decimal revenue = (result == DBNull.Value) ? 0 : Convert.ToDecimal(result);
            //            lblRevenueValue.Text = "$" + revenue.ToString("N0");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error loading dashboard statistics: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void LoadRecentReviews()
        {
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        string sql = @"
            //            SELECT TOP 10 
            //                R.ReviewID, 
            //                U.FirstName + ' ' + U.LastName AS Reviewer, 
            //                T.TripName, 
            //                R.Rating, 
            //                LEFT(R.ReviewText, 50) + '...' AS ReviewPreview, 
            //                R.DatePosted,
            //                R.IsApproved
            //            FROM Reviews R
            //            INNER JOIN Users U ON R.UserID = U.UserID
            //            INNER JOIN Trips T ON R.TripID = T.TripID
            //            ORDER BY R.DatePosted DESC";

            //        SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            //        DataTable reviewsTable = new DataTable();
            //        adapter.Fill(reviewsTable);

            //        // Add action button column
            //        DataGridViewButtonColumn approveColumn = new DataGridViewButtonColumn();
            //        approveColumn.HeaderText = "Action";
            //        approveColumn.Text = "Moderate";
            //        approveColumn.UseColumnTextForButtonValue = true;

            //        // Set up datagridview
            //        dgvRecentReviews.DataSource = reviewsTable;
            //        dgvRecentReviews.Columns.Add(approveColumn);

            //        // Format columns
            //        dgvRecentReviews.Columns["ReviewID"].Visible = false;
            //        dgvRecentReviews.Columns["IsApproved"].Visible = false;
            //        dgvRecentReviews.Columns["Reviewer"].Width = 100;
            //        dgvRecentReviews.Columns["TripName"].Width = 100;
            //        dgvRecentReviews.Columns["Rating"].Width = 50;
            //        dgvRecentReviews.Columns["ReviewPreview"].Width = 150;
            //        dgvRecentReviews.Columns["DatePosted"].Width = 100;

            //        // Alternate row coloring
            //        dgvRecentReviews.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            //        dgvRecentReviews.EnableHeadersVisualStyles = false;
            //        dgvRecentReviews.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 62, 85);
            //        dgvRecentReviews.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error loading reviews: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void LoadPendingApprovals()
        {
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        string sql = @"
            //            SELECT TOP 10 
            //                U.UserID, 
            //                U.FirstName + ' ' + U.LastName AS Name, 
            //                U.Email,
            //                UT.TypeName AS UserType,
            //                U.DateRegistered
            //            FROM Users U
            //            INNER JOIN UserTypes UT ON U.UserTypeID = UT.UserTypeID
            //            WHERE U.IsApproved = 0
            //            ORDER BY U.DateRegistered DESC";

            //        SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            //        DataTable approvalsTable = new DataTable();
            //        adapter.Fill(approvalsTable);

            //        // Add action buttons
            //        DataGridViewButtonColumn approveColumn = new DataGridViewButtonColumn();
            //        approveColumn.HeaderText = "Action";
            //        approveColumn.Text = "Approve/Reject";
            //        approveColumn.UseColumnTextForButtonValue = true;

            //        // Set up datagridview
            //        dgvPendingApprovals.DataSource = approvalsTable;
            //        dgvPendingApprovals.Columns.Add(approveColumn);

            //        // Format columns
            //        dgvPendingApprovals.Columns["UserID"].Visible = false;
            //        dgvPendingApprovals.Columns["Name"].Width = 100;
            //        dgvPendingApprovals.Columns["Email"].Width = 130;
            //        dgvPendingApprovals.Columns["UserType"].Width = 80;
            //        dgvPendingApprovals.Columns["DateRegistered"].Width = 100;

            //        // Alternate row coloring
            //        dgvPendingApprovals.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            //        dgvPendingApprovals.EnableHeadersVisualStyles = false;
            //        dgvPendingApprovals.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 62, 85);
            //        dgvPendingApprovals.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error loading pending approvals: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        #endregion

        #region Action Handlers

        private void dgvRecentReviews_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if clicked cell is a button and not the header
            //if (e.RowIndex >= 0 && dgvRecentReviews.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            //{
            //    // Get the review ID
            //    int reviewId = Convert.ToInt32(dgvRecentReviews.Rows[e.RowIndex].Cells["ReviewID"].Value);

            //    // Open review moderation dialog
            //    using (ReviewModerationDialog moderationDialog = new ReviewModerationDialog(reviewId))
            //    {
            //        if (moderationDialog.ShowDialog() == DialogResult.OK)
            //        {
            //            // Refresh the reviews list
            //            LoadRecentReviews();
            //        }
            //    }
            //}
        }

        private void dgvPendingApprovals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if clicked cell is a button and not the header
            //if (e.RowIndex >= 0 && dgvPendingApprovals.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            //{
            //    // Get the user ID
            //    int userId = Convert.ToInt32(dgvPendingApprovals.Rows[e.RowIndex].Cells["UserID"].Value);
            //    string userName = dgvPendingApprovals.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            //    string userType = dgvPendingApprovals.Rows[e.RowIndex].Cells["UserType"].Value.ToString();

            //    // Show approval dialog
            //    using (UserApprovalDialog approvalDialog = new UserApprovalDialog(userId, userName, userType))
            //    {
            //        if (approvalDialog.ShowDialog() == DialogResult.OK)
            //        {
            //            // Refresh the approvals list
            //            LoadPendingApprovals();
            //        }
            //    }
            //}
        }

        private void btnPlatformReport_Click(object sender, EventArgs e)
        {
            // Open platform growth report form
            //PlatformGrowthReport reportForm = new PlatformGrowthReport();
            //reportForm.ShowDialog();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            AbandonedBookingAnalysisForm ab = new AbandonedBookingAnalysisForm();
            ab.Show();
       
        }

        private void lblTripsValue_Click(object sender, EventArgs e)
        {

        }

        private void pnlSystemStats_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTravellersValue_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Traveler";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int travelerCount = (int)command.ExecuteScalar();
                        MessageBox.Show($"Total Travelers: {travelerCount}", "Traveler Statistics",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Optional: You could show more detailed statistics here
                        // For example, travelers by nationality or registration date
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving traveler data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblOperatorsValue_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM TourOperator";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int operatorCount = (int)command.ExecuteScalar();
                        MessageBox.Show($"Total Tour Operators: {operatorCount}", "Operator Statistics",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Optional: You could show additional details like
                        // How many are approved vs not approved
                        string approvalQuery = "SELECT COUNT(*) FROM TourOperator WHERE is_approved = 1";
                        using (SqlCommand approvalCommand = new SqlCommand(approvalQuery, connection))
                        {
                            int approvedCount = (int)approvalCommand.ExecuteScalar();
                            MessageBox.Show($"Approved Operators: {approvedCount}\nPending Approval: {operatorCount - approvedCount}",
                                "Operator Approval Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving operator data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblRevenueValue_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get total revenue from all successful bookings
                    string query = @"SELECT SUM(p.amount) 
                            FROM Payment p 
                            INNER JOIN Booking b ON p.payment_id = b.payment_id 
                            WHERE p.payment_status = 'Completed' 
                            AND p.transaction_status = 'Success' 
                            AND p.is_refunded = 0";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        decimal totalRevenue = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

                        MessageBox.Show($"Total Revenue: ${totalRevenue:N2}", "Revenue Statistics",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Optional: Show more detailed revenue breakdown
                        string monthlyQuery = @"SELECT 
                                        MONTH(b.booking_date) as Month, 
                                        YEAR(b.booking_date) as Year, 
                                        SUM(p.amount) as Revenue
                                      FROM Payment p 
                                      INNER JOIN Booking b ON p.payment_id = b.payment_id 
                                      WHERE p.payment_status = 'Completed' 
                                      AND p.transaction_status = 'Success' 
                                      AND p.is_refunded = 0
                                      AND b.booking_date >= DATEADD(month, -3, GETDATE())
                                      GROUP BY MONTH(b.booking_date), YEAR(b.booking_date)
                                      ORDER BY Year, Month";

                        StringBuilder revenueBreakdown = new StringBuilder("Last 3 Months Revenue:\n\n");

                        using (SqlCommand monthlyCommand = new SqlCommand(monthlyQuery, connection))
                        {
                            using (SqlDataReader reader = monthlyCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int month = reader.GetInt32(0);
                                    int year = reader.GetInt32(1);
                                    decimal revenue = reader.GetDecimal(2);

                                    string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                                    revenueBreakdown.AppendLine($"{monthName} {year}: ${revenue:N2}");
                                }
                            }
                        }

                        if (revenueBreakdown.Length > 25) // If we have data beyond the header
                        {
                            MessageBox.Show(revenueBreakdown.ToString(), "Monthly Revenue Breakdown",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving revenue data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblTripsValue_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get basic trip count
                    string query = "SELECT COUNT(*) FROM Trip";

                    // Get category breakdown
                    string categoryQuery = @"SELECT tc.category_name, COUNT(t.trip_id) as TripCount
                                    FROM Trip t
                                    INNER JOIN TripCategory tc ON t.category_id = tc.category_id
                                    GROUP BY tc.category_name
                                    ORDER BY TripCount DESC";

                    // Get upcoming trips
                    string upcomingQuery = @"SELECT COUNT(*) 
                                    FROM Trip 
                                    WHERE start_date > GETDATE()";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int tripCount = (int)command.ExecuteScalar();

                        // Get upcoming trips count
                        int upcomingTrips = 0;
                        using (SqlCommand upcomingCommand = new SqlCommand(upcomingQuery, connection))
                        {
                            upcomingTrips = (int)upcomingCommand.ExecuteScalar();
                        }

                        MessageBox.Show($"Total Trips: {tripCount}\nUpcoming Trips: {upcomingTrips}",
                            "Trip Statistics", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Build category breakdown
                        StringBuilder categoryBreakdown = new StringBuilder("Trips by Category:\n\n");

                        using (SqlCommand categoryCommand = new SqlCommand(categoryQuery, connection))
                        {
                            using (SqlDataReader reader = categoryCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string categoryName = reader.GetString(0);
                                    int count = reader.GetInt32(1);

                                    categoryBreakdown.AppendLine($"{categoryName}: {count}");
                                }
                            }
                        }

                        MessageBox.Show(categoryBreakdown.ToString(), "Trip Categories",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving trip data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void showingrr()
        {
            
            string query = @"
        SELECT TOP 10 
            R.review_id, 
            T.name AS traveler_name, 
            Tr.title AS trip_title, 
            R.comment, 
            R.rating, 
            R.review_date
        FROM Review R
        JOIN Traveler T ON R.traveler_id = T.traveler_id
        LEFT JOIN Trip Tr ON R.trip_id = Tr.trip_id
        ORDER BY R.review_date DESC";  

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvRecentReviews.DataSource = dt;
            }
        }
        //show pending approvals 
        private void showpapp()
        {
           
         
            string query = @"
            SELECT 
                'Traveler' AS user_type,
                traveler_id AS user_id,
                name,
                registration_date,
                is_approved
            FROM Traveler
            WHERE is_approved = 0
            UNION
            SELECT 
                'TourOperator' AS user_type,
                operator_id AS user_id,
                company_name AS name,
                registration_date,
                is_approved
            FROM TourOperator
            WHERE is_approved = 0
            UNION
            SELECT 
                'ServiceProvider' AS user_type,
                provider_id AS user_id,
                service_type AS name,
                registration_date,
                is_approved
            FROM ServiceProvider
            WHERE is_approved = 0
            ORDER BY registration_date;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPendingApprovals.DataSource = dt;
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DestinationPopularityReportForm des = new DestinationPopularityReportForm();
            des.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PaymentTransactionandFraudReport fr = new PaymentTransactionandFraudReport();
            fr.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PlatformGrowthReport platform = new PlatformGrowthReport();
            platform.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ServiceProviderEfficiencyReportForm ser = new ServiceProviderEfficiencyReportForm();
            ser.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TourOperatorReportForm tour = new TourOperatorReportForm();
            tour.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TravelerDemographicsForm travelerDemographicsForm = new TravelerDemographicsForm();
            travelerDemographicsForm.Show();
        }

        private void BtnProfileSetting_Click(object sender, EventArgs e)
        {
            TripReportForm tripReportForm = new TripReportForm();
            tripReportForm.Show();
        }

    }
}