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

namespace DBMS
{
    public partial class SignUp : Form
    {
        SqlConnection conn;
        public SignUp()
        {
            conn = new SqlConnection("Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False");
            InitializeComponent();
            comboBox1.Items.Add("Admin");
            /*comboBox1.Items.Add("Hotel Provider");
            comboBox1.Items.Add("Tour Operator");*/
            comboBox1.Items.Add("Traveler");

            comboBox1.SelectedIndex = 0; // Optional: Set default selection
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string role = comboBox1.SelectedItem.ToString();
            /*string username = email.Text;
            string password = this.password.Text;*/

            this.Hide();
            if (role == "Admin")
            {
                AdminDashboard admin = new AdminDashboard("", "");
                admin.Show();
            }
            /*else if (role == "Hotel Provider")
            {
                HotelProviderDashboard travel = new HotelProviderDashboard();
                travel.Show();

            }
            else if (role == "Tour Operator")
            {
                TourOperatorDashboard tour = new TourOperatorDashboard("", "");
                tour.Show();
            }*/
            else if (role == "Traveler")
            {
                string query = @"INSERT INTO Traveler (name, email, password, age, nationality)
                         VALUES (@Name, @Email, @Password, @Age, @Nationality)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name.Text);
                    cmd.Parameters.AddWithValue("@Email", email.Text);
                    cmd.Parameters.AddWithValue("@Password", password.Text);
                    cmd.Parameters.AddWithValue("@Age", int.Parse(age.Text));
                    cmd.Parameters.AddWithValue("@Nationality", nation.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Failed to register traveler.", "Error");
                    }
                }
                ChangePrefer tour = new ChangePrefer(email.Text, password.Text);
                tour.Show();
            }
        }

        private void email_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
