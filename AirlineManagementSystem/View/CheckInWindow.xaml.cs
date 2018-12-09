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
using System.Windows.Threading;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for CheckInWindow.xaml
    /// </summary>
    public partial class CheckInWindow : Window
    {
        Ticket curentTicket;
        List<Ticket> selectedTickets;
        List<CabinType> cabins;

        public CheckInWindow()
        {
            InitializeComponent();
            selectedTickets = new List<Ticket>();

            this.Loaded += CheckInWindow_Loaded;
            this.StateChanged += CheckInWindow_StateChanged;
            dgTickets.SelectedCellsChanged += DgTickets_SelectedCellsChanged;

            txtTicketId.TextChanged += TextChanged;
            txtBookingReference.TextChanged += TextChanged;
            txtPassportNumber.TextChanged += TextChanged;
        }

        private void CheckInWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                dgTickets.Height = 440;
            else
                dgTickets.Height = 265;
        }

        private void CheckInWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dgTickets.Height = 440;

            cabins = Db.Context.CabinTypes.ToList();
            cabins.Insert(0, new CabinType()
            {
                Name = "All cabins"
            });
            cbCabinTypes.ItemsSource = cabins;
            cbCabinTypes.DisplayMemberPath = "Name";
            cbCabinTypes.SelectedIndex = 0;

            SearchTickets();
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

            var today = DateTime.Now.Date;
            //today = new DateTime(2018, 12, 2);
            var time = DateTime.Now.TimeOfDay;

            var tickets = Db.Context.Schedules.Where(t => t.Date == today && t.Time >= time).SelectMany(t => t.Tickets).Where(t => t.Confirmed && t.Seat == null).ToList();

            foreach (var item in selectedTickets)
            {
                tickets.Remove(item);
            }

            if (txtTicketId.Text != "")
                tickets = tickets.Where(t => t.ID.ToString().Contains(txtTicketId.Text)).ToList();

            if (txtPassportNumber.Text != "")
                tickets = tickets.Where(t => t.PassportNumber.Contains(txtPassportNumber.Text)).ToList();

            if (txtBookingReference.Text != "")
                tickets = tickets.Where(t => t.BookingReference.Contains(txtBookingReference.Text.ToUpper())).ToList();

            if (cbCabinTypes.SelectedIndex != 0)
                tickets = tickets.Where(t => t.CabinType.Name == cabins[cbCabinTypes.SelectedIndex].Name).ToList();

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

            selectedTickets.Clear();
            SearchTickets();
            dgSelectedTicket.ItemsSource = null;
            this.ShowDialog();
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
            SearchTickets();
        }
    }
}
