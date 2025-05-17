using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class ReviewModeration : Form
    {
        private string connstr = @"Data Source=DESKTOP-2I8F60O\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";
        private DataGridView reviewsGridView;
        private TextBox reviewTextBox;
        private TextBox moderationNotesTextBox;
        ComboBox statusFilter = new ComboBox
        {
            Location = new Point(130, 68),
            Size = new Size(150, 25),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        ComboBox typeFilter = new ComboBox
        {
            Location = new Point(300, 68),
            Size = new Size(150, 25),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        ComboBox ratingFilter = new ComboBox
        {
            Location = new Point(470, 68),
            Size = new Size(150, 25),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        Button searchButton = new Button
        {
            Text = "Search",
            Location = new Point(640, 68),
            Size = new Size(100, 25),
            BackColor = Color.FromArgb(30, 120, 180),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };

        public ReviewModeration()
        {
            InitializeComponent();
            CreateReviewModerationInterface(); // Creates the controls
            reviewsGridView.SelectionChanged += ReviewsGridView_SelectionChanged; // Subscribe AFTER controls exist
            LoadReviews(); // Load after everything's ready
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ReviewModeration
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1195, 929);
            this.Name = "ReviewModeration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Review Moderation";
            this.Load += new System.EventHandler(this.ReviewModeration_Load);
            this.ResumeLayout(false);
        }

        public void CreateReviewModerationInterface()
        {
            // Header label
            Label headerLabel = new Label
            {
                Text = "Review Moderation",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 65, 100),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Filter controls
            Label filterLabel = new Label
            {
                Text = "Filter Reviews:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(20, 70)
            };

          
            statusFilter.Items.AddRange(new object[] { "All Reviews", "Under Review", "Approved", "Rejected" });
            statusFilter.SelectedIndex = 0;
            statusFilter.SelectedIndexChanged += StatusFilter_SelectedIndexChanged;

           
            typeFilter.Items.AddRange(new object[] { "All Types", "Trip Reviews", "Hotel Reviews", "Guide Reviews" });
            typeFilter.SelectedIndex = 0;
            typeFilter.SelectedIndexChanged += TypeFilter_SelectedIndexChanged;

           
            ratingFilter.Items.AddRange(new object[] { "All Ratings", "5 Stars", "4 Stars", "3 Stars", "2 Stars", "1 Star" });
            ratingFilter.SelectedIndex = 0;
            ratingFilter.SelectedIndexChanged += RatingFilter_SelectedIndexChanged;

           
            searchButton.Click += SearchButton_Click;

            Button resetButton = new Button
            {
                Text = "Reset",
                Location = new Point(750, 68),
                Size = new Size(100, 25),
                BackColor = Color.FromArgb(150, 150, 150),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            resetButton.Click += ResetButton_Click;

            // Reviews grid
            reviewsGridView = new DataGridView
            {
                Location = new Point(20, 110),
                Size = new Size(950, 400),
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

            // Review details panel
            Panel detailsPanel = new Panel
            {
                Location = new Point(20, 520),
                Size = new Size(950, 200),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label reviewTextLabel = new Label
            {
                Text = "Review Text:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(10, 10)
            };

            reviewTextBox = new TextBox
            {
                Location = new Point(10, 30),
                Size = new Size(600, 60),
                Font = new Font("Segoe UI", 9),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };

            Label moderationNotesLabel = new Label
            {
                Text = "Moderation Notes:",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(10, 100)
            };

            moderationNotesTextBox = new TextBox
            {
                Location = new Point(10, 120),
                Size = new Size(600, 60),
                Font = new Font("Segoe UI", 9),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // Action buttons
            Button approveButton = new Button
            {
                Text = "Approve",
                Size = new Size(120, 35),
                Location = new Point(650, 30),
                BackColor = Color.FromArgb(60, 140, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            approveButton.Click += ApproveButton_Click;

            Button rejectButton = new Button
            {
                Text = "Reject",
                Size = new Size(120, 35),
                Location = new Point(790, 30),
                BackColor = Color.FromArgb(180, 30, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            rejectButton.Click += RejectButton_Click;

            Button refreshButton = new Button
            {
                Text = "Refresh List",
                Size = new Size(120, 35),
                Location = new Point(720, 120),
                BackColor = Color.FromArgb(60, 140, 180),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            refreshButton.Click += RefreshButton_Click;

            // Add controls to details panel
            detailsPanel.Controls.Add(reviewTextLabel);
            detailsPanel.Controls.Add(reviewTextBox);
            detailsPanel.Controls.Add(moderationNotesLabel);
            detailsPanel.Controls.Add(moderationNotesTextBox);
            detailsPanel.Controls.Add(approveButton);
            detailsPanel.Controls.Add(rejectButton);
            detailsPanel.Controls.Add(refreshButton);

            // Add event handler for grid selection
            reviewsGridView.SelectionChanged += ReviewsGridView_SelectionChanged;

            // Add controls to form
            this.Controls.Add(headerLabel);
            this.Controls.Add(filterLabel);
            this.Controls.Add(statusFilter);
            this.Controls.Add(typeFilter);
            this.Controls.Add(ratingFilter);
            this.Controls.Add(searchButton);
            this.Controls.Add(resetButton);
            this.Controls.Add(reviewsGridView);
            this.Controls.Add(detailsPanel);
        }

        private void LoadReviews()
        {
            try
            {
                // Clear existing rows
                reviewsGridView.Columns.Clear();
                reviewsGridView.DataSource = null;

               
               
                // SQL query updated based on the actual schema
                string query = @"
    SELECT 
    r.review_id,
    CASE 
        WHEN h.hotel_id IS NOT NULL THEN 'Hotel'
        WHEN g.guide_id IS NOT NULL THEN 'Guide'
        ELSE 'Trip'
    END AS review_type,
    tr.name AS reviewer_name,
    r.rating AS user_rating,
    r.review_date,
    r.moderation_status,
    r.comment,
    COALESCE(t.title, h.address, g.specialization) AS item_name
FROM Review r
JOIN Traveler tr ON r.traveler_id = tr.traveler_id
LEFT JOIN Trip t ON r.trip_id = t.trip_id
LEFT JOIN is_assigned ia ON ia.trip_id = t.trip_id
LEFT JOIN ServiceAssignment sa ON sa.assignment_id = ia.assignment_id
LEFT JOIN ProviderAssignment pa ON pa.assignment_id = sa.assignment_id
LEFT JOIN ServiceProvider sp ON sp.provider_id = pa.provider_id
LEFT JOIN Hotel h ON h.provider_id = sp.provider_id
LEFT JOIN Guide_ g ON g.provider_id = sp.provider_id";
                try
                {
                    using (SqlConnection con = new SqlConnection(connstr))
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        reviewsGridView.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading reviews: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reviews: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }




        private void ReviewsGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (reviewsGridView == null || reviewTextBox == null || moderationNotesTextBox == null)
                return;

            if (reviewsGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = reviewsGridView.SelectedRows[0];

                reviewTextBox.Text = row.Tag != null
                    ? row.Tag.ToString()
                    : row.Cells["comment"].Value?.ToString();

                if (int.TryParse(row.Cells["review_id"].Value?.ToString(), out int reviewId))
                {
                    LoadModerationNotes(reviewId);
                }
            }
        }


        private void LoadModerationNotes(int reviewId)
        {
            try
            {
                string query = "SELECT moderation_notes FROM Review WHERE review_id = @ReviewId";

                using (SqlConnection conn = new SqlConnection(connstr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ReviewId", reviewId);

                    object result = cmd.ExecuteScalar();
                    moderationNotesTextBox.Text = result != DBNull.Value ? result.ToString() : "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading moderation notes: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApproveButton_Click(object sender, EventArgs e)
        {
            if (reviewsGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a review to approve.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = reviewsGridView.SelectedRows[0];
            int reviewId = Convert.ToInt32(row.Cells["review_id"].Value);
            string notes = moderationNotesTextBox.Text.Trim();

            // Confirm action
            DialogResult result = MessageBox.Show(
                "Are you sure you want to approve this review?",
                "Confirm Approval",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (UpdateReviewStatus(reviewId, "Approved", notes))
                {
                    row.Cells["moderation_status"].Value = "Approved";
                    MessageBox.Show("Review approved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void RejectButton_Click(object sender, EventArgs e)
        {
            if (reviewsGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a review to reject.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Require moderation notes for rejection
            if (string.IsNullOrWhiteSpace(moderationNotesTextBox.Text))
            {
                MessageBox.Show("Please provide moderation notes explaining the rejection reason.",
                    "Notes Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = reviewsGridView.SelectedRows[0];
            int reviewId = Convert.ToInt32(row.Cells["review_id"].Value);
            string notes = moderationNotesTextBox.Text.Trim();

            // Confirm action
            DialogResult result = MessageBox.Show(
                "Are you sure you want to reject this review?",
                "Confirm Rejection",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (UpdateReviewStatus(reviewId, "Rejected", notes))
                {
                    row.Cells["moderation_status"].Value = "Rejected";
                    MessageBox.Show("Review rejected successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool UpdateReviewStatus(int reviewId, string status, string notes)
        {
            try
            {
                string query = @"
                    UPDATE Review 
                    SET moderation_status = @Status,
                        moderation_date = @Date,
                        moderation_notes = @Notes
                    WHERE review_id = @ReviewId";

                using (SqlConnection conn = new SqlConnection(connstr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? DBNull.Value : (object)notes);
                    cmd.Parameters.AddWithValue("@ReviewId", reviewId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating review status: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadReviews();
            ClearDetailFields();
            MessageBox.Show("Review list refreshed.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearDetailFields()
        {
            reviewTextBox.Text = "";
            moderationNotesTextBox.Text = "";
        }

        private void StatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void TypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void RatingFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            // Reset all filters to their default state
            foreach (Control control in this.Controls)
            {
                if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = 0;
                }
            }

            // Reload all reviews
            LoadReviews();
            ClearDetailFields();
            MessageBox.Show("Filters reset.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ApplyFilters()
        {
            try
            {
                // Clear existing rows
                reviewsGridView.Columns.Clear();
                reviewsGridView.DataSource = null;

                // Get filter values
                string selectedStatus = statusFilter.SelectedItem?.ToString();
                string selectedType = typeFilter.SelectedItem?.ToString();
                string selectedRating = ratingFilter.SelectedItem?.ToString();

                // Base query (same structure as LoadReviews)
                string query = @"
                SELECT 
                    r.review_id,
                    CASE 
                        WHEN h.hotel_id IS NOT NULL THEN 'Hotel'
                        WHEN g.guide_id IS NOT NULL THEN 'Guide'
                        ELSE 'Trip'
                    END AS review_type,
                    tr.name AS reviewer_name,
                    r.rating AS user_rating,
                    r.review_date,
                    r.moderation_status,
                    r.comment,
                    COALESCE(t.title, h.address, g.specialization) AS item_name
                FROM Review r
                JOIN Traveler tr ON r.traveler_id = tr.traveler_id
                LEFT JOIN Trip t ON r.trip_id = t.trip_id
                LEFT JOIN is_assigned ia ON ia.trip_id = t.trip_id
                LEFT JOIN ServiceAssignment sa ON sa.assignment_id = ia.assignment_id
                LEFT JOIN ProviderAssignment pa ON pa.assignment_id = sa.assignment_id
                LEFT JOIN ServiceProvider sp ON sp.provider_id = pa.provider_id
                LEFT JOIN Hotel h ON h.provider_id = sp.provider_id
                LEFT JOIN Guide_ g ON g.provider_id = sp.provider_id
                WHERE 1 = 1
                ";

                // Append filters
                if (selectedStatus != "All Reviews")
                {
                    if (selectedStatus == "Pending")
                        query += " AND (r.moderation_status IS NULL OR r.moderation_status = 'Pending')";
                    else
                        query += " AND r.moderation_status = @Status";
                }

                if (selectedType != "All Types")
                {
                    string typeValue = selectedType.Replace(" Reviews", "");

                    if (typeValue == "Hotel")
                        query += " AND h.hotel_id IS NOT NULL";
                    else if (typeValue == "Guide")
                        query += " AND g.guide_id IS NOT NULL";
                    else if (typeValue == "Trip")
                        query += " AND h.hotel_id IS NULL AND g.guide_id IS NULL";
                }


                if (selectedRating != "All Ratings")
                {
                    query += " AND r.rating = @Rating";
                }

                query += " ORDER BY r.review_date DESC";

                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (selectedStatus != "All Reviews" && selectedStatus != "Under Review")
                        cmd.Parameters.AddWithValue("@Status", selectedStatus);

                    if (selectedRating != "All Ratings")
                    {
                        // Remove any text like " Stars" or " Star" before parsing
                        string cleanRating = selectedRating.Replace(" Stars", "").Replace(" Star", "").Trim();
                        cmd.Parameters.AddWithValue("@Rating", int.Parse(cleanRating));
                    }


                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    reviewsGridView.DataSource = dt;
                }

               ClearDetailFields();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filters: {ex.Message}", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ReviewModeration_Load(object sender, EventArgs e)
        {
            LoadReviews();
            statusFilter.SelectedIndexChanged += StatusFilter_SelectedIndexChanged;
            typeFilter.SelectedIndexChanged += TypeFilter_SelectedIndexChanged;
            ratingFilter.SelectedIndexChanged += RatingFilter_SelectedIndexChanged;

        }


    }
}