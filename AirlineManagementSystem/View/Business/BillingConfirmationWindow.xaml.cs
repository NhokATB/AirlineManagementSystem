using AirportManagerSystem.HelperClass;
using AirportManagerSystem.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for BillingConfirmationWindow.xaml
    /// </summary>
    public partial class BillingConfirmationWindow : Window
    {
        public List<Ticket> Tickets { get; internal set; }
        public double TotalPrice { get; internal set; }
        public bool IsConfirm { get; internal set; }

        public BillingConfirmationWindow()
        {
            InitializeComponent();
            this.Loaded += BillingConfirmationWindow_Loaded;
        }

        private void BillingConfirmationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tblTotalAmount.Text = TotalPrice.ToString("C0");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnIssueTicket_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Enter email to receive booking information", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!Regex.IsMatch(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                MessageBox.Show("Email is invalid", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string br = GetBr();
            foreach (var item in Tickets)
            {
                item.BookingReference = br;
                Db.Context.Tickets.Add(item);
            }

            string bookingInfor = $"Booking reference: {br}\n\n" + GetBookingInformation(Tickets);
            try
            {
                MailProcessing.SendMail(txtEmail.Text, "Your booking information from AMONIC Airline:", bookingInfor);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Please connect to the internet!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Db.Context.SaveChanges();
            IsConfirm = true;
            MessageBox.Show("Issue ticket successful\nBooking information was sent to passenger's email", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private string GetBookingInformation(List<Ticket> tickets)
        {
            string bookingInfor = "";

            var tickByDate = tickets.GroupBy(t => t.Schedule).ToList().OrderBy(t => t.Key.Date + t.Key.Time).ToList();
            foreach (var item in tickByDate)
            {
                bookingInfor += $"Schedule information: {(item.Key.Date).ToString("dd/MM/yyyy")} - From {item.Key.Route.Airport.IATACode}: {item.Key.Time.ToString(@"hh\:mm")} To {item.Key.Route.Airport1.IATACode}: {(item.Key.Date + item.Key.Time).AddMinutes(item.Key.Route.FlightTime).ToString(@"hh\:mm")}\n";

                foreach (var tick in item)
                {
                    bookingInfor += $"\t{tick.Firstname} {tick.Lastname} - {tick.CabinType.Name} - {tick.PassportNumber} - {tick.Country.Name}\n";
                }
            }

            return bookingInfor;
        }

        private string GetBr()
        {
            while (true)
            {
                var br = System.IO.Path.GetRandomFileName().Substring(0, 6).ToUpper();
                if (Db.Context.Tickets.Select(t => t.BookingReference).Contains(br) == false)
                {
                    return br;
                }
            }
        }
    }
}
