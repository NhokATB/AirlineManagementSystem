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
    /// Interaction logic for TicketControlWindow.xaml
    /// </summary>
    public partial class TicketControlWindow : Window
    {
        List<Airport> departureAirports;
        List<Airport> arrivalAirports;
        List<Schedule> flights;

        public TicketControlWindow()
        {
            InitializeComponent();
            this.Loaded += TicketControlWindow_Loaded;
        }

        private void TicketControlWindow_Loaded(object sender, RoutedEventArgs e)
        {
            departureAirports = Db.Context.Airports.ToList();
            cbDepatureAirport.ItemsSource = departureAirports;
            cbDepatureAirport.DisplayMemberPath = "Name";
            cbDepatureAirport.SelectedIndex = 0;

            arrivalAirports = Db.Context.Airports.ToList();
            cbArrivalAirport.ItemsSource = arrivalAirports;
            cbArrivalAirport.DisplayMemberPath = "Name";
            cbArrivalAirport.SelectedIndex = 1;


        }

        private void btnCheckTicket_Click(object sender, RoutedEventArgs e)
        {
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
                MessageBox.Show("Ticket id must be interger", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var ticket = flights[cbFlightList.SelectedIndex].Tickets.FirstOrDefault(t => t.ID == id);
            if (ticket == null)
            {
                MessageBox.Show("This ticket not in this flight", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                ticket.Controled = true;
                Db.Context.SaveChanges();
                LoadTickets();
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            btnCheckTicket.IsEnabled = false;

            if (cbArrivalAirport.Text == cbDepatureAirport.Text)
            {
                MessageBox.Show("Departure airport and arrival airport cannot be the same!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var from = cbDepatureAirport.Text;
            var to = cbArrivalAirport.Text;
            var date = DateTime.Now.Date;

            flights = Db.Context.Schedules.Where(t => t.Date == date && t.Route.Airport.Name == from && t.Route.Airport1.Name == to).ToList();
            var flightInfor = new List<string>();
            foreach (var item in flights)
            {
                flightInfor.Add($"{item.FlightNumber} - {item.Date.ToString("dd/MM/yyyy")} - {item.Time.ToString(@"hh\:mm")} - {item.Route.Airport.IATACode} To {item.Route.Airport1.IATACode}");
            }

            cbFlightList.ItemsSource = flightInfor;
            cbFlightList.SelectedIndex = 0;

            if (flights.Count > 0)
            {
                btnCheckTicket.IsEnabled = true;
            }
            else
            {
                MessageBox.Show($"Today not have any flight for route: {cbDepatureAirport.Text} to {cbArrivalAirport.Text}");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadTickets();
            }
            catch (Exception)
            {
            }
        }

        private void LoadTickets()
        {
            dgNotControledTickets.ItemsSource = null;
            dgControledTicket.ItemsSource = null;

            var notControledTickets = flights[cbFlightList.SelectedIndex].Tickets.Where(t => t.Controled == false).ToList();
            var controledTickets = flights[cbFlightList.SelectedIndex].Tickets.Where(t => t.Controled == true).ToList();

            dgControledTicket.ItemsSource = controledTickets;
            dgNotControledTickets.ItemsSource = notControledTickets;
        }
    }
}
