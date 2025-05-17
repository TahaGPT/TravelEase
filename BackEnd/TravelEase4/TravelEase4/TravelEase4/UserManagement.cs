using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class UserManagement : Form
    {
        private string connstr = @"Data Source=DESKTOP-2I8F60O\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

        public UserManagement()
        {
            InitializeComponent();
            CreateUserManagementInterface();
        }

        // Mark this method as private instead of allowing it to be generated automatically
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UserManagement
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1195, 929);
            this.Name = "UserManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Management";
            this.Load += new System.EventHandler(this.UserManagement_Load);
            this.ResumeLayout(false);
        }

        private void CreateUserManagementInterface()
        {
            // Header label
            Label headerLabel = new Label
            {
                Text = "User Management",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 65, 100),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            this.Controls.Add(headerLabel); // Add to form controls

            // Tab control for different user types
            TabControl userTabs = new TabControl
            {
                Location = new Point(20, 70),
                Size = new Size(950, 450),
                Font = new Font("Segoe UI", 10)
            };

            // Create tabs
            TabPage travelerTab = new TabPage("Travelers");
            TabPage operatorTab = new TabPage("Tour Operators");
            TabPage serviceProviderTab = new TabPage("Service Providers");
            TabPage pendingApprovalsTab = new TabPage("Pending Approvals");

            // Add tabs to control
            userTabs.TabPages.Add(travelerTab);
            userTabs.TabPages.Add(operatorTab);
            userTabs.TabPages.Add(serviceProviderTab);
            userTabs.TabPages.Add(pendingApprovalsTab);

            // Create DataGridView for each tab
            travelerTab.Controls.Add(CreateUserDataGridView("Travelers"));
            operatorTab.Controls.Add(CreateUserDataGridView("Operators"));
            serviceProviderTab.Controls.Add(CreateUserDataGridView("ServiceProviders"));
            pendingApprovalsTab.Controls.Add(CreatePendingApprovalsGridView("pendingapp"));

            // Add tab control to form
            this.Controls.Add(userTabs);

            // Create action buttons
            Button approveButton = new Button
            {
                Text = "Approve Selected",
                Size = new Size(150, 35),
                Location = new Point(20, 530),
                BackColor = Color.FromArgb(30, 120, 180),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            approveButton.Click += ApproveButton_Click;
            this.Controls.Add(approveButton); // Add to form controls

            Button rejectButton = new Button
            {
                Text = "Reject Selected",
                Size = new Size(150, 35),
                Location = new Point(180, 530),
                BackColor = Color.FromArgb(180, 30, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            rejectButton.Click += RejectButton_Click;
            this.Controls.Add(rejectButton); // Add to form controls

            // No deactivate button is added, as requested
        }

        private DataGridView CreateUserDataGridView(string userType)
        {
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(240, 240, 250) }
            };

            // We'll let the DataSource create columns automatically
            // instead of adding them manually

            LoadUserData(userType, dgv);
            return dgv;
        }

        private DataGridView CreatePendingApprovalsGridView(string userType)
        {
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(240, 240, 250) }
            };

            LoadUserData(userType, dgv);
            return dgv;
        }

        private void LoadUserData(string userType, DataGridView dgv)
        {
            insertdata(userType, dgv);
        }

        private void ApproveButton_Click(object sender, EventArgs e)
        {
            TabControl userTabs = this.Controls.OfType<TabControl>().FirstOrDefault();
            if (userTabs == null || userTabs.SelectedTab == null)
            {
                MessageBox.Show("Please select a tab first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string userType = userTabs.SelectedTab.Text;
            // Convert tab text to the type strings used in your code
            string dataType;
            switch (userType)
            {
                case "Travelers":
                    dataType = "Travelers";
                    break;
                case "Tour Operators":
                    dataType = "Operators";
                    break;
                case "Service Providers":
                    dataType = "ServiceProviders";
                    break;
                case "Pending Approvals":
                    dataType = "pendingapp";
                    break;
                default:
                    MessageBox.Show("Unknown user type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            // Get the DataGridView from the active tab
            DataGridView dgv = userTabs.SelectedTab.Controls.OfType<DataGridView>().FirstOrDefault();
            if (dgv == null)
            {
                MessageBox.Show("No data grid found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Call updateapproval with task = 1 for approval
            updateapproval(dataType, 0, 1, dgv);

            // Refresh the grid after update
            LoadUserData(dataType, dgv);

            MessageBox.Show("Selected user(s) have been approved.", "Approval Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RejectButton_Click(object sender, EventArgs e)
        {
            TabControl userTabs = this.Controls.OfType<TabControl>().FirstOrDefault();
            if (userTabs == null || userTabs.SelectedTab == null)
            {
                MessageBox.Show("Please select a tab first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string userType = userTabs.SelectedTab.Text;
            // Convert tab text to the type strings used in your code
            string dataType;
            switch (userType)
            {
                case "Travelers":
                    dataType = "Travelers";
                    break;
                case "Tour Operators":
                    dataType = "Operators";
                    break;
                case "Service Providers":
                    dataType = "ServiceProviders";
                    break;
                case "Pending Approvals":
                    dataType = "pendingapp";
                    break;
                default:
                    MessageBox.Show("Unknown user type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            // Get the DataGridView from the active tab
            DataGridView dgv = userTabs.SelectedTab.Controls.OfType<DataGridView>().FirstOrDefault();
            if (dgv == null)
            {
                MessageBox.Show("No data grid found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Call updateapproval with task = 0 for rejection
            updateapproval(dataType, 0, 0, dgv);

            // Refresh the grid after update
            LoadUserData(dataType, dgv);

            MessageBox.Show("Selected user(s) have been rejected.", "Rejection Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UserManagement_Load(object sender, EventArgs e)
        {
            // Form load event - can be used for initialization if needed
        }

        private void insertdata(string userType, DataGridView dgv)
        {
            if (userType == "Travelers")
            {
                {
                    string query = @"
                                           SELECT 
                        t.traveler_id as uid, 
                        t.name, 
                        t.email, 
                        t.age, 
                        t.nationality,
                        t.registration_date,
                        CASE WHEN t.is_approved = 1 THEN 'APPROVED' ELSE 'PENDING' END AS APPROVED,
                        (SELECT COUNT(*) FROM Booking b WHERE b.traveler_id = t.traveler_id) AS TripCount,
                        t.last_login_date 
                    FROM 
                        Traveler t
                    ORDER BY 
                        t.traveler_id";

                    using (SqlConnection conn = new SqlConnection(connstr))
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgv.DataSource = dt;
                    }
                }
            }
            else if (userType == "Operators")
            {
                string query = @"
                   SELECT 
                            O.operator_id AS uid,
                            O.company_name AS CompanyName,
                            O.email AS Email,
                            O.registration_date AS RegistrationDate,
                            CASE WHEN O.is_approved = 1 THEN 'APPROVED' ELSE 'PENDING' END AS APPROVED,
                            COUNT(DISTINCT T.trip_id) AS TripCount,
                            ISNULL(AVG(R.rating), 0) AS Rating
                        FROM TourOperator O
                        LEFT JOIN Trip T ON O.operator_id = T.operator_id
                        LEFT JOIN Review R ON T.trip_id = R.trip_id
                        GROUP BY O.operator_id, O.company_name, O.email, O.registration_date, O.is_approved";

                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            else if (userType == "ServiceProviders")
            {
                string query = @"
                                  SELECT 
                        SP.provider_id AS uid, 
                        SP.name AS ProviderName, 
                        SP.service_type AS ServiceType,
                        SP.registration_date AS RegistrationDate,
                        CASE WHEN SP.is_approved = 1 THEN 'APPROVED' ELSE 'PENDING' END AS APPROVED,
                        ISNULL(AVG(R.rating), 0) AS Rating
                    FROM ServiceProvider SP
                    LEFT JOIN ProviderAssignment PA ON PA.provider_id = SP.provider_id
                    LEFT JOIN ServiceAssignment SA ON SA.assignment_id = PA.assignment_id
                    LEFT JOIN is_assigned IA ON IA.assignment_id = SA.assignment_id
                    LEFT JOIN Trip T ON T.trip_id = IA.trip_id
                    LEFT JOIN Review R ON R.trip_id = T.trip_id
                    GROUP BY SP.provider_id, SP.name, SP.service_type, SP.registration_date, SP.is_approved";

                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            else if (userType == "pendingapp")
            {
                string query = @"
                       SELECT 
                        'Traveler' AS user_type,
                        traveler_id AS uid,
                        name,
                        registration_date,
                        CASE WHEN is_approved = 1 THEN 'APPROVED' ELSE 'PENDING' END AS APPROVED
                        FROM Traveler
                        WHERE is_approved = 0
                        UNION
                        SELECT 
                        'TourOperator' AS user_type,
                        operator_id AS uid,
                        company_name AS name,
                        registration_date,
                        CASE WHEN is_approved = 1 THEN 'APPROVED' ELSE 'PENDING' END AS APPROVED
                        FROM TourOperator
                        WHERE is_approved = 0
                        UNION
                        SELECT 
                        'ServiceProvider' AS user_type,
                        provider_id AS uid,
                        service_type AS name,
                        registration_date,
                        CASE WHEN is_approved = 1 THEN 'APPROVED' ELSE 'PENDING' END AS APPROVED
                        FROM ServiceProvider
                        WHERE is_approved = 0
                        ORDER BY registration_date";

                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
        }

        private void updateapproval(string usertype, int uid, int task, DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user row first.");
                return;
            }

            DataGridViewRow row = dgv.SelectedRows[0];
            int uid2 = Convert.ToInt32(row.Cells["uid"].Value);

            string query = "";

            // Special handling for pending approvals tab
            if (usertype == "pendingapp")
            {
                string selectedUserType = row.Cells["user_type"].Value.ToString();

                switch (selectedUserType)
                {
                    case "Traveler":
                        query = "UPDATE Traveler SET is_approved = @APPROVED WHERE traveler_id = @uid";
                        break;

                    case "TourOperator":
                        query = "UPDATE TourOperator SET is_approved = @APPROVED WHERE operator_id = @uid";
                        break;

                    case "ServiceProvider":
                        query = "UPDATE ServiceProvider SET is_approved = @APPROVED WHERE provider_id = @uid";
                        break;

                    default:
                        MessageBox.Show("Unknown user type in pending approvals.");
                        return;
                }
            }
            else
            {
                switch (usertype)
                {
                    case "Travelers":
                        query = "UPDATE Traveler SET is_approved = @APPROVED WHERE traveler_id = @uid";
                        break;

                    case "Operators":
                        query = "UPDATE TourOperator SET is_approved = @APPROVED WHERE operator_id = @uid";
                        break;

                    case "ServiceProviders":
                        query = "UPDATE ServiceProvider SET is_approved = @APPROVED WHERE provider_id = @uid";
                        break;

                    default:
                        MessageBox.Show("Unknown user type.");
                        return;
                }
            }

            execq(query, uid2, task);

            // Refresh the grid data after update
            LoadUserData(usertype, dgv);
        }

        // Keeping the DeleteAccount method in case it's needed elsewhere,
        // but it's not connected to any UI element as requested
        private void DeleteAccount(string usertype, DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user row first.");
                return;
            }

            DataGridViewRow row = dgv.SelectedRows[0];
            int uid = Convert.ToInt32(row.Cells["uid"].Value);

            string query = "";
            string tableName = "";

            // Special handling for pending approvals tab
            if (usertype == "pendingapp")
            {
                string selectedUserType = row.Cells["user_type"].Value.ToString();

                switch (selectedUserType)
                {
                    case "Traveler":
                        tableName = "Traveler";
                        break;

                    case "TourOperator":
                        tableName = "TourOperator";
                        break;

                    case "ServiceProvider":
                        tableName = "ServiceProvider";
                        break;

                    default:
                        MessageBox.Show("Unknown user type in pending approvals.");
                        return;
                }
            }
            else
            {
                switch (usertype)
                {
                    case "Travelers":
                        tableName = "Traveler";
                        break;

                    case "Operators":
                        tableName = "TourOperator";
                        break;

                    case "ServiceProviders":
                        tableName = "ServiceProvider";
                        break;

                    default:
                        MessageBox.Show("Unknown user type.");
                        return;
                }
            }

            try
            {
                // First attempt - try to delete directly
                query = $"DELETE FROM {tableName} WHERE {tableName.ToLower()}_id = @uid";

                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", uid);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Account successfully deleted from {tableName}.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"No rows deleted. User ID {uid} may not exist in {tableName}.");
                    }
                }
            }
            catch (Exception ex)
            {
                // If deletion fails, show the error and try alternative approach
                MessageBox.Show($"Error deleting account: {ex.Message}");

                // Since direct deletion probably failed due to foreign key constraints,
                // try updating the approval status to 0 instead
                try
                {
                    query = $"UPDATE {tableName} SET is_approved = 0 WHERE {tableName.ToLower()}_id = @uid";

                    using (SqlConnection conn = new SqlConnection(connstr))
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", uid);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Account has been set to not approved instead of deletion.");
                        }
                        else
                        {
                            MessageBox.Show($"Unable to update the account. User ID {uid} may not exist in {tableName}.");
                        }
                    }
                }
                catch (Exception updateEx)
                {
                    MessageBox.Show($"Error updating account: {updateEx.Message}");
                }
            }
        }

        private void execq(string query, int id, int task)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@APPROVED", task);
                cmd.Parameters.AddWithValue("@uid", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Action completed successfully.");
        }
    }
}