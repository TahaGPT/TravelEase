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

namespace TravelEase1
{
    public partial class ChangePrefer : Form
    {
        string username;
        string password; 
        int tID;
        public ChangePrefer(string u = "", string p = "")
        {
            InitializeComponent();
            username = u;
            password = p;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False"))
            {
                conn.Open();
                
                // extract travler id
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

                string query = @"INSERT INTO Traveler_preferences 
                            (preferred_trip_type, budget_min, budget_max, preferred_group_size, accessibility_needs, dietary_restrictions, sustainability_preference, traveler_id)
                            VALUES 
                            (@TripType, @BudgetMin, @BudgetMax, @GroupSize, @Accessibility, @Dietary, @Sustainability, @tID)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TripType", trip.Text);
                    cmd.Parameters.AddWithValue("@BudgetMin", int.Parse(min.Text));
                    cmd.Parameters.AddWithValue("@BudgetMax", int.Parse(max.Text));
                    cmd.Parameters.AddWithValue("@GroupSize", int.Parse(group.Text));
                    cmd.Parameters.AddWithValue("@Accessibility", int.Parse(access.Text));
                    cmd.Parameters.AddWithValue("@Dietary", int.Parse(diet.Text));
                    cmd.Parameters.AddWithValue("@Sustainability", int.Parse(sus.Text));
                    cmd.Parameters.AddWithValue("@tID", tID); // Assume you already have this

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Preferences saved successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to save preferences.");
                    }
                }
            }
            ProfileSetting prof = new ProfileSetting(username, password);
            this.Close();
            prof.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void sus_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProfileSetting prof = new ProfileSetting(username, password);
            this.Close();
            prof.Show();
        }
    }
}
