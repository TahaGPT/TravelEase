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
    public partial class MyServicesForm : Form
    {
        private string connectionString = @"Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";
        private int _providerId;

        public MyServicesForm(int providerId, string connString)
        {
            InitializeComponent();
            _providerId = providerId;
            connectionString = connString;
        }

        private void MyServicesForm_Load(object sender, EventArgs e)
        {
            LoadProviderServices();
        }

        private void LoadProviderServices()
        {
            try
            {
                // Clear existing data
                dgvServices.Rows.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT 
                        'Hotel' AS ServiceType, 
                        h.hotel_id AS ServiceId, 
                        sp.name AS ServiceName, 
                        CAST(h.star_rating AS VARCHAR(10)) AS Details, 
                        sp.is_available
                    FROM Hotel h
                    INNER JOIN ServiceProvider sp ON h.provider_id = sp.provider_id
                    WHERE h.provider_id = @ProviderId

                    UNION

                    SELECT 
                        'Transport' AS ServiceType, 
                        t.transport_id AS ServiceId, 
                        sp.name AS ServiceName, 
                        t.transport_type AS Details, 
                        sp.is_available
                    FROM TransportProvider t
                    INNER JOIN ServiceProvider sp ON t.provider_id = sp.provider_id
                    WHERE t.provider_id = @ProviderId

                    UNION

                    SELECT 
                        'Guide' AS ServiceType, 
                        g.guide_id AS ServiceId, 
                        sp.name AS ServiceName, 
                        g.specialization AS Details, 
                        sp.is_available
                    FROM Guide_ g
                    INNER JOIN ServiceProvider sp ON g.provider_id = sp.provider_id
                    WHERE g.provider_id = @ProviderId

                    ORDER BY ServiceType, ServiceName;
                    ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProviderId", _providerId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int rowIndex = dgvServices.Rows.Add();
                                DataGridViewRow row = dgvServices.Rows[rowIndex];

                                row.Cells["ServiceType"].Value = reader["ServiceType"].ToString();
                                row.Cells["ServiceName"].Value = reader["ServiceName"].ToString();
                                row.Cells["Details"].Value = reader["Details"].ToString();

                                // Convert bit to boolean
                                bool isAvailable = Convert.ToBoolean(reader["is_available"]);
                                row.Cells["Status"].Value = isAvailable ? "Available" : "Unavailable";

                                // Set the service ID as a tag to use when editing
                                row.Tag = reader["ServiceId"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading services: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            // Open the AddServiceForm
            using (AddServiceForm addServiceForm = new AddServiceForm(_providerId, connectionString))
            {
                if (addServiceForm.ShowDialog() == DialogResult.OK)
                {
                    // Reload services after adding a new one
                    LoadProviderServices();
                }
            }
        }

        private void dgvServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if clicked on Edit or Delete button
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvServices.Rows[e.RowIndex];
                string serviceType = row.Cells["ServiceType"].Value.ToString();
                int serviceId = (int)row.Tag;

                if (e.ColumnIndex == dgvServices.Columns["Edit"].Index)
                {
                    // Open edit form based on service type
                    EditService(serviceType, serviceId);
                }
                else if (e.ColumnIndex == dgvServices.Columns["Delete"].Index)
                {
                    // Confirm and delete the service
                    if (MessageBox.Show("Are you sure you want to delete this service?",
                                       "Confirm Delete", MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteService(serviceType, serviceId);
                    }
                }
                else if (e.ColumnIndex == dgvServices.Columns["ToggleStatus"].Index)
                {
                    // Toggle service availability
                    ToggleServiceStatus(serviceType, serviceId);
                }
            }
        }

        private void EditService(string serviceType, int serviceId)
        {
            // Implementation details would depend on your specific UI
            MessageBox.Show($"Edit {serviceType} service with ID {serviceId}", "Edit Service",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);

            // After editing, reload the services
            LoadProviderServices();
        }

        private void DeleteService(string serviceType, int serviceId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string tableName;
                    switch (serviceType)
                    {
                        case "Hotel":
                            tableName = "Hotel";
                            break;
                        case "Transport":
                            tableName = "TransportProvider";
                            break;
                        case "Guide":
                            tableName = "Guide_";
                            break;
                        default:
                            throw new ArgumentException("Invalid service type");
                    }

                    string idColumn;
                    switch (serviceType)
                    {
                        case "Hotel":
                            idColumn = "hotel_id";
                            break;
                        case "Transport":
                            idColumn = "transport_id";
                            break;
                        case "Guide":
                            idColumn = "guide_id";
                            break;
                        default:
                            throw new ArgumentException("Invalid service type");
                    }

                    string query = $"DELETE FROM {tableName} WHERE {idColumn} = @ServiceId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ServiceId", serviceId);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Service deleted successfully.", "Success",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProviderServices();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting service: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToggleServiceStatus(string serviceType, int serviceId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // First get the provider_id and current status
                    string getProviderIdQuery = string.Empty;

                    switch (serviceType)
                    {
                        case "Hotel":
                            getProviderIdQuery = "SELECT provider_id FROM Hotel WHERE hotel_id = @ServiceId";
                            break;
                        case "Transport":
                            getProviderIdQuery = "SELECT provider_id FROM TransportProvider WHERE transport_id = @ServiceId";
                            break;
                        case "Guide":
                            getProviderIdQuery = "SELECT provider_id FROM Guide_ WHERE guide_id = @ServiceId";
                            break;
                    }

                    int providerId = 0;

                    using (SqlCommand command = new SqlCommand(getProviderIdQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ServiceId", serviceId);
                        providerId = (int)command.ExecuteScalar();
                    }

                    // Get current availability
                    string getStatusQuery = "SELECT is_available FROM ServiceProvider WHERE provider_id = @ProviderId";
                    bool currentStatus;

                    using (SqlCommand command = new SqlCommand(getStatusQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProviderId", providerId);
                        currentStatus = Convert.ToBoolean(command.ExecuteScalar());
                    }

                    // Toggle status
                    string toggleQuery = "UPDATE ServiceProvider SET is_available = @NewStatus WHERE provider_id = @ProviderId";

                    using (SqlCommand command = new SqlCommand(toggleQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NewStatus", !currentStatus);
                        command.Parameters.AddWithValue("@ProviderId", providerId);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Service status updated successfully.", "Success",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProviderServices();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating service status: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
