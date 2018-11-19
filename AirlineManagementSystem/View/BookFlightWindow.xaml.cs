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

        private Flight CurrentOutboundFlight;
        private Flight CurrentReturnFlight;

        public BookFlightWindow()
        {
            InitializeComponent();
            this.Loaded += BookFlightWindow_Loaded;

            chbThreeDaysOutbound.Click += ChbThreeDaysOutbound_Click;
            chbThreeDaysReturn.Click += ChbThreeDaysReturn_Click;

            dgOutboundFlights.SelectionChanged += DgOutboundFlights_SelectionChanged;
            dgReturnFlights.SelectionChanged += DgReturnFlights_SelectionChanged;

            txtNum.Text = numValue.ToString();
            dpReturn.SelectedDate = DateTime.Now;
            dpOutbound.SelectedDate = DateTime.Now;
        }

        private void DgReturnFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CurrentReturnFlight = dgReturnFlights.CurrentItem as Flight;
            }
            catch (Exception)
            {
            }
        }

        private void DgOutboundFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CurrentOutboundFlight = dgOutboundFlights.CurrentItem as Flight;
            }
            catch (Exception)
            {
            }
        }

        private void ChbThreeDaysReturn_Click(object sender, RoutedEventArgs e)
        {
            if (isApplied)
                LoadData(dgReturnFlights, to, from, chbThreeDaysReturn.IsChecked, rdate);
        }

        private void ChbThreeDaysOutbound_Click(object sender, RoutedEventArgs e)
        {
            if (isApplied)
                LoadData(dgOutboundFlights, from, to, chbThreeDaysOutbound.IsChecked, odate);
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

        #region Numerric updown
        public int NumValue
        {
            get { return numValue; }
            set
            {
                numValue = value;
                txtNum.Text = value.ToString();
            }
        }

        public User User { get; internal set; }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            NumValue++;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            if (numValue > 1)
                NumValue--;
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null)
            {
                return;
            }

            if (!int.TryParse(txtNum.Text, out numValue))
                txtNum.Text = "1";
        }
        #endregion

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

                LoadData(dgReturnFlights, to, from, chbThreeDaysReturn.IsChecked, rdate);
            }
            LoadData(dgOutboundFlights, from, to, chbThreeDaysOutbound.IsChecked, odate);

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
                    Price = Flight.GetPrice(item, cbCabinType.SelectedItem as CabinType),
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
                        Price = Flight.GetPrice(s1, cbCabinType.SelectedItem as CabinType) + Flight.GetPrice(s2, cbCabinType.SelectedItem as CabinType),
                        Flights = new List<Schedule>() { s1, s2 },
                        NumberOfStop = 1,
                        FirstFlight = s1,
                        FlightNumbers = $"[{s1.FlightNumber}] - [{s2.FlightNumber}]",
                    });
                }
            }

            return results;
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
                            Price = Flight.GetPrice(s1, cbCabinType.SelectedItem as CabinType) + Flight.GetPrice(s2, cbCabinType.SelectedItem as CabinType) + Flight.GetPrice(s3, cbCabinType.SelectedItem as CabinType),
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

        private bool CheckDate(Flight flight1, Flight flight2)
        {
            var date1 = (flight1.Flights.Last().Date + flight1.Flights.Last().Time).AddMinutes(flight1.Flights.Last().Route.FlightTime);
            var date2 = flight2.Flights.First().Date + flight2.Flights.First().Time;

            if (date2 < date1)
            {
                MessageBox.Show("The first return flight can only after the last outbound flight", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void btnBookFlight_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentOutboundFlight != null)
            {
                if (CheckSeat(CurrentOutboundFlight, "Outbound flight") == false) return;
                if (rdbReturn.IsChecked.Value)
                {
                    try
                    {
                        if (CheckDate(CurrentOutboundFlight, CurrentReturnFlight) == false) return;
                        if (CheckSeat(CurrentReturnFlight, "Return flight") == false) return;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please choose return flight before book flight", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                BookConfirmationWindow wBookingConfirmation = new BookConfirmationWindow();
                wBookingConfirmation.User = User;
                wBookingConfirmation.Numpass = int.Parse(txtNum.Text);
                wBookingConfirmation.OutboundFlight = CurrentOutboundFlight;
                wBookingConfirmation.ReturnFlight = rdbReturn.IsChecked.Value ? CurrentReturnFlight : null;
                wBookingConfirmation.Cabin = cbCabinType.SelectedItem as CabinType;

                this.Hide();
                wBookingConfirmation.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Please choose outbound flight before book flight", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool CheckSeat(Flight flight, string v)
        {
            var cabin = cbCabinType.SelectedItem as CabinType;
            foreach (var item in flight.Flights)
            {
                var seat = item.Aircraft.EconomySeats;
                if (cabin.ID == 2) seat = item.Aircraft.BusinessSeats;
                if (cabin.ID == 3) seat = item.Aircraft.TotalSeats - item.Aircraft.EconomySeats - item.Aircraft.BusinessSeats;

                var numTick = item.Tickets.Where(t => t.Confirmed && t.CabinTypeID == cabin.ID).Count();
                var empty = seat - numTick;

                if (empty < int.Parse(txtNum.Text))
                {
                    MessageBox.Show($"{v}: Not enough seat for cabin {cabin.Name} and flight number {item.FlightNumber}. The maximum of empty seat for this flight is {empty}", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
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
