using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class TourCategories : Form
    {
        private string connstr = @"Data Source=DESKTOP-2I8F60O\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";
        private DataGridView categoriesGridView;
        private TextBox nameTextBox;
        private Button addButton;
        private Button updateButton;

        public TourCategories()
        {
            InitializeComponent();
            CreateTourCategoriesInterface();
            LoadCategories();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TourCategories
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1195, 929);
            this.Name = "TourCategories";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tour Categories";
            this.Load += new System.EventHandler(this.TourCategories_Load);
            this.ResumeLayout(false);
        }

        private void TourCategories_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void CreateTourCategoriesInterface()
        {
            // Header label
            Label headerLabel = new Label
            {
                Text = "Tour Categories Management",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 65, 100),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Categories grid
            categoriesGridView = new DataGridView
            {
                Location = new Point(20, 70),
                Size = new Size(850, 400),
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

            // Input panel
            Panel inputPanel = new Panel
            {
                Location = new Point(20, 480),
                Size = new Size(850, 100),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Category name input
            Label nameLabel = new Label
            {
                Text = "Category Name:",
                Location = new Point(10, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };

            nameTextBox = new TextBox
            {
                Location = new Point(120, 15),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Action buttons
            addButton = new Button
            {
                Text = "Add Category",
                Size = new Size(120, 35),
                Location = new Point(550, 15),
                BackColor = Color.FromArgb(30, 120, 180),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            addButton.Click += AddButton_Click;

            updateButton = new Button
            {
                Text = "Update",
                Size = new Size(120, 35),
                Location = new Point(550, 55),
                BackColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                Visible = false // Initially hidden
            };
            updateButton.Click += UpdateButton_Click;

            Button editButton = new Button
            {
                Text = "Edit Selected",
                Size = new Size(120, 35),
                Location = new Point(680, 55),
                BackColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            editButton.Click += EditButton_Click;

            Button deleteButton = new Button
            {
                Text = "Delete Selected",
                Size = new Size(120, 35),
                Location = new Point(680, 15),
                BackColor = Color.FromArgb(180, 30, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            deleteButton.Click += DeleteButton_Click;

            Button refreshButton = new Button
            {
                Text = "Refresh List",
                Size = new Size(120, 35),
                Location = new Point(400, 55),
                BackColor = Color.FromArgb(60, 140, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            refreshButton.Click += RefreshButton_Click;

            Button cancelButton = new Button
            {
                Text = "Cancel Edit",
                Size = new Size(120, 35),
                Location = new Point(400, 15),
                BackColor = Color.FromArgb(130, 130, 130),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            cancelButton.Click += CancelButton_Click;

            // Add controls to input panel
            inputPanel.Controls.Add(nameLabel);
            inputPanel.Controls.Add(nameTextBox);
            inputPanel.Controls.Add(addButton);
            inputPanel.Controls.Add(updateButton);
            inputPanel.Controls.Add(editButton);
            inputPanel.Controls.Add(deleteButton);
            inputPanel.Controls.Add(refreshButton);
            inputPanel.Controls.Add(cancelButton);

            // Add controls to form
            this.Controls.Add(headerLabel);
            this.Controls.Add(categoriesGridView);
            this.Controls.Add(inputPanel);
        }

        private void LoadCategories()
        {
            try
            {
                string query = @"
                    SELECT 
                        tc.category_id,
                        tc.category_name,
                        COUNT(DISTINCT t.trip_id) AS TripCount,
                        ISNULL(
                            (SELECT COUNT(*) + 1 FROM 
                                (SELECT COUNT(DISTINCT t2.trip_id) AS trip_count
                                FROM TripCategory tc2
                                LEFT JOIN Trip t2 ON tc2.category_id = t2.category_id
                                GROUP BY tc2.category_id
                                HAVING COUNT(DISTINCT t2.trip_id) > COUNT(DISTINCT t.trip_id)) AS ranks
                            ), 1) AS PopularityRank
                    FROM 
                        TripCategory tc
                        LEFT JOIN Trip t ON tc.category_id = t.category_id
                    GROUP BY 
                        tc.category_id, tc.category_name
                    ORDER BY 
                        TripCount DESC";

                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    categoriesGridView.DataSource = dt;

                    // Rename columns to match UI display names
                    if (categoriesGridView.Columns.Contains("category_id"))
                        categoriesGridView.Columns["category_id"].HeaderText = "ID";
                    if (categoriesGridView.Columns.Contains("category_name"))
                        categoriesGridView.Columns["category_name"].HeaderText = "Category Name";
                    if (categoriesGridView.Columns.Contains("TripCount"))
                        categoriesGridView.Columns["TripCount"].HeaderText = "Total Trips";
                    if (categoriesGridView.Columns.Contains("PopularityRank"))
                        categoriesGridView.Columns["PopularityRank"].HeaderText = "Popularity Rank";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("Please enter a category name.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Add new category
                string query = @"
                    INSERT INTO TripCategory (category_name)
                    VALUES (@CategoryName);";

                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", nameTextBox.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Category added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear inputs
                nameTextBox.Text = "";

                // Refresh grid
                LoadCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding category: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("Please enter a category name.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (categoriesGridView.Tag != null)
                {
                    // Update existing category
                    int categoryId = Convert.ToInt32(categoriesGridView.Tag);
                    string query = @"
                        UPDATE TripCategory 
                        SET category_name = @CategoryName
                        WHERE category_id = @CategoryId";

                    using (SqlConnection conn = new SqlConnection(connstr))
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", nameTextBox.Text);
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    MessageBox.Show("Category updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Exit edit mode
                    ExitEditMode();

                    // Refresh grid
                    LoadCategories();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating category: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (categoriesGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = categoriesGridView.SelectedRows[0];

                // Fill textbox with current value
                nameTextBox.Text = row.Cells["category_name"].Value.ToString();

                // Store category ID for later update
                categoriesGridView.Tag = row.Cells["category_id"].Value;

                // Switch to edit mode UI
                addButton.Visible = false;
                updateButton.Visible = true;
            }
            else
            {
                MessageBox.Show("Please select a category to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (categoriesGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a category to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = categoriesGridView.SelectedRows[0];
            int categoryId = Convert.ToInt32(row.Cells["category_id"].Value);
            string categoryName = row.Cells["category_name"].Value.ToString();
            int tripCount = Convert.ToInt32(row.Cells["TripCount"].Value);

            if (tripCount > 0)
            {
                // We need to handle trips since NULL is not allowed
                // Ask the user to select a replacement category
                string replacementMessage = $"The category '{categoryName}' is used by {tripCount} trips and cannot be set to NULL.\n\n" +
                                           "Please select a replacement category for these trips:";

                // Create a form to select replacement category
                using (Form selectionForm = new Form())
                {
                    selectionForm.Text = "Select Replacement Category";
                    selectionForm.Size = new Size(400, 300);
                    selectionForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    selectionForm.StartPosition = FormStartPosition.CenterParent;
                    selectionForm.MaximizeBox = false;
                    selectionForm.MinimizeBox = false;

                    Label promptLabel = new Label
                    {
                        Text = replacementMessage,
                        Location = new Point(20, 20),
                        Size = new Size(360, 60),
                        AutoSize = false
                    };

                    ComboBox categoryCombo = new ComboBox
                    {
                        Location = new Point(20, 90),
                        Size = new Size(250, 25),
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };

                    Button okButton = new Button
                    {
                        Text = "OK",
                        Location = new Point(100, 200),
                        Size = new Size(80, 30),
                        DialogResult = DialogResult.OK
                    };

                    Button cancelButton = new Button
                    {
                        Text = "Cancel",
                        Location = new Point(200, 200),
                        Size = new Size(80, 30),
                        DialogResult = DialogResult.Cancel
                    };

                    // Load available categories (excluding the one being deleted)
                    try
                    {
                        string query = "SELECT category_id, category_name FROM TripCategory WHERE category_id <> @CategoryId ORDER BY category_name";

                        using (SqlConnection conn = new SqlConnection(connstr))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        int id = reader.GetInt32(0);
                                        string name = reader.GetString(1);

                                        // Create an item with both ID and name
                                        ComboBoxItem item = new ComboBoxItem(id, name);
                                        categoryCombo.Items.Add(item);
                                    }
                                }
                            }
                        }

                        // Select the first item by default if any exist
                        if (categoryCombo.Items.Count > 0)
                        {
                            categoryCombo.SelectedIndex = 0;
                        }
                        else
                        {
                            MessageBox.Show("There are no other categories available to reassign trips to.",
                                "No Alternative Categories", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading categories: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    selectionForm.Controls.Add(promptLabel);
                    selectionForm.Controls.Add(categoryCombo);
                    selectionForm.Controls.Add(okButton);
                    selectionForm.Controls.Add(cancelButton);
                    selectionForm.AcceptButton = okButton;
                    selectionForm.CancelButton = cancelButton;

                    // Show dialog and check result
                    if (selectionForm.ShowDialog() == DialogResult.OK && categoryCombo.SelectedItem != null)
                    {
                        // Get selected replacement category ID
                        ComboBoxItem selectedItem = (ComboBoxItem)categoryCombo.SelectedItem;
                        int replacementCategoryId = selectedItem.Id;
                        string replacementCategoryName = selectedItem.Name;

                        // Confirm the operation
                        DialogResult confirmResult = MessageBox.Show(
                            $"Are you sure you want to:\n\n" +
                            $"1. Move all trips from category '{categoryName}' to '{replacementCategoryName}'\n" +
                            $"2. Delete the category '{categoryName}'\n\n" +
                            "This operation cannot be undone.",
                            "Confirm Delete and Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (confirmResult == DialogResult.Yes)
                        {
                            // Execute the operation with a transaction
                            try
                            {
                                using (SqlConnection conn = new SqlConnection(connstr))
                                {
                                    conn.Open();
                                    using (SqlTransaction transaction = conn.BeginTransaction())
                                    {
                                        // Update all trips to use the replacement category
                                        string updateQuery = "UPDATE Trip SET category_id = @ReplacementId WHERE category_id = @OldId";
                                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                                        {
                                            updateCmd.Parameters.AddWithValue("@ReplacementId", replacementCategoryId);
                                            updateCmd.Parameters.AddWithValue("@OldId", categoryId);
                                            updateCmd.ExecuteNonQuery();
                                        }

                                        // Delete the category
                                        string deleteQuery = "DELETE FROM TripCategory WHERE category_id = @CategoryId";
                                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, transaction))
                                        {
                                            deleteCmd.Parameters.AddWithValue("@CategoryId", categoryId);
                                            deleteCmd.ExecuteNonQuery();
                                        }

                                        // Commit if both operations succeeded
                                        transaction.Commit();

                                        MessageBox.Show(
                                            $"Successfully moved {tripCount} trips to category '{replacementCategoryName}' and deleted category '{categoryName}'.",
                                            "Operation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        // Refresh the grid
                                        LoadCategories();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error performing operation: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                // No trips use this category, we can delete it directly
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete the category '{categoryName}'?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string deleteQuery = "DELETE FROM TripCategory WHERE category_id = @CategoryId";

                        using (SqlConnection conn = new SqlConnection(connstr))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Category deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadCategories();
                                }
                                else
                                {
                                    MessageBox.Show("Failed to delete the category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting category: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Helper class for combo box items to store both ID and display name
        private class ComboBoxItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public ComboBoxItem(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            // Exit edit mode if active
            ExitEditMode();

            // Reload data
            LoadCategories();
            MessageBox.Show("Category list refreshed.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ExitEditMode();
        }

        private void ExitEditMode()
        {
            // Clear selections and edit mode
            categoriesGridView.Tag = null;
            nameTextBox.Text = "";

            // Restore normal UI state
            addButton.Visible = true;
            updateButton.Visible = false;
        }
    }
}