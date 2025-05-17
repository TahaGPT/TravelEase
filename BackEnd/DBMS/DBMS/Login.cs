using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelEase;
using TravelEase1;
using TravelEase2;
using TravelEase4;

namespace DBMS
{
    public partial class Login : Form
    {
        SqlConnection conn;
        public Login()
        {
            InitializeComponent();

            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("Hotel Provider");
            comboBox1.Items.Add("Tour Operator");
            comboBox1.Items.Add("Traveler");

            comboBox1.SelectedIndex = 0; // Optional: Set default selection
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string role = comboBox1.SelectedItem.ToString();
            string username = use.Text;
            string password = pass.Text;

            this.Hide();
            if (role == "Admin")
            {
                bool isAuthenticated = false;
                conn = new SqlConnection("Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False");
                conn.Open();
                string query = "SELECT COUNT(*) FROM Admin WHERE username = @Email AND password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", username);
                cmd.Parameters.AddWithValue("@Password", password);
                int count = (int)cmd.ExecuteScalar();
                isAuthenticated = count > 0;
                if (!isAuthenticated)
                {
                    MessageBox.Show("Invalid credentials or account not approved", "Error");
                    this.Close();
                    return;
                }
            
                AdminDashboard admin = new AdminDashboard(username, password);
                admin.Show();
            }
            else if (role == "Service Provider")
            {
                bool isAuthenticated = false;
                conn = new SqlConnection("Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False");
                conn.Open();
                string query = "SELECT COUNT(*) FROM ServiceProvider WHERE provider_id = @Email AND password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", username);
                cmd.Parameters.AddWithValue("@Password", password);
                int count = (int)cmd.ExecuteScalar();
                isAuthenticated = count > 0;
                if (!isAuthenticated)
                {
                    MessageBox.Show("Invalid credentials or account not approved", "Error");
                    this.Close();
                    return;
                }
                int u = int.Parse(username);
                HotelProviderDashboard travel = new HotelProviderDashboard(u);
                travel.Show();

            }
            else if (role == "Tour Operator")
            {
                bool isAuthenticated = false;
                conn = new SqlConnection("Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False");
                conn.Open();
                string query = "SELECT COUNT(*) FROM TourOperator WHERE operator_id = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", username);
                //cmd.Parameters.AddWithValue("@Password", password);
                int count = (int)cmd.ExecuteScalar();
                isAuthenticated = count > 0;
                if (!isAuthenticated)
                {
                    MessageBox.Show("Invalid credentials or account not approved", "Error");
                    this.Close();
                    return;
                }
                // convert username to int
                int u = int.Parse(username);
                TourOperatorDashboard tour = new TourOperatorDashboard(u, password);
                tour.Show();
            }
            else if (role == "Traveler")
            {
                bool isAuthenticated = false;
                conn = new SqlConnection("Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False");
                conn.Open();
                string query = "SELECT COUNT(*) FROM Traveler WHERE email = @Email AND password = @Password AND is_approved = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", username);
                cmd.Parameters.AddWithValue("@Password", password);
                int count = (int)cmd.ExecuteScalar();
                isAuthenticated = count > 0;
                if (!isAuthenticated)
                {
                    MessageBox.Show("Invalid credentials or account not approved", "Error");
                    this.Close();
                    return;
                }
                TravelerDashboard tour = new TravelerDashboard(username, password);
                tour.Show();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp sign = new SignUp();
            sign.Show();
            this.Hide();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
