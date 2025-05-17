using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TravelEase;

namespace TravelEase1
{
    public partial class ProfileSetting : Form
    {
        string username;
        string password;
        int tID;
        
        public ProfileSetting(string u = "", string p = "")
        {
            InitializeComponent();
            username = u;
            password = p;
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False"))
                {
                    conn.Open();

                    // Fetch Traveler data
                    string travelerQuery = "SELECT name, email, age, nationality, password, registration_date FROM Traveler WHERE email = @TravelerId";
                    using (SqlCommand cmd = new SqlCommand(travelerQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TravelerId", username);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            name.Text = reader["name"].ToString();
                            email.Text = reader["email"].ToString();
                            age.Text = reader["age"].ToString();
                            nation.Text = reader["nationality"].ToString();
                            pass.Text = reader["password"].ToString();
                            reg.Text = reader["registration_date"].ToString();

                        }
                        reader.Close();
                    }

                    string travelerIdQuery = "SELECT traveler_id FROM Traveler WHERE email = @TravelerId";
                    using (SqlCommand cmd = new SqlCommand(travelerIdQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TravelerId", username);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            tID = (int)reader["traveler_id"];
                        }
                        reader.Close();
                    }

                    // Fetch Traveler Preferences
                    string preferencesQuery = @"SELECT preferred_trip_type, budget_min, budget_max, 
                                               preferred_group_size, accessibility_needs, 
                                               dietary_restrictions, sustainability_preference 
                                        FROM Traveler_preferences 
                                        WHERE traveler_id = @TravelerId";

                    using (SqlCommand cmd = new SqlCommand(preferencesQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TravelerId", tID);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            trip.Text = reader["preferred_trip_type"].ToString();
                            min.Text = reader["budget_min"].ToString();
                            max.Text = reader["budget_max"].ToString();
                            group.Text = reader["preferred_group_size"].ToString();
                            access.Text = reader["accessibility_needs"].ToString();
                            diet.Text = reader["dietary_restrictions"].ToString();
                            sus.Text = reader["sustainability_preference"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No preferences found for this traveler.");
                        }
                        reader.Close();
                    }
                }
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePrefer pref = new ChangePrefer(username, password);
            this.Close();
            pref.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TravelerDashboard dash = new TravelerDashboard(username, password);
            dash.Show();
            this.Close();
        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
