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
    /// Interaction logic for ChangeTicketWindow.xaml
    /// </summary>
    public partial class ChangeTicketWindow : Window
    {
        List<CabinType> cabins = new List<CabinType>();
        NewFlight currentFlight;
        List<Ticket> changeableTickets;
        double costIncurred;
        public ChangeTicketWindow()
        {
            InitializeComponent();
            this.Loaded += ChangeTicketWindow_Loaded;
            dgFlights.SelectedCellsChanged += DgFlights_SelectedCellsChanged;
            cbCabinType.SelectionChanged += CbCabinType_SelectionChanged;

            changeableTickets = new List<Ticket>();
        }

        private void CbCabinType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CalculateCosts(changeableTickets[cbTickets.SelectedIndex]);
            }
            catch (Exception)
            {
            }
        }

        private void DgFlights_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentFlight = dgFlights.SelectedItem as NewFlight;
                CalculateCosts(changeableTickets[cbTickets.SelectedIndex]);
            }
            catch (Exception)
            {
            }
        }

        private void CalculateCosts(Ticket ticket)
        {
            var payed = FlightForBooking.GetPrice(ticket.Schedule, ticket.CabinType);
            var total = currentFlight == null ? FlightForBooking.GetPrice(ticket.Schedule, cabins[cbCabinType.SelectedIndex]) : FlightForBooking.GetPrice(currentFlight.Schedule, cabins[cbCabinType.SelectedIndex]);

            tblTotalAfterChange.Text = total.ToString("C0");
            tblTotalPayed.Text = payed.ToString("C0");
            tblTotalPayable.Text = (total - payed + costIncurred).ToString("C0");
            if (total - payed < 0) tblTotalPayable.Text += " (return for passenger)";
        }

        private void CalculateCostIncurred(Ticket ticket)
        {
            var ticketPrice = FlightForBooking.GetPrice(ticket.Schedule, ticket.CabinType);

            costIncurred = 0;
            var timeBeforeFlightTakeoff = (ticket.Schedule.Date + ticket.Schedule.Time) - DateTime.Now;

            if (timeBeforeFlightTakeoff.TotalHours <= 3)
                costIncurred = 20;
            else if (timeBeforeFlightTakeoff.TotalHours > 3 && timeBeforeFlightTakeoff.TotalHours <= 24)
                costIncurred = 15;
            else if (timeBeforeFlightTakeoff.TotalDays > 1 && timeBeforeFlightTakeoff.TotalDays <= 3)
                costIncurred = 10;

            costIncurred = (ticketPrice * costIncurred / 100);
            tblCostIncurred.Text = costIncurred.ToString("C0");
        }

        private void ChangeTicketWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cabins = Db.Context.CabinTypes.ToList();
            cbCabinType.ItemsSource = cabins;
            cbCabinType.DisplayMemberPath = "Name";

            ResetData();
        }

        private void ResetData()
        {
            changeableTickets.Clear();
            cbTickets.Items.Clear();

            cbCabinType.SelectedIndex = -1;
            dgFlights.ItemsSource = null;
            currentFlight = null;
            btnSearch.IsEnabled = false;
            btnSaveAndCofirm.IsEnabled = false;
            grSearchFlight.Header = "Search flight for route: ";

            tblAircraft.Text = "";
            tblBookingReference.Text = "";
            tblCountry.Text = "";
            tblDate.Text = "";
            tblEconomyPrice.Text = "";
            tblFlightNumber.Text = "";
            tblFullName.Text = "";
            tblPassportNumber.Text = "";
            tblPhone.Text = "";
            tblRoute.Text = "";
            tblTime.Text = "";
            tblTotalPayable.Text = "";

            tblCostIncurred.Text = "";
            tblTotalPayable.Text = "";
            tblTotalAfterChange.Text = "";
            tblTotalPayed.Text = "";
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            ResetData();

            if (txtBookingReference.Text.Trim() == "")
            {
                MessageBox.Show("Please enter booking reference", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var tickets = Db.Context.Tickets.Where(t => t.BookingReference == txtBookingReference.Text).ToList();
            if (tickets.Count == 0)
            {
                MessageBox.Show("This booking reference not found!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var item in tickets)
            {
                var date = item.Schedule.Date + item.Schedule.Time;

                if (date > DateTime.Now)
                {
                    changeableTickets.Add(item);

                    cbTickets.Items.Add($"{item.Schedule.FlightNumber}, {item.Schedule.Route.Airport.IATACode} - {item.Schedule.Route.Airport1.IATACode}, {item.Schedule.Date.ToString("dd/MM/yyyy")}, {item.Schedule.Time.ToString(@"hh\:mm")} - {item.Firstname} {item.Lastname} - {item.PassportNumber}");
                }
            }

            if (cbTickets.Items.Count == 0)
            {
                MessageBox.Show("Tickets for this booking reference cannot be changed because the flight for this ticket was took off", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                cbTickets.SelectedIndex = 0;

                btnSearch.IsEnabled = true;
                btnSaveAndCofirm.IsEnabled = true;
            }
        }

        private void LoadInformation()
        {
            LoadTicketInformation(changeableTickets[cbTickets.SelectedIndex]);
            LoadScheduleInformation(changeableTickets[cbTickets.SelectedIndex]);
            CalculateCostIncurred(changeableTickets[cbTickets.SelectedIndex]);
        }

        private void LoadTicketInformation(Ticket ticket)
        {
            tblFullName.Text = ticket.Firstname + " " + ticket.Lastname;
            tblPassportNumber.Text = ticket.PassportNumber;
            tblPhone.Text = ticket.Phone;
            tblBookingReference.Text = ticket.BookingReference;
            tblCountry.Text = ticket.Country.Name;

            cbCabinType.SelectedItem = ticket.CabinType;
        }

        private void LoadScheduleInformation(Ticket ticket)
        {
            grSearchFlight.Header += ticket.Schedule.Route.Airport.IATACode + " - " + ticket.Schedule.Route.Airport1.IATACode;

            tblAircraft.Text = ticket.Schedule.Aircraft.Name;
            tblDate.Text = ticket.Schedule.Date.ToString("dd/MM/yyyy");
            tblTime.Text = ticket.Schedule.Time.ToString(@"hh\:mm");
            tblRoute.Text = ticket.Schedule.Route.Airport.IATACode + " - " + ticket.Schedule.Route.Airport1.IATACode;
            tblEconomyPrice.Text = ticket.Schedule.EconomyPrice.ToString("C0");
            tblFlightNumber.Text = ticket.Schedule.FlightNumber;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (dpDate.Text != "")
            {
                if (dpDate.SelectedDate.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Date can only >= today", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                SearchFlight();
            }
            else
            {
                MessageBox.Show("Please choose date before searching", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchFlight()
        {
            dgFlights.ItemsSource = null;

            var date = dpDate.SelectedDate.Value.Date;
            var routeId = changeableTickets[cbTickets.SelectedIndex].Schedule.RouteID;
            var scheduleId = changeableTickets[cbTickets.SelectedIndex].Schedule.ID;
            var schedules = Db.Context.Schedules.Where(t => t.RouteID == routeId && t.Date == date && t.ID != scheduleId).ToList();

            if (date == DateTime.Now.Date)
            {
                var time = DateTime.Now.TimeOfDay;
                schedules = schedules.Where(t => t.Time > time).ToList();
            }

            List<NewFlight> flights = new List<NewFlight>();
            foreach (var item in schedules)
            {
                double price = (int)item.EconomyPrice;
                double bprice = Math.Floor(price * 1.35);
                double fprice = Math.Floor(bprice * 1.3);

                flights.Add(new NewFlight()
                {
                    EconomyPrice = price,
                    BusinessPrice = bprice,
                    FirstClassPrice = fprice,
                    Schedule = item,
                    Aircraft = item.Aircraft.Name + " " + item.Aircraft.MakeModel
                });
            }

            dgFlights.ItemsSource = flights;
        }

        private void btnSaveAndCofirm_Click(object sender, RoutedEventArgs e)
        {
            if (changeableTickets[cbTickets.SelectedIndex].CabinTypeID == cabins[cbCabinType.SelectedIndex].ID)
            {
                if (currentFlight == null || (currentFlight != null && changeableTickets[cbTickets.SelectedIndex].ScheduleID == currentFlight.Schedule.ID))
                {
                    MessageBox.Show("You have not made any changes", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            changeableTickets[cbTickets.SelectedIndex].CabinType = cabins[cbCabinType.SelectedIndex];
            if (currentFlight != null)
            {
                changeableTickets[cbTickets.SelectedIndex].Schedule = currentFlight.Schedule;
            }

            Db.Context.SaveChanges();
            CalculateCosts(changeableTickets[cbTickets.SelectedIndex]);
            LoadScheduleInformation(changeableTickets[cbTickets.SelectedIndex]);
            dgFlights.ItemsSource = null;

            MessageBox.Show("Change ticket successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnChangeTicketPolicy_Click(object sender, RoutedEventArgs e)
        {
            ChangeTicketPolicyWindow changeTicketPolicyWindow = new ChangeTicketPolicyWindow();
            changeTicketPolicyWindow.ShowDialog();
        }

        private void CbTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadInformation();
        }
    }
}
