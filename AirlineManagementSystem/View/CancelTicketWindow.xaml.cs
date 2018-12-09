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
    /// Interaction logic for CancelTicketWindow.xaml
    /// </summary>
    public partial class CancelTicketWindow : Window
    {
        List<CabinType> cabins;
        Ticket ticket;
        public CancelTicketWindow()
        {
            InitializeComponent();
            this.Loaded += CancelTicketWindow_Loaded;
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
                ticket.Confirmed = false;
                Db.Context.SaveChanges();
                ResetData();

                MessageBox.Show("Cancel ticket successful", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (txtTicketId.Text.Trim() == "")
            {
                MessageBox.Show("Please enter ticket id", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int id;
            try
            {
                id = int.Parse(txtTicketId.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("Ticket id must be integer", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ticket = Db.Context.Tickets.Find(id);

            if (ticket == null)
            {
                MessageBox.Show("Ticket id not found", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ticket.Confirmed == false)
            {
                MessageBox.Show("Your ticket was canceled", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (ticket.Schedule.Date < DateTime.Now.Date)
            {
                MessageBox.Show("This ticket cannot be changed because the flight for this ticket was took off", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LoadTicketInformation(ticket);
            LoadScheduleInformation(ticket.Schedule);
            CalculateCostIncurred();

            btnCancelTicket.IsEnabled = true;
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
            tblEconomyPrice.Text = schedule.EconomyPrice.ToString("C0");
            tblFlightNumber.Text = schedule.FlightNumber;
        }

        private void CalculateCostIncurred()
        {
            var ticketPrice = FlightForBooking.GetPrice(ticket.Schedule, ticket.CabinType);

            double costIncurred = 100;
            var timeBeforeFlightTakeoff = (ticket.Schedule.Date + ticket.Schedule.Time) - DateTime.Now;

            if (timeBeforeFlightTakeoff.TotalDays <= 3)
                MessageBox.Show("This ticket cann't be canceled! See cancel ticket policy for more information.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (timeBeforeFlightTakeoff.TotalDays > 3 && timeBeforeFlightTakeoff.TotalDays <= 5)
                costIncurred = 20;
            else if (timeBeforeFlightTakeoff.TotalDays > 5 && timeBeforeFlightTakeoff.TotalDays <= 7)
                costIncurred = 10;
            else costIncurred = 0;

            costIncurred = (ticketPrice * costIncurred / 100);

            tblReturn.Text = (ticketPrice - costIncurred).ToString("C0");
            tblCostIncurred.Text = costIncurred.ToString("C0");
        }

        private void ResetData()
        {
            cbCabinType.SelectedIndex = -1;
            ticket = null;
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
    }
}
