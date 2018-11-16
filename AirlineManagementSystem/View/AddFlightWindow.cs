using AirportManagerSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirportManagerSystem.View
{
    public partial class AddFlightWindow : Form
    {
        public AddFlightWindow()
        {
            InitializeComponent();
            dtpDate.Value = DateTime.Now.AddDays(1);
        }

        public FlightManagementWindow ManageWindow { get; internal set; }

        private void AddFlightWindow_Load(object sender, EventArgs e)
        {
            cbFrom.DataSource = Db.Context.Airports.ToList();
            cbFrom.DisplayMember = "IATACode";

            cbTo.DataSource = Db.Context.Airports.ToList();
            cbTo.DisplayMember = "IATACode";

            cbAircraft.DataSource = Db.Context.Aircrafts.ToList();
            cbAircraft.DisplayMember = "Name";

            var flightNumbers = Db.Context.Schedules.Select(t => t.FlightNumber).Distinct().ToList();
            cbFlightNumber.DataSource = flightNumbers;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbFrom.Text == cbTo.Text)
            {
                MessageBox.Show("Departure airport and arrival airport cannot be the same", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpDate.Value.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("Date of new flight can only after today", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Db.Context.Schedules.Where(t => t.Date == dtpDate.Value.Date && t.FlightNumber == cbFlightNumber.Text).FirstOrDefault() != null)
            {
                MessageBox.Show($"Duplicate schedule with date {dtpDate.Value.ToString("dd/MM/yyyy")} and flight number {cbFlightNumber.Text}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var flight = new Schedule();
            var from = cbFrom.Text;
            var to = cbTo.Text;

            flight.FlightNumber = cbFlightNumber.Text;
            flight.Aircraft = cbAircraft.SelectedItem as Aircraft;
            flight.RouteID = Db.Context.Routes.Where(t => t.Airport.IATACode == from && t.Airport1.IATACode == to).FirstOrDefault().ID;
            flight.Date = dtpDate.Value.Date;
            flight.Time = dtpTime.Value.TimeOfDay;
            flight.EconomyPrice = (int)nudPrice.Value;

            Db.Context.SaveChanges();
            ManageWindow.LoadFlights();
            MessageBox.Show("Edit flight Successful", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
