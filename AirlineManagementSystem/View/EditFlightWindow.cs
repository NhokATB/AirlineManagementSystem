using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirportManagerSystem.Model;

namespace AirportManagerSystem.View
{
    public partial class EditFlightWindow : Form
    {
        public EditFlightWindow()
        {
            InitializeComponent();
        }

        public FlightManagementWindow ManageWindow { get; internal set; }
        internal NewFlight Flight { get; set; }

        private void EditFlightWindow_Load(object sender, EventArgs e)
        {
            //lblFrom.Text = Flight.From.IATACode;
            //lblTo.Text = Flight.To.IATACode;
            //lblAircraft.Text = Flight.Aircraft.Name;
            //dtpDate.Value = Flight.Date;
            //dtpTime.Value = Flight.Date + Flight.Time;
            lblFrom.Text = Flight.Schedule.Route.Airport.IATACode;
            lblTo.Text = Flight.Schedule.Route.Airport1.IATACode;
            lblAircraft.Text = Flight.Schedule.Aircraft.Name;
            dtpDate.Value = Flight.Schedule.Date;
            dtpTime.Value = Flight.Schedule.Date + Flight.Schedule.Time;
            nudPrice.Value = (int)Flight.EconomyPrice;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dtpDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Date of new flight can only after today", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpDate.Value.Date == DateTime.Now.Date && dtpTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay)
            {
                MessageBox.Show("Time of flight can only after now", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Flight.Schedule.Date != dtpDate.Value.Date)
            {
                if (Db.Context.Schedules.Where(t => t.Date == dtpDate.Value.Date && t.FlightNumber == Flight.Schedule.FlightNumber).FirstOrDefault() != null)
                {
                    MessageBox.Show($"Duplicate schedule with date {dtpDate.Value.ToString("dd/MM/yyyy")} and flight number {Flight.Schedule.FlightNumber}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Flight.Schedule.Date = dtpDate.Value.Date;
            Flight.Schedule.Time = dtpTime.Value.TimeOfDay;
            Flight.Schedule.EconomyPrice = (int)nudPrice.Value;

            Db.Context.SaveChanges();
            ManageWindow.LoadFlights();
            MessageBox.Show("Edit flight Successful", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
