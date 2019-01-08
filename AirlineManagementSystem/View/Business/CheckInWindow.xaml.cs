using AirportManagerSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for CheckInWindow.xaml
    /// </summary>
    public partial class CheckInWindow : Window
    {
        private Ticket curentTicket;
        private List<Ticket> selectedTickets;
        private List<Schedule> flights;
        private List<CabinType> cabins;

        public CheckInWindow()
        {
            InitializeComponent();
            selectedTickets = new List<Ticket>();

            this.Loaded += CheckInWindow_Loaded;
            this.StateChanged += CheckInWindow_StateChanged;
            dgTickets.SelectedCellsChanged += DgTickets_SelectedCellsChanged;

            txtPassportNumber.TextChanged += TextChanged;
        }

        private void CheckInWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                dgTickets.Height = 440;
            }
            else
            {
                dgTickets.Height = 265;
            }
        }

        private void CheckInWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dgTickets.Height = 440;

            ReloadFlight();
            foreach (var item in flights)
            {
                cbFlights.Items.Add($"{item.FlightNumber} - {(item.Date + item.Time).ToString("dd/MM/yyyy - HH:mm")} - {item.Route.Airport.IATACode} to {item.Route.Airport1.IATACode}");
            }
            cbFlights.SelectedIndex = 0;

            cabins = Db.Context.CabinTypes.ToList();
            cabins.Insert(0, new CabinType()
            {
                Name = "All cabins"
            });
            cbCabinTypes.ItemsSource = cabins;
            cbCabinTypes.DisplayMemberPath = "Name";
            cbCabinTypes.SelectedIndex = 0;

            try
            {
                SearchTickets();
            }
            catch (Exception)
            {
            }
        }

        private void DgTickets_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                curentTicket = dgTickets.CurrentItem as Ticket;
            }
            catch (Exception)
            {
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchTickets();
        }

        private void SearchTickets()
        {
            dgTickets.ItemsSource = null;

            var tickets = flights[cbFlights.SelectedIndex].Tickets.Where(t => t.Confirmed && t.Seat == null).ToList();

            foreach (var item in selectedTickets)
            {
                tickets.Remove(item);
            }

            if (txtPassportNumber.Text != "")
            {
                tickets = tickets.Where(t => t.PassportNumber.Contains(txtPassportNumber.Text)).ToList();
            }

            if (cbCabinTypes.SelectedIndex != 0)
            {
                tickets = tickets.Where(t => t.CabinType.Name == cabins[cbCabinTypes.SelectedIndex].Name).ToList();
            }

            dgTickets.ItemsSource = tickets;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            if (rdbSingleChekcin.IsChecked.Value)
            {
                if (selectedTickets.Count < 1)
                {
                    MessageBox.Show("'Single check in' require 1 ticket", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                if (selectedTickets.Count < 2)
                {
                    MessageBox.Show("'Dual check in' require 2 ticket", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            this.Hide();
            SeatModelWindow seatModelWindow = new SeatModelWindow();
            seatModelWindow.Tickets = selectedTickets;
            seatModelWindow.ShowDialog();

            ReloadFlight();
            selectedTickets.Clear();
            SearchTickets();
            dgSelectedTicket.ItemsSource = null;
            this.ShowDialog();
        }

        private void ReloadFlight()
        {
            var today = DateTime.Now.Date;
            var time = DateTime.Now.TimeOfDay;
            flights = Db.Context.Schedules.Where(t => t.Date == today && t.Time >= time).ToList().OrderBy(t => t.Date + t.Time).ToList();
        }

        private void DeleleTicket(object sender, RoutedEventArgs e)
        {
            var currentSelectedTicket = dgSelectedTicket.SelectedItem as Ticket;
            selectedTickets.Remove(currentSelectedTicket);
            SearchTickets();

            dgSelectedTicket.ItemsSource = null;
            dgSelectedTicket.ItemsSource = selectedTickets;
        }

        private void btnAddToSelectedTicket_Click(object sender, RoutedEventArgs e)
        {
            if (curentTicket != null)
            {
                if (rdbSingleChekcin.IsChecked.Value)
                {
                    if (selectedTickets.Count == 1)
                    {
                        MessageBox.Show("Only 1 ticket can be checked in", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    if (selectedTickets.Count == 1)
                    {
                        if (selectedTickets[0].ScheduleID != curentTicket.ScheduleID || selectedTickets[0].CabinTypeID != curentTicket.CabinTypeID)
                        {
                            MessageBox.Show("Two tickets must be in the same flight and the same cabin", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    if (selectedTickets.Count == 2)
                    {
                        MessageBox.Show("Only 2 ticket can be checked in", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                selectedTickets.Add(curentTicket);
                SearchTickets();

                dgSelectedTicket.ItemsSource = null;
                dgSelectedTicket.ItemsSource = selectedTickets;
            }
            else
            {
                MessageBox.Show("Please choose a ticket", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbCabinTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SearchTickets();
            }
            catch (Exception)
            {
            }
        }

        private void cbFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SearchTickets();
            }
            catch (Exception)
            {
            }
        }
    }
}
