using System;
using System.Collections.Generic;
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
    public partial class CreateTripForm : Form
    {
        private int operatorId; // Store the current operator's ID
        private SqlConnection connection;
        private string connectionString;

        public CreateTripForm(int operatorId, string conn)
        {
            InitializeComponent();
            this.operatorId = operatorId;
            connectionString = conn;
            this.connection = new SqlConnection(connectionString);
            LoadTripCategories();
            InitializeTripTypes();
        }

        private void LoadTripCategories()
        {
            try
            {
                connection.Open();
                string query = "SELECT category_id, category_name FROM TripCategory";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable categoriesTable = new DataTable();
                adapter.Fill(categoriesTable);

                comboBoxCategory.DisplayMember = "category_name";
                comboBoxCategory.ValueMember = "category_id";
                comboBoxCategory.DataSource = categoriesTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip categories: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void InitializeTripTypes()
        {
            // Based on database schema, trip_type is a VARCHAR field
            comboBoxTripType.Items.Add("Adventure");
            comboBoxTripType.Items.Add("Cultural");
            comboBoxTripType.Items.Add("Beach");
            comboBoxTripType.Items.Add("Urban");
            comboBoxTripType.Items.Add("Wildlife");
            comboBoxTripType.Items.Add("Cruise");
            comboBoxTripType.Items.Add("Educational");
            comboBoxTripType.SelectedIndex = 0; // Default selection
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtTripName.Text) ||
                string.IsNullOrWhiteSpace(txtDestination.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtDuration.Text) ||
                string.IsNullOrWhiteSpace(txtCapacity.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                comboBoxCategory.SelectedIndex == -1 ||
                comboBoxTripType.SelectedIndex == -1 ||
                dateTimePickerStart.Value == null)
            {
                MessageBox.Show("Please fill all required fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate numeric values
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtDuration.Text, out int duration) || duration <= 0)
            {
                MessageBox.Show("Please enter a valid duration in days", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity <= 0)
            {
                MessageBox.Show("Please enter a valid capacity", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                connection.Open();

                // Generate a new trip_id manually
                int newTripId;
                using (SqlCommand getMaxIdCmd = new SqlCommand("SELECT ISNULL(MAX(trip_id), 0) + 1 FROM Trip", connection))
                {
                    newTripId = Convert.ToInt32(getMaxIdCmd.ExecuteScalar());
                }

                // Calculate end date based on start date and duration
                DateTime startDate = dateTimePickerStart.Value;
                DateTime endDate = startDate.AddDays(duration);

                // Additional fields based on the database schema
                int minGroupSize = 1; // Default values, could be additional form fields
                int maxGroupSize = capacity;
                int sustainabilityScore = 1; // Default values, could be additional form fields
                int viewsCount = 0;
                int conversionRate = 0;
                int minBudget = (int)(price * 0.9m);
                int maxBudget = (int)(price * 1.1m);

                int physicalIntensityLevel = 2; // Default values, could be additional form fields

                // Create SQL command to insert trip data based on the database schema
                SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Trip (
            trip_id,
            title, 
            description, 
            destination, 
            price_per_person, 
            capacity, 
            start_date, 
            end_date, 
            sustainability_score, 
            trip_type, 
            duration, 
            min_group_size, 
            max_group_size, 
            views_count, 
            conversion_rate, 
            min_budget, 
            max_budget, 
            physical_intensity_level, 
            category_id, 
            operator_id
        ) 
        VALUES (
            @TripId,
            @Title, 
            @Description, 
            @Destination, 
            @Price, 
            @Capacity, 
            @StartDate, 
            @EndDate, 
            @SustainabilityScore, 
            @TripType, 
            @Duration, 
            @MinGroupSize, 
            @MaxGroupSize, 
            @ViewsCount, 
            @ConversionRate, 
            @MinBudget, 
            @MaxBudget, 
            @PhysicalIntensityLevel, 
            @CategoryID, 
            @OperatorID
        );", connection);

                // Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@TripId", newTripId);
                cmd.Parameters.AddWithValue("@Title", txtTripName.Text);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@Destination", txtDestination.Text);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Capacity", capacity);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                cmd.Parameters.AddWithValue("@SustainabilityScore", sustainabilityScore);
                cmd.Parameters.AddWithValue("@TripType", comboBoxTripType.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Duration", duration);
                cmd.Parameters.AddWithValue("@MinGroupSize", minGroupSize);
                cmd.Parameters.AddWithValue("@MaxGroupSize", maxGroupSize);
                cmd.Parameters.AddWithValue("@ViewsCount", viewsCount);
                cmd.Parameters.AddWithValue("@ConversionRate", conversionRate);
                cmd.Parameters.AddWithValue("@MinBudget", minBudget);
                cmd.Parameters.AddWithValue("@MaxBudget", maxBudget);
                cmd.Parameters.AddWithValue("@PhysicalIntensityLevel", physicalIntensityLevel);
                cmd.Parameters.AddWithValue("@CategoryID", comboBoxCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@OperatorID", operatorId);

                // Execute the command
                cmd.ExecuteNonQuery();

                MessageBox.Show("Trip created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating trip: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }


        private void AddTripInclusions(int tripId, string inclusionsText)
        {
            // This is an example of how you might parse and add inclusions
            // Split the inclusions text by commas or new lines
            string[] inclusions = inclusionsText.Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string inclusion in inclusions)
            {
                string trimmedInclusion = inclusion.Trim();
                if (!string.IsNullOrEmpty(trimmedInclusion))
                {
                    // Determine the type of inclusion and add to appropriate table
                    // For example, if it seems like a meal:
                    if (trimmedInclusion.ToLower().Contains("meal") ||
                        trimmedInclusion.ToLower().Contains("breakfast") ||
                        trimmedInclusion.ToLower().Contains("lunch") ||
                        trimmedInclusion.ToLower().Contains("dinner"))
                    {
                        AddTripMeal(tripId, trimmedInclusion);
                    }
                    // Add more types as needed or just add as a generic tag
                    else
                    {
                        AddTripTag(tripId, trimmedInclusion);
                    }
                }
            }
        }

        private void AddTripMeal(int tripId, string mealDescription)
        {
            try
            {
                // Determine the meal type
                string mealType = "Other";
                if (mealDescription.ToLower().Contains("breakfast"))
                    mealType = "Breakfast";
                else if (mealDescription.ToLower().Contains("lunch"))
                    mealType = "Lunch";
                else if (mealDescription.ToLower().Contains("dinner"))
                    mealType = "Dinner";

                string query = @"
                INSERT INTO Trip_Meals (
                    meal_type, 
                    meal_description, 
                    dietary_options, 
                    included_in_price, 
                    venue, 
                    trip_id
                ) VALUES (
                    @MealType, 
                    @MealDescription, 
                    @DietaryOptions, 
                    @IncludedInPrice, 
                    @Venue, 
                    @TripId
                )";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MealType", mealType);
                cmd.Parameters.AddWithValue("@MealDescription", mealDescription);
                cmd.Parameters.AddWithValue("@DietaryOptions", "Standard"); // Default value
                cmd.Parameters.AddWithValue("@IncludedInPrice", 1); // Assuming 1 means true
                cmd.Parameters.AddWithValue("@Venue", "At destination"); // Default value
                cmd.Parameters.AddWithValue("@TripId", tripId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log the error but don't stop the process
                Console.WriteLine("Error adding meal: " + ex.Message);
            }
        }

        private void AddTripTag(int tripId, string tagName)
        {
            try
            {
                string query = @"
                INSERT INTO TripTags_ (
                    tag_name, 
                    trip_id
                ) VALUES (
                    @TagName, 
                    @TripId
                )";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@TagName", tagName);
                cmd.Parameters.AddWithValue("@TripId", tripId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log the error but don't stop the process
                Console.WriteLine("Error adding tag: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); // Go back to previous form
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            // You can add logic here if needed, for example:
            // Automatically update an end date picker based on the start date and duration
            if (!string.IsNullOrWhiteSpace(txtDuration.Text) && int.TryParse(txtDuration.Text, out int duration))
            {
                // If you have an end date display, you could update it here
                // For example: labelEndDate.Text = dateTimePickerStart.Value.AddDays(duration).ToShortDateString();
            }
        }

        // If you want to have the end date automatically update when duration changes
        private void txtDuration_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDuration.Text) && int.TryParse(txtDuration.Text, out int duration))
            {
                // If you have an end date display, you could update it here
                // For example: labelEndDate.Text = dateTimePickerStart.Value.AddDays(duration).ToShortDateString();
            }
        }

        // Add any additional helper methods as needed
    }
}