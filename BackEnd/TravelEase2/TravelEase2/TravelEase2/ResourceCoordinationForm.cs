using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace TravelEase
{
    public partial class ResourceCoordinationForm : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

        // Form controls
        private ComboBox cbTrip;
        private ComboBox cbTraveler;
        private ComboBox cbHotel;
        private ComboBox cbRoomType;
        private ComboBox cbGuide;
        private ComboBox cbTransport;
        private ComboBox cbMealPlan;
        private Button btnAssign;
        private Button btnClear;
        private Button btnBack;
        private TableLayoutPanel mainPanel;

        public ResourceCoordinationForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form properties
            this.Text = "Resource Coordination";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Font = new Font("Segoe UI", 9F);

            // Create main layout
            mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                ColumnCount = 2,
                RowCount = 9,
                AutoSize = true
            };

            // Set column widths
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            // Create controls
            CreateControls();

            // Add controls to panel
            AddControlsToPanel();

            // Add panel to form
            this.Controls.Add(mainPanel);

            // Wire up events
            btnAssign.Click += BtnAssign_Click;
            btnClear.Click += BtnClear_Click;
            btnBack.Click += (s, e) => this.Close();

            // Load data from database
            LoadAllData();
        }

        private void CreateControls()
        {
            // Create labels
            Label lblTitle = new Label
            {
                Text = "Resource Assignment",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            Label lblTrip = new Label { Text = "Trip:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight };
            Label lblTraveler = new Label { Text = "Traveler:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight };
            Label lblHotel = new Label { Text = "Hotel:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight };
            Label lblRoomType = new Label { Text = "Room Type:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight };
            Label lblGuide = new Label { Text = "Guide:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight };
            Label lblTransport = new Label { Text = "Transport:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight };
            Label lblMealPlan = new Label { Text = "Meal Plan:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight };

            // Create ComboBoxes
            cbTrip = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbTraveler = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbHotel = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbRoomType = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbGuide = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbTransport = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbMealPlan = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };

            // Event handlers for the hotel combo box to load room types
            cbHotel.SelectedIndexChanged += CbHotel_SelectedIndexChanged;

            // Create button panel
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoSize = true
            };

            // Create buttons
            btnAssign = new Button
            {
                Text = "Assign Resources",
                Width = 150,
                Height = 30,
                Margin = new Padding(5),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White
            };

            btnClear = new Button
            {
                Text = "Clear",
                Width = 100,
                Height = 30,
                Margin = new Padding(5)
            };

            btnBack = new Button
            {
                Text = "Back",
                Width = 100,
                Height = 30,
                Margin = new Padding(5)
            };

            // Add buttons to button panel
            buttonPanel.Controls.Add(btnAssign);
            buttonPanel.Controls.Add(btnClear);
            buttonPanel.Controls.Add(btnBack);

            // Store the controls in the table layout panel
            mainPanel.Controls.Add(lblTitle, 0, 0);
            mainPanel.SetColumnSpan(lblTitle, 2);

            mainPanel.Controls.Add(lblTrip, 0, 1);
            mainPanel.Controls.Add(cbTrip, 1, 1);

            mainPanel.Controls.Add(lblTraveler, 0, 2);
            mainPanel.Controls.Add(cbTraveler, 1, 2);

            mainPanel.Controls.Add(lblHotel, 0, 3);
            mainPanel.Controls.Add(cbHotel, 1, 3);

            mainPanel.Controls.Add(lblRoomType, 0, 4);
            mainPanel.Controls.Add(cbRoomType, 1, 4);

            mainPanel.Controls.Add(lblGuide, 0, 5);
            mainPanel.Controls.Add(cbGuide, 1, 5);

            mainPanel.Controls.Add(lblTransport, 0, 6);
            mainPanel.Controls.Add(cbTransport, 1, 6);

            mainPanel.Controls.Add(lblMealPlan, 0, 7);
            mainPanel.Controls.Add(cbMealPlan, 1, 7);

            mainPanel.Controls.Add(buttonPanel, 0, 8);
            mainPanel.SetColumnSpan(buttonPanel, 2);
        }

        private void AddControlsToPanel()
        {
            // Set row heights
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Title
            for (int i = 0; i < 7; i++)
            {
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Combo boxes
            }
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Buttons
        }

        private void CbHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHotel.SelectedItem != null)
            {
                var selectedHotel = (ComboBoxItem)cbHotel.SelectedItem;
                LoadRoomTypesForHotel(selectedHotel.Value);
            }
        }

        private void LoadRoomTypesForHotel(string hotelId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT rt.room_type_id, rt.room_type_name 
                        FROM HotelRoomTypes rt 
                        WHERE rt.hotel_id = @HotelId", conn))
                    {
                        cmd.Parameters.AddWithValue("@HotelId", hotelId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cbRoomType.Items.Clear();
                            while (reader.Read())
                            {
                                cbRoomType.Items.Add(new ComboBoxItem(
                                    reader["room_type_id"].ToString(),
                                    reader["room_type_name"].ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room types: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllData()
        {
            try
            {
                LoadTrips();
                LoadTravelers();
                LoadHotels();
                LoadGuides();
                LoadTransports();
                LoadMealPlans();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTrips()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT trip_id, title FROM Trip WHERE start_date > GETDATE()", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cbTrip.Items.Clear();
                        while (reader.Read())
                        {
                            cbTrip.Items.Add(new ComboBoxItem(
                                reader["trip_id"].ToString(),
                                reader["title"].ToString()));
                        }
                    }
                }
            }
        }

        private void LoadTravelers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT traveler_id, name FROM Traveler WHERE is_approved = 1", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cbTraveler.Items.Clear();
                        while (reader.Read())
                        {
                            cbTraveler.Items.Add(new ComboBoxItem(
                                reader["traveler_id"].ToString(),
                                reader["name"].ToString()));
                        }
                    }
                }
            }
        }

        private void LoadHotels()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT h.hotel_id, sp.name 
                    FROM Hotel h
                    JOIN ServiceProvider sp ON h.provider_id = sp.provider_id
                    WHERE sp.is_available = 1", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cbHotel.Items.Clear();
                        while (reader.Read())
                        {
                            cbHotel.Items.Add(new ComboBoxItem(
                                reader["hotel_id"].ToString(),
                                reader["name"].ToString()));
                        }
                    }
                }
            }
        }

        private void LoadGuides()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT g.guide_id, sp.name 
                    FROM Guide_ g
                    JOIN ServiceProvider sp ON g.provider_id = sp.provider_id
                    WHERE sp.is_available = 1", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cbGuide.Items.Clear();
                        while (reader.Read())
                        {
                            cbGuide.Items.Add(new ComboBoxItem(
                                reader["guide_id"].ToString(),
                                reader["name"].ToString()));
                        }
                    }
                }
            }
        }

        private void LoadTransports()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT t.transport_id, sp.name 
                    FROM TransportProvider t
                    JOIN ServiceProvider sp ON t.provider_id = sp.provider_id
                    WHERE sp.is_available = 1", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cbTransport.Items.Clear();
                        while (reader.Read())
                        {
                            cbTransport.Items.Add(new ComboBoxItem(
                                reader["transport_id"].ToString(),
                                reader["name"].ToString()));
                        }
                    }
                }
            }
        }

        private void LoadMealPlans()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT meal_id, meal_type 
                    FROM Trip_Meals 
                    WHERE included_in_price = 1", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        cbMealPlan.Items.Clear();
                        while (reader.Read())
                        {
                            cbMealPlan.Items.Add(new ComboBoxItem(
                                reader["meal_id"].ToString(),
                                reader["meal_type"].ToString()));
                        }
                    }
                }
            }
        }

        private void BtnAssign_Click(object sender, EventArgs e)
        {
            if (!ValidateSelections())
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Create service assignment record
                            int assignmentId = CreateServiceAssignment(conn, transaction);

                            // Get selected items
                            var selectedTrip = (ComboBoxItem)cbTrip.SelectedItem;
                            var selectedTraveler = (ComboBoxItem)cbTraveler.SelectedItem;
                            var selectedHotel = (ComboBoxItem)cbHotel.SelectedItem;
                            var selectedRoomType = (ComboBoxItem)cbRoomType.SelectedItem;
                            var selectedGuide = (ComboBoxItem)cbGuide.SelectedItem;
                            var selectedTransport = (ComboBoxItem)cbTransport.SelectedItem;
                            var selectedMealPlan = (ComboBoxItem)cbMealPlan.SelectedItem;

                            // Create links in the is_assigned table
                            AssignTripToAssignment(conn, transaction, selectedTrip.Value, assignmentId.ToString());

                            // Add providers to the assignment
                            AssignProviderToAssignment(conn, transaction, GetProviderId(conn, transaction, "Hotel", selectedHotel.Value), assignmentId.ToString());
                            AssignProviderToAssignment(conn, transaction, GetProviderId(conn, transaction, "Guide_", selectedGuide.Value), assignmentId.ToString());
                            AssignProviderToAssignment(conn, transaction, GetProviderId(conn, transaction, "TransportProvider", selectedTransport.Value), assignmentId.ToString());

                            // Create booking record
                            int bookingId = CreateBookingRecord(conn, transaction, selectedTrip.Value, selectedTraveler.Value);

                            // Create booking services
                            CreateBookingService(conn, transaction, GetProviderId(conn, transaction, "Hotel", selectedHotel.Value), bookingId.ToString(), "Confirmed");
                            CreateBookingService(conn, transaction, GetProviderId(conn, transaction, "Guide_", selectedGuide.Value), bookingId.ToString(), "Confirmed");
                            CreateBookingService(conn, transaction, GetProviderId(conn, transaction, "TransportProvider", selectedTransport.Value), bookingId.ToString(), "Confirmed");

                            // Record meal plan selection
                            RecordMealPlanSelection(conn, transaction, bookingId.ToString(), selectedMealPlan.Value);

                            transaction.Commit();
                            MessageBox.Show("Resources successfully assigned!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClearSelections();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Transaction failed: {ex.Message}", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error assigning resources: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int CreateServiceAssignment(SqlConnection conn, SqlTransaction transaction)
        {
            // First get the maximum assignment_id
            int nextAssignmentId;
            using (SqlCommand getMaxCmd = new SqlCommand(@"
        SELECT ISNULL(MAX(assignment_id), 0) + 1 
        FROM ServiceAssignment", conn, transaction))
            {
                nextAssignmentId = Convert.ToInt32(getMaxCmd.ExecuteScalar());
            }

            // Then insert with the manually determined ID
            using (SqlCommand cmd = new SqlCommand(@"
        INSERT INTO ServiceAssignment (assignment_id, status, assigned_date, is_approved)
        VALUES (@AssignmentId, 'Active', @AssignedDate, 1)", conn, transaction))
            {
                cmd.Parameters.AddWithValue("@AssignmentId", nextAssignmentId);
                cmd.Parameters.AddWithValue("@AssignedDate", DateTime.Now);
                cmd.ExecuteNonQuery();
                return nextAssignmentId;
            }
        }

        private void AssignTripToAssignment(SqlConnection conn, SqlTransaction transaction, string tripId, string assignmentId)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO is_assigned (trip_id, assignment_id)
                VALUES (@TripId, @AssignmentId)", conn, transaction))
            {
                cmd.Parameters.AddWithValue("@TripId", tripId);
                cmd.Parameters.AddWithValue("@AssignmentId", assignmentId);
                cmd.ExecuteNonQuery();
            }
        }

        private string GetProviderId(SqlConnection conn, SqlTransaction transaction, string entityType, string entityId)
        {
            string query = "";

            if (entityType == "Hotel")
            {
                query = "SELECT provider_id FROM Hotel WHERE hotel_id = @EntityId";
            }
            else if (entityType == "Guide_")
            {
                query = "SELECT provider_id FROM Guide_ WHERE guide_id = @EntityId";
            }
            else if (entityType == "TransportProvider")
            {
                query = "SELECT provider_id FROM TransportProvider WHERE transport_id = @EntityId";
            }

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@EntityId", entityId);
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        private void AssignProviderToAssignment(SqlConnection conn, SqlTransaction transaction, string providerId, string assignmentId)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO ProviderAssignment (provider_id, assignment_id)
                VALUES (@ProviderId, @AssignmentId)", conn, transaction))
            {
                cmd.Parameters.AddWithValue("@ProviderId", providerId);
                cmd.Parameters.AddWithValue("@AssignmentId", assignmentId);
                cmd.ExecuteNonQuery();
            }
        }

        private int CreateBookingRecord(SqlConnection conn, SqlTransaction transaction, string tripId, string travelerId)
        {
            // First, get the next pass_id
            int passId;
            using (SqlCommand getMaxPassCmd = new SqlCommand(@"
        SELECT ISNULL(MAX(pass_id), 0) + 1 
        FROM DigitalTravelPass", conn, transaction))
            {
                passId = Convert.ToInt32(getMaxPassCmd.ExecuteScalar());
            }

            // Create digital travel pass with manual pass_id
            using (SqlCommand cmd = new SqlCommand(@"
        INSERT INTO DigitalTravelPass (pass_id, e_ticket, hotel_voucher, activity_pass, generated_on)
        VALUES (@PassId, @ETicket, @HotelVoucher, @ActivityPass, @GeneratedOn)", conn, transaction))
            {
                cmd.Parameters.AddWithValue("@PassId", passId);
                cmd.Parameters.AddWithValue("@ETicket", "E-" + Guid.NewGuid().ToString().Substring(0, 8));
                cmd.Parameters.AddWithValue("@HotelVoucher", "H-" + Guid.NewGuid().ToString().Substring(0, 8));
                cmd.Parameters.AddWithValue("@ActivityPass", "A-" + Guid.NewGuid().ToString().Substring(0, 8));
                cmd.Parameters.AddWithValue("@GeneratedOn", DateTime.Now);
                cmd.ExecuteNonQuery();
            }

            // Get the next payment_id
            int paymentId;
            using (SqlCommand getMaxPaymentCmd = new SqlCommand(@"
        SELECT ISNULL(MAX(payment_id), 0) + 1 
        FROM Payment", conn, transaction))
            {
                paymentId = Convert.ToInt32(getMaxPaymentCmd.ExecuteScalar());
            }

            // Create payment record with manual payment_id
            using (SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Payment (payment_id, amount, payment_method, payment_status, is_refunded, payment_date, 
                           transaction_status, is_disputed, dispute_reason, transaction_id, 
                           payment_gateway, failure_reason, fraud_flag)
        VALUES (@PaymentId, @Amount, 'TBD', 'Pending', 0, @PaymentDate, 'Pending', 0, '', @TransactionId, 
                'System', '', 0)", conn, transaction))
            {
                cmd.Parameters.AddWithValue("@PaymentId", paymentId);
                cmd.Parameters.AddWithValue("@Amount", 1.00m); // Set a minimum positive amount to satisfy the constraint
                cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@TransactionId", "T-" + Guid.NewGuid().ToString());
                cmd.ExecuteNonQuery();
            }

            // Get the next booking_id
            int bookingId;
            using (SqlCommand getMaxBookingCmd = new SqlCommand(@"
        SELECT ISNULL(MAX(booking_id), 0) + 1 
        FROM Booking", conn, transaction))
            {
                bookingId = Convert.ToInt32(getMaxBookingCmd.ExecuteScalar());
            }

            // Create booking record with manual booking_id
            using (SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Booking (booking_id, booking_date, status, total_amount, cancellation_policy, 
                           Group_Size, Booking_status, booking_timestamp, 
                           pass_id, payment_id, trip_id, traveler_id)
        VALUES (@BookingId, @BookingDate, 'Confirmed', 0, 'Standard', 
                1, 1, @BookingTimestamp, 
                @PassId, @PaymentId, @TripId, @TravelerId)", conn, transaction))
            {
                cmd.Parameters.AddWithValue("@BookingId", bookingId);
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@BookingTimestamp", DateTime.Now);
                cmd.Parameters.AddWithValue("@PassId", passId);
                cmd.Parameters.AddWithValue("@PaymentId", paymentId);
                cmd.Parameters.AddWithValue("@TripId", tripId);
                cmd.Parameters.AddWithValue("@TravelerId", travelerId);
                cmd.ExecuteNonQuery();
                return bookingId;
            }
        }

        private void CreateBookingService(SqlConnection conn, SqlTransaction transaction, string providerId, string bookingId, string status)
        {
            // First get the maximum booking_service_id
            int nextBookingServiceId;
            using (SqlCommand getMaxCmd = new SqlCommand(@"
        SELECT ISNULL(MAX(booking_service_id_), 0) + 1 
        FROM BookingServices_", conn, transaction))
            {
                nextBookingServiceId = Convert.ToInt32(getMaxCmd.ExecuteScalar());
            }

            // Then insert with the manually determined ID
            using (SqlCommand cmd = new SqlCommand(@"
        INSERT INTO BookingServices_ (booking_service_id_, service_status, provider_id, booking_id)
        VALUES (@BookingServiceId, @ServiceStatus, @ProviderId, @BookingId)", conn, transaction))
            {
                cmd.Parameters.AddWithValue("@BookingServiceId", nextBookingServiceId);
                cmd.Parameters.AddWithValue("@ServiceStatus", status);
                cmd.Parameters.AddWithValue("@ProviderId", providerId);
                cmd.Parameters.AddWithValue("@BookingId", bookingId);
                cmd.ExecuteNonQuery();
            }
        }

        private void RecordMealPlanSelection(SqlConnection conn, SqlTransaction transaction, string bookingId, string mealId)
        {
            // This method would update or create additional records as needed to associate
            // the meal plan with the booking - structure depends on your DB schema
            // For this example, we'll assume this would be handled via a separate process
        }

        private bool ValidateSelections()
        {
            if (cbTrip.SelectedItem == null)
            {
                MessageBox.Show("Please select a trip.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbTraveler.SelectedItem == null)
            {
                MessageBox.Show("Please select a traveler.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbHotel.SelectedItem == null)
            {
                MessageBox.Show("Please select a hotel.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbRoomType.SelectedItem == null)
            {
                MessageBox.Show("Please select a room type.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbGuide.SelectedItem == null)
            {
                MessageBox.Show("Please select a guide.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbTransport.SelectedItem == null)
            {
                MessageBox.Show("Please select transportation.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbMealPlan.SelectedItem == null)
            {
                MessageBox.Show("Please select a meal plan.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearSelections();
        }

        private void ClearSelections()
        {
            cbTrip.SelectedIndex = -1;
            cbTraveler.SelectedIndex = -1;
            cbHotel.SelectedIndex = -1;
            cbRoomType.SelectedIndex = -1;
            cbGuide.SelectedIndex = -1;
            cbTransport.SelectedIndex = -1;
            cbMealPlan.SelectedIndex = -1;
        }
    }

    // Helper class for ComboBox items
    public class ComboBoxItem
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public ComboBoxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}