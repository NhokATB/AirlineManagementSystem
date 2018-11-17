using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AirportManagerSystem.Model;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for EditTicketWindow.xaml
    /// </summary>
    public partial class EditTicketWindow : Window
    {
        List<Country> countries;
        List<CabinType> cabins;
        public EditTicketWindow()
        {
            InitializeComponent();
            this.Loaded += EditTicketWindow_Loaded;
        }

        private void EditTicketWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cabins = Db.Context.CabinTypes.ToList();
            cbCabinType.ItemsSource = cabins;
            cbCabinType.DisplayMemberPath = "Name";
            cbCabinType.SelectedItem = NewTicket.Ticket.CabinType;

            countries = Db.Context.Countries.ToList();
            cbCountry.ItemsSource = countries;
            cbCountry.DisplayMemberPath = "Name";
            cbCountry.SelectedItem = NewTicket.Ticket.Country;

            tblAircraft.Text = NewTicket.Ticket.Schedule.Aircraft.Name;
            tblDate.Text = NewTicket.Ticket.Schedule.Date.ToString("dd/MM/yyyy");
            tblTime.Text = NewTicket.Ticket.Schedule.Time.ToString(@"hh\:mm");
            tblFrom.Text = NewTicket.Ticket.Schedule.Route.Airport.IATACode;
            tblTo.Text = NewTicket.Ticket.Schedule.Route.Airport1.IATACode;

            txtFirstName.Text = NewTicket.Ticket.Firstname;
            txtLastName.Text = NewTicket.Ticket.Lastname;
            txtBr.Text = NewTicket.Ticket.BookingReference;
            txtPassportNumber.Text = NewTicket.Ticket.PassportNumber;
            txtPhone.Text = NewTicket.Ticket.Phone.Replace("-","");

            GetCabinPrice();
        }

        private void GetCabinPrice()
        {
            var total = Flight.GetPrice(NewTicket.Ticket.Schedule, cabins[cbCabinType.SelectedIndex]);
            var totalPayed = Flight.GetPrice(NewTicket.Ticket.Schedule, NewTicket.Ticket.CabinType);
            var totalPayable = total - totalPayed;

            tblTicketPrice.Text = total.ToString("C0");
            tblTotalPayable.Text = totalPayable.ToString("C0");
            tblTotalPayed.Text = totalPayed.ToString("C0");
        }

        public TicketsManagementWindow ManageWindow { get; internal set; }
        internal NewTicket NewTicket { get; set; }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text.Trim() == "")
            {
                MessageBox.Show("First name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtLastName.Text.Trim() == "")
            {
                MessageBox.Show("Last name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtPassportNumber.Text.Trim() == "")
            {
                MessageBox.Show("Passport number was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (txtPassportNumber.Text != NewTicket.Ticket.PassportNumber)
                {
                    if (NewTicket.Ticket.Schedule.Tickets.Where(t => t.PassportNumber == txtPassportNumber.Text).FirstOrDefault() != null)
                    {
                        MessageBox.Show("This Passport number was used for this flight! Please check again.", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Phone was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (Regex.IsMatch(txtPhone.Text, @"\D"))
            {
                MessageBox.Show("Phone must be digits!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewTicket.Ticket.Firstname = txtFirstName.Text;
            NewTicket.Ticket.Lastname = txtLastName.Text;
            NewTicket.Ticket.Phone = txtPhone.Text;
            NewTicket.Ticket.PassportNumber = txtPassportNumber.Text;
            NewTicket.Ticket.Country = countries[cbCountry.SelectedIndex];
            NewTicket.Ticket.CabinType = cabins[cbCabinType.SelectedIndex];

            Db.Context.SaveChanges();
            ManageWindow.LoadTickets();
            MessageBox.Show("Edit ticket successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbCabinType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetCabinPrice();
        }
    }
}
