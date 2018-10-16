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
using AirportManagerSystem.Model;

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
            string br = GetBr();
            foreach (var item in Tickets)
            {
                item.BookingReference = br;
                Db.Context.Tickets.Add(item);
            }
            Db.Context.SaveChanges();

            IsConfirm = true;
            MessageBox.Show("Issue ticket successful","Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private string GetBr()
        {
            while (true)
            {
                var br = System.IO.Path.GetRandomFileName().Substring(0, 6).ToUpper();
                if (Db.Context.Tickets.Select(t => t.BookingReference).Contains(br) == false)
                    return br;
            }
        }
    }
}
