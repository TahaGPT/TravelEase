using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace TravelEase
{
    public partial class BookingRequestsForm : Form
    {
        private string connectionString;
        private int providerId;

        public BookingRequestsForm(int providerId, string conns)
        {
            InitializeComponent();
            this.providerId = providerId;
            this.Load += BookingRequestsForm_Load;
            connectionString = conns;
        }

        private void BookingRequestsForm_Load(object sender, EventArgs e)
        {
            // Set default selection to "All"
            cboStatus.SelectedIndex = 0;
            LoadBookingRequests();
        }

        private void LoadBookingRequests()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Get filter value
                    string statusFilter = cboStatus.SelectedItem?.ToString() ?? "All";

                    // Build the SQL query based on filter
                    string query = @"
                        SELECT b.booking_id AS BookingID, 
                               t.name AS TravelerName,
                               tr.title AS TripTitle,
                               b.booking_date AS BookingDate,
                               b.total_amount AS Amount,
                               b.status AS Status,
                               b.Group_Size AS GroupSize
                        FROM Booking b
                        INNER JOIN BookingServices_ bs ON b.booking_id = bs.booking_id
                        INNER JOIN Traveler t ON b.traveler_id = t.traveler_id
                        INNER JOIN Trip tr ON b.trip_id = tr.trip_id
                        WHERE bs.provider_id = @ProviderId";

                    // Add status filter if not "All"
                    if (statusFilter != "All")
                    {
                        query += " AND b.status = @Status";
                    }

                    query += " ORDER BY b.booking_date DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProviderId", providerId);

                    if (statusFilter != "All")
                    {
                        cmd.Parameters.AddWithValue("@Status", statusFilter);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Clear existing data and columns
                    dgvBookings.DataSource = null;
                    dgvBookings.Columns.Clear();

                    // Set the data source
                    dgvBookings.DataSource = dt;

                    // Add action buttons if there are rows
                    if (dt.Rows.Count > 0)
                    {
                        // Action Buttons for Accept/Reject
                        DataGridViewButtonColumn acceptCol = new DataGridViewButtonColumn();
                        acceptCol.HeaderText = "Accept";
                        acceptCol.Name = "Accept";
                        acceptCol.Text = "Accept";
                        acceptCol.UseColumnTextForButtonValue = true;
                        dgvBookings.Columns.Add(acceptCol);

                        DataGridViewButtonColumn rejectCol = new DataGridViewButtonColumn();
                        rejectCol.HeaderText = "Reject";
                        rejectCol.Name = "Reject";
                        rejectCol.Text = "Reject";
                        rejectCol.UseColumnTextForButtonValue = true;
                        dgvBookings.Columns.Add(rejectCol);
                    }

                    // Format the data grid view
                    FormatDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading booking requests: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            // Auto-size columns for better display
            dgvBookings.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // Style the grid
            dgvBookings.RowHeadersVisible = false;
            dgvBookings.EnableHeadersVisualStyles = false;
            dgvBookings.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvBookings.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBookings.ColumnHeadersDefaultCellStyle.Font = new Font(dgvBookings.Font, FontStyle.Bold);
            dgvBookings.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            // Make certain columns prettier
            foreach (DataGridViewColumn column in dgvBookings.Columns)
            {
                // Format column headers to be more readable
                column.HeaderText = FormatColumnName(column.HeaderText);

                // Format currency columns
                if (column.Name == "Amount")
                {
                    column.DefaultCellStyle.Format = "C2";
                }

                // Format date columns
                if (column.Name == "BookingDate")
                {
                    column.DefaultCellStyle.Format = "MMM dd, yyyy";
                }

                // Format status columns with colors
                if (column.Name == "Status")
                {
                    column.DefaultCellStyle.ForeColor = Color.White;
                    dgvBookings.CellFormatting += DgvBookings_CellFormatting;
                }
            }
        }

        private void DgvBookings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Color-code the status cells
            if (dgvBookings.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                switch (status)
                {
                    case "Pending":
                        e.CellStyle.BackColor = Color.FromArgb(243, 156, 18); // Orange
                        break;
                    case "Confirmed":
                        e.CellStyle.BackColor = Color.FromArgb(46, 204, 113); // Green
                        break;
                    case "Rejected":
                        e.CellStyle.BackColor = Color.FromArgb(231, 76, 60); // Red
                        break;
                    case "Completed":
                        e.CellStyle.BackColor = Color.FromArgb(52, 152, 219); // Blue
                        break;
                }
            }
        }

        private string FormatColumnName(string name)
        {
            // Format column names by adding spaces before capital letters
            if (string.IsNullOrEmpty(name))
                return name;

            string result = name[0].ToString();
            for (int i = 1; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]) && !char.IsUpper(name[i - 1]))
                    result += " " + name[i];
                else
                    result += name[i];
            }
            return result;
        }

        private void UpdateBookingStatus(int bookingId, string newStatus)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Begin transaction for data integrity
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Update the booking status
                        string updateQuery = "UPDATE Booking SET status = @Status WHERE booking_id = @BookingID";
                        SqlCommand cmd = new SqlCommand(updateQuery, conn, transaction);
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@BookingID", bookingId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Update the BookingServices_ status to match
                        string updateServiceQuery = "UPDATE BookingServices_ SET service_status = @Status WHERE booking_id = @BookingID AND provider_id = @ProviderID";
                        SqlCommand serviceCmd = new SqlCommand(updateServiceQuery, conn, transaction);
                        serviceCmd.Parameters.AddWithValue("@Status", newStatus);
                        serviceCmd.Parameters.AddWithValue("@BookingID", bookingId);
                        serviceCmd.Parameters.AddWithValue("@ProviderID", providerId);

                        serviceCmd.ExecuteNonQuery();

                        // Commit the transaction
                        transaction.Commit();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Booking has been {newStatus.ToLower()} successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reload bookings to reflect changes
                            LoadBookingRequests();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update booking status. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Roll back the transaction on error
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating booking status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadBookingRequests();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional: Auto-apply filter when selection changes
            LoadBookingRequests();
        }

        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // This method can remain empty since we're handling clicks in dgvBookings_CellClick
        }

        private void dgvBookings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle accept and reject buttons
            if (e.RowIndex < 0) return; // Ignore header row clicks

            // Get the current status
            string currentStatus = dgvBookings.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            int bookingId = Convert.ToInt32(dgvBookings.Rows[e.RowIndex].Cells["BookingID"].Value);

            // Don't allow actions on already processed bookings
            if (currentStatus != "Pending")
            {
                MessageBox.Show("This booking has already been processed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (e.ColumnIndex == dgvBookings.Columns["Accept"].Index)
            {
                // Confirm before accepting
                DialogResult result = MessageBox.Show("Are you sure you want to accept this booking?",
                    "Confirm Accept", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    UpdateBookingStatus(bookingId, "Confirmed");
                }
            }
            else if (e.ColumnIndex == dgvBookings.Columns["Reject"].Index)
            {
                // Confirm before rejecting
                DialogResult result = MessageBox.Show("Are you sure you want to reject this booking?",
                    "Confirm Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    UpdateBookingStatus(bookingId, "Rejected");
                }
            }
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dgvBookings = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 60);
            this.pnlHeader.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(251, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Booking Requests";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlFilter.Controls.Add(this.lblStatus);
            this.pnlFilter.Controls.Add(this.cboStatus);
            this.pnlFilter.Controls.Add(this.btnFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 60);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(800, 50);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(20, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(132, 25);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Filter by Status:";
            // 
            // cboStatus
            // 
            this.cboStatus.Items.AddRange(new object[] {
            "All",
            "Pending",
            "Confirmed",
            "Rejected",
            "Completed"});
            this.cboStatus.Location = new System.Drawing.Point(135, 17);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(150, 28);
            this.cboStatus.TabIndex = 1;
            this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(298, 11);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(100, 30);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "Apply Filter";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // dgvBookings
            // 
            this.dgvBookings.AllowUserToAddRows = false;
            this.dgvBookings.AllowUserToDeleteRows = false;
            this.dgvBookings.BackgroundColor = System.Drawing.Color.White;
            this.dgvBookings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookings.Location = new System.Drawing.Point(20, 120);
            this.dgvBookings.Name = "dgvBookings";
            this.dgvBookings.ReadOnly = true;
            this.dgvBookings.RowHeadersWidth = 51;
            this.dgvBookings.Size = new System.Drawing.Size(760, 350);
            this.dgvBookings.TabIndex = 0;
            this.dgvBookings.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBookings_CellClick);
            this.dgvBookings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBookings_CellContentClick);
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlFooter.Controls.Add(this.btnClose);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 480);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(800, 50);
            this.pnlFooter.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(680, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // BookingRequestsForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 530);
            this.Controls.Add(this.dgvBookings);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BookingRequestsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Booking Requests";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // Form fields
        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlFilter;
        private ComboBox cboStatus;
        private Label lblStatus;
        private Button btnFilter;
        private DataGridView dgvBookings;
        private Panel pnlFooter;
        private Button btnClose;
    }
}