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
    /// Interaction logic for CheckInWindow.xaml
    /// </summary>
    public partial class CheckInWindow : Window
    {
        public CheckInWindow()
        {
            InitializeComponent();
            txtTicketId.TextChanged += TextChanged;
            txtBookingReference.TextChanged += TextChanged;
            txtPassportNumber.TextChanged += TextChanged;

            dgTickets.SelectedCellsChanged += DgTickets_SelectedCellsChanged;
            this.Loaded += CheckInWindow_Loaded;
        }

        private void CheckInWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SearchTickets();
        }

        private void DgTickets_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                selectedTicket = dgTickets.CurrentItem as Ticket;
            }
            catch (Exception)
            {
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchTickets();
        }

        Ticket selectedTicket;

        private void SearchTickets()
        {
            var now = DateTime.Now.Date;
            var time = DateTime.Now.TimeOfDay;

            var tickets = Db.Context.Tickets.Where(t => t.Schedule.Date == now && t.Schedule.Time >= time).ToList();
            tickets = Db.Context.Tickets.Where(t => t.Schedule.Date == now).ToList();

            if (txtTicketId.Text != "")
                tickets = tickets.Where(t => t.ID.ToString().Contains(txtTicketId.Text)).ToList();
            if (txtPassportNumber.Text != "")
                tickets = tickets.Where(t => t.PassportNumber.Contains(txtPassportNumber.Text)).ToList();
            if (txtBookingReference.Text != "")
                tickets = tickets.Where(t => t.BookingReference.Contains(txtBookingReference.Text.ToUpper())).ToList();

            dgTickets.ItemsSource = tickets;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCheckIn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
