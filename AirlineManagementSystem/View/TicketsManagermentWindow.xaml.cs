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
    /// Interaction logic for TicketsManagermentWindow.xaml
    /// </summary>
    public partial class TicketsManagementWindow : Window
    {
        List<string> criterias = new List<string>() { "Time", "Cabin type", "Flight number" };
        List<string> ticketTypes = new List<string>() { "All", "Confirmed", "Canceled" };
        List<CabinType> cabins;
        List<Airport> arrivalAirports;
        List<Airport> departureAirports;
        List<NewTicket> newTickets;
        List<Ticket> tickets;
        private NewTicket currentTicket;

        public TicketsManagementWindow()
        {
            InitializeComponent();
            this.Loaded += TicketsManagermentWindow_Loaded;
            dgTickets.LoadingRow += DgTickets_LoadingRow;
            dgTickets.SelectedCellsChanged += DgTickets_SelectedCellsChanged;
        }

        private void TicketsManagermentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            arrivalAirports = Db.Context.Airports.ToList();
            arrivalAirports.Insert(0, new Airport() { Name = "All airports" });
            cbArrivalAirport.ItemsSource = arrivalAirports;
            cbArrivalAirport.DisplayMemberPath = "Name";
            cbArrivalAirport.SelectedIndex = 0;

            departureAirports = Db.Context.Airports.ToList();
            departureAirports.Insert(0, new Airport() { Name = "All airports" });
            cbDepatureAirport.ItemsSource = departureAirports;
            cbDepatureAirport.DisplayMemberPath = "Name";
            cbDepatureAirport.SelectedIndex = 0;

            cbTicketType.ItemsSource = ticketTypes;
            cbTicketType.SelectedIndex = 0;

            dpOutbound.SelectedDate = DateTime.Now.Date;

            cabins = Db.Context.CabinTypes.ToList();
            cabins.Insert(0, new CabinType() { Name = "All cabins" });
            cbCabinType.ItemsSource = cabins;
            cbCabinType.DisplayMemberPath = "Name";
            cbCabinType.SelectedIndex = 0;

            cbSorBy.ItemsSource = criterias;
            cbSorBy.SelectedIndex = 0;
        }

        private void DgTickets_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentTicket = dgTickets.CurrentItem as NewTicket;

                if (currentTicket.Ticket.Confirmed)
                {
                    btnCancelTicket.Content = "Cancel Flight";
                    btnCancelTicket.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    btnCancelTicket.Content = "Confirm Flight";
                    btnCancelTicket.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            catch (Exception)
            {
            }
        }

        private void DgTickets_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var row = e.Row;
            var flight = e.Row.Item as NewTicket;
            if (flight.Ticket.Confirmed == false)
            {
                row.Background = new SolidColorBrush(Color.FromRgb(247, 148, 32));
            }
            else
            {
                row.Background = new SolidColorBrush(Colors.White);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEditFlight_Click(object sender, RoutedEventArgs e)
        {
            if (currentTicket != null)
            {
                try
                {
                    if (currentTicket.Ticket.Schedule.Date + currentTicket.Ticket.Schedule.Time < DateTime.Now)
                    {
                        MessageBox.Show("This ticket cannot be changed because the flight for this ticket took off!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    EditTicketWindow wEditTicket = new EditTicketWindow();
                    wEditTicket.NewTicket = currentTicket;
                    wEditTicket.ManageWindow = this;
                    wEditTicket.ShowDialog();
                    currentTicket = null;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show("Please choose a ticket!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancelTicket_Click(object sender, RoutedEventArgs e)
        {
            if (currentTicket != null)
            {
                if (currentTicket.Ticket.Schedule.Date + currentTicket.Ticket.Schedule.Time < DateTime.Now)
                {
                    MessageBox.Show("This ticket cannot be changed because the flight for this ticket took off!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                currentTicket.Ticket.Confirmed = !currentTicket.Ticket.Confirmed;
                Db.Context.SaveChanges();
                LoadTickets();
                MessageBox.Show(btnCancelTicket.Content + " successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please choose a ticket", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteTicket_Click(object sender, RoutedEventArgs e)
        {
            if (currentTicket != null)
            {
                if (MessageBox.Show("All information related to this ticket will be permanently deleted, do you want to delete this ticket?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    var amenTickets = currentTicket.Ticket.AmenitiesTickets.ToList();

                    Db.Context.AmenitiesTickets.RemoveRange(amenTickets);
                    Db.Context.Tickets.Remove(currentTicket.Ticket);
                    Db.Context.SaveChanges();

                    LoadTickets();
                    currentTicket = null;

                    MessageBox.Show("Delete ticket successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please choose a ticket!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (dpOutbound.SelectedDate == null)
            {
                MessageBox.Show("Date was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LoadTickets();
        }

        public void LoadTickets()
        {
            FilterTickets();
            SortTickets();
            DisplayTickets();
        }

        private void DisplayTickets()
        {
            dgTickets.ItemsSource = null;
            newTickets = new List<NewTicket>();

            foreach (var item in tickets)
            {
                newTickets.Add(new NewTicket()
                {
                    Ticket = item,
                    FullName = item.Firstname + " " + item.Lastname,
                    Price = Flight.GetPrice(item.Schedule, item.CabinType),
                    Route = item.Schedule.Route.Airport.IATACode + " - " + item.Schedule.Route.Airport1.IATACode
                });
            }

            dgTickets.ItemsSource = newTickets;
        }

        private void SortTickets()
        {
            if (cbSorBy.SelectedIndex == 0)
                tickets = tickets.OrderByDescending(t => t.Schedule.Time).ToList();
            else if (cbSorBy.SelectedIndex == 1)
                tickets = tickets.OrderByDescending(t => t.CabinTypeID).ToList();
            else tickets = tickets.OrderByDescending(t => t.Schedule.FlightNumber).ToList();
        }

        private void FilterTickets()
        {
            var date = dpOutbound.SelectedDate.Value.Date;
            tickets = Db.Context.Tickets.Where(t => t.Schedule.Date == date).ToList();

            if (cbDepatureAirport.SelectedIndex != 0)
                tickets = tickets.Where(t => t.Schedule.Route.Airport.Name == cbDepatureAirport.Text).ToList();
            if (cbArrivalAirport.SelectedIndex != 0)
                tickets = tickets.Where(t => t.Schedule.Route.Airport1.Name == cbArrivalAirport.Text).ToList();
            if (cbCabinType.SelectedIndex != 0)
                tickets = tickets.Where(t => t.CabinType.Name == cbCabinType.Text).ToList();
            if (cbTicketType.SelectedIndex == 1)
                tickets = tickets.Where(t => t.Confirmed).ToList();
            if (cbTicketType.SelectedIndex == 2)
                tickets = tickets.Where(t => t.Confirmed == false).ToList();
        }
    }
}
