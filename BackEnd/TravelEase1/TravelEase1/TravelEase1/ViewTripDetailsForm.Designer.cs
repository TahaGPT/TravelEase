
namespace TravelEase
{
    partial class ViewTripDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label tp;
        private System.Windows.Forms.Label dest;
        private System.Windows.Forms.Label pr;
        private System.Windows.Forms.Label sd;
        private System.Windows.Forms.TextBox descrip;
        private System.Windows.Forms.Label rated;
        private System.Windows.Forms.DataGridView gridrating;
        private System.Windows.Forms.Button bn;
        private System.Windows.Forms.Button backout;

        private void InitializeComponent()
        {
            this.tp = new System.Windows.Forms.Label();
            this.dest = new System.Windows.Forms.Label();
            this.pr = new System.Windows.Forms.Label();
            this.sd = new System.Windows.Forms.Label();
            this.descrip = new System.Windows.Forms.TextBox();
            this.rated = new System.Windows.Forms.Label();
            this.gridrating = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bn = new System.Windows.Forms.Button();
            this.backout = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridrating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tp
            // 
            this.tp.Location = new System.Drawing.Point(20, 20);
            this.tp.Name = "tp";
            this.tp.Size = new System.Drawing.Size(400, 20);
            this.tp.TabIndex = 0;
            // 
            // dest
            // 
            this.dest.Location = new System.Drawing.Point(20, 50);
            this.dest.Name = "dest";
            this.dest.Size = new System.Drawing.Size(400, 20);
            this.dest.TabIndex = 1;
            // 
            // pr
            // 
            this.pr.Location = new System.Drawing.Point(20, 80);
            this.pr.Name = "pr";
            this.pr.Size = new System.Drawing.Size(400, 20);
            this.pr.TabIndex = 2;
            // 
            // sd
            // 
            this.sd.Location = new System.Drawing.Point(20, 110);
            this.sd.Name = "sd";
            this.sd.Size = new System.Drawing.Size(400, 20);
            this.sd.TabIndex = 3;
            this.sd.Click += new System.EventHandler(this.sd_Click);
            // 
            // descrip
            // 
            this.descrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.descrip.Location = new System.Drawing.Point(20, 140);
            this.descrip.Multiline = true;
            this.descrip.Name = "descrip";
            this.descrip.ReadOnly = true;
            this.descrip.Size = new System.Drawing.Size(500, 60);
            this.descrip.TabIndex = 4;
            // 
            // rated
            // 
            this.rated.Location = new System.Drawing.Point(20, 210);
            this.rated.Name = "rated";
            this.rated.Size = new System.Drawing.Size(200, 20);
            this.rated.TabIndex = 5;
            this.rated.Text = "Ratings:";
            // 
            // gridrating
            // 
            this.gridrating.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(247)))), ((int)(((byte)(197)))));
            this.gridrating.ColumnHeadersHeight = 29;
            this.gridrating.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.gridrating.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.gridrating.Location = new System.Drawing.Point(20, 240);
            this.gridrating.Name = "gridrating";
            this.gridrating.RowHeadersWidth = 51;
            this.gridrating.Size = new System.Drawing.Size(500, 150);
            this.gridrating.TabIndex = 6;
            this.gridrating.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridrating_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "User";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Rating";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Comment";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // bn
            // 
            this.bn.BackColor = System.Drawing.Color.DodgerBlue;
            this.bn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.bn.Location = new System.Drawing.Point(292, 427);
            this.bn.Name = "bn";
            this.bn.Size = new System.Drawing.Size(143, 43);
            this.bn.TabIndex = 7;
            this.bn.Text = "Book Now";
            this.bn.UseVisualStyleBackColor = false;
            this.bn.Click += new System.EventHandler(this.bn_Click);
            // 
            // backout
            // 
            this.backout.BackColor = System.Drawing.Color.DeepPink;
            this.backout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.backout.Location = new System.Drawing.Point(73, 427);
            this.backout.Name = "backout";
            this.backout.Size = new System.Drawing.Size(126, 43);
            this.backout.TabIndex = 8;
            this.backout.Text = "Back";
            this.backout.UseVisualStyleBackColor = false;
            this.backout.Click += new System.EventHandler(this.backout_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.pictureBox1.Image = global::TravelEase1.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(3, -2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 92);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // ViewTripDetailsForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1195, 929);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tp);
            this.Controls.Add(this.dest);
            this.Controls.Add(this.pr);
            this.Controls.Add(this.sd);
            this.Controls.Add(this.descrip);
            this.Controls.Add(this.rated);
            this.Controls.Add(this.gridrating);
            this.Controls.Add(this.bn);
            this.Controls.Add(this.backout);
            this.Name = "ViewTripDetailsForm";
            this.Text = "Trip Details";
            ((System.ComponentModel.ISupportInitialize)(this.gridrating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
