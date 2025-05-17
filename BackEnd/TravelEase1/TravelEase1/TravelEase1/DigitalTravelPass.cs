using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace TravelEase
{
    public partial class DigitalTravelPassForm : Form
    {
        private Panel titlePanel;
        private Panel contentPanel;
        private Panel tripSelectionPanel;
        private Label selectTripLabel;
        private ComboBox tripComboBox;
        private Panel tabPanel;
        private Button ticketsButton;
        private Button vouchersButton;
        private Button activitiesButton;
        private Panel passContentContainer;
        private Panel buttonsPanel;
        private Button printButton;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private Label lblHotelName;
        private Label lblTitle;
        private Button downloadButton;
        private Panel currentPassPanel;

        public DigitalTravelPassForm()
        {
            InitializeComponent();
            LoadTrips();
            ShowTicketsPanel(); // Default to showing e-tickets
        }

        private void InitializeComponent()
        {
            this.titlePanel = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblHotelName = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.tripSelectionPanel = new System.Windows.Forms.Panel();
            this.selectTripLabel = new System.Windows.Forms.Label();
            this.tripComboBox = new System.Windows.Forms.ComboBox();
            this.tabPanel = new System.Windows.Forms.Panel();
            this.ticketsButton = new System.Windows.Forms.Button();
            this.vouchersButton = new System.Windows.Forms.Button();
            this.activitiesButton = new System.Windows.Forms.Button();
            this.passContentContainer = new System.Windows.Forms.Panel();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.downloadButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.titlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tripSelectionPanel.SuspendLayout();
            this.tabPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlePanel
            // 
            this.titlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.titlePanel.Controls.Add(this.textBox1);
            this.titlePanel.Controls.Add(this.pictureBox1);
            this.titlePanel.Controls.Add(this.lblHotelName);
            this.titlePanel.Controls.Add(this.lblTitle);
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel.Location = new System.Drawing.Point(0, 0);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(1195, 92);
            this.titlePanel.TabIndex = 0;
            this.titlePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.titlePanel_Paint);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(954, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(224, 26);
            this.textBox1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.pictureBox1.Image = global::TravelEase1.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(22, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 92);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // lblHotelName
            // 
            this.lblHotelName.AutoSize = true;
            this.lblHotelName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.lblHotelName.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Italic);
            this.lblHotelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.lblHotelName.Location = new System.Drawing.Point(819, 41);
            this.lblHotelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHotelName.Name = "lblHotelName";
            this.lblHotelName.Size = new System.Drawing.Size(102, 26);
            this.lblHotelName.TabIndex = 5;
            this.lblHotelName.Text = "Traveler";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.lblTitle.Location = new System.Drawing.Point(221, 22);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(326, 40);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Digital Travel Pass";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(20);
            this.contentPanel.Size = new System.Drawing.Size(200, 100);
            this.contentPanel.TabIndex = 0;
            // 
            // tripSelectionPanel
            // 
            this.tripSelectionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.tripSelectionPanel.Controls.Add(this.selectTripLabel);
            this.tripSelectionPanel.Controls.Add(this.tripComboBox);
            this.tripSelectionPanel.Location = new System.Drawing.Point(40, 149);
            this.tripSelectionPanel.Name = "tripSelectionPanel";
            this.tripSelectionPanel.Padding = new System.Windows.Forms.Padding(10);
            this.tripSelectionPanel.Size = new System.Drawing.Size(992, 69);
            this.tripSelectionPanel.TabIndex = 1;
            // 
            // selectTripLabel
            // 
            this.selectTripLabel.AutoSize = true;
            this.selectTripLabel.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.selectTripLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.selectTripLabel.Location = new System.Drawing.Point(10, 25);
            this.selectTripLabel.Name = "selectTripLabel";
            this.selectTripLabel.Size = new System.Drawing.Size(138, 26);
            this.selectTripLabel.TabIndex = 0;
            this.selectTripLabel.Text = "Select Trip:";
            // 
            // tripComboBox
            // 
            this.tripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tripComboBox.Font = new System.Drawing.Font("Arial", 10F);
            this.tripComboBox.Location = new System.Drawing.Point(172, 25);
            this.tripComboBox.Name = "tripComboBox";
            this.tripComboBox.Size = new System.Drawing.Size(300, 31);
            this.tripComboBox.TabIndex = 1;
            this.tripComboBox.SelectedIndexChanged += new System.EventHandler(this.TripComboBox_SelectedIndexChanged);
            // 
            // tabPanel
            // 
            this.tabPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.tabPanel.Controls.Add(this.ticketsButton);
            this.tabPanel.Controls.Add(this.vouchersButton);
            this.tabPanel.Controls.Add(this.activitiesButton);
            this.tabPanel.Location = new System.Drawing.Point(40, 239);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.Size = new System.Drawing.Size(992, 39);
            this.tabPanel.TabIndex = 2;
            // 
            // ticketsButton
            // 
            this.ticketsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.ticketsButton.FlatAppearance.BorderSize = 0;
            this.ticketsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ticketsButton.Font = new System.Drawing.Font("Arial", 9F);
            this.ticketsButton.ForeColor = System.Drawing.Color.White;
            this.ticketsButton.Location = new System.Drawing.Point(0, 0);
            this.ticketsButton.Name = "ticketsButton";
            this.ticketsButton.Size = new System.Drawing.Size(140, 40);
            this.ticketsButton.TabIndex = 0;
            this.ticketsButton.Text = "E-Tickets";
            this.ticketsButton.UseVisualStyleBackColor = false;
            this.ticketsButton.Click += new System.EventHandler(this.TabButton_Click);
            // 
            // vouchersButton
            // 
            this.vouchersButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(60)))));
            this.vouchersButton.FlatAppearance.BorderSize = 0;
            this.vouchersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vouchersButton.Font = new System.Drawing.Font("Arial", 9F);
            this.vouchersButton.ForeColor = System.Drawing.Color.White;
            this.vouchersButton.Location = new System.Drawing.Point(140, 0);
            this.vouchersButton.Name = "vouchersButton";
            this.vouchersButton.Size = new System.Drawing.Size(140, 40);
            this.vouchersButton.TabIndex = 1;
            this.vouchersButton.Text = "Hotel Vouchers";
            this.vouchersButton.UseVisualStyleBackColor = false;
            this.vouchersButton.Click += new System.EventHandler(this.TabButton_Click);
            // 
            // activitiesButton
            // 
            this.activitiesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(80)))));
            this.activitiesButton.FlatAppearance.BorderSize = 0;
            this.activitiesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.activitiesButton.Font = new System.Drawing.Font("Arial", 9F);
            this.activitiesButton.ForeColor = System.Drawing.Color.White;
            this.activitiesButton.Location = new System.Drawing.Point(280, 0);
            this.activitiesButton.Name = "activitiesButton";
            this.activitiesButton.Size = new System.Drawing.Size(140, 40);
            this.activitiesButton.TabIndex = 2;
            this.activitiesButton.Text = "Activity Passes";
            this.activitiesButton.UseVisualStyleBackColor = false;
            this.activitiesButton.Click += new System.EventHandler(this.TabButton_Click);
            // 
            // passContentContainer
            // 
            this.passContentContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.passContentContainer.Location = new System.Drawing.Point(40, 279);
            this.passContentContainer.Name = "passContentContainer";
            this.passContentContainer.Size = new System.Drawing.Size(992, 249);
            this.passContentContainer.TabIndex = 3;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonsPanel.Controls.Add(this.downloadButton);
            this.buttonsPanel.Controls.Add(this.printButton);
            this.buttonsPanel.Location = new System.Drawing.Point(40, 549);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(992, 59);
            this.buttonsPanel.TabIndex = 4;
            // 
            // downloadButton
            // 
            this.downloadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.downloadButton.FlatAppearance.BorderSize = 0;
            this.downloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadButton.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.downloadButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.downloadButton.Location = new System.Drawing.Point(614, 8);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(209, 40);
            this.downloadButton.TabIndex = 0;
            this.downloadButton.Text = "Download Pass";
            this.downloadButton.UseVisualStyleBackColor = false;
            this.downloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // printButton
            // 
            this.printButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.printButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printButton.Font = new System.Drawing.Font("Arial", 9F);
            this.printButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.printButton.Location = new System.Drawing.Point(120, 10);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(160, 40);
            this.printButton.TabIndex = 1;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = false;
            this.printButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // DigitalTravelPassForm
            // 
            this.BackColor = System.Drawing.Color.DarkTurquoise;
            this.ClientSize = new System.Drawing.Size(1195, 929);
            this.Controls.Add(this.titlePanel);
            this.Controls.Add(this.tripSelectionPanel);
            this.Controls.Add(this.tabPanel);
            this.Controls.Add(this.passContentContainer);
            this.Controls.Add(this.buttonsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DigitalTravelPassForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TravelEase | Digital Travel Pass";
            this.titlePanel.ResumeLayout(false);
            this.titlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tripSelectionPanel.ResumeLayout(false);
            this.tripSelectionPanel.PerformLayout();
            this.tabPanel.ResumeLayout(false);
            this.buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void LoadTrips()
        {
            ComboBox tripComboBox = (ComboBox)Controls.Find("tripComboBox", true)[0];

            // Sample trip data - in a real app, this would come from your database
            List<TripItem> trips = new List<TripItem>
            {
                new TripItem { TripId = "TR-1001", Destination = "Paris, France", Dates = "May 15 - May 22", Status = "Confirmed" },
                new TripItem { TripId = "TR-1002", Destination = "Tokyo, Japan", Dates = "June 10 - June 17", Status = "Pending" },
                new TripItem { TripId = "TR-1003", Destination = "Bali, Indonesia", Dates = "July 5 - July 12", Status = "Confirmed" }
            };

            tripComboBox.DataSource = trips;
            tripComboBox.DisplayMember = "Display";
            tripComboBox.ValueMember = "TripId";
        }

        private void TripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reload the current pass panel with data for the selected trip
            ComboBox comboBox = (ComboBox)sender;
            TripItem selectedTrip = (TripItem)comboBox.SelectedItem;

            // Refresh the current panel with the selected trip data
            if (currentPassPanel != null)
            {
                if (currentPassPanel.Name == "ticketsPanel")
                    ShowTicketsPanel();
                else if (currentPassPanel.Name == "vouchersPanel")
                    ShowVouchersPanel();
                else if (currentPassPanel.Name == "activitiesPanel")
                    ShowActivitiesPanel();
            }
        }

        private void TabButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Reset all tab button colors
            foreach (Control control in clickedButton.Parent.Controls)
            {
                if (control is Button)
                {
                    control.BackColor = Color.FromArgb(52, 152, 219); // Normal blue
                }
            }

            // Set clicked button to darker blue
            clickedButton.BackColor = Color.FromArgb(41, 128, 185); // Darker blue

            // Show the appropriate panel
            if (clickedButton.Name == "ticketsButton")
                ShowTicketsPanel();
            else if (clickedButton.Name == "vouchersButton")
                ShowVouchersPanel();
            else if (clickedButton.Name == "activitiesButton")
                ShowActivitiesPanel();
        }

        private void ShowTicketsPanel()
        {
            Panel container = (Panel)Controls.Find("passContentContainer", true)[0];
            ComboBox tripComboBox = (ComboBox)Controls.Find("tripComboBox", true)[0];
            TripItem selectedTrip = tripComboBox.SelectedItem as TripItem;

            // Remove the current panel if exists
            if (currentPassPanel != null)
            {
                container.Controls.Remove(currentPassPanel);
                currentPassPanel.Dispose();
            }

            // Create a new panel for tickets
            Panel ticketsPanel = new Panel();
            ticketsPanel.Name = "ticketsPanel";
            ticketsPanel.Size = new Size(700, 230);
            ticketsPanel.Location = new Point(10, 10);
            ticketsPanel.BackColor = Color.White;

            if (selectedTrip != null)
            {
                // Left side - Trip information
                Label destinationLabel = new Label();
                destinationLabel.Text = selectedTrip.Destination;
                destinationLabel.Font = new Font("Arial", 14, FontStyle.Bold);
                destinationLabel.Location = new Point(20, 20);
                destinationLabel.AutoSize = true;
                ticketsPanel.Controls.Add(destinationLabel);

                Label datesLabel = new Label();
                datesLabel.Text = selectedTrip.Dates + ", 2025";
                datesLabel.Font = new Font("Arial", 10);
                datesLabel.ForeColor = Color.FromArgb(127, 140, 141); // Gray
                datesLabel.Location = new Point(20, 50);
                datesLabel.AutoSize = true;
                ticketsPanel.Controls.Add(datesLabel);

                Label passengerLabel = new Label();
                passengerLabel.Text = "Passenger: John Doe";
                passengerLabel.Font = new Font("Arial", 10);
                passengerLabel.Location = new Point(20, 90);
                passengerLabel.AutoSize = true;
                ticketsPanel.Controls.Add(passengerLabel);

                Label confirmationLabel = new Label();
                confirmationLabel.Text = "Confirmation: " + selectedTrip.TripId;
                confirmationLabel.Font = new Font("Arial", 10);
                confirmationLabel.Location = new Point(20, 120);
                confirmationLabel.AutoSize = true;
                ticketsPanel.Controls.Add(confirmationLabel);

                Label statusLabel = new Label();
                statusLabel.Text = "Status: " + selectedTrip.Status;
                statusLabel.Font = new Font("Arial", 10);
                statusLabel.Location = new Point(20, 150);
                statusLabel.AutoSize = true;
                ticketsPanel.Controls.Add(statusLabel);

                // Add a divider
                Panel divider = new Panel();
                divider.Size = new Size(1, 200);
                divider.Location = new Point(270, 15);
                divider.BackColor = Color.FromArgb(189, 195, 199); // Light gray
                ticketsPanel.Controls.Add(divider);

                // Right side - Flight details (sample data)
                Label flightLabel = new Label();
                flightLabel.Text = "Flight Details";
                flightLabel.Font = new Font("Arial", 12, FontStyle.Bold);
                flightLabel.Location = new Point(290, 20);
                flightLabel.AutoSize = true;
                ticketsPanel.Controls.Add(flightLabel);

                // Populate with flight details based on the destination
                string departureAirport = selectedTrip.Destination.Contains("Paris") ? "CDG" :
                                         selectedTrip.Destination.Contains("Tokyo") ? "NRT" : "DPS";

                Label departureLabel = new Label();
                departureLabel.Text = "Departure: " + departureAirport + " 9:45 AM";
                departureLabel.Font = new Font("Arial", 10);
                departureLabel.Location = new Point(290, 60);
                departureLabel.AutoSize = true;
                ticketsPanel.Controls.Add(departureLabel);

                Label arrivalLabel = new Label();
                arrivalLabel.Text = "Arrival: JFK 12:30 PM";
                arrivalLabel.Font = new Font("Arial", 10);
                arrivalLabel.Location = new Point(290, 90);
                arrivalLabel.AutoSize = true;
                ticketsPanel.Controls.Add(arrivalLabel);

                Label flightNumberLabel = new Label();
                flightNumberLabel.Text = "Flight: TL789";
                flightNumberLabel.Font = new Font("Arial", 10);
                flightNumberLabel.Location = new Point(290, 120);
                flightNumberLabel.AutoSize = true;
                ticketsPanel.Controls.Add(flightNumberLabel);

                Label seatLabel = new Label();
                seatLabel.Text = "Seat: 23B";
                seatLabel.Font = new Font("Arial", 10);
                seatLabel.Location = new Point(290, 150);
                seatLabel.AutoSize = true;
                ticketsPanel.Controls.Add(seatLabel);

                // QR Code (placeholder)
                Panel qrCodePanel = new Panel();
                qrCodePanel.Size = new Size(100, 100);
                qrCodePanel.Location = new Point(580, 50);
                qrCodePanel.BackColor = Color.White;
                qrCodePanel.BorderStyle = BorderStyle.FixedSingle;

                // Simple pattern to simulate QR code
                Panel qrElement1 = new Panel();
                qrElement1.Size = new Size(20, 20);
                qrElement1.Location = new Point(10, 10);
                qrElement1.BackColor = Color.Black;
                qrCodePanel.Controls.Add(qrElement1);

                Panel qrElement2 = new Panel();
                qrElement2.Size = new Size(20, 20);
                qrElement2.Location = new Point(70, 10);
                qrElement2.BackColor = Color.Black;
                qrCodePanel.Controls.Add(qrElement2);

                Panel qrElement3 = new Panel();
                qrElement3.Size = new Size(20, 20);
                qrElement3.Location = new Point(10, 70);
                qrElement3.BackColor = Color.Black;
                qrCodePanel.Controls.Add(qrElement3);

                ticketsPanel.Controls.Add(qrCodePanel);
            }

            container.Controls.Add(ticketsPanel);
            currentPassPanel = ticketsPanel;
        }

        private void ShowVouchersPanel()
        {
            Panel container = (Panel)Controls.Find("passContentContainer", true)[0];
            ComboBox tripComboBox = (ComboBox)Controls.Find("tripComboBox", true)[0];
            TripItem selectedTrip = tripComboBox.SelectedItem as TripItem;

            // Remove the current panel if exists
            if (currentPassPanel != null)
            {
                container.Controls.Remove(currentPassPanel);
                currentPassPanel.Dispose();
            }

            // Create a new panel for hotel vouchers
            Panel vouchersPanel = new Panel();
            vouchersPanel.Name = "vouchersPanel";
            vouchersPanel.Size = new Size(700, 230);
            vouchersPanel.Location = new Point(10, 10);
            vouchersPanel.BackColor = Color.White;

            if (selectedTrip != null)
            {
                // Left side - Hotel information
                Label hotelLabel = new Label();

                // Set hotel name based on destination
                string hotelName = selectedTrip.Destination.Contains("Paris") ? "Grand Paris Hotel" :
                                  selectedTrip.Destination.Contains("Tokyo") ? "Tokyo Luxury Suites" : "Bali Beach Resort";

                hotelLabel.Text = hotelName;
                hotelLabel.Font = new Font("Arial", 14, FontStyle.Bold);
                hotelLabel.Location = new Point(20, 20);
                hotelLabel.AutoSize = true;
                vouchersPanel.Controls.Add(hotelLabel);

                Label locationLabel = new Label();
                locationLabel.Text = selectedTrip.Destination;
                locationLabel.Font = new Font("Arial", 10);
                locationLabel.ForeColor = Color.FromArgb(127, 140, 141); // Gray
                locationLabel.Location = new Point(20, 50);
                locationLabel.AutoSize = true;
                vouchersPanel.Controls.Add(locationLabel);

                Label checkInLabel = new Label();
                checkInLabel.Text = "Check-in: " + selectedTrip.Dates.Split('-')[0].Trim();
                checkInLabel.Font = new Font("Arial", 10);
                checkInLabel.Location = new Point(20, 90);
                checkInLabel.AutoSize = true;
                vouchersPanel.Controls.Add(checkInLabel);

                Label checkOutLabel = new Label();
                checkOutLabel.Text = "Check-out: " + selectedTrip.Dates.Split('-')[1].Trim();
                checkOutLabel.Font = new Font("Arial", 10);
                checkOutLabel.Location = new Point(20, 120);
                checkOutLabel.AutoSize = true;
                vouchersPanel.Controls.Add(checkOutLabel);

                Label confirmationLabel = new Label();
                confirmationLabel.Text = "Confirmation: HV-" + selectedTrip.TripId.Substring(3);
                confirmationLabel.Font = new Font("Arial", 10);
                confirmationLabel.Location = new Point(20, 150);
                confirmationLabel.AutoSize = true;
                vouchersPanel.Controls.Add(confirmationLabel);

                // Add a divider
                Panel divider = new Panel();
                divider.Size = new Size(1, 200);
                divider.Location = new Point(270, 15);
                divider.BackColor = Color.FromArgb(189, 195, 199); // Light gray
                vouchersPanel.Controls.Add(divider);

                // Right side - Room details
                Label roomLabel = new Label();
                roomLabel.Text = "Room Details";
                roomLabel.Font = new Font("Arial", 12, FontStyle.Bold);
                roomLabel.Location = new Point(290, 20);
                roomLabel.AutoSize = true;
                vouchersPanel.Controls.Add(roomLabel);

                Label roomTypeLabel = new Label();
                roomTypeLabel.Text = "Room Type: Deluxe Double";
                roomTypeLabel.Font = new Font("Arial", 10);
                roomTypeLabel.Location = new Point(290, 60);
                roomTypeLabel.AutoSize = true;
                vouchersPanel.Controls.Add(roomTypeLabel);

                Label guestsLabel = new Label();
                guestsLabel.Text = "Guests: 2 Adults";
                guestsLabel.Font = new Font("Arial", 10);
                guestsLabel.Location = new Point(290, 90);
                guestsLabel.AutoSize = true;
                vouchersPanel.Controls.Add(guestsLabel);

                Label amenitiesLabel = new Label();
                amenitiesLabel.Text = "Amenities: Breakfast, WiFi, Pool";
                amenitiesLabel.Font = new Font("Arial", 10);
                amenitiesLabel.Location = new Point(290, 120);
                amenitiesLabel.AutoSize = true;
                vouchersPanel.Controls.Add(amenitiesLabel);

                Label specialLabel = new Label();
                specialLabel.Text = "Special Requests: None";
                specialLabel.Font = new Font("Arial", 10);
                specialLabel.Location = new Point(290, 150);
                specialLabel.AutoSize = true;
                vouchersPanel.Controls.Add(specialLabel);

                // QR Code (placeholder)
                Panel qrCodePanel = new Panel();
                qrCodePanel.Size = new Size(100, 100);
                qrCodePanel.Location = new Point(580, 50);
                qrCodePanel.BackColor = Color.White;
                qrCodePanel.BorderStyle = BorderStyle.FixedSingle;

                // Simple pattern to simulate QR code
                Panel qrElement1 = new Panel();
                qrElement1.Size = new Size(20, 20);
                qrElement1.Location = new Point(10, 10);
                qrElement1.BackColor = Color.Black;
                qrCodePanel.Controls.Add(qrElement1);

                Panel qrElement2 = new Panel();
                qrElement2.Size = new Size(20, 20);
                qrElement2.Location = new Point(70, 10);
                qrElement2.BackColor = Color.Black;
                qrCodePanel.Controls.Add(qrElement2);

                Panel qrElement3 = new Panel();
                qrElement3.Size = new Size(20, 20);
                qrElement3.Location = new Point(10, 70);
                qrElement3.BackColor = Color.Black;
                qrCodePanel.Controls.Add(qrElement3);

                vouchersPanel.Controls.Add(qrCodePanel);
            }

            container.Controls.Add(vouchersPanel);
            currentPassPanel = vouchersPanel;
        }

        private void ShowActivitiesPanel()
        {
            Panel container = (Panel)Controls.Find("passContentContainer", true)[0];
            ComboBox tripComboBox = (ComboBox)Controls.Find("tripComboBox", true)[0];
            TripItem selectedTrip = tripComboBox.SelectedItem as TripItem;

            // Remove the current panel if exists
            if (currentPassPanel != null)
            {
                container.Controls.Remove(currentPassPanel);
                currentPassPanel.Dispose();
            }

            // Create a new panel for activity passes
            Panel activitiesPanel = new Panel();
            activitiesPanel.Name = "activitiesPanel";
            activitiesPanel.Size = new Size(700, 230);
            activitiesPanel.Location = new Point(10, 10);
            activitiesPanel.BackColor = Color.White;

            if (selectedTrip != null)
            {
                // Determine activity based on destination
                string activityName = "";
                string activityDetails = "";

                if (selectedTrip.Destination.Contains("Paris"))
                {
                    activityName = "Louvre Museum Pass";
                    activityDetails = "Skip-the-line access to all Louvre Museum exhibits";
                }
                else if (selectedTrip.Destination.Contains("Tokyo"))
                {
                    activityName = "Tokyo Skytree Tour";
                    activityDetails = "VIP access to Tokyo Skytree observation deck";
                }
                else
                {
                    activityName = "Bali Water Temple Tour";
                    activityDetails = "Guided tour of sacred water temples";
                }

                // Left side - Activity information
                Label activityLabel = new Label();
                activityLabel.Text = activityName;
                activityLabel.Font = new Font("Arial", 14, FontStyle.Bold);
                activityLabel.Location = new Point(20, 20);
                activityLabel.AutoSize = true;
                activitiesPanel.Controls.Add(activityLabel);

                Label locationLabel = new Label();
                locationLabel.Text = selectedTrip.Destination;
                locationLabel.Font = new Font("Arial", 10);
                locationLabel.ForeColor = Color.FromArgb(127, 140, 141); // Gray
                locationLabel.Location = new Point(20, 50);
                locationLabel.AutoSize = true;
                activitiesPanel.Controls.Add(locationLabel);

                Label detailsLabel = new Label();
                detailsLabel.Text = activityDetails;
                detailsLabel.Font = new Font("Arial", 10);
                detailsLabel.Location = new Point(20, 90);
                detailsLabel.AutoSize = true;
                activitiesPanel.Controls.Add(detailsLabel);

                Label dateLabel = new Label();
                dateLabel.Text = "Date: " + selectedTrip.Dates.Split('-')[0].Trim();
                dateLabel.Font = new Font("Arial", 10);
                dateLabel.Location = new Point(20, 120);
                dateLabel.AutoSize = true;
                activitiesPanel.Controls.Add(dateLabel);

                Label confirmationLabel = new Label();
                confirmationLabel.Text = "Confirmation: AP-" + selectedTrip.TripId.Substring(3);
                confirmationLabel.Font = new Font("Arial", 10);
                confirmationLabel.Location = new Point(20, 150);
                confirmationLabel.AutoSize = true;
                activitiesPanel.Controls.Add(confirmationLabel);

                // Add a divider
                Panel divider = new Panel();
                divider.Size = new Size(1, 200);
                divider.Location = new Point(270, 15);
                divider.BackColor = Color.FromArgb(189, 195, 199); // Light gray
                activitiesPanel.Controls.Add(divider);

                // Right side - Activity details
                Label infoLabel = new Label();
                infoLabel.Text = "Activity Information";
                infoLabel.Font = new Font("Arial", 12, FontStyle.Bold);
                infoLabel.Location = new Point(290, 20);
                infoLabel.AutoSize = true;
                activitiesPanel.Controls.Add(infoLabel);

                Label timeLabel = new Label();
                timeLabel.Text = "Time: 10:00 AM - 2:00 PM";
                timeLabel.Font = new Font("Arial", 10);
                timeLabel.Location = new Point(290, 60);
                timeLabel.AutoSize = true;
                activitiesPanel.Controls.Add(timeLabel);

                Label participantsLabel = new Label();
                participantsLabel.Text = "Participants: 2 Adults";
                participantsLabel.Font = new Font("Arial", 10);
                participantsLabel.Location = new Point(290, 90);
                participantsLabel.AutoSize = true;
                activitiesPanel.Controls.Add(participantsLabel);

                Label meetingLabel = new Label();
                meetingLabel.Text = "Meeting Point: Hotel Lobby";
                meetingLabel.Font = new Font("Arial", 10);
                meetingLabel.Location = new Point(290, 120);
                meetingLabel.AutoSize = true;
                activitiesPanel.Controls.Add(meetingLabel);

                Label guideLabel = new Label();
                guideLabel.Text = "Guide: English Speaking";
                guideLabel.Font = new Font("Arial", 10);
                guideLabel.Location = new Point(290, 150);
                guideLabel.AutoSize = true;
                activitiesPanel.Controls.Add(guideLabel);

                // QR Code (placeholder)
                Panel qrCodePanel = new Panel();
                qrCodePanel.Size = new Size(100, 100);
                qrCodePanel.Location = new Point(580, 50);
                qrCodePanel.BackColor = Color.White;
                qrCodePanel.BorderStyle = BorderStyle.FixedSingle;

                // Simple pattern to simulate QR code
                Panel qrElement1 = new Panel();
                qrElement1.Size = new Size(20, 20);
                qrElement1.Location = new Point(10, 10);
                qrElement1.BackColor = Color.Black;
                qrCodePanel.Controls.Add(qrElement1);

                Panel qrElement2 = new Panel();
                qrElement2.Size = new Size(20, 20);
                qrElement2.Location = new Point(70, 10);
                qrElement2.BackColor = Color.Black;
                qrCodePanel.Controls.Add(qrElement2);

                Panel qrElement3 = new Panel();
                qrElement3.Size = new Size(20, 20);
                qrElement3.Location = new Point(10, 70);
                qrElement3.BackColor = Color.Black;
                qrCodePanel.Controls.Add(qrElement3);

                activitiesPanel.Controls.Add(qrCodePanel);
            }

            container.Controls.Add(activitiesPanel);
            currentPassPanel = activitiesPanel;
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            ComboBox tripComboBox = (ComboBox)Controls.Find("tripComboBox", true)[0];
            TripItem selectedTrip = tripComboBox.SelectedItem as TripItem;

            if (selectedTrip == null)
                return;

            // Create a SaveFileDialog to let the user choose where to save the pass
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveDialog.Title = "Save Digital Travel Pass";
            saveDialog.FileName = $"TravelPass_{selectedTrip.TripId}";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                // In a real application, generate a PDF and save it
                MessageBox.Show($"Travel pass for {selectedTrip.Destination} has been downloaded.",
                                "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            ComboBox tripComboBox = (ComboBox)Controls.Find("tripComboBox", true)[0];
            TripItem selectedTrip = tripComboBox.SelectedItem as TripItem;

            if (selectedTrip == null)
                return;

            // In a real application, this would open a print dialog
            MessageBox.Show($"Printing travel pass for {selectedTrip.Destination}...",
                            "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void titlePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }

    // Class to hold trip information
    public class TripItem
    {
        public string TripId { get; set; }
        public string Destination { get; set; }
        public string Dates { get; set; }
        public string Status { get; set; }

        public string Display
        {
            get { return $"{TripId} - {Destination}"; }
        }
    }
}