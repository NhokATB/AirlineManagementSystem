using AirportManagerSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for TicketControlWindow.xaml
    /// </summary>
    public partial class TicketControlWindow : Window
    {
        private List<Schedule> flights;
        private Ticket currentTicket;

        public TicketControlWindow()
        {
            InitializeComponent();
            this.Loaded += TicketControlWindow_Loaded;
            dgNotControledTickets.SelectedCellsChanged += DgNotControledTickets_SelectedCellsChanged;
        }

        private void DgNotControledTickets_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentTicket = dgNotControledTickets.SelectedItem as Ticket;
            }
            catch (Exception)
            {
            }
        }

        private void TicketControlWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var date = DateTime.Now.Date;

            flights = Db.Context.Schedules.Where(t => t.Date == date).ToList();

            var flightInfor = new List<string>();
            foreach (var item in flights)
            {
                flightInfor.Add($"{item.FlightNumber} - {item.Date.ToString("dd/MM/yyyy")} - {item.Time.ToString(@"hh\:mm")} - {item.Route.Airport.IATACode} To {item.Route.Airport1.IATACode}");
            }

            cbFlightList.ItemsSource = flightInfor;
            cbFlightList.SelectedIndex = 0;
        }

        private void btnCheckTicket_Click(object sender, RoutedEventArgs e)
        {
            if (currentTicket != null)
            {
                currentTicket.Controled = true;
                Db.Context.SaveChanges();
                LoadTickets();
                currentTicket = null;
            }
            else
            {
                MessageBox.Show("Choose a ticket before check", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            btnCheckTicket.IsEnabled = false;

            var date = DateTime.Now.Date;

            flights = Db.Context.Schedules.Where(t => t.Date == date).ToList();
            var flightInfor = new List<string>();
            foreach (var item in flights)
            {
                flightInfor.Add($"{item.FlightNumber} - {item.Date.ToString("dd/MM/yyyy")} - {item.Time.ToString(@"hh\:mm")} - {item.Route.Airport.IATACode} To {item.Route.Airport1.IATACode}");
            }

            cbFlightList.ItemsSource = flightInfor;
            cbFlightList.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadTickets()
        {
            dgNotControledTickets.ItemsSource = null;
            dgControledTicket.ItemsSource = null;

            var notControledTickets = flights[cbFlightList.SelectedIndex].Tickets.Where(t => t.Controled == false).ToList();
            if (txtPassportNumber.Text != "")
            {
                notControledTickets = notControledTickets.Where(t => t.PassportNumber.Contains(txtPassportNumber.Text)).ToList();
            }

            var controledTickets = flights[cbFlightList.SelectedIndex].Tickets.Where(t => t.Controled == true).ToList();

            dgControledTicket.ItemsSource = controledTickets;
            dgNotControledTickets.ItemsSource = notControledTickets;
        }

        private void CbFlightList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LoadTickets();
            }
            catch (Exception)
            {
            }
        }

        private void TxtPassportNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadTickets();
        }
    }
}
