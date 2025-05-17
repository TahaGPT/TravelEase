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

namespace TravelEase
{

    public partial class search : Form
    {
        string username;
        public search(string u)
        {

            SqlConnection conn;

            SqlCommand cm;
            InitializeComponent();
            conn = new SqlConnection("Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT trip_type FROM Trip", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBoxActivityType.Items.Add(dr["trip_type"].ToString());
            }
            dr.Close();
            /*comboBox1.Items.Add("Adventure");
            comboBox1.Items.Add("Cultural");
            comboBox1.Items.Add("Beach");
            comboBox1.Items.Add("Wildlife Safari");
            comboBox1.Items.Add("Urban Exploration");
            comboBox1.Items.Add("Mountain Trekking");
            comboBox1.Items.Add("Historical Tour");
            comboBox1.Items.Add("Culinary Experience");
            comboBox1.Items.Add("Wellness Retreat");
            comboBox1.Items.Add("Island Hopping");
            comboBox1.Items.Add("Photography Tour");
            comboBox1.Items.Add("Eco Tourism");
            comboBox1.Items.Add("Luxury Travel");
            comboBox1.Items.Add("Budget Travel");
            comboBox1.Items.Add("Family Friendly");
            comboBox1.Items.Add("Solo Travel");
            comboBox1.Items.Add("Honeymoon");
            comboBox1.Items.Add("Educational Tour");
            comboBox1.Items.Add("Religious Pilgrimage");
            comboBox1.Items.Add("Music Festival");
            comboBox1.Items.Add("Sports Event");
            comboBox1.Items.Add("Cruise");
            comboBox1.Items.Add("Road Trip");
            comboBox1.Items.Add("Camping");
            comboBox1.Items.Add("Winter Sports");
            comboBox1.Items.Add("Diving & Snorkeling");
            comboBox1.Items.Add("Hiking");
            comboBox1.Items.Add("Wine Tour");
            comboBox1.Items.Add("Volunteer Travel");
            comboBox1.Items.Add("Archaeological Tour");
            comboBox1.Items.Add("Backpacking");
            comboBox1.Items.Add("National Parks");
            comboBox1.Items.Add("UNESCO Heritage Sites");
            comboBox1.Items.Add("Festival & Celebrations");
            comboBox1.Items.Add("Art & Museum Tour");
            comboBox1.Items.Add("Desert Safari");
            comboBox1.Items.Add("Cycling Tour");
            comboBox1.Items.Add("Sailing");
            comboBox1.Items.Add("Bird Watching");
            comboBox1.Items.Add("Motorcycle Tour");
            comboBox1.Items.Add("Fishing Trip");
            comboBox1.Items.Add("Golf Tour");
            comboBox1.Items.Add("Spiritual Retreat");
            comboBox1.Items.Add("Rainforest Adventure");
            comboBox1.Items.Add("Farm Stay");
            comboBox1.Items.Add("Architectural Tour");
            comboBox1.Items.Add("Scenic Rail Journey");
            comboBox1.Items.Add("Yoga Retreat");
            comboBox1.Items.Add("Hot Air Balloon Tour");
            comboBox1.Items.Add("Northern Lights");*/
            username = u;
            comboBoxActivityType.SelectedIndex = 0;

        }
        public DataTable GetTripsFromDatabase()
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT title, destination, price_per_person, start_date FROM Trip";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void search_Load(object sender, EventArgs e)
        {
            selectedTripsGrid.Columns.Clear();
            selectedTripsGrid.Rows.Clear();
            selectedTripsGrid.AllowUserToAddRows = false;

            selectedTripsGrid.Columns.Add("title", "Trip Name");
            selectedTripsGrid.Columns.Add("destination", "Destination");
            selectedTripsGrid.Columns.Add("price_per_person", "Price");
            selectedTripsGrid.Columns.Add("start_date", "Start Date");

            DataGridViewButtonColumn viewCol = new DataGridViewButtonColumn();
            viewCol.Name = "ViewDetailsColumn";
            viewCol.Text = "View Details";
            viewCol.UseColumnTextForButtonValue = true;
            selectedTripsGrid.Columns.Add(viewCol);

            DataGridViewButtonColumn bookCol = new DataGridViewButtonColumn();
            bookCol.Name = "BookNowColumn";
            bookCol.Text = "Book Now";
            bookCol.UseColumnTextForButtonValue = true;
            selectedTripsGrid.Columns.Add(bookCol);

            // 🔽 Fetch from database instead of hardcoding
            DataTable trips = GetTripsFromDatabase();
            foreach (DataRow row in trips.Rows)
            {
                selectedTripsGrid.Rows.Add(
                    row["title"].ToString(),
                    row["destination"].ToString(),
                    row["price_per_person"].ToString(),
                    Convert.ToDateTime(row["start_date"]).ToShortDateString()
                );
            }

            selectedTripsGrid.CellPainting += selectedTripsGrid_CellPainting;
        }



        private void selectedTripsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = selectedTripsGrid.Columns[e.ColumnIndex].Name;
            string tripName = selectedTripsGrid.Rows[e.RowIndex].Cells["title"].Value.ToString();
            string destination = selectedTripsGrid.Rows[e.RowIndex].Cells["destination"].Value.ToString();
            decimal price = Convert.ToDecimal(selectedTripsGrid.Rows[e.RowIndex].Cells["price_per_person"].Value);
            DateTime startDate = Convert.ToDateTime(selectedTripsGrid.Rows[e.RowIndex].Cells["start_date"].Value);

            if (columnName == "ViewDetailsColumn")
            {
                ViewTripDetailsForm f = new ViewTripDetailsForm(tripName, destination, price, startDate, username);
                f.ShowDialog();
            }
            else if (columnName == "BookNowColumn")
            {
                TripBookingForm f = new TripBookingForm(tripName, destination, price, startDate, username);
                f.ShowDialog();
            }
        }
        private void selectedTripsGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
{
    // Make sure it's not the header row
    if (e.RowIndex < 0) return;

    // Check if the column is a button column (ViewDetailsColumn or BookNowColumn)
    if (selectedTripsGrid.Columns[e.ColumnIndex].Name == "ViewDetailsColumn" ||
        selectedTripsGrid.Columns[e.ColumnIndex].Name == "BookNowColumn")
    {
        e.PaintBackground(e.CellBounds, true); // Paint the background first

        // Check for the specific button column
        if (selectedTripsGrid.Columns[e.ColumnIndex].Name == "ViewDetailsColumn")
        {
            // Red background with white text for "View Details"
            using (SolidBrush brush = new SolidBrush(Color.Red))
            {
                e.Graphics.FillRectangle(brush, e.CellBounds);
            }
            using (SolidBrush textBrush = new SolidBrush(Color.White))
            {
                e.Graphics.DrawString("View Details", e.CellStyle.Font, textBrush, e.CellBounds);
            }
        }
        else if (selectedTripsGrid.Columns[e.ColumnIndex].Name == "BookNowColumn")
        {
            // Green background with white text for "Book Now"
            using (SolidBrush brush = new SolidBrush(Color.Green))
            {
                e.Graphics.FillRectangle(brush, e.CellBounds);
            }
            using (SolidBrush textBrush = new SolidBrush(Color.White))
            {
                e.Graphics.DrawString("Book Now", e.CellStyle.Font, textBrush, e.CellBounds);
            }
        }

        e.Handled = true; // Indicate that painting is handled
    }
}

        private void label3_Click(object sender, EventArgs e)
        {

        }



        public DataTable GetTripsFromDatabaseFiltered(string tripType, int? minPrice, int? maxPrice, DateTime? startDate, DateTime? endDate, int? minGroupSize, int? maxGroupSize)
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=DESKTOP-2I8F60O\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<string> conditions = new List<string>();
                SqlCommand cmd = new SqlCommand();

                string query = "SELECT title AS TripName, destination, price_per_person AS Price, start_date, end_date FROM Trip WHERE 1=1";

                if (!string.IsNullOrEmpty(tripType))
                {
                    conditions.Add("trip_type = @TripType");
                    cmd.Parameters.AddWithValue("@TripType", tripType);
                }
                if (minPrice.HasValue)
                {
                    conditions.Add("price_per_person >= @MinPrice");
                    cmd.Parameters.AddWithValue("@MinPrice", minPrice.Value);
                }
                if (maxPrice.HasValue)
                {
                    conditions.Add("price_per_person <= @MaxPrice");
                    cmd.Parameters.AddWithValue("@MaxPrice", maxPrice.Value);
                }
                if (startDate.HasValue)
                {
                    conditions.Add("start_date >= @StartDate");
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                }
                if (endDate.HasValue)
                {
                    conditions.Add("end_date <= @EndDate");
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Value);
                }
                if (minGroupSize.HasValue)
                {
                    conditions.Add("min_group_size >= @MinGroupSize");
                    cmd.Parameters.AddWithValue("@MinGroupSize", minGroupSize.Value);
                }
                if (maxGroupSize.HasValue)
                {
                    conditions.Add("max_group_size <= @MaxGroupSize");
                    cmd.Parameters.AddWithValue("@MaxGroupSize", maxGroupSize.Value);
                }

                if (conditions.Any())
                {
                    query += " AND " + string.Join(" AND ", conditions);
                }

                cmd.CommandText = query;
                cmd.Connection = conn;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            return dt;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            selectedTripsGrid.Columns.Clear();
            selectedTripsGrid.Rows.Clear();
            selectedTripsGrid.AllowUserToAddRows = false;

            selectedTripsGrid.Columns.Add("title", "Trip Name");
            selectedTripsGrid.Columns.Add("destination", "Destination");
            selectedTripsGrid.Columns.Add("price_per_person", "Price");
            selectedTripsGrid.Columns.Add("start_date", "Start Date");

            DataGridViewButtonColumn viewCol = new DataGridViewButtonColumn();
            viewCol.Name = "ViewDetailsColumn";
            viewCol.Text = "View Details";
            viewCol.UseColumnTextForButtonValue = true;
            selectedTripsGrid.Columns.Add(viewCol);

            DataGridViewButtonColumn bookCol = new DataGridViewButtonColumn();
            bookCol.Name = "BookNowColumn";
            bookCol.Text = "Book Now";
            bookCol.UseColumnTextForButtonValue = true;
            selectedTripsGrid.Columns.Add(bookCol);

            string tripType = comboBoxActivityType.Text;
            int? minPrice = string.IsNullOrWhiteSpace(textBox2.Text) ? (int?)null : Convert.ToInt32(textBox2.Text);
            int? maxPrice = string.IsNullOrWhiteSpace(max.Text) ? (int?)null : Convert.ToInt32(max.Text);
            DateTime? startDate = datePickerStart.Value;
            DateTime? endDate = datePickerEnd.Value;
            int? minGroup = (int)numericMinGroup.Value;
            int? maxGroup = (int)numericMaxGroup.Value;

            var filteredTrips = GetTripsFromDatabaseFiltered(tripType, minPrice, maxPrice, startDate, endDate, minGroup, maxGroup);

            selectedTripsGrid.Rows.Clear();
            foreach (DataRow row in filteredTrips.Rows)
            {
                selectedTripsGrid.Rows.Add(
                    row["TripName"].ToString(),
                    row["destination"].ToString(),
                    row["Price"].ToString(),
                    Convert.ToDateTime(row["StartDate"]).ToShortDateString()
                );
            }

            selectedTripsGrid.CellPainting += selectedTripsGrid_CellPainting;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
