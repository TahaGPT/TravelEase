using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TravelEase
{
    public partial class TripBookingForm : Form
    {
        private decimal price1;
        private string tname2;
        int tripID;
        int travelerID;
        string username;

        public TripBookingForm(string name, string destination, decimal price, DateTime startDate, string u)
        {
            InitializeComponent();
            username = u;
            tname2 = name;
            tname.Text = "Trip Name: " + name;
            dest.Text = "Destination: " + destination;
            pp.Text = "Price per Person:PKR" + price.ToString("N2");
            sd.Text = "Start Date: " + startDate.ToShortDateString();
            price1 = price;

            pm2.Items.AddRange(new string[] { "Credit Card", "JazzCash", "EasyPaisa", "Cash" });
            pm2.SelectedIndex = 0;

            messagetxt.Visible = false;
            UpdateTotalCost(); // initialize
        }

        private void nump_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotalCost();
        }

        private void UpdateTotalCost()
        {
            int persons = (int)nump.Value;
            decimal total = price1 * persons;
            tamount.Text = "Total Cost: PKR" + total.ToString("N2");
        }

        private bool InsertBooking(int totalAmountint, int groupSize, int tripId, int travelerId)
        {
            string connectionString = "Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Booking
            (status, total_amount, Group_Size, trip_id, traveler_id)
            VALUES
            (@Status, @TotalAmount, @GroupSize, @TripId, @TravelerId)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmountint);
                    cmd.Parameters.AddWithValue("@GroupSize", groupSize);

                    
                    cmd.Parameters.AddWithValue("@TripId", tripId);
                    cmd.Parameters.AddWithValue("@TravelerId", travelerId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        private void confirmbut_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False");
            if (pm2.SelectedItem.ToString() == "Credit Card" && string.IsNullOrWhiteSpace(cnum2.Text))
            {
                MessageBox.Show("Please enter a card number for credit card payments.");
                return;
            }

            try
            {
                string title = tname.Text.Trim();
                int totalAmount = int.Parse(tamount.Text);
                int groupSize = int.Parse(nump.Text);


                string travIdQuery = "SELECT trip_id FROM Trip WHERE title = @TravelerId";
                using (SqlCommand cmd = new SqlCommand(travIdQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@TravelerId", title);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        tripID = (int)reader["trip_id"];
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
                        travelerID = (int)reader["traveler_id"];
                    }
                    reader.Close();
                }

                bool success = InsertBooking(totalAmount, groupSize,tripID, travelerID);

                if (success)
                    MessageBox.Show("Booking inserted successfully!");
                else
                    MessageBox.Show("Booking insertion failed.");
            }
            catch (FormatException fe)
            {
                MessageBox.Show("Please enter valid numeric values for amount, group size, IDs.\n\n" + fe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred.\n\n" + ex.Message);
            }

            messagetxt.Text = "Booking Confirmed!";
            messagetxt.Visible = true;
            confirmbut.Enabled = false;
        }

        private void messagetxt_Click(object sender, EventArgs e)
        {
            // Optional: hide message or add more logic
        }

        private void TripBookingForm_Load(object sender, EventArgs e)
        {
            // Optional: additional load logic
        }

        private void p_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sd_Click(object sender, EventArgs e)
        {

        }

        private void tamount_Click(object sender, EventArgs e)
        {

        }
    }
}
