namespace TravelEase
{
    partial class AddServiceForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();

            // Common controls
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboServiceType = new System.Windows.Forms.ComboBox();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.chkAvailable = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            // Hotel panel
            this.pnlHotel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalRooms = new System.Windows.Forms.TextBox();
            this.txtStarRating = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.chkWheelchairAccessible = new System.Windows.Forms.CheckBox();

            // Transport panel
            this.pnlTransport = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboTransportType = new System.Windows.Forms.ComboBox();
            this.txtFleetSize = new System.Windows.Forms.TextBox();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.txtLicenseDetails = new System.Windows.Forms.TextBox();

            // Guide panel
            this.pnlGuide = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSpecialization = new System.Windows.Forms.TextBox();
            this.txtLanguages = new System.Windows.Forms.TextBox();
            this.txtExperienceYears = new System.Windows.Forms.TextBox();

            this.panel1.SuspendLayout();
            this.pnlHotel.SuspendLayout();
            this.pnlTransport.SuspendLayout();
            this.pnlGuide.SuspendLayout();
            this.SuspendLayout();

            // panel1
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 60);
            this.panel1.TabIndex = 0;

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add Service";

            // Common controls
            // label2 (Service Type)
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Service Type:";

            // cboServiceType
            this.cboServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServiceType.FormattingEnabled = true;
            this.cboServiceType.Location = new System.Drawing.Point(120, 77);
            this.cboServiceType.Name = "cboServiceType";
            this.cboServiceType.Size = new System.Drawing.Size(200, 23);
            this.cboServiceType.TabIndex = 2;
            this.cboServiceType.SelectedIndexChanged += new System.EventHandler(this.cboServiceType_SelectedIndexChanged);

            // label3 (Service Name)
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Service Name:";

            // txtServiceName
            this.txtServiceName.Location = new System.Drawing.Point(120, 107);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(200, 23);
            this.txtServiceName.TabIndex = 4;

            // label4 (Phone)
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Phone:";

            // txtPhone
            this.txtPhone.Location = new System.Drawing.Point(120, 137);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 23);
            this.txtPhone.TabIndex = 6;

            // chkAvailable
            this.chkAvailable.AutoSize = true;
            this.chkAvailable.Checked = true;
            this.chkAvailable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAvailable.Location = new System.Drawing.Point(120, 167);
            this.chkAvailable.Name = "chkAvailable";
            this.chkAvailable.Size = new System.Drawing.Size(73, 19);
            this.chkAvailable.TabIndex = 7;
            this.chkAvailable.Text = "Available";
            this.chkAvailable.UseVisualStyleBackColor = true;

            // Hotel Panel
            this.pnlHotel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHotel.Controls.Add(this.chkWheelchairAccessible);
            this.pnlHotel.Controls.Add(this.txtAddress);
            this.pnlHotel.Controls.Add(this.txtStarRating);
            this.pnlHotel.Controls.Add(this.txtTotalRooms);
            this.pnlHotel.Controls.Add(this.label7);
            this.pnlHotel.Controls.Add(this.label6);
            this.pnlHotel.Controls.Add(this.label5);
            this.pnlHotel.Location = new System.Drawing.Point(20, 200);
            this.pnlHotel.Name = "pnlHotel";
            this.pnlHotel.Size = new System.Drawing.Size(544, 200);
            this.pnlHotel.TabIndex = 8;

            // label5 (Total Rooms)
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 15);
            this.label5.Text = "Total Rooms:";

            // txtTotalRooms
            this.txtTotalRooms.Location = new System.Drawing.Point(150, 17);
            this.txtTotalRooms.Name = "txtTotalRooms";
            this.txtTotalRooms.Size = new System.Drawing.Size(100, 23);
            this.txtTotalRooms.TabIndex = 9;

            // label6 (Star Rating)
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 15);
            this.label6.Text = "Star Rating:";

            // txtStarRating
            this.txtStarRating.Location = new System.Drawing.Point(150, 47);
            this.txtStarRating.Name = "txtStarRating";
            this.txtStarRating.Size = new System.Drawing.Size(100, 23);
            this.txtStarRating.TabIndex = 10;

            // label7 (Address)
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.Text = "Address:";

            // txtAddress
            this.txtAddress.Location = new System.Drawing.Point(150, 77);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(370, 70);
            this.txtAddress.TabIndex = 11;

            // chkWheelchairAccessible
            this.chkWheelchairAccessible.AutoSize = true;
            this.chkWheelchairAccessible.Location = new System.Drawing.Point(150, 160);
            this.chkWheelchairAccessible.Name = "chkWheelchairAccessible";
            this.chkWheelchairAccessible.Size = new System.Drawing.Size(145, 19);
            this.chkWheelchairAccessible.TabIndex = 12;
            this.chkWheelchairAccessible.Text = "Wheelchair Accessible";
            this.chkWheelchairAccessible.UseVisualStyleBackColor = true;

            // Transport Panel
            this.pnlTransport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTransport.Controls.Add(this.txtLicenseDetails);
            this.pnlTransport.Controls.Add(this.txtCapacity);
            this.pnlTransport.Controls.Add(this.txtFleetSize);
            this.pnlTransport.Controls.Add(this.cboTransportType);
            this.pnlTransport.Controls.Add(this.label10);
            this.pnlTransport.Controls.Add(this.label9);
            this.pnlTransport.Controls.Add(this.label8);
            this.pnlTransport.Location = new System.Drawing.Point(20, 200);
            this.pnlTransport.Name = "pnlTransport";
            this.pnlTransport.Size = new System.Drawing.Size(544, 200);
            this.pnlTransport.TabIndex = 13;
            this.pnlTransport.Visible = false;

            // label8 (Transport Type)
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 15);
            this.label8.Text = "Transport Type:";

            // cboTransportType
            this.cboTransportType.FormattingEnabled = true;
            this.cboTransportType.Items.AddRange(new object[] {
                "Bus",
                "Car",
                "Van",
                "Boat",
                "Other"
            });
            this.cboTransportType.Location = new System.Drawing.Point(150, 17);
            this.cboTransportType.Name = "cboTransportType";
            this.cboTransportType.Size = new System.Drawing.Size(150, 23);
            this.cboTransportType.TabIndex = 14;

            // label9 (Fleet Size)
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 15);
            this.label9.Text = "Fleet Size:";

            // txtFleetSize
            this.txtFleetSize.Location = new System.Drawing.Point(150, 47);
            this.txtFleetSize.Name = "txtFleetSize";
            this.txtFleetSize.Size = new System.Drawing.Size(100, 23);
            this.txtFleetSize.TabIndex = 15;

            // label10 (Capacity)
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 15);
            this.label10.Text = "Capacity per Vehicle:";

            // txtCapacity
            this.txtCapacity.Location = new System.Drawing.Point(150, 77);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(100, 23);
            this.txtCapacity.TabIndex = 16;

            // License Details (label)
            System.Windows.Forms.Label labelLicenseDetails = new System.Windows.Forms.Label();
            labelLicenseDetails.AutoSize = true;
            labelLicenseDetails.Location = new System.Drawing.Point(10, 110);
            labelLicenseDetails.Name = "labelLicenseDetails";
            labelLicenseDetails.Size = new System.Drawing.Size(93, 15);
            labelLicenseDetails.Text = "License Details:";
            this.pnlTransport.Controls.Add(labelLicenseDetails);

            // txtLicenseDetails
            this.txtLicenseDetails.Location = new System.Drawing.Point(150, 107);
            this.txtLicenseDetails.Multiline = true;
            this.txtLicenseDetails.Name = "txtLicenseDetails";
            this.txtLicenseDetails.Size = new System.Drawing.Size(370, 70);
            this.txtLicenseDetails.TabIndex = 17;

            // Guide Panel
            this.pnlGuide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGuide.Controls.Add(this.txtExperienceYears);
            this.pnlGuide.Controls.Add(this.txtLanguages);
            this.pnlGuide.Controls.Add(this.txtSpecialization);
            this.pnlGuide.Controls.Add(this.label13);
            this.pnlGuide.Controls.Add(this.label12);
            this.pnlGuide.Controls.Add(this.label11);
            this.pnlGuide.Location = new System.Drawing.Point(20, 200);
            this.pnlGuide.Name = "pnlGuide";
            this.pnlGuide.Size = new System.Drawing.Size(544, 200);
            this.pnlGuide.TabIndex = 18;
            this.pnlGuide.Visible = false;

            // label11 (Specialization)
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 15);
            this.label11.Text = "Specialization:";

            // txtSpecialization
            this.txtSpecialization.Location = new System.Drawing.Point(150, 17);
            this.txtSpecialization.Name = "txtSpecialization";
            this.txtSpecialization.Size = new System.Drawing.Size(200, 23);
            this.txtSpecialization.TabIndex = 19;

            // label12 (Languages)
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 15);
            this.label12.Text = "Languages Spoken:";

            // txtLanguages
            this.txtLanguages.Location = new System.Drawing.Point(150, 47);
            this.txtLanguages.Name = "txtLanguages";
            this.txtLanguages.Size = new System.Drawing.Size(200, 23);
            this.txtLanguages.TabIndex = 20;

            // label13 (Experience Years)
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 15);
            this.label13.Text = "Experience Years:";

            // txtExperienceYears
            this.txtExperienceYears.Location = new System.Drawing.Point(150, 77);
            this.txtExperienceYears.Name = "txtExperienceYears";
            this.txtExperienceYears.Size = new System.Drawing.Size(100, 23);
            this.txtExperienceYears.TabIndex = 21;

            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(376, 420);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(474, 420);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;

            // AddServiceForm
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 471);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlGuide);
            this.Controls.Add(this.pnlTransport);
            this.Controls.Add(this.pnlHotel);
            this.Controls.Add(this.chkAvailable);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtServiceName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboServiceType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddServiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Service";
            this.Load += new System.EventHandler(this.AddServiceForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlHotel.ResumeLayout(false);
            this.pnlHotel.PerformLayout();
            this.pnlTransport.ResumeLayout(false);
            this.pnlTransport.PerformLayout();
            this.pnlGuide.ResumeLayout(false);
            this.pnlGuide.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;

        // Common controls
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboServiceType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.CheckBox chkAvailable;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        // Hotel controls
        private System.Windows.Forms.Panel pnlHotel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalRooms;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStarRating;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.CheckBox chkWheelchairAccessible;

        // Transport controls
        private System.Windows.Forms.Panel pnlTransport;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboTransportType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFleetSize;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCapacity;
        private System.Windows.Forms.TextBox txtLicenseDetails;

        // Guide controls
        private System.Windows.Forms.Panel pnlGuide;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSpecialization;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLanguages;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtExperienceYears;
    }
}