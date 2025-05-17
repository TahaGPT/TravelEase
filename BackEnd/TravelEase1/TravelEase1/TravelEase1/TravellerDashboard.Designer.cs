namespace TravelEase
{
    partial class TravelerDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnMaximize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnProfileSettings = new System.Windows.Forms.Button();
            this.btnReviews = new System.Windows.Forms.Button();
            this.btnTravelPass = new System.Windows.Forms.Button();
            this.btnSearchTrips = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.btnBookNewTrip = new System.Windows.Forms.Button();
            this.lblRecentBookings = new System.Windows.Forms.Label();
            this.dgvRecentTrips = new System.Windows.Forms.DataGridView();
            this.panelTotalSpent = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblTotalSpentTitle = new System.Windows.Forms.Label();
            this.panelUpcomingTrips = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblUpcomingTripsTitle = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelNavigation.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentTrips)).BeginInit();
            this.panelTotalSpent.SuspendLayout();
            this.panelUpcomingTrips.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.panelHeader.Controls.Add(this.pictureBox1);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.btnMinimize);
            this.panelHeader.Controls.Add(this.btnMaximize);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1333, 73);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.pictureBox1.Image = global::TravelEase1.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(-12, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.lblTitle.Location = new System.Drawing.Point(187, 16);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(291, 34);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Traveler Dashboard";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click_1);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(64)))), ((int)(((byte)(83)))));
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new System.Drawing.Point(1173, 4);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(53, 45);
            this.btnMinimize.TabIndex = 4;
            this.btnMinimize.Text = "_";
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(64)))), ((int)(((byte)(83)))));
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaximize.ForeColor = System.Drawing.Color.White;
            this.btnMaximize.Location = new System.Drawing.Point(1227, 4);
            this.btnMaximize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(53, 45);
            this.btnMaximize.TabIndex = 3;
            this.btnMaximize.Text = "□";
            this.btnMaximize.UseVisualStyleBackColor = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(64)))), ((int)(((byte)(83)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1280, 4);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(53, 45);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelNavigation
            // 
            this.panelNavigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.panelNavigation.Controls.Add(this.btnLogout);
            this.panelNavigation.Controls.Add(this.btnProfileSettings);
            this.panelNavigation.Controls.Add(this.btnReviews);
            this.panelNavigation.Controls.Add(this.btnTravelPass);
            this.panelNavigation.Controls.Add(this.btnSearchTrips);
            this.panelNavigation.Controls.Add(this.btnDashboard);
            this.panelNavigation.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelNavigation.Location = new System.Drawing.Point(0, 73);
            this.panelNavigation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(267, 665);
            this.panelNavigation.TabIndex = 1;
            this.panelNavigation.Paint += new System.Windows.Forms.PaintEventHandler(this.panelNavigation_Paint);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.btnLogout.Location = new System.Drawing.Point(0, 310);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(267, 62);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnProfileSettings
            // 
            this.btnProfileSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.btnProfileSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProfileSettings.FlatAppearance.BorderSize = 0;
            this.btnProfileSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfileSettings.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfileSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.btnProfileSettings.Location = new System.Drawing.Point(0, 248);
            this.btnProfileSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProfileSettings.Name = "btnProfileSettings";
            this.btnProfileSettings.Size = new System.Drawing.Size(267, 62);
            this.btnProfileSettings.TabIndex = 5;
            this.btnProfileSettings.Text = "Profile Settings";
            this.btnProfileSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfileSettings.UseVisualStyleBackColor = false;
            this.btnProfileSettings.Click += new System.EventHandler(this.btnProfileSettings_Click);
            // 
            // btnReviews
            // 
            this.btnReviews.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.btnReviews.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReviews.FlatAppearance.BorderSize = 0;
            this.btnReviews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReviews.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReviews.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.btnReviews.Location = new System.Drawing.Point(0, 186);
            this.btnReviews.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReviews.Name = "btnReviews";
            this.btnReviews.Size = new System.Drawing.Size(267, 62);
            this.btnReviews.TabIndex = 4;
            this.btnReviews.Text = "Reviews";
            this.btnReviews.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReviews.UseVisualStyleBackColor = false;
            this.btnReviews.Click += new System.EventHandler(this.btnReviews_Click);
            // 
            // btnTravelPass
            // 
            this.btnTravelPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.btnTravelPass.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTravelPass.FlatAppearance.BorderSize = 0;
            this.btnTravelPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTravelPass.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTravelPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.btnTravelPass.Location = new System.Drawing.Point(0, 124);
            this.btnTravelPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTravelPass.Name = "btnTravelPass";
            this.btnTravelPass.Size = new System.Drawing.Size(267, 62);
            this.btnTravelPass.TabIndex = 3;
            this.btnTravelPass.Text = "Travel Pass";
            this.btnTravelPass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTravelPass.UseVisualStyleBackColor = false;
            this.btnTravelPass.Click += new System.EventHandler(this.btnTravelPass_Click);
            // 
            // btnSearchTrips
            // 
            this.btnSearchTrips.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.btnSearchTrips.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSearchTrips.FlatAppearance.BorderSize = 0;
            this.btnSearchTrips.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchTrips.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchTrips.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.btnSearchTrips.Location = new System.Drawing.Point(0, 62);
            this.btnSearchTrips.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearchTrips.Name = "btnSearchTrips";
            this.btnSearchTrips.Size = new System.Drawing.Size(267, 62);
            this.btnSearchTrips.TabIndex = 2;
            this.btnSearchTrips.Text = "Search Trips";
            this.btnSearchTrips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchTrips.UseVisualStyleBackColor = false;
            this.btnSearchTrips.Click += new System.EventHandler(this.btnSearchTrips_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.btnDashboard.Location = new System.Drawing.Point(0, 0);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(267, 62);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panelContent.Controls.Add(this.btnBookNewTrip);
            this.panelContent.Controls.Add(this.lblRecentBookings);
            this.panelContent.Controls.Add(this.dgvRecentTrips);
            this.panelContent.Controls.Add(this.panelTotalSpent);
            this.panelContent.Controls.Add(this.panelUpcomingTrips);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(267, 73);
            this.panelContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.panelContent.Size = new System.Drawing.Size(1066, 665);
            this.panelContent.TabIndex = 2;
            this.panelContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContent_Paint);
            // 
            // btnBookNewTrip
            // 
            this.btnBookNewTrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBookNewTrip.FlatAppearance.BorderSize = 0;
            this.btnBookNewTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBookNewTrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBookNewTrip.ForeColor = System.Drawing.Color.White;
            this.btnBookNewTrip.Location = new System.Drawing.Point(31, 617);
            this.btnBookNewTrip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBookNewTrip.Name = "btnBookNewTrip";
            this.btnBookNewTrip.Size = new System.Drawing.Size(200, 33);
            this.btnBookNewTrip.TabIndex = 4;
            this.btnBookNewTrip.Text = "Book New Trip";
            this.btnBookNewTrip.UseVisualStyleBackColor = false;
            this.btnBookNewTrip.Click += new System.EventHandler(this.btnBookNewTrip_Click);
            // 
            // lblRecentBookings
            // 
            this.lblRecentBookings.AutoSize = true;
            this.lblRecentBookings.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecentBookings.Location = new System.Drawing.Point(31, 263);
            this.lblRecentBookings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecentBookings.Name = "lblRecentBookings";
            this.lblRecentBookings.Size = new System.Drawing.Size(251, 25);
            this.lblRecentBookings.TabIndex = 3;
            this.lblRecentBookings.Text = "Recent Trip Bookings";
            this.lblRecentBookings.Click += new System.EventHandler(this.lblRecentBookings_Click);
            // 
            // dgvRecentTrips
            // 
            this.dgvRecentTrips.AllowUserToAddRows = false;
            this.dgvRecentTrips.AllowUserToDeleteRows = false;
            this.dgvRecentTrips.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecentTrips.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.dgvRecentTrips.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvRecentTrips.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentTrips.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecentTrips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentTrips.Location = new System.Drawing.Point(31, 296);
            this.dgvRecentTrips.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvRecentTrips.Name = "dgvRecentTrips";
            this.dgvRecentTrips.ReadOnly = true;
            this.dgvRecentTrips.RowHeadersVisible = false;
            this.dgvRecentTrips.RowHeadersWidth = 51;
            this.dgvRecentTrips.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentTrips.Size = new System.Drawing.Size(1000, 298);
            this.dgvRecentTrips.TabIndex = 2;
            this.dgvRecentTrips.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecentTrips_CellClick);
            this.dgvRecentTrips.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecentTrips_CellContentClick);
            // 
            // panelTotalSpent
            // 
            this.panelTotalSpent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTotalSpent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.panelTotalSpent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTotalSpent.Controls.Add(this.textBox2);
            this.panelTotalSpent.Controls.Add(this.lblTotalSpentTitle);
            this.panelTotalSpent.Location = new System.Drawing.Point(552, 32);
            this.panelTotalSpent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTotalSpent.Name = "panelTotalSpent";
            this.panelTotalSpent.Size = new System.Drawing.Size(478, 211);
            this.panelTotalSpent.TabIndex = 1;
            this.panelTotalSpent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTotalSpent_Paint);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(142, 92);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(181, 22);
            this.textBox2.TabIndex = 2;
            // 
            // lblTotalSpentTitle
            // 
            this.lblTotalSpentTitle.AutoSize = true;
            this.lblTotalSpentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSpentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.lblTotalSpentTitle.Location = new System.Drawing.Point(16, 15);
            this.lblTotalSpentTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalSpentTitle.Name = "lblTotalSpentTitle";
            this.lblTotalSpentTitle.Size = new System.Drawing.Size(238, 25);
            this.lblTotalSpentTitle.TabIndex = 0;
            this.lblTotalSpentTitle.Text = "Total Amount Spent ($)";
            this.lblTotalSpentTitle.Click += new System.EventHandler(this.lblTotalSpentTitle_Click);
            // 
            // panelUpcomingTrips
            // 
            this.panelUpcomingTrips.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.panelUpcomingTrips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUpcomingTrips.Controls.Add(this.textBox1);
            this.panelUpcomingTrips.Controls.Add(this.lblUpcomingTripsTitle);
            this.panelUpcomingTrips.Location = new System.Drawing.Point(31, 32);
            this.panelUpcomingTrips.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelUpcomingTrips.Name = "panelUpcomingTrips";
            this.panelUpcomingTrips.Size = new System.Drawing.Size(479, 211);
            this.panelUpcomingTrips.TabIndex = 0;
            this.panelUpcomingTrips.Paint += new System.Windows.Forms.PaintEventHandler(this.panelUpcomingTrips_Paint);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(132, 92);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(181, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblUpcomingTripsTitle
            // 
            this.lblUpcomingTripsTitle.AutoSize = true;
            this.lblUpcomingTripsTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.lblUpcomingTripsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpcomingTripsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.lblUpcomingTripsTitle.Location = new System.Drawing.Point(16, 15);
            this.lblUpcomingTripsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUpcomingTripsTitle.Name = "lblUpcomingTripsTitle";
            this.lblUpcomingTripsTitle.Size = new System.Drawing.Size(195, 25);
            this.lblUpcomingTripsTitle.TabIndex = 0;
            this.lblUpcomingTripsTitle.Text = "Total Trips Booked";
            this.lblUpcomingTripsTitle.Click += new System.EventHandler(this.lblUpcomingTripsTitle_Click);
            // 
            // TravelerDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 738);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelNavigation);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TravelerDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TravelEase - Traveler Dashboard";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelNavigation.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentTrips)).EndInit();
            this.panelTotalSpent.ResumeLayout(false);
            this.panelTotalSpent.PerformLayout();
            this.panelUpcomingTrips.ResumeLayout(false);
            this.panelUpcomingTrips.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnSearchTrips;
        private System.Windows.Forms.Button btnTravelPass;
        private System.Windows.Forms.Button btnReviews;
        private System.Windows.Forms.Button btnProfileSettings;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelUpcomingTrips;
        private System.Windows.Forms.Label lblUpcomingTripsTitle;
        private System.Windows.Forms.Panel panelTotalSpent;
        private System.Windows.Forms.Label lblTotalSpentTitle;
        private System.Windows.Forms.DataGridView dgvRecentTrips;
        private System.Windows.Forms.Label lblRecentBookings;
        private System.Windows.Forms.Button btnBookNewTrip;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMaximize;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}