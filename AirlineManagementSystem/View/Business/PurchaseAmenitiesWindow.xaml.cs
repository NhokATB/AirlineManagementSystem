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
    /// Interaction logic for PurchaseAmenitiesWindow.xaml
    /// </summary>
    public partial class PurchaseAmenitiesWindow : Window
    {

        public PurchaseAmenitiesWindow()
        {
            InitializeComponent();
            this.Loaded += PurchaseAmenitiesWindow_Loaded;
            cbFlights.SelectionChanged += CbFlights_SelectionChanged;
        }

        private void CbFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetAmen();
        }

        private void PurchaseAmenitiesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ResetAll();

            if (BookingReference != null)
            {
                txtBookingReference.Text = BookingReference;
                txtBookingReference.IsEnabled = false;
                btnOk_Click(btnOk, new RoutedEventArgs());
            }
        }

        Ticket updatedTicket;
        List<int> ticketIds = new List<int>();
        decimal total = 0, duty = 0, dutyPayed = 0, payed = 0;

        public string BookingReference { get; internal set; }

        private void ResetAll()
        {
            ticketIds.Clear();
            cbFlights.Items.Clear();
            btnShowAmenities.IsEnabled = false;
            ResetAmen();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();

            if (txtBookingReference.Text == "")
            {
                MessageBox.Show("Enter booking reference!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var tickets = Db.Context.Tickets.Where(t => t.BookingReference == txtBookingReference.Text && t.Confirmed).ToList();
            if (tickets.Count == 0)
            {
                MessageBox.Show("This booking reference not found!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var item in tickets)
            {
                var date = item.Schedule.Date + item.Schedule.Time;

                if ((date - DateTime.Now).TotalHours >= 24)
                {
                    ticketIds.Add(item.ID);
                    cbFlights.Items.Add($"{item.Schedule.FlightNumber}, {item.Schedule.Route.Airport.IATACode} - {item.Schedule.Route.Airport1.IATACode}, {item.Schedule.Date.ToString("dd/MM/yyyy")}, {item.Schedule.Time.ToString(@"hh\:mm")} - {item.Firstname} {item.Lastname}");
                }
            }

            if (ticketIds.Count == 0)
            {
                MessageBox.Show("This service is avalable up to 24 hours before each flight", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                cbFlights.SelectedIndex = 0;
                btnShowAmenities.IsEnabled = true;
            }
        }

        private void btnShowAmenities_Click(object sender, RoutedEventArgs e)
        {
            ResetAmen();
            Db.Context = new AirlineManagementSystemEntities();

            updatedTicket = Db.Context.Tickets.Find(ticketIds[cbFlights.SelectedIndex]);

            tblFullName.Text = updatedTicket.Firstname + " " + updatedTicket.Lastname;
            tblPassportNumber.Text = updatedTicket.PassportNumber;
            tblCabinType.Text = updatedTicket.CabinType.Name;

            LoadAmenities();

            if (updatedTicket.Confirmed == false)
            {
                wpAmenities.IsEnabled = false;
                MessageBox.Show("This ticket was cancelled", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (updatedTicket.Schedule.Confirmed == false)
            {
                wpAmenities.IsEnabled = false;
                MessageBox.Show("This schedule was cancelled", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                wpAmenities.IsEnabled = true;
            }
        }

        private void LoadAmenities()
        {
            var amenities = updatedTicket.CabinType.Amenities.ToList();
            foreach (var item in amenities)
            {
                CheckBox c = new CheckBox()
                {
                    Content = item.Price == 0 ? $"{item.Service} (Free)" : $"{item.Service} ({item.Price.ToString("C0")})",
                    Width = 300,
                    Tag = item,
                    IsEnabled = item.Price != 0,
                    Margin = new Thickness(5)
                };

                c.Checked += C_Checked;
                c.Unchecked += C_Unchecked;
                c.IsChecked = item.Price == 0;
                
                if (updatedTicket.AmenitiesTickets.Select(t => t.AmenityID).Contains(item.ID))
                {
                    payed += item.Price;
                    dutyPayed += item.Price * 5 / 100;
                    c.IsChecked = true;
                }

                wpAmenities.Children.Add(c);
            }

            btnSaveAndCofirm.IsEnabled = false;
        }

        private void C_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckedChanged(sender);
        }

        private void C_Checked(object sender, RoutedEventArgs e)
        {
            CheckedChanged(sender);
        }

        private void CheckedChanged(object sender)
        {
            var c = sender as CheckBox;
            var price = (c.Tag as Amenity).Price;
            var id = (c.Tag as Amenity).ID;
            if (c.IsChecked.Value)
            {
                total += price;
                duty += price * 5 / 100;

                if (price != 0)
                {
                    if (updatedTicket.AmenitiesTickets.Select(t => t.AmenityID).Contains(id) == false)
                    {
                        updatedTicket.AmenitiesTickets.Add(new AmenitiesTicket()
                        {
                            Price = price,
                            AmenityID = id
                        });
                    }
                }
            }
            else
            {
                total -= price;
                duty -= price * 5 / 100;

                var amen = updatedTicket.AmenitiesTickets.Where(t => t.AmenityID == id).FirstOrDefault();
                updatedTicket.AmenitiesTickets.Remove(amen);
            }

            tblTotalSelected.Text = total.ToString("C2");
            tblDuties.Text = duty.ToString("C2");
            tblPayable.Text = (total - payed + duty - dutyPayed).ToString("C2");
            tblPayed.Text = payed.ToString("C2");

            if (total < payed)
                tblPayable.Text += " return";

            btnSaveAndCofirm.IsEnabled = true;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetAmen()
        {
            wpAmenities.Children.Clear();
            tblFullName.Text = tblPassportNumber.Text = tblCabinType.Text = "";
            tblTotalSelected.Text = tblPayable.Text = tblDuties.Text = tblPayed.Text = "";
            btnSaveAndCofirm.IsEnabled = false;
            total = duty = dutyPayed = payed = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSaveAndCofirm_Click(object sender, RoutedEventArgs e)
        {
            Db.Context.SaveChanges();
            tblPayable.Text = "$0.00";
            dutyPayed = duty;
            payed = total;
            btnSaveAndCofirm.IsEnabled = false;
            MessageBox.Show("Purchase amenities successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
