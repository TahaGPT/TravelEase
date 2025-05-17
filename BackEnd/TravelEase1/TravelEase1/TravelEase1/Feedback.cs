using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelEase;

namespace TravelEase1
{
    public partial class Feedback : Form
    {
        string username;
        string password;
        public Feedback()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TravelerDashboard dash = new TravelerDashboard(username, password);
            dash.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            TravelerDashboard dash = new TravelerDashboard(username, password);
            dash.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Feedback_Load(object sender, EventArgs e)
        {

        }
    }
}
