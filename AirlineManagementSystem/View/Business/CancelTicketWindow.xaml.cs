using AirportManagerSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for CancelTicketWindow.xaml
    /// </summary>
    public partial class CancelTicketWindow : Window
    {
        private List<CabinType> cabins;
        private List<Ticket> changeableTickets;
        public CancelTicketWindow()
        {
            InitializeComponent();
            this.Loaded += CancelTicketWindow_Loaded;

            changeableTickets = new List<Ticket>();
        }

        private void CancelTicketWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cabins = Db.Context.CabinTypes.ToList();
            cbCabinType.ItemsSource = cabins;
            cbCabinType.DisplayMemberPath = "Name";

            ResetData();
        }

        private void btnCancelTicketPolicy_Click(object sender, RoutedEventArgs e)
        {
            CancelTicketPolicyWindow cancelTicketPolicyWindow = new CancelTicketPolicyWindow();
            cancelTicketPolicyWindow.ShowDialog();
        }

        private void btnCancelTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                changeableTickets[cbTickets.SelectedIndex].Confirmed = false;
                Db.Context.SaveChanges();
                ResetData();

                MessageBox.Show("Cancel ticket successful", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                ResetData();
            }
            catch (Exception)
            {
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            ResetData();

            if (txtBookingReference.Text.Trim() == "")
            {
                MessageBox.Show("Please enter booking reference", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var tickets = Db.Context.Tickets.Where(t => t.BookingReference == txtBookingReference.Text && t.Confirmed).ToList();
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

                btnCancelTicket.IsEnabled = true;
            }
        }

        private void LoadInfomation()
        {
            LoadTicketInformation(changeableTickets[cbTickets.SelectedIndex]);
            LoadScheduleInformation(changeableTickets[cbTickets.SelectedIndex].Schedule);
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

        private void LoadScheduleInformation(Schedule schedule)
        {
            tblAircraft.Text = schedule.Aircraft.Name;
            tblDate.Text = schedule.Date.ToString("dd/MM/yyyy");
            tblTime.Text = schedule.Time.ToString(@"hh\:mm");
            tblRoute.Text = schedule.Route.Airport.IATACode + " - " + schedule.Route.Airport1.IATACode;
            tblEconomyPrice.Text = schedule.EconomyPrice.ToString("C2");
            tblFlightNumber.Text = schedule.FlightNumber;
        }

        private void CalculateCostIncurred(Ticket ticket)
        {
            var ticketPrice = FlightForBooking.GetPrice(ticket.Schedule, ticket.CabinType);

            double costIncurred = 100;
            var timeBeforeFlightTakeoff = (ticket.Schedule.Date + ticket.Schedule.Time) - DateTime.Now;

            if (timeBeforeFlightTakeoff.TotalHours < 12)
            {
                MessageBox.Show("This ticket cann't be canceled! See cancel ticket policy for more information.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (timeBeforeFlightTakeoff.TotalDays >= 3 && timeBeforeFlightTakeoff.TotalDays <= 5)
            {
                costIncurred = 20;
            }
            else if (timeBeforeFlightTakeoff.TotalDays > 5 && timeBeforeFlightTakeoff.TotalDays <= 7)
            {
                costIncurred = 10;
            }
            else
            {
                costIncurred = 0;
            }

            costIncurred = (ticketPrice * costIncurred / 100);

            tblReturn.Text = (ticketPrice - costIncurred).ToString("C2");
            tblCostIncurred.Text = costIncurred.ToString("C2");
        }

        private void ResetData()
        {
            cbTickets.Items.Clear();
            changeableTickets.Clear();

            cbCabinType.SelectedIndex = -1;
            btnCancelTicket.IsEnabled = false;

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

            tblCostIncurred.Text = "";
            tblReturn.Text = "";
        }

        private void CbTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LoadInfomation();
            }
            catch (Exception)
            {
            }
        }
    }
}
