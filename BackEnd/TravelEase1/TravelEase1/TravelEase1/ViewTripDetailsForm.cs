
using System;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class ViewTripDetailsForm : Form
    {
        private string tripName;
        private decimal price;
        private DateTime startDate;
        private string destination;
        string username;

        public ViewTripDetailsForm(string name, string dest, decimal tripPrice, DateTime date, string u)
        {
            InitializeComponent();
            tripName = name;
            price = tripPrice;
            startDate = date;
            destination = dest;
            username = u;
            tp.Text = "Trip: " + name;
            this.dest.Text = "Destination: " + dest;
            pr.Text = "Price: " + price.ToString("C");
            sd.Text = "Start Date: " + startDate.ToShortDateString();
            descrip.Text = "A wonderful trip to " + dest + " with scenic views and guided tours.";

            // Dummy ratings
            gridrating.Rows.Add("Ali", 4, "Loved it!");
            gridrating.Rows.Add("Fatima", 5, "Amazing experience.");
            gridrating.Rows.Add("John", 3, "Could be better.");
            this.username = username;
        }

        private void bn_Click(object sender, EventArgs e)
        {
            TripBookingForm bookingForm = new TripBookingForm(tripName, destination, price, startDate, username);
            bookingForm.ShowDialog();
        }

        private void backout_Click(object sender, EventArgs e)
        {
            this.Close();
            TravelerDashboard dash = new TravelerDashboard();
            dash.Show();
        }

        private void sd_Click(object sender, EventArgs e)
        {

        }

        private void gridrating_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
