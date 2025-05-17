namespace TravelEase
{
    partial class HotelProviderDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblHotelName = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.btnbookReq = new System.Windows.Forms.Button();
            this.btnPerformRep = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnProfileSet = new System.Windows.Forms.Button();
            this.my_Service_Button = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panelMainContent = new System.Windows.Forms.Panel();
            this.lbRecBookings = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMoneyCount = new System.Windows.Forms.Label();
            this.RevenueLabel = new System.Windows.Forms.Label();
            this.dgvRecentBookings = new System.Windows.Forms.DataGridView();
            this.BookingID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TourOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckInDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Actions = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panelPendingBookings = new System.Windows.Forms.Panel();
            this.lblCounting = new System.Windows.Forms.Label();
            this.labelPendingTitle = new System.Windows.Forms.Label();
            this.lblViewRevenueDetails = new System.Windows.Forms.Label();
            this.lblRevenueAmount = new System.Windows.Forms.Label();
            this.lblRevenue = new System.Windows.Forms.Label();
            this.lblViewAllPending = new System.Windows.Forms.Label();
            this.lblPendingCount = new System.Windows.Forms.Label();
            this.lblPendingTitle = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelNavigation.SuspendLayout();
            this.panelMainContent.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentBookings)).BeginInit();
            this.panelPendingBookings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.panelHeader.Controls.Add(this.textBox1);
            this.panelHeader.Controls.Add(this.pictureBox1);
            this.panelHeader.Controls.Add(this.lblHotelName);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1062, 74);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(852, 33);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 22);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TravelEase.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(24, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblHotelName
            // 
            this.lblHotelName.AutoSize = true;
            this.lblHotelName.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Italic);
            this.lblHotelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.lblHotelName.Location = new System.Drawing.Point(721, 33);
            this.lblHotelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHotelName.Name = "lblHotelName";
            this.lblHotelName.Size = new System.Drawing.Size(117, 22);
            this.lblHotelName.TabIndex = 1;
            this.lblHotelName.Text = "Hotel Name";
            this.lblHotelName.Click += new System.EventHandler(this.lblHotelName_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.lblTitle.Location = new System.Drawing.Point(201, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(404, 34);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Service Provider Dashboard";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // panelNavigation
            // 
            this.panelNavigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.panelNavigation.Controls.Add(this.btnbookReq);
            this.panelNavigation.Controls.Add(this.btnPerformRep);
            this.panelNavigation.Controls.Add(this.btnLogout);
            this.panelNavigation.Controls.Add(this.btnProfileSet);
            this.panelNavigation.Controls.Add(this.my_Service_Button);
            this.panelNavigation.Controls.Add(this.btnDashboard);
            this.panelNavigation.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelNavigation.Location = new System.Drawing.Point(0, 74);
            this.panelNavigation.Margin = new System.Windows.Forms.Padding(4);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(227, 669);
            this.panelNavigation.TabIndex = 1;
            this.panelNavigation.Paint += new System.Windows.Forms.PaintEventHandler(this.panelNavigation_Paint);
            // 
            // btnbookReq
            // 
            this.btnbookReq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.btnbookReq.FlatAppearance.BorderSize = 0;
            this.btnbookReq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbookReq.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.btnbookReq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.btnbookReq.Location = new System.Drawing.Point(0, 216);
            this.btnbookReq.Margin = new System.Windows.Forms.Padding(4);
            this.btnbookReq.Name = "btnbookReq";
            this.btnbookReq.Size = new System.Drawing.Size(227, 49);
            this.btnbookReq.TabIndex = 5;
            this.btnbookReq.Text = "Booking Requests";
            this.btnbookReq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnbookReq.UseVisualStyleBackColor = false;
            this.btnbookReq.Click += new System.EventHandler(this.btnbookReq_Click);
            // 
            // btnPerformRep
            // 
            this.btnPerformRep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.btnPerformRep.FlatAppearance.BorderSize = 0;
            this.btnPerformRep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerformRep.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.btnPerformRep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.btnPerformRep.Location = new System.Drawing.Point(0, 291);
            this.btnPerformRep.Margin = new System.Windows.Forms.Padding(4);
            this.btnPerformRep.Name = "btnPerformRep";
            this.btnPerformRep.Size = new System.Drawing.Size(227, 49);
            this.btnPerformRep.TabIndex = 8;
            this.btnPerformRep.Text = "Performance Report";
            this.btnPerformRep.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPerformRep.UseVisualStyleBackColor = false;
            this.btnPerformRep.Click += new System.EventHandler(this.btnPerformRep_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.btnLogout.Location = new System.Drawing.Point(0, 436);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(227, 49);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnProfileSet
            // 
            this.btnProfileSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.btnProfileSet.FlatAppearance.BorderSize = 0;
            this.btnProfileSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfileSet.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.btnProfileSet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.btnProfileSet.Location = new System.Drawing.Point(0, 365);
            this.btnProfileSet.Margin = new System.Windows.Forms.Padding(4);
            this.btnProfileSet.Name = "btnProfileSet";
            this.btnProfileSet.Size = new System.Drawing.Size(227, 49);
            this.btnProfileSet.TabIndex = 7;
            this.btnProfileSet.Text = "Profile Settings";
            this.btnProfileSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfileSet.UseVisualStyleBackColor = false;
            this.btnProfileSet.Click += new System.EventHandler(this.btnProfileSet_Click);
            // 
            // my_Service_Button
            // 
            this.my_Service_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.my_Service_Button.FlatAppearance.BorderSize = 0;
            this.my_Service_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.my_Service_Button.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.my_Service_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.my_Service_Button.Location = new System.Drawing.Point(0, 144);
            this.my_Service_Button.Margin = new System.Windows.Forms.Padding(4);
            this.my_Service_Button.Name = "my_Service_Button";
            this.my_Service_Button.Size = new System.Drawing.Size(227, 49);
            this.my_Service_Button.TabIndex = 6;
            this.my_Service_Button.Text = "My Services";
            this.my_Service_Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.my_Service_Button.UseVisualStyleBackColor = false;
            this.my_Service_Button.Click += new System.EventHandler(this.my_Service_Button_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.btnDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.btnDashboard.Location = new System.Drawing.Point(0, 71);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(4);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(227, 49);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // panelMainContent
            // 
            this.panelMainContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(255)))), ((int)(((byte)(239)))));
            this.panelMainContent.Controls.Add(this.lbRecBookings);
            this.panelMainContent.Controls.Add(this.panel1);
            this.panelMainContent.Controls.Add(this.dgvRecentBookings);
            this.panelMainContent.Controls.Add(this.panelPendingBookings);
            this.panelMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContent.Location = new System.Drawing.Point(227, 74);
            this.panelMainContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContent.Name = "panelMainContent";
            this.panelMainContent.Size = new System.Drawing.Size(835, 669);
            this.panelMainContent.TabIndex = 2;
            this.panelMainContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMainContent_Paint);
            // 
            // lbRecBookings
            // 
            this.lbRecBookings.AutoSize = true;
            this.lbRecBookings.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbRecBookings.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.lbRecBookings.Location = new System.Drawing.Point(16, 179);
            this.lbRecBookings.Name = "lbRecBookings";
            this.lbRecBookings.Size = new System.Drawing.Size(238, 22);
            this.lbRecBookings.TabIndex = 8;
            this.lbRecBookings.Text = "Recent Booking Requests";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblMoneyCount);
            this.panel1.Controls.Add(this.RevenueLabel);
            this.panel1.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.panel1.Location = new System.Drawing.Point(436, 20);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 120);
            this.panel1.TabIndex = 7;
            // 
            // lblMoneyCount
            // 
            this.lblMoneyCount.AutoSize = true;
            this.lblMoneyCount.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoneyCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.lblMoneyCount.Location = new System.Drawing.Point(30, 50);
            this.lblMoneyCount.Name = "lblMoneyCount";
            this.lblMoneyCount.Size = new System.Drawing.Size(174, 48);
            this.lblMoneyCount.TabIndex = 7;
            this.lblMoneyCount.Text = "$4,350";
            this.lblMoneyCount.Click += new System.EventHandler(this.lblMoneyCount_Click);
            // 
            // RevenueLabel
            // 
            this.RevenueLabel.AutoSize = true;
            this.RevenueLabel.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.RevenueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.RevenueLabel.Location = new System.Drawing.Point(19, 15);
            this.RevenueLabel.Name = "RevenueLabel";
            this.RevenueLabel.Size = new System.Drawing.Size(215, 22);
            this.RevenueLabel.TabIndex = 0;
            this.RevenueLabel.Text = "Total Revenue (Month)";
            // 
            // dgvRecentBookings
            // 
            this.dgvRecentBookings.AllowUserToAddRows = false;
            this.dgvRecentBookings.AllowUserToDeleteRows = false;
            this.dgvRecentBookings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.dgvRecentBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentBookings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BookingID,
            this.TourOperator,
            this.CheckInDate,
            this.Status,
            this.Actions});
            this.dgvRecentBookings.Location = new System.Drawing.Point(20, 216);
            this.dgvRecentBookings.Margin = new System.Windows.Forms.Padding(4);
            this.dgvRecentBookings.Name = "dgvRecentBookings";
            this.dgvRecentBookings.ReadOnly = true;
            this.dgvRecentBookings.RowHeadersVisible = false;
            this.dgvRecentBookings.RowHeadersWidth = 51;
            this.dgvRecentBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentBookings.Size = new System.Drawing.Size(714, 259);
            this.dgvRecentBookings.TabIndex = 3;
            this.dgvRecentBookings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecentBookings_CellContentClick);
            // 
            // BookingID
            // 
            this.BookingID.HeaderText = "Booking ID";
            this.BookingID.MinimumWidth = 6;
            this.BookingID.Name = "BookingID";
            this.BookingID.ReadOnly = true;
            this.BookingID.Width = 90;
            // 
            // TourOperator
            // 
            this.TourOperator.HeaderText = "TourOperator";
            this.TourOperator.MinimumWidth = 6;
            this.TourOperator.Name = "TourOperator";
            this.TourOperator.ReadOnly = true;
            this.TourOperator.Width = 125;
            // 
            // CheckInDate
            // 
            this.CheckInDate.HeaderText = "Check_In Date";
            this.CheckInDate.MinimumWidth = 6;
            this.CheckInDate.Name = "CheckInDate";
            this.CheckInDate.ReadOnly = true;
            this.CheckInDate.Width = 125;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 125;
            // 
            // Actions
            // 
            this.Actions.HeaderText = "Actions";
            this.Actions.MinimumWidth = 6;
            this.Actions.Name = "Actions";
            this.Actions.ReadOnly = true;
            this.Actions.Width = 125;
            // 
            // panelPendingBookings
            // 
            this.panelPendingBookings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.panelPendingBookings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPendingBookings.Controls.Add(this.lblCounting);
            this.panelPendingBookings.Controls.Add(this.labelPendingTitle);
            this.panelPendingBookings.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.panelPendingBookings.Location = new System.Drawing.Point(20, 20);
            this.panelPendingBookings.Margin = new System.Windows.Forms.Padding(4);
            this.panelPendingBookings.Name = "panelPendingBookings";
            this.panelPendingBookings.Size = new System.Drawing.Size(250, 120);
            this.panelPendingBookings.TabIndex = 6;
            this.panelPendingBookings.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPendingBookings_Paint);
            // 
            // lblCounting
            // 
            this.lblCounting.AutoSize = true;
            this.lblCounting.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCounting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.lblCounting.Location = new System.Drawing.Point(87, 50);
            this.lblCounting.Name = "lblCounting";
            this.lblCounting.Size = new System.Drawing.Size(0, 48);
            this.lblCounting.TabIndex = 7;
            this.lblCounting.Click += new System.EventHandler(this.lblCounting_Click);
            // 
            // labelPendingTitle
            // 
            this.labelPendingTitle.AutoSize = true;
            this.labelPendingTitle.Font = new System.Drawing.Font("Verdana", 10.8F);
            this.labelPendingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.labelPendingTitle.Location = new System.Drawing.Point(26, 15);
            this.labelPendingTitle.Name = "labelPendingTitle";
            this.labelPendingTitle.Size = new System.Drawing.Size(191, 22);
            this.labelPendingTitle.TabIndex = 0;
            this.labelPendingTitle.Text = "Confirmed Bookings";
            this.labelPendingTitle.Click += new System.EventHandler(this.labelPendingTitle_Click);
            // 
            // lblViewRevenueDetails
            // 
            this.lblViewRevenueDetails.Location = new System.Drawing.Point(0, 0);
            this.lblViewRevenueDetails.Name = "lblViewRevenueDetails";
            this.lblViewRevenueDetails.Size = new System.Drawing.Size(100, 23);
            this.lblViewRevenueDetails.TabIndex = 0;
            // 
            // lblRevenueAmount
            // 
            this.lblRevenueAmount.Location = new System.Drawing.Point(0, 0);
            this.lblRevenueAmount.Name = "lblRevenueAmount";
            this.lblRevenueAmount.Size = new System.Drawing.Size(100, 23);
            this.lblRevenueAmount.TabIndex = 0;
            // 
            // lblRevenue
            // 
            this.lblRevenue.Location = new System.Drawing.Point(0, 0);
            this.lblRevenue.Name = "lblRevenue";
            this.lblRevenue.Size = new System.Drawing.Size(100, 23);
            this.lblRevenue.TabIndex = 0;
            // 
            // lblViewAllPending
            // 
            this.lblViewAllPending.Location = new System.Drawing.Point(0, 0);
            this.lblViewAllPending.Name = "lblViewAllPending";
            this.lblViewAllPending.Size = new System.Drawing.Size(100, 23);
            this.lblViewAllPending.TabIndex = 0;
            // 
            // lblPendingCount
            // 
            this.lblPendingCount.Location = new System.Drawing.Point(0, 0);
            this.lblPendingCount.Name = "lblPendingCount";
            this.lblPendingCount.Size = new System.Drawing.Size(100, 23);
            this.lblPendingCount.TabIndex = 0;
            // 
            // lblPendingTitle
            // 
            this.lblPendingTitle.Location = new System.Drawing.Point(0, 0);
            this.lblPendingTitle.Name = "lblPendingTitle";
            this.lblPendingTitle.Size = new System.Drawing.Size(100, 23);
            this.lblPendingTitle.TabIndex = 0;
            // 
            // HotelProviderDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 743);
            this.Controls.Add(this.panelMainContent);
            this.Controls.Add(this.panelNavigation);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "HotelProviderDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TravelEase - Hotel Provider Dashboard";
            this.Load += new System.EventHandler(this.HotelProviderDashboard_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelNavigation.ResumeLayout(false);
            this.panelMainContent.ResumeLayout(false);
            this.panelMainContent.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentBookings)).EndInit();
            this.panelPendingBookings.ResumeLayout(false);
            this.panelPendingBookings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblHotelName;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnbookReq;
        private System.Windows.Forms.Panel panelMainContent;
        private System.Windows.Forms.Panel panelPendingBookings;
        private System.Windows.Forms.Label lblPendingTitle;
        private System.Windows.Forms.Label lblPendingCount;
        private System.Windows.Forms.Label lblViewAllPending;
        private System.Windows.Forms.Label lblRevenue;
        private System.Windows.Forms.Label lblRevenueAmount;
        private System.Windows.Forms.Label lblViewRevenueDetails;
        private System.Windows.Forms.DataGridView dgvRecentBookings;
        private System.Windows.Forms.Button btnPerformRep;
        private System.Windows.Forms.Button btnProfileSet;
        private System.Windows.Forms.Button my_Service_Button;
        private System.Windows.Forms.Label labelPendingTitle;
        private System.Windows.Forms.Label lblCounting;
        private System.Windows.Forms.Label lbRecBookings;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMoneyCount;
        private System.Windows.Forms.Label RevenueLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn BookingID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TourOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckInDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewButtonColumn Actions;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

