using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class TourOperatorBookingRequestsForm : Form
    {
        private string connectionString;
        private int operatorId;

        public TourOperatorBookingRequestsForm(string con, int op)
        {
            InitializeComponent();
            this.operatorId = op;
            this.connectionString = con;
                this.Load += TourOperatorBookingRequestsForm_Load;
        }

        private void TourOperatorBookingRequestsForm_Load(object sender, EventArgs e)
        {
            LoadBookingRequests();
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            // Configure columns
            dgvBookings.Columns.Clear();
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BookingID",
                HeaderText = "Booking ID",
                DataPropertyName = "booking_id"
            });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TripTitle",
                HeaderText = "Trip",
                DataPropertyName = "title"
            });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TravelerName",
                HeaderText = "Traveler",
                DataPropertyName = "traveler_name"
            });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BookingDate",
                HeaderText = "Booking Date",
                DataPropertyName = "booking_date"
            });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalAmount",
                HeaderText = "Total Amount",
                DataPropertyName = "total_amount"
            });
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "status"
            });

            // Add Action Columns
            DataGridViewButtonColumn acceptColumn = new DataGridViewButtonColumn
            {
                Name = "Accept",
                HeaderText = "Accept",
                Text = "Accept",
                UseColumnTextForButtonValue = true
            };
            dgvBookings.Columns.Add(acceptColumn);

            DataGridViewButtonColumn rejectColumn = new DataGridViewButtonColumn
            {
                Name = "Reject",
                HeaderText = "Reject",
                Text = "Reject",
                UseColumnTextForButtonValue = true
            };
            dgvBookings.Columns.Add(rejectColumn);
        }

        // Diagnostic method to help identify why the query might not be returning results
        private void DiagnoseBookingQuery()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder diagnosticInfo = new StringBuilder();

                    // Check Tour Operator exists
                    string operatorCheckQuery = "SELECT COUNT(*) FROM TourOperator WHERE operator_id = @OperatorId";
                    using (SqlCommand cmd = new SqlCommand(operatorCheckQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@OperatorId", operatorId);
                        int operatorCount = Convert.ToInt32(cmd.ExecuteScalar());
                        diagnosticInfo.AppendLine($"Tour Operator Count: {operatorCount}");
                    }

                    // Check Trips for this Operator
                    string tripsCheckQuery = "SELECT COUNT(*) FROM Trip WHERE operator_id = @OperatorId";
                    using (SqlCommand cmd = new SqlCommand(tripsCheckQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@OperatorId", operatorId);
                        int tripCount = Convert.ToInt32(cmd.ExecuteScalar());
                        diagnosticInfo.AppendLine($"Trips Count: {tripCount}");
                    }

                    // Check Bookings for Trips of this Operator
                    string bookingsCheckQuery = @"
                    SELECT COUNT(*) 
                    FROM Booking b 
                    JOIN Trip t ON b.trip_id = t.trip_id 
                    WHERE t.operator_id = @OperatorId";
                    using (SqlCommand cmd = new SqlCommand(bookingsCheckQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@OperatorId", operatorId);
                        int bookingCount = Convert.ToInt32(cmd.ExecuteScalar());
                        diagnosticInfo.AppendLine($"Bookings Count: {bookingCount}");
                    }

                    // Detailed Query for Investigation
                    string detailedQuery = @"
                    SELECT 
                        b.booking_id, 
                        t.title, 
                        tv.name as traveler_name, 
                        b.booking_date, 
                        b.total_amount, 
                        b.status,
                        t.operator_id as trip_operator_id,
                        op.operator_id as operator_table_id
                    FROM Booking b 
                    JOIN Trip t ON b.trip_id = t.trip_id 
                    JOIN Traveler tv ON b.traveler_id = tv.traveler_id 
                    JOIN TourOperator op ON t.operator_id = op.operator_id
                    WHERE op.operator_id = @OperatorId";

                    using (SqlCommand cmd = new SqlCommand(detailedQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@OperatorId", operatorId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int rowCount = 0;
                            while (reader.Read())
                            {
                                rowCount++;
                                diagnosticInfo.AppendLine($"Booking ID: {reader["booking_id"]}, " +
                                    $"Trip: {reader["title"]}, " +
                                    $"Traveler: {reader["traveler_name"]}, " +
                                    $"Trip Operator ID: {reader["trip_operator_id"]}, " +
                                    $"Operator Table ID: {reader["operator_table_id"]}");
                            }
                            diagnosticInfo.AppendLine($"Total Rows Found: {rowCount}");
                        }
                    }

                    // Display diagnostic information
                    MessageBox.Show(diagnosticInfo.ToString(), "Query Diagnostic Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Diagnostic Error: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Modified LoadBookingRequests method with diagnostic fallback
        private void LoadBookingRequests()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                    SELECT 
                        b.booking_id, 
                        t.title, 
                        tv.name as traveler_name, 
                        b.booking_date, 
                        b.total_amount, 
                        b.status
                    FROM Booking b 
                    JOIN Trip t ON b.trip_id = t.trip_id 
                    JOIN Traveler tv ON b.traveler_id = tv.traveler_id 
                    WHERE t.operator_id = @OperatorId
                    ORDER BY b.booking_date DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@OperatorId", operatorId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    int rowsAffected = adapter.Fill(dt);

                    if (rowsAffected == 0)
                    {
                        // Fallback to diagnostic method
                        MessageBox.Show("No bookings found. Running diagnostic...",
                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DiagnoseBookingQuery();
                    }

                    dgvBookings.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading booking requests: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DiagnoseBookingQuery();
            }
        }

        private void UpdateBookingStatus(int bookingId, string newStatus)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        UPDATE Booking 
                        SET status = @Status 
                        WHERE booking_id = @BookingId 
                        AND EXISTS (
                            SELECT 1 FROM Trip t 
                            JOIN TourOperator op ON t.operator_id = op.operator_id
                            WHERE t.trip_id = Booking.trip_id AND op.operator_id = @OperatorId
                        )";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Status", newStatus);
                    command.Parameters.AddWithValue("@BookingId", bookingId);
                    command.Parameters.AddWithValue("@OperatorId", operatorId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Booking {bookingId} has been {newStatus.ToLower()}.",
                            "Booking Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookingRequests(); // Refresh the grid
                    }
                    else
                    {
                        MessageBox.Show("Unable to update booking. Please try again.",
                            "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating booking status: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                MessageBox.Show("This booking has already been processed.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (e.ColumnIndex == dgvBookings.Columns["Accept"].Index)
            {
                UpdateBookingStatus(bookingId, "Accepted");
            }
            else if (e.ColumnIndex == dgvBookings.Columns["Reject"].Index)
            {
                UpdateBookingStatus(bookingId, "Rejected");
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Filter bookings by status
            string selectedStatus = cboStatus.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedStatus) || selectedStatus == "All")
            {
                // Reset to show all bookings
                (dgvBookings.DataSource as DataTable).DefaultView.RowFilter = "";
                return;
            }

            // Apply filter
            (dgvBookings.DataSource as DataTable).DefaultView.RowFilter =
                $"status = '{selectedStatus}'";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Form initialization code (similar to previous implementation)
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
            this.pnlHeader.Size = new System.Drawing.Size(1000, 60);
            this.pnlHeader.TabIndex = 2;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(387, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tour Operator Booking Requests";
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
            this.pnlFilter.Size = new System.Drawing.Size(1000, 50);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(20, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(109, 20);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Filter by Status:";
            // 
            // cboStatus
            // 
            this.cboStatus.Items.AddRange(new object[] {
            "All",
            "Pending",
            "Accepted",
            "Rejected",
            "Completed"});
            this.cboStatus.Location = new System.Drawing.Point(135, 17);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(150, 24);
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
            this.pnlFooter.Location = new System.Drawing.Point(0, 650);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1000, 50);
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
            // TourOperatorBookingRequestsForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.dgvBookings);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TourOperatorBookingRequestsForm";
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

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }


}