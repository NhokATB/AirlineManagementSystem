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
    public partial class MyCommissionWindow : Form
    {
        public MyCommissionWindow()
        {
            InitializeComponent();
        }

        UserReport userReport;
        public string Type { get; internal set; }
        public string User { get; internal set; }

        private void FrmDetail_Load(object sender, EventArgs e)
        {
            var years = Db.Context.Schedules.Select(t => t.Date.Year).Distinct().OrderBy(t => t).ToList();
            cbYear.DataSource = years;

            var user = Db.Context.Users.FirstOrDefault(t => t.FirstName + " " + t.LastName == User);
            userReport = new UserReport
            {
                User = user,
                Tickets = user.Tickets.Where(k => k.Confirmed).ToList(),
                Commission = 0.0
            };

            cbCriterias.SelectedIndex = 0;
            this.Text = $"{Type} detail of {User}";

            this.Cursor = Cursors.Default;
        }

        private void LoadChart()
        {
            chartDetail.Series[0].Points.Clear();

            if (rdbByDate.Checked)
                LoadChartByDateInMonth();
            else if (rdbByMonth.Checked)
                LoadChartByMonthInYear();
            else LoadChartByYear();
        }
        private void LoadChartByYear()
        {
            var years = Db.Context.Schedules.Select(t => t.Date.Year).Distinct().OrderBy(t => t).ToList();

            foreach (var item in years)
            {
                if (Type.Contains("Amenities Sold"))
                {
                    chartDetail.Series[0].Points.AddXY(item.ToString(), GetAmen(0, item));
                }
                else if (Type.Contains("Tickets Sold"))
                {
                    chartDetail.Series[0].Points.AddXY(item.ToString(), GetTicket(0, item));
                }
                else
                {
                    chartDetail.Series[0].Points.AddXY(item.ToString(), GetCommission(0, item));
                }
            }

            chartDetail.ChartAreas[0].AxisX.Interval = 1;

            var value = "";
            if (Type.Contains("Amenities Sold"))
            {
                value = userReport.Tickets.ToList().SelectMany(t => t.AmenitiesTickets).Count().ToString();
            }
            else if (Type.Contains("Tickets Sold"))
            {
                value = userReport.Tickets.Count().ToString();
            }
            else
            {
                var tickets = userReport.Tickets.ToList();
                value = UpdateCommission(tickets).ToString("C0");
            }

            chartDetail.Series[0].Name = $"{Type} at All time";
        }

        private void LoadChartByMonthInYear()
        {
            var date = dtpDate.Value.Date;

            var year = int.Parse(cbYear.Text);

            for (int i = 1; i < 13; i++)
            {
                if (Type.Contains("Amenities Sold"))
                {
                    chartDetail.Series[0].Points.AddXY(i.ToString("00"), GetAmen(i, year));
                }
                else if (Type.Contains("Tickets Sold"))
                {
                    chartDetail.Series[0].Points.AddXY(i.ToString("00"), GetTicket(i, year));
                }
                else
                {
                    chartDetail.Series[0].Points.AddXY(i.ToString("00"), GetCommission(i, year));
                }
            }

            chartDetail.ChartAreas[0].AxisX.Interval = 1;

            var value = "";
            if (Type.Contains("Amenities Sold"))
            {
                value = userReport.Tickets.Where(t => t.Schedule.Date.Year == year).ToList().SelectMany(t => t.AmenitiesTickets).Count().ToString();
            }
            else if (Type.Contains("Tickets Sold"))
            {
                value = userReport.Tickets.Where(t => t.Schedule.Date.Year == year).Count().ToString();
            }
            else
            {
                var tickets = userReport.Tickets.Where(t => t.Schedule.Date.Year == year).ToList();
                value = UpdateCommission(tickets).ToString("C0");
            }

            chartDetail.Series[0].Name = $"{Type} at {cbYear.Text}";
        }
        private void LoadChartByDateInMonth()
        {
            var date = dtpDate.Value.Date;

            var from = new DateTime(date.Year, date.Month, 1);
            var to = from.AddMonths(1);

            for (DateTime i = from; i < to; i = i.AddDays(1))
            {
                if (Type.Contains("Amenities Sold"))
                {
                    chartDetail.Series[0].Points.AddXY(i.ToString("dd"), GetAmen(i));
                }
                else if (Type.Contains("Tickets Sold"))
                {
                    chartDetail.Series[0].Points.AddXY(i.ToString("dd"), GetTicket(i));
                }
                else
                {
                    chartDetail.Series[0].Points.AddXY(i.ToString("dd"), GetCommission(i));
                }
            }

            chartDetail.ChartAreas[0].AxisX.Interval = 1;

            var value = "";
            if (Type.Contains("Amenities Sold"))
            {
                value = userReport.Tickets.Where(t => t.Schedule.Date.Month == date.Month && t.Schedule.Date.Year == date.Year).ToList().SelectMany(t => t.AmenitiesTickets).Count().ToString();
            }
            else if (Type.Contains("Tickets Sold"))
            {
                value = userReport.Tickets.Where(t => t.Schedule.Date.Month == date.Month && t.Schedule.Date.Year == date.Year).Count().ToString();
            }
            else
            {
                var tickets = userReport.Tickets.Where(t => t.Schedule.Date.Month == date.Month && t.Schedule.Date.Year == date.Year).ToList();
                value = UpdateCommission(tickets).ToString("C0");
            }

            chartDetail.Series[0].Name = $"{Type} at {dtpDate.Value.ToString("MM/yyyy")}";
        }

        private double GetCommission(DateTime i)
        {
            var tickets = userReport.Tickets.Where(t => t.Schedule.Date == i).ToList();
            var revenue = UpdateCommission(tickets);
            return revenue;
        }
        private double GetCommission(int month, int year)
        {
            if (month != 0)
            {
                var tickets = userReport.Tickets.Where(t => t.Schedule.Date.Month == month && t.Schedule.Date.Year == year).ToList();
                var revenue = UpdateCommission(tickets);
                return revenue;
            }
            else
            {
                var tickets = userReport.Tickets.Where(t => t.Schedule.Date.Year == year).ToList();
                var revenue = UpdateCommission(tickets);
                return revenue;
            }
        }

        private double UpdateCommission(List<Ticket> tickets)
        {
            double revenue = 0;
            revenue += FlightForBooking.GetPrice(tickets.Where(t=>t.CabinTypeID == 1).ToList(), 1);
            revenue += FlightForBooking.GetPrice(tickets.Where(t => t.CabinTypeID == 2).ToList(), 2);
            revenue += FlightForBooking.GetPrice(tickets.Where(t => t.CabinTypeID == 3).ToList(), 3);

            revenue += tickets.Sum(t => t.AmenitiesTickets.Sum(k => (int)k.Price));

            return revenue * 0.003;
        }

        private double GetTicket(DateTime i)
        {
            return userReport.Tickets.Count(t => t.Confirmed && t.Schedule.Date == i);
        }
        private double GetTicket(int month, int year)
        {
            if (month != 0)
                return userReport.Tickets.Count(t => t.Confirmed && t.Schedule.Date.Month == month && t.Schedule.Date.Year == year);
            else
                return userReport.Tickets.Count(t => t.Confirmed && t.Schedule.Date.Year == year);
        }

        private double GetAmen(DateTime i)
        {
            return userReport.Tickets.Where(t => t.Schedule.Date == i).SelectMany(t => t.AmenitiesTickets).Count();
        }
        private double GetAmen(int month, int year)
        {
            if (month != 0)
                return userReport.Tickets.Where(t => t.Schedule.Date.Month == month && t.Schedule.Date.Year == year).SelectMany(t => t.AmenitiesTickets).Count();
            else
                return userReport.Tickets.Where(t => t.Schedule.Date.Year == year).SelectMany(t => t.AmenitiesTickets).Count();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            LoadChart();
            this.Cursor = Cursors.Default;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type = cbCriterias.Text;
            chartDetail.Series[0].Points.Clear();
        }

        private void rdbByDate_CheckedChanged(object sender, EventArgs e)
        {
            RdbCheckedChanged();
        }

        private void rdbByMonth_CheckedChanged(object sender, EventArgs e)
        {
            RdbCheckedChanged();
        }

        private void rdbByYear_CheckedChanged(object sender, EventArgs e)
        {
            RdbCheckedChanged();
        }
        private void RdbCheckedChanged()
        {
            if (rdbByDate.Checked)
            {
                cbYear.Enabled = false;
                dtpDate.Enabled = true;
            }
            else if (rdbByMonth.Checked)
            {
                cbYear.Enabled = true;
                dtpDate.Enabled = false;
            }
            else
            {
                cbYear.Enabled = false;
                dtpDate.Enabled = false;
            }
        }
    }
}
