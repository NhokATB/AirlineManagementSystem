using AirportManagerSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for ShortSummaryWindow.xaml
    /// </summary>
    public partial class ShortSummaryWindow : Window
    {
        public ShortSummaryWindow()
        {
            InitializeComponent();
            imgLogo.Source = new BitmapImage(new Uri(@"/AirportManagerSystem;component/Images/WSC2017_TP09_color@4x.png", UriKind.Relative));
            this.Loaded += ShortSummaryWindow_Loaded;
        }

        private void ShortSummaryWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadShortSummary();
        }

        private void LoadShortSummary()
        {
            var start = DateTime.Now;
            var now = start;
            var thirty = now.AddDays(-30);

            var schedules = Db.Context.Schedules.ToList().Where(t => t.Date + t.Time >= thirty && t.Date + t.Time <= now).ToList();
            var schedules1 = Db.Context.Schedules.ToList().Where(t => t.Date >= thirty.Date && t.Date < now.Date && t.Confirmed).ToList();

            tblNumberOfConfirmed.Text = $"Number of confirmed: {schedules.Count(t => t.Confirmed)}";
            tblNumberOfCanceled.Text = $"Number of canceled: {schedules.Count(t => t.Confirmed == false)}";
            tblDailyFlightTime.Text = $"Average of daily flight time: {((schedules1.Sum(k => (int?)k.Route.FlightTime) ?? 0) * 1.0 / 30).ToString("0.00")} minutes";

            var tickets = Db.Context.Tickets.Where(t => t.Schedule.Date >= thirty.Date && t.Schedule.Date < now.Date && t.Confirmed && t.Schedule.Confirmed).ToList();

            var cus = tickets.Select(t => new
            {
                t.Firstname,
                t.Lastname,
                NumTick = tickets.Count(k => k.Firstname == t.Firstname && k.Lastname == t.Lastname && k.PassportNumber == t.PassportNumber)
            }).Distinct().OrderByDescending(t => t.NumTick).ToList();

            try
            {
                tblTopCus1.Text = $"1. {cus[0].Firstname} {cus[0].Lastname} ({cus[0].NumTick} ticket(s))";
            }
            catch (Exception)
            {
                tblTopCus1.Text = "1. No data";
            }

            try
            {
                tblTopCus2.Text = $"2. {cus[1].Firstname} {cus[1].Lastname} ({cus[1].NumTick} ticket(s))";
            }
            catch (Exception)
            {
                tblTopCus2.Text = "2. No data";
            }

            try
            {
                tblTopCus3.Text = $"3. {cus[2].Firstname} {cus[2].Lastname} ({cus[2].NumTick} ticket(s))";
            }
            catch (Exception)
            {
                tblTopCus3.Text = "3. No data";
            }

            var fly = tickets.GroupBy(t => t.Schedule.Date).Select(t => new
            {
                Date = t.Key,
                NumTick = t.Count()
            }).OrderByDescending(t => t.NumTick).ToList();

            try
            {
                tblBusiest.Text = $"Busiest day: {fly.First().Date.ToString("dd/MM/yyyy")} with {fly.First().NumTick} flying";
            }
            catch (Exception)
            {
                tblBusiest.Text = $"Busiest day: No data";
            }

            try
            {
                tblMostQuite.Text = $"The most quite day: {fly.Last().Date.ToString("dd/MM/yyyy")} with {fly.Last().NumTick} flying";
            }
            catch (Exception)
            {
                tblMostQuite.Text = $"The most quite day: No data";
            }

            var off = Db.Context.Offices.Select(t => new TopOffice
            {
                Title = t.Title,
                Tickets = t.Users.SelectMany(k => k.Tickets).Where(k => k.Schedule.Date >= thirty.Date && k.Schedule.Date < now.Date && k.Confirmed && k.Schedule.Confirmed).ToList(),
                Revenue = 0.0
            }).ToList();

            foreach (var item in off)
            {
                item.Revenue = UpdateRevenue(item.Tickets);
            }

            off = off.Where(t => t.Revenue != 0).OrderByDescending(t => t.Revenue).ToList();

            try
            {
                tblTopOffice1.Text = $"1. {off[0].Title} ({off[0].Revenue.ToString("C0")})";
            }
            catch (Exception)
            {
                tblTopOffice1.Text = "1. No data";
            }
            try
            {
                tblTopOffice2.Text = $"2. {off[1].Title} ({off[1].Revenue.ToString("C0")})";
            }
            catch (Exception)
            {
                tblTopOffice2.Text = "2. No data";
            }
            try
            {
                tblTopOffice3.Text = $"3. {off[2].Title} ({off[2].Revenue.ToString("C0")})";
            }
            catch (Exception)
            {
                tblTopOffice3.Text = "3. No data";
            }

            var yes = now.Date.AddDays(-1);
            var two = now.Date.AddDays(-2);
            var three = now.Date.AddDays(-3);
            var tYes = tickets.Where(t => t.Schedule.Date == yes).ToList();
            var tTwo = tickets.Where(t => t.Schedule.Date == two).ToList();
            var tThree = tickets.Where(t => t.Schedule.Date == three).ToList();

            Revenue(tYes, tblYesterday, "Yesterday: ");
            Revenue(tTwo, tblTwoDaysAgo, "Two days ago: ");
            Revenue(tThree, tblThreeDaysAgo, "Three days ago: ");

            var dayofWeek = (int)now.DayOfWeek == 0 ? 7 : (int)now.DayOfWeek;
            var thisWeek = now.Date.AddDays(-dayofWeek + 1);
            var lastWeek = thisWeek.AddDays(-7);
            var twoWeekAgo = thisWeek.AddDays(-14);

            EmptySeat(thisWeek, now.Date, tblThisWeek, "This week: ");
            EmptySeat(lastWeek, thisWeek, tblLastWeek, "Last week: ");
            EmptySeat(twoWeekAgo, lastWeek, tblThreeWeekAgo, "Two week ago: ");
        }
        private void EmptySeat(DateTime from, DateTime to, TextBlock l, string v)
        {
            var schedules = Db.Context.Schedules.ToList().Where(t => t.Date >= from && t.Date + t.Time < to && t.Confirmed).ToList();
            var total = schedules.Sum(t => (int?)t.Aircraft.TotalSeats) ?? 0;
            var numTick = schedules.Sum(t => (int?)t.Tickets.Where(k => k.Confirmed).Count()) ?? 0;

            l.Text = v + (total == 0 ? "No data" : ((total - numTick) * 100.0 / total).ToString("0.00") + "%");
        }

        private void Revenue(List<Ticket> tickets, TextBlock l, string v)
        {
            l.Text = v + UpdateRevenue(tickets).ToString("C0");
        }

        private double UpdateRevenue(List<Ticket> tickets)
        {
            double revenue = 0;
            foreach (var item in tickets)
            {
                revenue += FlightForBooking.GetPrice(item.Schedule, item.CabinType);
            }

            return revenue;
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    internal class TopOffice
    {
        public double Revenue { get; set; }
        public List<Ticket> Tickets { get; set; }
        public string Title { get; set; }
    }
}
