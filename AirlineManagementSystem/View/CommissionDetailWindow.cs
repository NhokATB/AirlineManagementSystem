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
    public partial class CommissionDetailWindow : Form
    {
        public CommissionDetailWindow()
        {
            InitializeComponent();
        }

        List<UserReport> userReports;
        public string Type { get; internal set; }
        public string User { get; internal set; }

        private void FrmDetail_Load(object sender, EventArgs e)
        {
            var years = Db.Context.Schedules.Select(t => t.Date.Year).Distinct().OrderBy(t => t).ToList();
            cbMonth.DataSource = years;

            var users = Db.Context.Users.Where(t => t.FirstName + " " + t.LastName == User).ToList();
            userReports = users.Select(t => new UserReport
            {
                User = t,
                Amenities = t.Tickets.Where(k => k.Confirmed).SelectMany(k => k.AmenitiesTickets).ToList(),
                Tickets = t.Tickets.Where(k => k.Confirmed).ToList(),
                Commission = 0.0
            }).ToList();

            this.Text += $"{Type} detail of {User}";
        }

        private void LoadChart()
        {
            chart1.Series[0].Points.Clear();

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
                if (Type.Contains("Amen"))
                {
                    chart1.Series[0].Points.AddXY(item.ToString(), GetAmen(0, item));
                }
                else if (Type.Contains("Ticket"))
                {
                    chart1.Series[0].Points.AddXY(item.ToString(), GetTicket(0, item));
                }
                else
                {
                    chart1.Series[0].Points.AddXY(item.ToString(), GetCommission(0, item));
                }
            }

            chart1.ChartAreas[0].AxisX.Interval = 1;

            var value = "";
            if (Type.Contains("Amen"))
            {
                value = userReports.FirstOrDefault().Tickets.ToList().SelectMany(t => t.AmenitiesTickets).Count().ToString();
            }
            else if (Type.Contains("Ticket"))
            {
                value = userReports.FirstOrDefault().Tickets.Count().ToString();
            }
            else
            {
                var tickets = userReports.FirstOrDefault().Tickets.ToList();
                value = UpdateCommission(tickets).ToString("C0");
            }

            chart1.Series[0].Name = $"{Type} at All time";
        }

        private void LoadChartByMonthInYear()
        {
            var date = dtpDate.Value.Date;

            var year = int.Parse(cbMonth.Text);

            for (int i = 1; i < 13; i++)
            {
                if (Type.Contains("Amen"))
                {
                    chart1.Series[0].Points.AddXY(i.ToString("00"), GetAmen(i, year));
                }
                else if (Type.Contains("Ticket"))
                {
                    chart1.Series[0].Points.AddXY(i.ToString("00"), GetTicket(i, year));
                }
                else
                {
                    chart1.Series[0].Points.AddXY(i.ToString("00"), GetCommission(i, year));
                }
            }

            chart1.ChartAreas[0].AxisX.Interval = 1;

            var value = "";
            if (Type.Contains("Amen"))
            {
                value = userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date.Year == year).ToList().SelectMany(t => t.AmenitiesTickets).Count().ToString();
            }
            else if (Type.Contains("Ticket"))
            {
                value = userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date.Year == year).Count().ToString();
            }
            else
            {
                var tickets = userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date.Year == year).ToList();
                value = UpdateCommission(tickets).ToString("C0");
            }

            chart1.Series[0].Name = $"{Type} at {cbMonth.Text}";
        }
        private void LoadChartByDateInMonth()
        {
            var date = dtpDate.Value.Date;

            var from = new DateTime(date.Year, date.Month, 1);
            var to = from.AddMonths(1);

            for (DateTime i = from; i < to; i = i.AddDays(1))
            {
                if (Type.Contains("Amen"))
                {
                    chart1.Series[0].Points.AddXY(i.ToString("dd"), GetAmen(i));
                }
                else if (Type.Contains("Ticket"))
                {
                    chart1.Series[0].Points.AddXY(i.ToString("dd"), GetTicket(i));
                }
                else
                {
                    chart1.Series[0].Points.AddXY(i.ToString("dd"), GetCommission(i));
                }
            }

            chart1.ChartAreas[0].AxisX.Interval = 1;

            var value = "";
            if (Type.Contains("Amen"))
            {
                value = userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date.Month == date.Month && t.Schedule.Date.Year == date.Year).ToList().SelectMany(t => t.AmenitiesTickets).Count().ToString();
            }
            else if (Type.Contains("Ticket"))
            {
                value = userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date.Month == date.Month && t.Schedule.Date.Year == date.Year).Count().ToString();
            }
            else
            {
                var tickets = userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date.Month == date.Month && t.Schedule.Date.Year == date.Year).ToList();
                value = UpdateCommission(tickets).ToString("C0");
            }

            chart1.Series[0].Name = $"{Type} at {dtpDate.Value.ToString("MM/yyyy")}";
        }

        private double GetCommission(DateTime i)
        {
            var tickets = userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date == i).ToList();
            var revenue = UpdateCommission(tickets);
            return revenue;
        }
        private double GetCommission(int month, int year)
        {
            if (month != 0)
            {
                var tickets = userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date.Month == month && t.Schedule.Date.Year == year).ToList();
                var revenue = UpdateCommission(tickets);
                return revenue;
            }
            else
            {
                var tickets = userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date.Year == year).ToList();
                var revenue = UpdateCommission(tickets);
                return revenue;
            }
        }

        private double UpdateCommission(List<Ticket> tickets)
        {
            double revenue = 0;
            foreach (var item in tickets)
            {
                double price = (int)item.Schedule.EconomyPrice;
                double bprice = Math.Floor(price * 1.35);
                double fprice = Math.Floor(bprice * 1.3);

                double amenprice = item.AmenitiesTickets.Sum(t => (int?)t.Price) ?? 0;

                revenue += amenprice;
                revenue += item.CabinTypeID == 1 ? price : (item.CabinTypeID == 2 ? bprice : fprice);
            }

            return revenue * 0.003;

        }

        private double GetTicket(DateTime i)
        {
            return userReports.FirstOrDefault().Tickets.Count(t => t.Confirmed && t.Schedule.Date == i);
        }
        private double GetTicket(int month, int year)
        {
            if (month != 0)
                return userReports.FirstOrDefault().Tickets.Count(t => t.Confirmed && t.Schedule.Date.Month == month && t.Schedule.Date.Year == year);
            else
                return userReports.FirstOrDefault().Tickets.Count(t => t.Confirmed && t.Schedule.Date.Year == year);
        }

        private double GetAmen(DateTime i)
        {
            return userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date == i).SelectMany(t => t.AmenitiesTickets).Count();
        }
        private double GetAmen(int month, int year)
        {
            if (month != 0)
                return userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date.Month == month && t.Schedule.Date.Year == year).SelectMany(t => t.AmenitiesTickets).Count();
            else
                return userReports.FirstOrDefault().Tickets.Where(t => t.Schedule.Date.Year == year).SelectMany(t => t.AmenitiesTickets).Count();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void rdbByMonth_CheckedChanged(object sender, EventArgs e)
        {
            rdbCheckedChanged();
        }

        private void rdbByYear_CheckedChanged(object sender, EventArgs e)
        {
            rdbCheckedChanged();
        }

        private void rdbByDate_CheckedChanged(object sender, EventArgs e)
        {
            rdbCheckedChanged();
        }

        private void rdbCheckedChanged()
        {
            if (rdbByDate.Checked)
            {
                dtpDate.Enabled = true;
                cbMonth.Enabled = false;
            }
            else if (rdbByMonth.Checked)
            {
                dtpDate.Enabled = false;
                cbMonth.Enabled = true;
            }
            else
            {
                dtpDate.Enabled = false;
                cbMonth.Enabled = false;
            }
        }
    }
}
