using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class ServiceListingForm : Form
    {
        private string connectionString = "Data Source=Rafique\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";
        private int serviceProviderId;
        private PictureBox pictureBox1;
        private Label lblTitle;
        private string selectedImagePath = string.Empty;

        public ServiceListingForm(int providerId)
        {
            InitializeComponent();
            serviceProviderId = providerId;
            LoadServiceTypes();
            LoadServiceList();
            SetupCalendar();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControlServiceListing = new System.Windows.Forms.TabControl();
            this.tabPageAddService = new System.Windows.Forms.TabPage();
            this.btnClearForm = new System.Windows.Forms.Button();
            this.btnAddService = new System.Windows.Forms.Button();
            this.groupBoxAvailability = new System.Windows.Forms.GroupBox();
            this.monthCalendarAvailability = new System.Windows.Forms.MonthCalendar();
            this.lblAvailableDates = new System.Windows.Forms.Label();
            this.groupBoxServiceDetails = new System.Windows.Forms.GroupBox();
            this.richTextBoxTermsAndPolicies = new System.Windows.Forms.RichTextBox();
            this.lblTermsAndPolicies = new System.Windows.Forms.Label();
            this.comboBoxServiceType = new System.Windows.Forms.ComboBox();
            this.lblServiceType = new System.Windows.Forms.Label();
            this.numericUpDownCapacity = new System.Windows.Forms.NumericUpDown();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.numericUpDownPrice = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.textBoxServiceName = new System.Windows.Forms.TextBox();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.tabPageManageServices = new System.Windows.Forms.TabPage();
            this.btnDeleteService = new System.Windows.Forms.Button();
            this.btnEditService = new System.Windows.Forms.Button();
            this.dataGridViewServices = new System.Windows.Forms.DataGridView();
            this.lblManageServicesTitle = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControlServiceListing.SuspendLayout();
            this.tabPageAddService.SuspendLayout();
            this.groupBoxAvailability.SuspendLayout();
            this.groupBoxServiceDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrice)).BeginInit();
            this.tabPageManageServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewServices)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.panelHeader.Controls.Add(this.pictureBox1);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1308, 74);
            this.panelHeader.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.pictureBox1.Image = global::TravelEase.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(20, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.lblTitle.Location = new System.Drawing.Point(197, 17);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(132, 34);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "Services";
            // 
            // tabControlServiceListing
            // 
            this.tabControlServiceListing.Controls.Add(this.tabPageAddService);
            this.tabControlServiceListing.Controls.Add(this.tabPageManageServices);
            this.tabControlServiceListing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlServiceListing.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlServiceListing.Location = new System.Drawing.Point(0, 74);
            this.tabControlServiceListing.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlServiceListing.Name = "tabControlServiceListing";
            this.tabControlServiceListing.SelectedIndex = 0;
            this.tabControlServiceListing.Size = new System.Drawing.Size(1308, 745);
            this.tabControlServiceListing.TabIndex = 1;
            // 
            // tabPageAddService
            // 
            this.tabPageAddService.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageAddService.Controls.Add(this.btnClearForm);
            this.tabPageAddService.Controls.Add(this.btnAddService);
            this.tabPageAddService.Controls.Add(this.groupBoxAvailability);
            this.tabPageAddService.Controls.Add(this.groupBoxServiceDetails);
            this.tabPageAddService.Location = new System.Drawing.Point(4, 30);
            this.tabPageAddService.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageAddService.Name = "tabPageAddService";
            this.tabPageAddService.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageAddService.Size = new System.Drawing.Size(1300, 711);
            this.tabPageAddService.TabIndex = 0;
            this.tabPageAddService.Text = "Add New Service";
            // 
            // btnClearForm
            // 
            this.btnClearForm.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClearForm.FlatAppearance.BorderSize = 0;
            this.btnClearForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearForm.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearForm.ForeColor = System.Drawing.Color.White;
            this.btnClearForm.Location = new System.Drawing.Point(807, 644);
            this.btnClearForm.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearForm.Name = "btnClearForm";
            this.btnClearForm.Size = new System.Drawing.Size(173, 43);
            this.btnClearForm.TabIndex = 3;
            this.btnClearForm.Text = "Clear Form";
            this.btnClearForm.UseVisualStyleBackColor = false;
            this.btnClearForm.Click += new System.EventHandler(this.btnClearForm_Click);
            // 
            // btnAddService
            // 
            this.btnAddService.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnAddService.FlatAppearance.BorderSize = 0;
            this.btnAddService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddService.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddService.ForeColor = System.Drawing.Color.White;
            this.btnAddService.Location = new System.Drawing.Point(995, 644);
            this.btnAddService.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(173, 43);
            this.btnAddService.TabIndex = 2;
            this.btnAddService.Text = "Add Service";
            this.btnAddService.UseVisualStyleBackColor = false;
            this.btnAddService.Click += new System.EventHandler(this.btnAddService_Click);
            // 
            // groupBoxAvailability
            // 
            this.groupBoxAvailability.Controls.Add(this.monthCalendarAvailability);
            this.groupBoxAvailability.Controls.Add(this.lblAvailableDates);
            this.groupBoxAvailability.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAvailability.Location = new System.Drawing.Point(801, 18);
            this.groupBoxAvailability.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxAvailability.Name = "groupBoxAvailability";
            this.groupBoxAvailability.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxAvailability.Size = new System.Drawing.Size(411, 610);
            this.groupBoxAvailability.TabIndex = 1;
            this.groupBoxAvailability.TabStop = false;
            this.groupBoxAvailability.Text = "Availability Calendar";
            // 
            // monthCalendarAvailability
            // 
            this.monthCalendarAvailability.CalendarDimensions = new System.Drawing.Size(1, 2);
            this.monthCalendarAvailability.Location = new System.Drawing.Point(31, 81);
            this.monthCalendarAvailability.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.monthCalendarAvailability.MaxSelectionCount = 365;
            this.monthCalendarAvailability.Name = "monthCalendarAvailability";
            this.monthCalendarAvailability.SelectionRange = new System.Windows.Forms.SelectionRange(new System.DateTime(2025, 1, 1, 0, 0, 0, 0), new System.DateTime(2025, 1, 7, 0, 0, 0, 0));
            this.monthCalendarAvailability.ShowToday = false;
            this.monthCalendarAvailability.ShowTodayCircle = false;
            this.monthCalendarAvailability.TabIndex = 1;
            this.monthCalendarAvailability.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarAvailability_DateChanged);
            // 
            // lblAvailableDates
            // 
            this.lblAvailableDates.AutoSize = true;
            this.lblAvailableDates.Location = new System.Drawing.Point(27, 47);
            this.lblAvailableDates.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvailableDates.Name = "lblAvailableDates";
            this.lblAvailableDates.Size = new System.Drawing.Size(372, 23);
            this.lblAvailableDates.TabIndex = 0;
            this.lblAvailableDates.Text = "Select available dates (click and drag for range):";
            // 
            // groupBoxServiceDetails
            // 
            this.groupBoxServiceDetails.Controls.Add(this.richTextBoxTermsAndPolicies);
            this.groupBoxServiceDetails.Controls.Add(this.lblTermsAndPolicies);
            this.groupBoxServiceDetails.Controls.Add(this.comboBoxServiceType);
            this.groupBoxServiceDetails.Controls.Add(this.lblServiceType);
            this.groupBoxServiceDetails.Controls.Add(this.numericUpDownCapacity);
            this.groupBoxServiceDetails.Controls.Add(this.lblCapacity);
            this.groupBoxServiceDetails.Controls.Add(this.numericUpDownPrice);
            this.groupBoxServiceDetails.Controls.Add(this.lblPrice);
            this.groupBoxServiceDetails.Controls.Add(this.richTextBoxDescription);
            this.groupBoxServiceDetails.Controls.Add(this.lblDescription);
            this.groupBoxServiceDetails.Controls.Add(this.textBoxServiceName);
            this.groupBoxServiceDetails.Controls.Add(this.lblServiceName);
            this.groupBoxServiceDetails.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxServiceDetails.Location = new System.Drawing.Point(21, 18);
            this.groupBoxServiceDetails.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxServiceDetails.Name = "groupBoxServiceDetails";
            this.groupBoxServiceDetails.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxServiceDetails.Size = new System.Drawing.Size(751, 670);
            this.groupBoxServiceDetails.TabIndex = 0;
            this.groupBoxServiceDetails.TabStop = false;
            this.groupBoxServiceDetails.Text = "Service Details";
            this.groupBoxServiceDetails.Enter += new System.EventHandler(this.groupBoxServiceDetails_Enter);
            // 
            // richTextBoxTermsAndPolicies
            // 
            this.richTextBoxTermsAndPolicies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxTermsAndPolicies.Location = new System.Drawing.Point(325, 313);
            this.richTextBoxTermsAndPolicies.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxTermsAndPolicies.Name = "richTextBoxTermsAndPolicies";
            this.richTextBoxTermsAndPolicies.Size = new System.Drawing.Size(395, 191);
            this.richTextBoxTermsAndPolicies.TabIndex = 11;
            this.richTextBoxTermsAndPolicies.Text = "";
            // 
            // lblTermsAndPolicies
            // 
            this.lblTermsAndPolicies.AutoSize = true;
            this.lblTermsAndPolicies.Location = new System.Drawing.Point(321, 288);
            this.lblTermsAndPolicies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTermsAndPolicies.Name = "lblTermsAndPolicies";
            this.lblTermsAndPolicies.Size = new System.Drawing.Size(152, 23);
            this.lblTermsAndPolicies.TabIndex = 10;
            this.lblTermsAndPolicies.Text = "Terms and Policies:";
            // 
            // comboBoxServiceType
            // 
            this.comboBoxServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxServiceType.FormattingEnabled = true;
            this.comboBoxServiceType.Location = new System.Drawing.Point(325, 132);
            this.comboBoxServiceType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxServiceType.Name = "comboBoxServiceType";
            this.comboBoxServiceType.Size = new System.Drawing.Size(395, 29);
            this.comboBoxServiceType.TabIndex = 9;
            this.comboBoxServiceType.SelectedIndexChanged += new System.EventHandler(this.comboBoxServiceType_SelectedIndexChanged);
            // 
            // lblServiceType
            // 
            this.lblServiceType.AutoSize = true;
            this.lblServiceType.Location = new System.Drawing.Point(321, 107);
            this.lblServiceType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServiceType.Name = "lblServiceType";
            this.lblServiceType.Size = new System.Drawing.Size(107, 23);
            this.lblServiceType.TabIndex = 8;
            this.lblServiceType.Text = "Service Type:";
            // 
            // numericUpDownCapacity
            // 
            this.numericUpDownCapacity.Location = new System.Drawing.Point(493, 215);
            this.numericUpDownCapacity.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownCapacity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCapacity.Name = "numericUpDownCapacity";
            this.numericUpDownCapacity.Size = new System.Drawing.Size(228, 29);
            this.numericUpDownCapacity.TabIndex = 7;
            this.numericUpDownCapacity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCapacity.ValueChanged += new System.EventHandler(this.numericUpDownCapacity_ValueChanged);
            // 
            // lblCapacity
            // 
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.Location = new System.Drawing.Point(489, 191);
            this.lblCapacity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(79, 23);
            this.lblCapacity.TabIndex = 6;
            this.lblCapacity.Text = "Capacity:";
            // 
            // numericUpDownPrice
            // 
            this.numericUpDownPrice.DecimalPlaces = 2;
            this.numericUpDownPrice.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownPrice.Location = new System.Drawing.Point(325, 215);
            this.numericUpDownPrice.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownPrice.Name = "numericUpDownPrice";
            this.numericUpDownPrice.Size = new System.Drawing.Size(127, 29);
            this.numericUpDownPrice.TabIndex = 5;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(321, 191);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(51, 23);
            this.lblPrice.TabIndex = 4;
            this.lblPrice.Text = "Price:";
            // 
            // richTextBoxDescription
            // 
            this.richTextBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxDescription.Location = new System.Drawing.Point(31, 132);
            this.richTextBoxDescription.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxDescription.Name = "richTextBoxDescription";
            this.richTextBoxDescription.Size = new System.Drawing.Size(265, 312);
            this.richTextBoxDescription.TabIndex = 3;
            this.richTextBoxDescription.Text = "";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(27, 107);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(100, 23);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            // 
            // textBoxServiceName
            // 
            this.textBoxServiceName.Location = new System.Drawing.Point(31, 63);
            this.textBoxServiceName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxServiceName.Name = "textBoxServiceName";
            this.textBoxServiceName.Size = new System.Drawing.Size(689, 29);
            this.textBoxServiceName.TabIndex = 1;
            // 
            // lblServiceName
            // 
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.Location = new System.Drawing.Point(27, 38);
            this.lblServiceName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(118, 23);
            this.lblServiceName.TabIndex = 0;
            this.lblServiceName.Text = "Service Name:";
            // 
            // tabPageManageServices
            // 
            this.tabPageManageServices.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageManageServices.Controls.Add(this.btnDeleteService);
            this.tabPageManageServices.Controls.Add(this.btnEditService);
            this.tabPageManageServices.Controls.Add(this.dataGridViewServices);
            this.tabPageManageServices.Controls.Add(this.lblManageServicesTitle);
            this.tabPageManageServices.Location = new System.Drawing.Point(4, 30);
            this.tabPageManageServices.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageManageServices.Name = "tabPageManageServices";
            this.tabPageManageServices.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageManageServices.Size = new System.Drawing.Size(1300, 711);
            this.tabPageManageServices.TabIndex = 1;
            this.tabPageManageServices.Text = "Manage Services";
            this.tabPageManageServices.Click += new System.EventHandler(this.tabPageManageServices_Click);
            // 
            // btnDeleteService
            // 
            this.btnDeleteService.BackColor = System.Drawing.Color.Firebrick;
            this.btnDeleteService.FlatAppearance.BorderSize = 0;
            this.btnDeleteService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteService.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteService.ForeColor = System.Drawing.Color.White;
            this.btnDeleteService.Location = new System.Drawing.Point(1117, 642);
            this.btnDeleteService.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteService.Name = "btnDeleteService";
            this.btnDeleteService.Size = new System.Drawing.Size(156, 43);
            this.btnDeleteService.TabIndex = 3;
            this.btnDeleteService.Text = "Delete";
            this.btnDeleteService.UseVisualStyleBackColor = false;
            this.btnDeleteService.Click += new System.EventHandler(this.btnDeleteService_Click);
            // 
            // btnEditService
            // 
            this.btnEditService.BackColor = System.Drawing.Color.SteelBlue;
            this.btnEditService.FlatAppearance.BorderSize = 0;
            this.btnEditService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditService.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditService.ForeColor = System.Drawing.Color.White;
            this.btnEditService.Location = new System.Drawing.Point(943, 642);
            this.btnEditService.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditService.Name = "btnEditService";
            this.btnEditService.Size = new System.Drawing.Size(156, 43);
            this.btnEditService.TabIndex = 2;
            this.btnEditService.Text = "Edit";
            this.btnEditService.UseVisualStyleBackColor = false;
            this.btnEditService.Click += new System.EventHandler(this.btnEditService_Click);
            // 
            // dataGridViewServices
            // 
            this.dataGridViewServices.AllowUserToAddRows = false;
            this.dataGridViewServices.AllowUserToDeleteRows = false;
            this.dataGridViewServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewServices.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewServices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewServices.ColumnHeadersHeight = 40;
            this.dataGridViewServices.EnableHeadersVisualStyles = false;
            this.dataGridViewServices.Location = new System.Drawing.Point(28, 64);
            this.dataGridViewServices.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewServices.MultiSelect = false;
            this.dataGridViewServices.Name = "dataGridViewServices";
            this.dataGridViewServices.ReadOnly = true;
            this.dataGridViewServices.RowHeadersVisible = false;
            this.dataGridViewServices.RowHeadersWidth = 51;
            this.dataGridViewServices.RowTemplate.Height = 35;
            this.dataGridViewServices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewServices.Size = new System.Drawing.Size(1245, 554);
            this.dataGridViewServices.TabIndex = 1;
            this.dataGridViewServices.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewServices_CellContentClick);
            // 
            // lblManageServicesTitle
            // 
            this.lblManageServicesTitle.AutoSize = true;
            this.lblManageServicesTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManageServicesTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblManageServicesTitle.Location = new System.Drawing.Point(21, 18);
            this.lblManageServicesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblManageServicesTitle.Name = "lblManageServicesTitle";
            this.lblManageServicesTitle.Size = new System.Drawing.Size(239, 32);
            this.lblManageServicesTitle.TabIndex = 0;
            this.lblManageServicesTitle.Text = "Manage My Services";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            this.openFileDialog.Title = "Select Service Image";
            // 
            // ServiceListingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1308, 819);
            this.Controls.Add(this.tabControlServiceListing);
            this.Controls.Add(this.panelHeader);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1326, 814);
            this.Name = "ServiceListingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TravelEase - Service Listing";
            this.Load += new System.EventHandler(this.ServiceListingForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControlServiceListing.ResumeLayout(false);
            this.tabPageAddService.ResumeLayout(false);
            this.groupBoxAvailability.ResumeLayout(false);
            this.groupBoxAvailability.PerformLayout();
            this.groupBoxServiceDetails.ResumeLayout(false);
            this.groupBoxServiceDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrice)).EndInit();
            this.tabPageManageServices.ResumeLayout(false);
            this.tabPageManageServices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewServices)).EndInit();
            this.ResumeLayout(false);

        }

        #region Private Members
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.TabControl tabControlServiceListing;
        private System.Windows.Forms.TabPage tabPageAddService;
        private System.Windows.Forms.TabPage tabPageManageServices;
        private System.Windows.Forms.GroupBox groupBoxServiceDetails;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.TextBox textBoxServiceName;
        private System.Windows.Forms.RichTextBox richTextBoxDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.NumericUpDown numericUpDownPrice;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numericUpDownCapacity;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.ComboBox comboBoxServiceType;
        private System.Windows.Forms.Label lblServiceType;
        private System.Windows.Forms.RichTextBox richTextBoxTermsAndPolicies;
        private System.Windows.Forms.Label lblTermsAndPolicies;
        private System.Windows.Forms.GroupBox groupBoxAvailability;
        private System.Windows.Forms.Label lblAvailableDates;
        private System.Windows.Forms.MonthCalendar monthCalendarAvailability;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.Button btnClearForm;
        private System.Windows.Forms.Label lblManageServicesTitle;
        private System.Windows.Forms.DataGridView dataGridViewServices;
        private System.Windows.Forms.Button btnEditService;
        private System.Windows.Forms.Button btnDeleteService;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        #endregion

        #region Form Load and Initialization
        private void ServiceListingForm_Load(object sender, EventArgs e)
        {
            // Set tooltips
            toolTip.SetToolTip(monthCalendarAvailability, "Select dates when this service is available");
            toolTip.SetToolTip(comboBoxServiceType, "Select the type of service you're offering");
        }

        private void LoadServiceTypes()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ServiceTypeID, TypeName FROM ServiceTypes ORDER BY TypeName";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    comboBoxServiceType.Items.Clear();
                    while (reader.Read())
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Text = reader["TypeName"].ToString();
                        item.Value = reader["ServiceTypeID"];
                        comboBoxServiceType.Items.Add(item);
                    }

                    if (comboBoxServiceType.Items.Count > 0)
                        comboBoxServiceType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading service types: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadServiceList()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT s.ServiceID, s.ServiceName, st.TypeName as ServiceType, s.Price, 
                               s.Capacity, s.IsAvailable
                        FROM Services s
                        INNER JOIN ServiceTypes st ON s.ServiceTypeID = st.ServiceTypeID
                        WHERE s.ServiceProviderID = @ProviderID
                        ORDER BY s.ServiceName";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProviderID", serviceProviderId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridViewServices.DataSource = dataTable;

                    // Format the grid
                    if (dataGridViewServices.Columns.Contains("ServiceID"))
                        dataGridViewServices.Columns["ServiceID"].Visible = false;

                    if (dataGridViewServices.Columns.Contains("Price"))
                        dataGridViewServices.Columns["Price"].DefaultCellStyle.Format = "C2";

                    if (dataGridViewServices.Columns.Contains("IsAvailable"))
                    {
                        dataGridViewServices.Columns["IsAvailable"].HeaderText = "Status";
                        dataGridViewServices.Columns["IsAvailable"].DefaultCellStyle.Format = "Yes/No";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading services: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupCalendar()
        {
            // Set the calendar to allow multiple date selection
            monthCalendarAvailability.MaxSelectionCount = 366; // Allow up to a year of selection

            // Set the calendar to start from tomorrow
            monthCalendarAvailability.MinDate = DateTime.Today.AddDays(1);

            // Set the calendar to show dates up to a year from now
            monthCalendarAvailability.MaxDate = DateTime.Today.AddYears(1);
        }
        #endregion

        #region Button Click Events


        private void btnClearForm_Click(object sender, EventArgs e)
        {
            // Clear all form fields
            textBoxServiceName.Clear();
            richTextBoxDescription.Clear();
            numericUpDownPrice.Value = 0;
            numericUpDownCapacity.Value = 1;
            if (comboBoxServiceType.Items.Count > 0)
                comboBoxServiceType.SelectedIndex = 0;
            richTextBoxTermsAndPolicies.Clear();
            selectedImagePath = string.Empty;

            // Reset calendar selection
            monthCalendarAvailability.SelectionStart = DateTime.Today.AddDays(1);
            monthCalendarAvailability.SelectionEnd = DateTime.Today.AddDays(1);
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            if (!ValidateServiceForm())
                return;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // 1. Insert into the Services table
                    string insertServiceQuery = @"
                        INSERT INTO Services (
                            ServiceName, ServiceTypeID, Description, Price, 
                            Capacity, TermsAndPolicies, ServiceProviderID, IsAvailable
                        ) 
                        VALUES (
                            @ServiceName, @ServiceTypeID, @Description, @Price, 
                            @Capacity, @TermsAndPolicies, @ServiceProviderID, @IsAvailable
                        );
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand command = new SqlCommand(insertServiceQuery, connection);
                    command.Parameters.AddWithValue("@ServiceName", textBoxServiceName.Text.Trim());

                    ComboBoxItem selectedType = (ComboBoxItem)comboBoxServiceType.SelectedItem;
                    command.Parameters.AddWithValue("@ServiceTypeID", selectedType.Value);

                    command.Parameters.AddWithValue("@Description", richTextBoxDescription.Text.Trim());
                    command.Parameters.AddWithValue("@Price", numericUpDownPrice.Value);
                    command.Parameters.AddWithValue("@Capacity", numericUpDownCapacity.Value);
                    command.Parameters.AddWithValue("@TermsAndPolicies", richTextBoxTermsAndPolicies.Text.Trim());
                    command.Parameters.AddWithValue("@ServiceProviderID", serviceProviderId);
                    command.Parameters.AddWithValue("@IsAvailable", true);

                    // Execute the command and get the new Service ID
                    int serviceId = Convert.ToInt32(command.ExecuteScalar());

                    // 2. Save the image (if selected)
                    if (!string.IsNullOrEmpty(selectedImagePath))
                    {
                        SaveServiceImage(serviceId, selectedImagePath);
                    }

                    // 3. Insert the availability dates
                    SaveAvailabilityDates(serviceId, connection);

                    MessageBox.Show("Service has been added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear the form and reload the service list
                    btnClearForm_Click(sender, e);
                    LoadServiceList();

                    // Switch to the Manage Services tab
                    tabControlServiceListing.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding service: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditService_Click(object sender, EventArgs e)
        {
            if (dataGridViewServices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int serviceId = Convert.ToInt32(dataGridViewServices.SelectedRows[0].Cells["ServiceID"].Value);

            // Open service edit form passing the service ID
            // EditServiceForm editForm = new EditServiceForm(serviceId, serviceProviderId);
            // editForm.ShowDialog();

            // After the edit form is closed, refresh the service list to show updated data
            LoadServiceList();
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            if (dataGridViewServices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int serviceId = Convert.ToInt32(dataGridViewServices.SelectedRows[0].Cells["ServiceID"].Value);
            string serviceName = dataGridViewServices.SelectedRows[0].Cells["ServiceName"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete the service '{serviceName}'?\n\nThis action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Check if the service has any bookings
                        string checkBookingsQuery = "SELECT COUNT(*) FROM Bookings WHERE ServiceID = @ServiceID";
                        SqlCommand checkCommand = new SqlCommand(checkBookingsQuery, connection);
                        checkCommand.Parameters.AddWithValue("@ServiceID", serviceId);

                        int bookingCount = (int)checkCommand.ExecuteScalar();

                        if (bookingCount > 0)
                        {
                            // If there are bookings, just mark as unavailable instead of deleting
                            string deactivateQuery = "UPDATE Services SET IsAvailable = 0 WHERE ServiceID = @ServiceID";
                            SqlCommand deactivateCommand = new SqlCommand(deactivateQuery, connection);
                            deactivateCommand.Parameters.AddWithValue("@ServiceID", serviceId);
                            deactivateCommand.ExecuteNonQuery();

                            MessageBox.Show(
                                "This service has existing bookings and cannot be deleted. It has been marked as unavailable instead.",
                                "Service Deactivated",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                        else
                        {
                            // If no bookings, delete related records and the service
                            string deleteAvailabilityQuery = "DELETE FROM ServiceAvailability WHERE ServiceID = @ServiceID";
                            SqlCommand deleteAvailabilityCommand = new SqlCommand(deleteAvailabilityQuery, connection);
                            deleteAvailabilityCommand.Parameters.AddWithValue("@ServiceID", serviceId);
                            deleteAvailabilityCommand.ExecuteNonQuery();

                            string deleteImagesQuery = "DELETE FROM ServiceImages WHERE ServiceID = @ServiceID";
                            SqlCommand deleteImagesCommand = new SqlCommand(deleteImagesQuery, connection);
                            deleteImagesCommand.Parameters.AddWithValue("@ServiceID", serviceId);
                            deleteImagesCommand.ExecuteNonQuery();

                            string deleteServiceQuery = "DELETE FROM Services WHERE ServiceID = @ServiceID";
                            SqlCommand deleteServiceCommand = new SqlCommand(deleteServiceQuery, connection);
                            deleteServiceCommand.Parameters.AddWithValue("@ServiceID", serviceId);
                            deleteServiceCommand.ExecuteNonQuery();

                            MessageBox.Show(
                                "Service has been deleted successfully.",
                                "Delete Successful",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }

                        // Refresh the service list
                        LoadServiceList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting service: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Helper Methods
        private bool ValidateServiceForm()
        {
            // Check if service name is empty
            if (string.IsNullOrWhiteSpace(textBoxServiceName.Text))
            {
                MessageBox.Show("Please enter a service name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxServiceName.Focus();
                return false;
            }

            // Check if description is empty
            if (string.IsNullOrWhiteSpace(richTextBoxDescription.Text))
            {
                MessageBox.Show("Please enter a service description.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                richTextBoxDescription.Focus();
                return false;
            }

            // Check if price is set
            if (numericUpDownPrice.Value <= 0)
            {
                MessageBox.Show("Please set a valid price for the service.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDownPrice.Focus();
                return false;
            }

            // Check if a service type is selected
            if (comboBoxServiceType.SelectedItem == null)
            {
                MessageBox.Show("Please select a service type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxServiceType.Focus();
                return false;
            }

            // Check if terms and policies are provided
            if (string.IsNullOrWhiteSpace(richTextBoxTermsAndPolicies.Text))
            {
                MessageBox.Show("Please enter terms and policies for the service.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                richTextBoxTermsAndPolicies.Focus();
                return false;
            }

            return true;
        }

        private void SaveServiceImage(int serviceId, string imagePath)
        {
            try
            {
                // Read the image file into a byte array
                byte[] imageData = File.ReadAllBytes(imagePath);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertImageQuery = @"
                        INSERT INTO ServiceImages (ServiceID, ImageData, IsPrimary, UploadDate)
                        VALUES (@ServiceID, @ImageData, 1, GETDATE())";

                    SqlCommand command = new SqlCommand(insertImageQuery, connection);
                    command.Parameters.AddWithValue("@ServiceID", serviceId);
                    command.Parameters.AddWithValue("@ImageData", imageData);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving service image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAvailabilityDates(int serviceId, SqlConnection connection)
        {
            // Get the selected date range
            DateTime startDate = monthCalendarAvailability.SelectionStart;
            DateTime endDate = monthCalendarAvailability.SelectionEnd;

            // Create a command to insert availability dates
            string insertAvailabilityQuery = @"
                INSERT INTO ServiceAvailability (ServiceID, AvailableDate, IsAvailable)
                VALUES (@ServiceID, @AvailableDate, 1)";

            SqlCommand command = new SqlCommand(insertAvailabilityQuery, connection);
            command.Parameters.Add("@ServiceID", SqlDbType.Int);
            command.Parameters.Add("@AvailableDate", SqlDbType.Date);

            command.Parameters["@ServiceID"].Value = serviceId;

            // Loop through each date in the range and insert it
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                command.Parameters["@AvailableDate"].Value = date;
                command.ExecuteNonQuery();
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void lblFormTitle_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendarAvailability_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void numericUpDownCapacity_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBoxServiceDetails_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridViewServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPageManageServices_Click(object sender, EventArgs e)
        {

        }
    }

    // Helper class for ComboBox items with text and value
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
