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
    public partial class AddServiceForm : Form
    {
        private string connectionString = @"Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";
        private int _providerId;

        public AddServiceForm(int providerId, string connString)
        {
            InitializeComponent();
            _providerId = providerId;
            connectionString = connString;
        }

        private void AddServiceForm_Load(object sender, EventArgs e)
        {
            // Initialize combobox with service types
            cboServiceType.Items.Add("Hotel");
            cboServiceType.Items.Add("Transport");
            cboServiceType.Items.Add("Guide");
            cboServiceType.SelectedIndex = 0;

            // Show/hide appropriate fields based on initial service type
            ShowHideFields(cboServiceType.SelectedItem.ToString());
        }

        private void cboServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show/hide fields based on service type
            ShowHideFields(cboServiceType.SelectedItem.ToString());
        }

        private void ShowHideFields(string serviceType)
        {
            // Hide all service-specific panels first
            pnlHotel.Visible = false;
            pnlTransport.Visible = false;
            pnlGuide.Visible = false;

            // Show the panel for the selected service type
            switch (serviceType)
            {
                case "Hotel":
                    pnlHotel.Visible = true;
                    break;
                case "Transport":
                    pnlTransport.Visible = true;
                    break;
                case "Guide":
                    pnlGuide.Visible = true;
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtServiceName.Text))
                {
                    MessageBox.Show("Please enter a service name.", "Validation Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // First add/update the ServiceProvider record
                            int providerId = EnsureServiceProvider(connection, transaction);

                            // Then add the specific service type
                            string serviceType = cboServiceType.SelectedItem.ToString();

                            switch (serviceType)
                            {
                                case "Hotel":
                                    AddHotel(providerId, connection, transaction);
                                    break;
                                case "Transport":
                                    AddTransport(providerId, connection, transaction);
                                    break;
                                case "Guide":
                                    AddGuide(providerId, connection, transaction);
                                    break;
                            }

                            transaction.Commit();
                            MessageBox.Show("Service added successfully!", "Success",
                                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error saving service: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int EnsureServiceProvider(SqlConnection connection, SqlTransaction transaction)
        {
            // Check if service provider exists or create a new one
            string checkQuery = "SELECT COUNT(*) FROM ServiceProvider WHERE provider_id = @ProviderId";

            using (SqlCommand command = new SqlCommand(checkQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@ProviderId", _providerId);
                int count = (int)command.ExecuteScalar();

                if (count == 0)
                {
                    // Create new service provider
                    string insertQuery = @"
                    INSERT INTO ServiceProvider (provider_id, name, service_type, is_available, phone, is_approved, registration_date)
                    VALUES (@ProviderId, @Name, @ServiceType, @IsAvailable, @Phone, @IsApproved, @RegistrationDate);";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction))
                    {
                        insertCmd.Parameters.AddWithValue("@ProviderId", _providerId);
                        insertCmd.Parameters.AddWithValue("@Name", txtServiceName.Text);
                        insertCmd.Parameters.AddWithValue("@ServiceType", cboServiceType.SelectedItem.ToString());
                        insertCmd.Parameters.AddWithValue("@IsAvailable", chkAvailable.Checked);
                        insertCmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        insertCmd.Parameters.AddWithValue("@IsApproved", 1); // Auto-approve for this example
                        insertCmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
                        insertCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Update existing service provider
                    string updateQuery = @"
                    UPDATE ServiceProvider 
                    SET name = @Name, service_type = @ServiceType, is_available = @IsAvailable, phone = @Phone
                    WHERE provider_id = @ProviderId";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection, transaction))
                    {
                        updateCmd.Parameters.AddWithValue("@ProviderId", _providerId);
                        updateCmd.Parameters.AddWithValue("@Name", txtServiceName.Text);
                        updateCmd.Parameters.AddWithValue("@ServiceType", cboServiceType.SelectedItem.ToString());
                        updateCmd.Parameters.AddWithValue("@IsAvailable", chkAvailable.Checked);
                        updateCmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        updateCmd.ExecuteNonQuery();
                    }
                }

                return _providerId;
            }
        }

        private void AddHotel(int providerId, SqlConnection connection, SqlTransaction transaction)
        {
            // Validate hotel inputs
            if (string.IsNullOrWhiteSpace(txtAddress.Text) ||
                !int.TryParse(txtTotalRooms.Text, out int totalRooms) ||
                !int.TryParse(txtStarRating.Text, out int starRating))
            {
                throw new ArgumentException("Please fill in all required hotel fields with valid values.");
            }

            // Get the next available hotel ID
            string getMaxIdQuery = "SELECT ISNULL(MAX(hotel_id), 0) + 1 FROM Hotel";
            int hotelId;

            using (SqlCommand cmd = new SqlCommand(getMaxIdQuery, connection, transaction))
            {
                hotelId = (int)cmd.ExecuteScalar();
            }

            // Insert hotel record
            string insertQuery = @"
            INSERT INTO Hotel (hotel_id, total_rooms, star_rating, address, wheelchair_accessible, provider_id)
            VALUES (@HotelId, @TotalRooms, @StarRating, @Address, @WheelchairAccessible, @ProviderId)";

            using (SqlCommand cmd = new SqlCommand(insertQuery, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@HotelId", hotelId);
                cmd.Parameters.AddWithValue("@TotalRooms", totalRooms);
                cmd.Parameters.AddWithValue("@StarRating", starRating);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@WheelchairAccessible", chkWheelchairAccessible.Checked);
                cmd.Parameters.AddWithValue("@ProviderId", providerId);
                cmd.ExecuteNonQuery();
            }
        }

        private void AddTransport(int providerId, SqlConnection connection, SqlTransaction transaction)
        {
            // Validate transport inputs
            if (string.IsNullOrWhiteSpace(cboTransportType.Text) ||
                !int.TryParse(txtFleetSize.Text, out int fleetSize) ||
                !int.TryParse(txtCapacity.Text, out int capacity))
            {
                throw new ArgumentException("Please fill in all required transport fields with valid values.");
            }

            // Get the next available transport ID
            string getMaxIdQuery = "SELECT ISNULL(MAX(transport_id), 0) + 1 FROM TransportProvider";
            int transportId;

            using (SqlCommand cmd = new SqlCommand(getMaxIdQuery, connection, transaction))
            {
                transportId = (int)cmd.ExecuteScalar();
            }

            // Insert transport record
            string insertQuery = @"
            INSERT INTO TransportProvider (transport_id, transport_type, fleet_size, license_details, capacity_per_vehicle, provider_id)
            VALUES (@TransportId, @TransportType, @FleetSize, @LicenseDetails, @Capacity, @ProviderId)";

            using (SqlCommand cmd = new SqlCommand(insertQuery, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@TransportId", transportId);
                cmd.Parameters.AddWithValue("@TransportType", cboTransportType.Text);
                cmd.Parameters.AddWithValue("@FleetSize", fleetSize);
                cmd.Parameters.AddWithValue("@LicenseDetails", txtLicenseDetails.Text);
                cmd.Parameters.AddWithValue("@Capacity", capacity);
                cmd.Parameters.AddWithValue("@ProviderId", providerId);
                cmd.ExecuteNonQuery();
            }
        }

        private void AddGuide(int providerId, SqlConnection connection, SqlTransaction transaction)
        {
            // Validate guide inputs
            if (string.IsNullOrWhiteSpace(txtSpecialization.Text) ||
                !int.TryParse(txtExperienceYears.Text, out int experienceYears))
            {
                throw new ArgumentException("Please fill in all required guide fields with valid values.");
            }

            // Get the next available guide ID
            string getMaxIdQuery = "SELECT ISNULL(MAX(guide_id), 0) + 1 FROM Guide_";
            int guideId;

            using (SqlCommand cmd = new SqlCommand(getMaxIdQuery, connection, transaction))
            {
                guideId = (int)cmd.ExecuteScalar();
            }

            // Insert guide record
            string insertQuery = @"
            INSERT INTO Guide_ (guide_id, specialization, languages_spoken, experience_years, provider_id)
            VALUES (@GuideId, @Specialization, @Languages, @ExperienceYears, @ProviderId)";

            using (SqlCommand cmd = new SqlCommand(insertQuery, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@GuideId", guideId);
                cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text);
                cmd.Parameters.AddWithValue("@Languages", txtLanguages.Text);
                cmd.Parameters.AddWithValue("@ExperienceYears", experienceYears);
                cmd.Parameters.AddWithValue("@ProviderId", providerId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
