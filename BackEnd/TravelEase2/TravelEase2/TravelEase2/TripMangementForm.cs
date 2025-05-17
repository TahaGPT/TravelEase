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
    public partial class TripManagementForm : Form
    {
        // Connection string should be stored in app.config in a real application
        private string connectionString;
        private DataTable tripsTable;

        public TripManagementForm(string conn)
        {
            connectionString = conn;
            InitializeComponent();
        }

        private void TripManagementForm_Load(object sender, EventArgs e)
        {
            LoadTrips();
            SetupDataGridView();
        }

        private void LoadTrips()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT t.trip_id AS TripID, t.title AS TripName, 
                                    t.destination AS Destination, t.price_per_person AS Price,
                                    t.start_date AS StartDate, t.end_date AS EndDate, 
                                    t.max_group_size AS MaxCapacity, t.trip_type AS TripType,
                                    CASE 
                                        WHEN t.start_date > GETDATE() THEN 'Upcoming'
                                        WHEN t.end_date < GETDATE() THEN 'Completed'
                                        ELSE 'Active'
                                    END AS Status
                                    FROM Trip t
                                    INNER JOIN TourOperator op ON t.operator_id = op.operator_id
                                    ORDER BY t.start_date DESC";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        tripsTable = new DataTable();
                        adapter.Fill(tripsTable);
                        dgvTrips.DataSource = tripsTable;
                    }
                }

                // Show record count in status label if you have one
                // lblRecordCount.Text = $"Showing {tripsTable.Rows.Count} trips";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trips: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            // Configure the DataGridView appearance and behavior
            dgvTrips.AutoGenerateColumns = false;
            dgvTrips.AllowUserToAddRows = false;
            dgvTrips.ReadOnly = true;
            dgvTrips.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTrips.MultiSelect = false;
            dgvTrips.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTrips.RowHeadersVisible = false;
            dgvTrips.BackgroundColor = Color.White;
            dgvTrips.BorderStyle = BorderStyle.None;
            dgvTrips.RowTemplate.Height = 40;

            // Clear existing columns
            dgvTrips.Columns.Clear();

            // Create and configure columns
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "TripID";
            idColumn.HeaderText = "ID";
            idColumn.Width = 50;
            dgvTrips.Columns.Add(idColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "TripName";
            nameColumn.HeaderText = "Trip Name";
            nameColumn.Width = 200;
            dgvTrips.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn destColumn = new DataGridViewTextBoxColumn();
            destColumn.DataPropertyName = "Destination";
            destColumn.HeaderText = "Destination";
            destColumn.Width = 150;
            dgvTrips.Columns.Add(destColumn);

            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.DataPropertyName = "Price";
            priceColumn.HeaderText = "Price ($)";
            priceColumn.Width = 100;
            priceColumn.DefaultCellStyle.Format = "C2";
            dgvTrips.Columns.Add(priceColumn);

            DataGridViewTextBoxColumn startDateColumn = new DataGridViewTextBoxColumn();
            startDateColumn.DataPropertyName = "StartDate";
            startDateColumn.HeaderText = "Start Date";
            startDateColumn.Width = 100;
            startDateColumn.DefaultCellStyle.Format = "d";
            dgvTrips.Columns.Add(startDateColumn);

            DataGridViewTextBoxColumn endDateColumn = new DataGridViewTextBoxColumn();
            endDateColumn.DataPropertyName = "EndDate";
            endDateColumn.HeaderText = "End Date";
            endDateColumn.Width = 100;
            endDateColumn.DefaultCellStyle.Format = "d";
            dgvTrips.Columns.Add(endDateColumn);

            DataGridViewTextBoxColumn capacityColumn = new DataGridViewTextBoxColumn();
            capacityColumn.DataPropertyName = "MaxCapacity";
            capacityColumn.HeaderText = "Capacity";
            capacityColumn.Width = 80;
            dgvTrips.Columns.Add(capacityColumn);

            DataGridViewTextBoxColumn typeColumn = new DataGridViewTextBoxColumn();
            typeColumn.DataPropertyName = "TripType";
            typeColumn.HeaderText = "Type";
            typeColumn.Width = 100;
            dgvTrips.Columns.Add(typeColumn);

            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.DataPropertyName = "Status";
            statusColumn.HeaderText = "Status";
            statusColumn.Width = 100;
            dgvTrips.Columns.Add(statusColumn);

            // Create Edit and Delete button columns
            DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
            editColumn.HeaderText = "Edit";
            editColumn.Text = "Edit";
            editColumn.UseColumnTextForButtonValue = true;
            editColumn.Width = 70;
            dgvTrips.Columns.Add(editColumn);

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.HeaderText = "Delete";
            deleteColumn.Text = "Delete";
            deleteColumn.UseColumnTextForButtonValue = true;
            deleteColumn.Width = 70;
            dgvTrips.Columns.Add(deleteColumn);
        }

        private void btnCreateTrip_Click(object sender, EventArgs e)
        {
            // Open create trip form
            CreateTripForm tripForm = new CreateTripForm(1, connectionString);
            if (tripForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh the trips list to show the newly created trip
                LoadTrips();
            }
        }

        private void dgvTrips_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if it's a valid row index and not a header click
            if (e.RowIndex < 0)
                return;

            // Get the selected trip ID - using column index instead of name to avoid errors
            int tripId = Convert.ToInt32(dgvTrips.Rows[e.RowIndex].Cells[0].Value); // First column is TripID

            // Check if Edit button is clicked (second-to-last column)
            if (e.ColumnIndex == dgvTrips.Columns.Count - 2)
            {
                // Open edit trip form
                CreateTripForm tripForm = new CreateTripForm(tripId, connectionString);
                if (tripForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the trips list to show the updated trip
                    LoadTrips();
                }
            }
            // Check if Delete button is clicked (last column)
            else if (e.ColumnIndex == dgvTrips.Columns.Count - 1)
            {
                // Confirm deletion
                if (MessageBox.Show("Are you sure you want to delete this trip?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DeleteTrip(tripId);
                    LoadTrips();
                }
            }
        }

        private void DeleteTrip(int tripId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // First check if there are any bookings for this trip
                    string checkBookingsQuery = "SELECT COUNT(*) FROM Booking WHERE trip_id = @TripId";
                    using (SqlCommand checkCmd = new SqlCommand(checkBookingsQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@TripId", tripId);
                        int bookingCount = (int)checkCmd.ExecuteScalar();

                        if (bookingCount > 0)
                        {
                            MessageBox.Show("Cannot delete this trip because it has associated bookings.",
                                "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Use a transaction for deleting related records
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Delete related records first
                            string[] deleteQueries = new string[]
                            {
                                "DELETE FROM TripLanguages WHERE trip_id = @TripId",
                                "DELETE FROM TripKeywords WHERE trip_id = @TripId",
                                "DELETE FROM TripTags_ WHERE trip_id = @TripId",
                                "DELETE FROM TripAccessibilityFeatures_ WHERE trip_id = @TripId",
                                "DELETE FROM TripItinerary WHERE trip_id = @TripId",
                                "DELETE FROM Trip_Meals WHERE trip_id = @TripId",
                                "DELETE FROM Wishlist WHERE trip_id = @TripId",
                                "DELETE FROM Review WHERE trip_id = @TripId",
                                "DELETE FROM is_assigned WHERE trip_id = @TripId",
                                "DELETE FROM Trip WHERE trip_id = @TripId"
                            };

                            foreach (string query in deleteQueries)
                            {
                                using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@TripId", tripId);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Trip deleted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error deleting trip: " + ex.Message, "Delete Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to database: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTrips();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (tripsTable == null)
                return;

            try
            {
                string searchText = txtSearch.Text.Trim().ToLower();

                if (string.IsNullOrEmpty(searchText))
                {
                    // If search box is empty, show all trips
                    dgvTrips.DataSource = tripsTable;
                }
                else
                {
                    // Filter the data
                    DataView dv = tripsTable.DefaultView;
                    dv.RowFilter = string.Format(
                        "TripName LIKE '%{0}%' OR Destination LIKE '%{0}%' OR TripType LIKE '%{0}%'",
                        searchText.Replace("'", "''"));  // Escape single quotes

                    dgvTrips.DataSource = dv.ToTable();
                }
            }
            catch (Exception ex)
            {
                // In case of invalid search pattern
                MessageBox.Show("Search error: " + ex.Message, "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}