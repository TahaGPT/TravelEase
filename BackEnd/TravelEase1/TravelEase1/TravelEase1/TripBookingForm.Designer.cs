
namespace TravelEase
{
    partial class TripBookingForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label tname;
        private System.Windows.Forms.Label dest;
        private System.Windows.Forms.Label pp;
        private System.Windows.Forms.Label sd;
        private System.Windows.Forms.NumericUpDown nump;
        private System.Windows.Forms.ComboBox pm2;
        private System.Windows.Forms.TextBox cnum2;
        private System.Windows.Forms.TextBox txtCNIC;
        private System.Windows.Forms.Label tamount;
        private System.Windows.Forms.Label p;
        private System.Windows.Forms.Label cnum;
        private System.Windows.Forms.Label cnicc;
        private System.Windows.Forms.Label pm;

        private void InitializeComponent()
        {
            this.tname = new System.Windows.Forms.Label();
            this.dest = new System.Windows.Forms.Label();
            this.pp = new System.Windows.Forms.Label();
            this.sd = new System.Windows.Forms.Label();
            this.p = new System.Windows.Forms.Label();
            this.nump = new System.Windows.Forms.NumericUpDown();
            this.pm = new System.Windows.Forms.Label();
            this.pm2 = new System.Windows.Forms.ComboBox();
            this.cnum = new System.Windows.Forms.Label();
            this.cnum2 = new System.Windows.Forms.TextBox();
            this.cnicc = new System.Windows.Forms.Label();
            this.txtCNIC = new System.Windows.Forms.TextBox();
            this.tamount = new System.Windows.Forms.Label();
            this.messagetxt = new System.Windows.Forms.Label();
            this.confirmbut = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nump)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tname
            // 
            this.tname.Location = new System.Drawing.Point(20, 20);
            this.tname.Name = "tname";
            this.tname.Size = new System.Drawing.Size(400, 20);
            this.tname.TabIndex = 0;
            this.tname.Text = "Trip Name: ";
            // 
            // dest
            // 
            this.dest.Location = new System.Drawing.Point(20, 50);
            this.dest.Name = "dest";
            this.dest.Size = new System.Drawing.Size(400, 20);
            this.dest.TabIndex = 1;
            this.dest.Text = "Destination: ";
            // 
            // pp
            // 
            this.pp.Location = new System.Drawing.Point(20, 80);
            this.pp.Name = "pp";
            this.pp.Size = new System.Drawing.Size(400, 20);
            this.pp.TabIndex = 2;
            this.pp.Text = "Price per Person: ";
            // 
            // sd
            // 
            this.sd.Location = new System.Drawing.Point(20, 110);
            this.sd.Name = "sd";
            this.sd.Size = new System.Drawing.Size(400, 20);
            this.sd.TabIndex = 3;
            this.sd.Text = "Start Date: ";
            this.sd.Click += new System.EventHandler(this.sd_Click);
            // 
            // p
            // 
            this.p.Location = new System.Drawing.Point(20, 150);
            this.p.Name = "p";
            this.p.Size = new System.Drawing.Size(120, 20);
            this.p.TabIndex = 4;
            this.p.Text = "Number of People:";
            this.p.Click += new System.EventHandler(this.p_Click);
            // 
            // nump
            // 
            this.nump.Location = new System.Drawing.Point(160, 150);
            this.nump.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nump.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nump.Name = "nump";
            this.nump.Size = new System.Drawing.Size(60, 26);
            this.nump.TabIndex = 5;
            this.nump.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nump.ValueChanged += new System.EventHandler(this.nump_ValueChanged);
            // 
            // pm
            // 
            this.pm.Location = new System.Drawing.Point(20, 190);
            this.pm.Name = "pm";
            this.pm.Size = new System.Drawing.Size(120, 20);
            this.pm.TabIndex = 6;
            this.pm.Text = "Payment Method:";
            // 
            // pm2
            // 
            this.pm2.Location = new System.Drawing.Point(160, 190);
            this.pm2.Name = "pm2";
            this.pm2.Size = new System.Drawing.Size(200, 28);
            this.pm2.TabIndex = 7;
            // 
            // cnum
            // 
            this.cnum.Location = new System.Drawing.Point(20, 230);
            this.cnum.Name = "cnum";
            this.cnum.Size = new System.Drawing.Size(120, 20);
            this.cnum.TabIndex = 8;
            this.cnum.Text = "Card Number:";
            // 
            // cnum2
            // 
            this.cnum2.Location = new System.Drawing.Point(160, 230);
            this.cnum2.Name = "cnum2";
            this.cnum2.Size = new System.Drawing.Size(200, 26);
            this.cnum2.TabIndex = 9;
            // 
            // cnicc
            // 
            this.cnicc.Location = new System.Drawing.Point(20, 270);
            this.cnicc.Name = "cnicc";
            this.cnicc.Size = new System.Drawing.Size(120, 20);
            this.cnicc.TabIndex = 10;
            this.cnicc.Text = "CNIC:";
            // 
            // txtCNIC
            // 
            this.txtCNIC.Location = new System.Drawing.Point(160, 270);
            this.txtCNIC.Name = "txtCNIC";
            this.txtCNIC.Size = new System.Drawing.Size(200, 26);
            this.txtCNIC.TabIndex = 11;
            // 
            // tamount
            // 
            this.tamount.Location = new System.Drawing.Point(20, 310);
            this.tamount.Name = "tamount";
            this.tamount.Size = new System.Drawing.Size(400, 20);
            this.tamount.TabIndex = 12;
            this.tamount.Text = "Total Cost: ";
            this.tamount.Click += new System.EventHandler(this.tamount_Click);
            // 
            // messagetxt
            // 
            this.messagetxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.messagetxt.ForeColor = System.Drawing.Color.White;
            this.messagetxt.Location = new System.Drawing.Point(157, 401);
            this.messagetxt.Name = "messagetxt";
            this.messagetxt.Size = new System.Drawing.Size(126, 20);
            this.messagetxt.TabIndex = 14;
            this.messagetxt.Text = "Booking Confirmed!";
            // 
            // confirmbut
            // 
            this.confirmbut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.confirmbut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmbut.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.confirmbut.Location = new System.Drawing.Point(380, 441);
            this.confirmbut.Name = "confirmbut";
            this.confirmbut.Size = new System.Drawing.Size(185, 54);
            this.confirmbut.TabIndex = 13;
            this.confirmbut.Text = "Confirm";
            this.confirmbut.UseVisualStyleBackColor = false;
            this.confirmbut.Click += new System.EventHandler(this.confirmbut_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Blackadder ITC", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(880, 42);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(210, 58);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TravelEase";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DeepPink;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(24, 441);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 54);
            this.button1.TabIndex = 15;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.panelHeader.Controls.Add(this.button1);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.confirmbut);
            this.panelHeader.Controls.Add(this.messagetxt);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1195, 860);
            this.panelHeader.TabIndex = 15;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            // 
            // TripBookingForm
            // 
            this.ClientSize = new System.Drawing.Size(1195, 929);
            this.Controls.Add(this.tname);
            this.Controls.Add(this.dest);
            this.Controls.Add(this.pp);
            this.Controls.Add(this.sd);
            this.Controls.Add(this.p);
            this.Controls.Add(this.nump);
            this.Controls.Add(this.pm);
            this.Controls.Add(this.pm2);
            this.Controls.Add(this.cnum);
            this.Controls.Add(this.cnum2);
            this.Controls.Add(this.cnicc);
            this.Controls.Add(this.txtCNIC);
            this.Controls.Add(this.tamount);
            this.Controls.Add(this.panelHeader);
            this.Name = "TripBookingForm";
            this.Text = "Trip Booking";
            this.Load += new System.EventHandler(this.TripBookingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nump)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label messagetxt;
        private System.Windows.Forms.Button confirmbut;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelHeader;
    }
}
