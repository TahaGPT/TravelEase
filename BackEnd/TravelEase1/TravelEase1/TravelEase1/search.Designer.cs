namespace TravelEase
{
    partial class search
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.max = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxActivityType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.datePickerStart = new System.Windows.Forms.DateTimePicker();
            this.datePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericMinGroup = new System.Windows.Forms.NumericUpDown();
            this.numericMaxGroup = new System.Windows.Forms.NumericUpDown();
            this.lbRecBookings = new System.Windows.Forms.Label();
            this.selectedTripsGrid = new System.Windows.Forms.DataGridView();
            this.Trip_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Destination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActivityType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupsize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viewdetails = new System.Windows.Forms.DataGridViewButtonColumn();
            this.book = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedTripsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.panelHeader.Controls.Add(this.pictureBox1);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1195, 92);
            this.panelHeader.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.pictureBox1.Image = global::TravelEase1.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(1, -1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 92);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.lblTitle.Location = new System.Drawing.Point(200, 21);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(205, 40);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Trip Search";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.button1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.button1.Location = new System.Drawing.Point(969, 303);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 36);
            this.button1.TabIndex = 5;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(393, 174);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(77, 26);
            this.textBox2.TabIndex = 6;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // max
            // 
            this.max.Location = new System.Drawing.Point(393, 226);
            this.max.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.max.Name = "max";
            this.max.Size = new System.Drawing.Size(77, 26);
            this.max.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.label1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.label1.Location = new System.Drawing.Point(260, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Min Price";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.label2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.label2.Location = new System.Drawing.Point(261, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "Max Price";
            // 
            // comboBoxActivityType
            // 
            this.comboBoxActivityType.FormattingEnabled = true;
            this.comboBoxActivityType.Location = new System.Drawing.Point(625, 111);
            this.comboBoxActivityType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxActivityType.Name = "comboBoxActivityType";
            this.comboBoxActivityType.Size = new System.Drawing.Size(148, 28);
            this.comboBoxActivityType.TabIndex = 17;
            this.comboBoxActivityType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.label3.Font = new System.Drawing.Font("Verdana", 8F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.label3.Location = new System.Drawing.Point(461, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 18);
            this.label3.TabIndex = 18;
            this.label3.Text = "ActivityType";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.label4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.label4.Location = new System.Drawing.Point(260, 278);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 18);
            this.label4.TabIndex = 19;
            this.label4.Text = "Min Group size";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // datePickerStart
            // 
            this.datePickerStart.Location = new System.Drawing.Point(777, 172);
            this.datePickerStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datePickerStart.Name = "datePickerStart";
            this.datePickerStart.Size = new System.Drawing.Size(224, 26);
            this.datePickerStart.TabIndex = 20;
            this.datePickerStart.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // datePickerEnd
            // 
            this.datePickerEnd.Location = new System.Drawing.Point(777, 226);
            this.datePickerEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datePickerEnd.Name = "datePickerEnd";
            this.datePickerEnd.Size = new System.Drawing.Size(224, 26);
            this.datePickerEnd.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.DarkTurquoise;
            this.label5.Font = new System.Drawing.Font("Verdana", 8F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.label5.Location = new System.Drawing.Point(640, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "start date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.DarkTurquoise;
            this.label6.Font = new System.Drawing.Font("Verdana", 8F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.label6.Location = new System.Drawing.Point(640, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 18);
            this.label6.TabIndex = 23;
            this.label6.Text = "end date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.DarkTurquoise;
            this.label7.Font = new System.Drawing.Font("Verdana", 8F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.label7.Location = new System.Drawing.Point(640, 284);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 18);
            this.label7.TabIndex = 24;
            this.label7.Text = "Max Group size";
            // 
            // numericMinGroup
            // 
            this.numericMinGroup.Location = new System.Drawing.Point(393, 275);
            this.numericMinGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericMinGroup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMinGroup.Name = "numericMinGroup";
            this.numericMinGroup.Size = new System.Drawing.Size(78, 26);
            this.numericMinGroup.TabIndex = 25;
            this.numericMinGroup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericMaxGroup
            // 
            this.numericMaxGroup.Location = new System.Drawing.Point(777, 277);
            this.numericMaxGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericMaxGroup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxGroup.Name = "numericMaxGroup";
            this.numericMaxGroup.Size = new System.Drawing.Size(78, 26);
            this.numericMaxGroup.TabIndex = 26;
            this.numericMaxGroup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbRecBookings
            // 
            this.lbRecBookings.AutoSize = true;
            this.lbRecBookings.BackColor = System.Drawing.Color.DarkTurquoise;
            this.lbRecBookings.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbRecBookings.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRecBookings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.lbRecBookings.Location = new System.Drawing.Point(259, 312);
            this.lbRecBookings.Name = "lbRecBookings";
            this.lbRecBookings.Size = new System.Drawing.Size(256, 29);
            this.lbRecBookings.TabIndex = 28;
            this.lbRecBookings.Text = "Recent Booking Requests";
            // 
            // selectedTripsGrid
            // 
            this.selectedTripsGrid.AllowUserToAddRows = false;
            this.selectedTripsGrid.AllowUserToDeleteRows = false;
            this.selectedTripsGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.selectedTripsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectedTripsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Trip_Name,
            this.Destination,
            this.Price,
            this.StartDate,
            this.ActivityType,
            this.groupsize,
            this.viewdetails,
            this.book});
            this.selectedTripsGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.selectedTripsGrid.Location = new System.Drawing.Point(280, 348);
            this.selectedTripsGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selectedTripsGrid.Name = "selectedTripsGrid";
            this.selectedTripsGrid.ReadOnly = true;
            this.selectedTripsGrid.RowHeadersVisible = false;
            this.selectedTripsGrid.RowHeadersWidth = 51;
            this.selectedTripsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.selectedTripsGrid.Size = new System.Drawing.Size(807, 305);
            this.selectedTripsGrid.TabIndex = 27;
            this.selectedTripsGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.selectedTripsGrid_CellContentClick);
            // 
            // Trip_Name
            // 
            this.Trip_Name.HeaderText = "Trip Name";
            this.Trip_Name.MinimumWidth = 6;
            this.Trip_Name.Name = "Trip_Name";
            this.Trip_Name.ReadOnly = true;
            this.Trip_Name.Width = 125;
            // 
            // Destination
            // 
            this.Destination.HeaderText = "Destination";
            this.Destination.MinimumWidth = 6;
            this.Destination.Name = "Destination";
            this.Destination.ReadOnly = true;
            this.Destination.Width = 125;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.MinimumWidth = 6;
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Width = 125;
            // 
            // StartDate
            // 
            this.StartDate.HeaderText = "Start Date";
            this.StartDate.MinimumWidth = 6;
            this.StartDate.Name = "StartDate";
            this.StartDate.ReadOnly = true;
            this.StartDate.Width = 125;
            // 
            // ActivityType
            // 
            this.ActivityType.HeaderText = "Activity Type";
            this.ActivityType.MinimumWidth = 6;
            this.ActivityType.Name = "ActivityType";
            this.ActivityType.ReadOnly = true;
            this.ActivityType.Width = 125;
            // 
            // groupsize
            // 
            this.groupsize.HeaderText = "groupsize";
            this.groupsize.MinimumWidth = 6;
            this.groupsize.Name = "groupsize";
            this.groupsize.ReadOnly = true;
            this.groupsize.Width = 125;
            // 
            // viewdetails
            // 
            this.viewdetails.HeaderText = "view details";
            this.viewdetails.MinimumWidth = 6;
            this.viewdetails.Name = "viewdetails";
            this.viewdetails.ReadOnly = true;
            this.viewdetails.Width = 125;
            // 
            // book
            // 
            this.book.HeaderText = "book now";
            this.book.MinimumWidth = 6;
            this.book.Name = "book";
            this.book.ReadOnly = true;
            this.book.Width = 125;
            // 
            // search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkTurquoise;
            this.ClientSize = new System.Drawing.Size(1195, 929);
            this.Controls.Add(this.lbRecBookings);
            this.Controls.Add(this.selectedTripsGrid);
            this.Controls.Add(this.numericMaxGroup);
            this.Controls.Add(this.numericMinGroup);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.datePickerEnd);
            this.Controls.Add(this.datePickerStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxActivityType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.max);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelHeader);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "search";
            this.Text = "search";
            this.Load += new System.EventHandler(this.search_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedTripsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox max;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxActivityType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker datePickerStart;
        private System.Windows.Forms.DateTimePicker datePickerEnd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericMinGroup;
        private System.Windows.Forms.NumericUpDown numericMaxGroup;
        private System.Windows.Forms.Label lbRecBookings;
        private System.Windows.Forms.DataGridView selectedTripsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Trip_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Destination;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActivityType;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupsize;
        private System.Windows.Forms.DataGridViewButtonColumn viewdetails;
        private System.Windows.Forms.DataGridViewButtonColumn book;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
    }
}