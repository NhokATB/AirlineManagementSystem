using AirportManagerSystem.ChartControls;
using AirportManagerSystem.HelperClass;
using AirportManagerSystem.Model;
using AirportManagerSystem.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for SeatManagementWindow.xaml
    /// </summary>
    public partial class SeatManagementWindow : Window
    {
        DispatcherTimer timer;
        List<Schedule> schedules;
        List<string> chartTypes = new List<string>() { "Column", "Pie" };

        public SeatManagementWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 5);
            timer.Tick += Timer_Tick;

            dpnCheckedInSeat.Background = new SolidColorBrush(AMONICColor.CheckedIn);
            dpnEmptySeat.Background = new SolidColorBrush(AMONICColor.Empty);

            this.Loaded += SeatManagementWindow_Loaded;
            this.Closed += SeatManagementWindow_Closed;
            this.WindowState = WindowState.Maximized;
        }

        private void SeatManagementWindow_Closed(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                LoadSeat();
                ShowEmptySeat();
                ShowDualSeat();
                LoadChart();
            }
            catch (Exception)
            {
            }
        }

        private void SeatManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dpDate.SelectedDate = DateTime.Now.Date;

            cbChartType.ItemsSource = chartTypes;
            cbChartType.SelectedIndex = 0;

            timer.Start();
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpDate.SelectedDate != null)
            {
                var date = dpDate.SelectedDate.Value;

                schedules = Db.Context.Schedules.Where(t => t.Date == date && t.Confirmed).OrderBy(t => t.Time).ToList();
                List<string> scheduleInfo = new List<string>();
                foreach (var item in schedules)
                {
                    scheduleInfo.Add($"{item.FlightNumber} - {item.Date.ToString("dd/MM/yyyy")} - {item.Time.ToString(@"hh\:mm")} - {item.Route.Airport.IATACode} to {item.Route.Airport1.IATACode}");
                }

                cbFlights.ItemsSource = scheduleInfo;
                cbFlights.SelectedIndex = 0;
            }
        }

        private void cbFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LoadSeat();
                ShowEmptySeat();
                ShowDualSeat();
                LoadChart();
            }
            catch (Exception)
            {
            }
        }

        private void LoadSeat()
        {
            wpSeats.Children.Clear();

            Db.Context = new AirlineManagementSystemEntities();
            var flight = Db.Context.Schedules.Find(schedules[cbFlights.SelectedIndex].ID);

            var numE = flight.Aircraft.EconomySeats;
            var numB = flight.Aircraft.BusinessSeats;
            var numF = flight.Aircraft.TotalSeats - numE - numB;

            var dayF = numF / 2;
            var dayB = numB / 4;
            var dayE = numE / 6;

            AddSeat(flight, 1, dayF, 3);
            AddSeat(flight, dayF + 1, dayF + dayB, 2);
            AddSeat(flight, dayF + dayB + 1, dayF + dayB + dayE, 1);
        }

        private void AddSeat(Schedule flight, int from, int to, int cabinId)
        {
            string seatName1 = "AB";
            string seatName2 = "ABCD";
            string seatName3 = "ABCDEF";
            var seatName = cabinId == 3 ? seatName1 : (cabinId == 2 ? seatName2 : seatName3);

            UcSeat previous = new UcSeat();

            for (int i = from; i <= to; i++)
            {
                foreach (var item in seatName)
                {
                    UcSeat uc = new UcSeat();
                    uc.Seat = i + item.ToString();
                    uc.Ticket = flight.Tickets.Where(t => t.Seat == uc.Seat).FirstOrDefault();
                    uc.Flight = flight;
                    uc.CabinId = cabinId;

                    if (cabinId == 2)
                    {
                        if (uc.Seat.Contains("A")) { previous = uc; }
                        else if (uc.Seat.Contains("B")) { uc.Previous = previous; previous.After = uc; }
                        else if (uc.Seat.Contains("C")) { previous = uc; }
                        else if (uc.Seat.Contains("D")) { uc.Previous = previous; previous.After = uc; }
                    }
                    else if (cabinId == 1)
                    {
                        if (uc.Seat.Contains("A")) { previous = uc; }
                        else if (uc.Seat.Contains("B")) { uc.Previous = previous; previous.After = uc; previous = uc; }
                        else if (uc.Seat.Contains("C")) { uc.Previous = previous; previous.After = uc; }
                        else if (uc.Seat.Contains("D")) { previous = uc; }
                        else if (uc.Seat.Contains("E")) { uc.Previous = previous; previous.After = uc; previous = uc; }
                        else if (uc.Seat.Contains("F")) { uc.Previous = previous; previous.After = uc; }
                    }

                    if (cabinId == 3)
                    {
                        if (uc.Seat.Contains("A")) uc.Width = 175;
                    }
                    else if (cabinId == 2)
                    {
                        if (uc.Seat.Contains("B")) uc.Width = 175;
                    }
                    else if (uc.Seat.Contains("C")) uc.Width = 175;

                    wpSeats.Children.Add(uc);
                }
            }
        }
        private void ShowEmptySeat()
        {
            var flight = schedules[cbFlights.SelectedIndex];

            var eSeat = flight.Aircraft.EconomySeats;
            var bSeat = flight.Aircraft.BusinessSeats;
            var fSeat = flight.Aircraft.TotalSeats - eSeat - bSeat;

            var numE = flight.Tickets.Where(t => t.Seat != null && t.CabinTypeID == 1).Count();
            var numB = flight.Tickets.Where(t => t.Seat != null && t.CabinTypeID == 2).Count();
            var numF = flight.Tickets.Where(t => t.Seat != null && t.CabinTypeID == 3).Count();

            tblFirstEmpty.Text = (fSeat - numF).ToString();
            tblBusinessEmpty.Text = (bSeat - numB).ToString();
            tblEconomyEmpty.Text = (eSeat - numE).ToString();
        }

        private void ShowDualSeat()
        {
            var flight = schedules[cbFlights.SelectedIndex];

            var dualE = 0;
            var dualB = 0;
            var dualF = 0;

            var controls = wpSeats.Children;

            for (int i = 0; i < controls.Count - 1; i++)
            {
                var uc1 = (UcSeat)controls[i];
                var uc2 = (UcSeat)controls[i + 1];

                if (uc1.CabinId == 2)
                {
                    if (uc1.Ticket == null && uc2.Ticket == null && (uc1.Seat.Contains("C") || uc1.Seat.Contains("A")))
                    {
                        if (uc1.CabinId == 1) dualE++;
                        else if (uc1.CabinId == 2) dualB++;
                        else dualF++;
                    }
                }

                if (uc1.CabinId == 1)
                {
                    if (uc1.Ticket == null && uc2.Ticket == null && (uc1.Seat.Contains("A") || uc1.Seat.Contains("B") || uc1.Seat.Contains("D") || uc1.Seat.Contains("E")))
                    {
                        if (uc1.CabinId == 1) dualE++;
                        else if (uc1.CabinId == 2) dualB++;
                        else dualF++;
                    }
                }
            }

            tblFirstDualEmpty.Text = dualF.ToString();
            tblBusinessDualEmpty.Text = dualB.ToString();
            tblEconomyDualEmpty.Text = dualE.ToString();
        }

        private void LoadChart()
        {
            var flight = schedules[cbFlights.SelectedIndex];
            int left = 0, right = 0;

            var economyTickets = flight.Tickets.Where(t => t.CabinTypeID == 1 && t.Confirmed && t.Seat != null).ToList();
            var businessTickets = flight.Tickets.Where(t => t.CabinTypeID == 2 && t.Confirmed && t.Seat != null).ToList();
            var firstclassTickets = flight.Tickets.Where(t => t.CabinTypeID == 3 && t.Confirmed && t.Seat != null).ToList();

            left += firstclassTickets.ToList().Where(t => t.Seat.Contains("A")).Count();
            right += firstclassTickets.ToList().Where(t => t.Seat.Contains("B")).Count();

            left += businessTickets.ToList().Where(t => (t.Seat.Contains("A") || t.Seat.Contains("B"))).Count();
            right += businessTickets.ToList().Where(t => (t.Seat.Contains("D") || t.Seat.Contains("C"))).Count();

            left += economyTickets.ToList().Where(t => (t.Seat.Contains("A") || t.Seat.Contains("B") || t.Seat.Contains("C"))).Count();
            right += economyTickets.ToList().Where(t => (t.Seat.Contains("D") || t.Seat.Contains("E") || t.Seat.Contains("F"))).Count();

            grdChartContainer.Children.Clear();
            int i = 0;
            if (chartTypes[cbChartType.SelectedIndex] == "Pie")
            {
                UcSeatPieChart pieChart = new UcSeatPieChart();

                List<KeyValuePair<string, int>> sources = new List<KeyValuePair<string, int>>();
                sources.Add(new KeyValuePair<string, int>("Left", left));
                sources.Add(new KeyValuePair<string, int>("Right", right));

                ((PieSeries)(pieChart.mcChart).Series[i]).ItemsSource = sources;
                i++;

                grdChartContainer.Children.Add(pieChart);
            }
            else
            {
                UcSeatColumnChart columnChart = new UcSeatColumnChart();

                List<KeyValuePair<string, int>> sources = new List<KeyValuePair<string, int>>();
                sources.Add(new KeyValuePair<string, int>("Left", left));
                sources.Add(new KeyValuePair<string, int>("Right", right));

                ((ColumnSeries)(columnChart.mcChart).Series[i]).ItemsSource = sources;
                i++;

                columnChart.mcChart.Title = "Number of Checked in Seat";
                grdChartContainer.Children.Add(columnChart);

            }
        }

        private void cbChartType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LoadChart();
            }
            catch (Exception)
            {
            }
        }
    }
}
