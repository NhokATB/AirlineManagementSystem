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
    /// Interaction logic for BookFlightWindow.xaml
    /// </summary>
    /// 
    public partial class BookFlightWindow : Window
    {
        string from, to;
        DateTime odate, rdate;
        private bool isApplied;
        private int numValue = 0;
        private List<Airport> departureAirport;
        private List<Airport> arrivalAirport;
        private List<CabinType> cabins;
        private List<Flight> outboundFlights;
        private List<Flight> returnFlights;

        public BookFlightWindow()
        {
            InitializeComponent();
            this.Loaded += BookFlightWindow_Loaded;

            txtNum.Text = numValue.ToString();
            dpReturn.SelectedDate = DateTime.Now;
            dpOutbound.SelectedDate = DateTime.Now;
        }

        private void BookFlightWindow_Loaded(object sender, RoutedEventArgs e)
        {
            departureAirport = Db.Context.Airports.ToList();
            cbDepatureAirport.ItemsSource = departureAirport;
            cbDepatureAirport.DisplayMemberPath = "IATACode";
            cbDepatureAirport.SelectedIndex = 0;

            arrivalAirport = Db.Context.Airports.ToList();
            cbArrivalAirport.ItemsSource = arrivalAirport;
            cbArrivalAirport.DisplayMemberPath = "IATACode";
            cbArrivalAirport.SelectedIndex = 0;

            cabins = Db.Context.CabinTypes.ToList();
            cbCabinType.ItemsSource = cabins;
            cbCabinType.DisplayMemberPath = "Name";
            cbCabinType.SelectedIndex = 0;
        }

        public int NumValue
        {
            get { return numValue; }
            set
            {
                numValue = value;
                txtNum.Text = value.ToString();
            }
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            NumValue++;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            NumValue--;
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null)
            {
                return;
            }

            if (!int.TryParse(txtNum.Text, out numValue))
                txtNum.Text = numValue.ToString();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (cbDepatureAirport.Text == cbArrivalAirport.Text)
            {
                MessageBox.Show("Airport cannot be the same");
                return;
            }

            SetParameter();

            if (rdbReturn.IsChecked.Value)
            {
                if (dpReturn.SelectedDate.Value.Date < dpOutbound.SelectedDate.Value.Date)
                {
                    MessageBox.Show("Return date can only after outbound date");
                    return;
                }

                returnFlights = LoadData(dgReturnFlights, to, from, chbThreeDaysReturn.IsChecked, rdate);
            }
            outboundFlights = LoadData(dgOutboundFlights, from, to, chbThreeDaysOutbound.IsChecked, odate);

            if (dgOutboundFlights.Items.Count == 0)
            {
                MessageBox.Show("No result for outbound flight");
            }

            if (rdbReturn.IsChecked.Value)
            {
                if (dgReturnFlights.Items.Count == 0)
                {
                    MessageBox.Show("No result for reutrn flight");
                }
            }
            isApplied = true;
        }

        private List<Flight> LoadData(DataGrid dg, string from, string to, bool? isChecked, DateTime date)
        {
            dg.ItemsSource = null;
            List<Flight> flights = new List<Flight>();

            var before = date.AddDays(-3);
            var after = date.AddDays(3);
            var sch1 = Db.Context.Schedules.Where(t => t.Date >= before && t.Date <= after && t.Route.Airport.IATACode == from && t.Route.Airport1.IATACode == to && t.Confirmed).ToList();
            if (!isChecked.Value)
                sch1 = sch1.Where(t => t.Date == date).ToList();

            foreach (var item in sch1)
            {
                flights.Add(new Flight()
                {
                    From = from,
                    To = to,
                    FirstFlight = item,
                    NumberOfStop = 0,
                    Price = Schedule.GetPrice(item, cbCabinType.SelectedItem as CabinType),
                    Flights = new List<Schedule>() { item },
                    FlightNumbers = $"[{item.FlightNumber}]"
                });
            }

            flights.AddRange(IndirectFlight1(from, to, isChecked.Value, date));
            flights.AddRange(IndirectFlight2(from, to, isChecked.Value, date));
            dg.ItemsSource = flights;

            return flights;
        }
        private List<Flight> IndirectFlight1(string from, string to, bool isChecked, DateTime date)
        {
            List<Flight> results = new List<Flight>();
            var before = date.AddDays(-3);
            var after = date.AddDays(3);
            var sch1 = Db.Context.Schedules.Where(t => t.Date >= before && t.Date <= after && t.Route.Airport.IATACode == from && t.Confirmed).ToList();
            if (!isChecked)
                sch1 = sch1.Where(t => t.Date == date).ToList();

            foreach (var s1 in sch1)
            {
                var arrivalTime = (s1.Date + s1.Time).AddMinutes(s1.Route.FlightTime);
                var sch2 = Db.Context.Schedules.Where(t => t.Route.Airport.IATACode == s1.Route.Airport1.IATACode && t.Route.Airport1.IATACode == to && t.Confirmed).ToList();

                sch2 = sch2.Where(t => t.Date + t.Time >= arrivalTime).ToList();

                foreach (var s2 in sch2)
                {
                    results.Add(new Flight()
                    {
                        From = from,
                        To = to,
                        Price = Schedule.GetPrice(s1, cbCabinType.SelectedItem as CabinType) + Schedule.GetPrice(s2, cbCabinType.SelectedItem as CabinType),
                        Flights = new List<Schedule>() { s1, s2 },
                        NumberOfStop = 1,
                        FirstFlight = s1,
                        FlightNumbers = $"[{s1.FlightNumber}] - [{s2.FlightNumber}]",
                    });
                }
            }

            return results;
        }

        private void chbThreeDaysOutbound_Checked(object sender, RoutedEventArgs e)
        {
            if (isApplied)
                outboundFlights = LoadData(dgOutboundFlights, from, to, chbThreeDaysOutbound.IsChecked, odate);
        }

        private void chbThreeDaysReturn_Checked(object sender, RoutedEventArgs e)
        {
            if (isApplied)
                returnFlights = LoadData(dgReturnFlights, to, from, chbThreeDaysReturn.IsChecked, rdate);
        }

        private void rdbReturn_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                tblReturn.Visibility = Visibility.Visible;
                dpReturn.Visibility = Visibility.Visible;
                grReturnFlightsDetail.Visibility = Visibility.Visible;
                dgReturnFlights.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
            }

        }

        private void rdbOneway_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                tblReturn.Visibility = Visibility.Hidden;
                dpReturn.Visibility = Visibility.Hidden;
                grReturnFlightsDetail.Visibility = Visibility.Hidden;
                dgReturnFlights.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private List<Flight> IndirectFlight2(string from, string to, bool isChecked, DateTime date)
        {
            List<Flight> results = new List<Flight>();
            var before = date.AddDays(-3);
            var after = date.AddDays(3);
            var sch1 = Db.Context.Schedules.Where(t => t.Date >= before && t.Date <= after && t.Route.Airport.IATACode == from && t.Route.Airport1.IATACode != from && t.Route.Airport1.IATACode != to && t.Confirmed).ToList();
            if (!isChecked)
                sch1 = sch1.Where(t => t.Date == date).ToList();

            foreach (var s1 in sch1)
            {
                var arrivalTime = (s1.Date + s1.Time).AddMinutes(s1.Route.FlightTime);
                var sch2 = Db.Context.Schedules.Where(t => t.Route.Airport.IATACode == s1.Route.Airport1.IATACode && t.Route.Airport1.IATACode != from && t.Route.Airport1.IATACode != to && t.Confirmed).ToList();

                sch2 = sch2.Where(t => t.Date + t.Time >= arrivalTime).ToList();

                foreach (var s2 in sch2)
                {
                    arrivalTime = (s2.Date + s2.Time).AddMinutes(s2.Route.FlightTime);
                    var sch3 = Db.Context.Schedules.Where(t => t.Route.Airport.IATACode == s2.Route.Airport1.IATACode && t.Route.Airport1.IATACode == to && t.Confirmed).ToList();

                    sch3 = sch3.Where(t => t.Date + t.Time >= arrivalTime).ToList();

                    foreach (var s3 in sch3)
                    {
                        results.Add(new Flight()
                        {
                            From = from,
                            To = to,
                            Price = Schedule.GetPrice(s1, cbCabinType.SelectedItem as CabinType) + Schedule.GetPrice(s2, cbCabinType.SelectedItem as CabinType) + Schedule.GetPrice(s3, cbCabinType.SelectedItem as CabinType),
                            Flights = new List<Schedule>() { s1, s2, s3 },
                            NumberOfStop = 2,
                            FirstFlight = s1,
                            FlightNumbers = $"[{s1.FlightNumber}] - [{s2.FlightNumber}] - [{ s3.FlightNumber }]",
                        });
                    }
                }
            }

            return results;
        }

        private void SetParameter()
        {
            from = cbDepatureAirport.Text;
            to = cbArrivalAirport.Text;
            odate = dpOutbound.SelectedDate.Value.Date;
            rdate = dpReturn.SelectedDate.Value.Date;
        }
    }
}
