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
    /// Interaction logic for FlightManagementWindow.xaml
    /// </summary>
    public partial class FlightManagementWindow : Window
    {
        List<string> criterias = new List<string>() { "Date - Time", "Economy price", "Confirmed" };
        List<Airport> arrivalAirports;
        List<Airport> departureAirports;
        List<Schedule> schedules;
        List<NewFlight> flights;
        NewFlight currentFlight;

        string from, to, flightNumber;
        DateTime? date;
        int sortBy;

        public FlightManagementWindow()
        {
            InitializeComponent();
            this.Loaded += FlightManagementWindow_Loaded;
            dgFlights.LoadingRow += dgFlights_LoadingRow;
            dgFlights.SelectedCellsChanged += DgFlights_SelectedCellsChanged;
        }

        private void DgFlights_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentFlight = dgFlights.CurrentItem as NewFlight;

                if (currentFlight.Schedule.Confirmed)
                {
                    btnCancelFlight.Content = "Cancel Flight";
                    btnCancelFlight.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    btnCancelFlight.Content = "Confirm Flight";
                    btnCancelFlight.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgFlights_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var row = e.Row;
            var flight = e.Row.Item as NewFlight;
            if (flight.Schedule.Confirmed == false)
            {
                row.Background = new SolidColorBrush(Color.FromRgb(247, 148, 32));
            }
            else
            {
                row.Background = new SolidColorBrush(Colors.White);
            }
        }

        private void FlightManagementWindow_Loaded(object sender, RoutedEventArgs e)
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

            cbSorBy.ItemsSource = criterias;
            cbSorBy.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (cbDepatureAirport.Text == cbArrivalAirport.Text && cbDepatureAirport.SelectedIndex != 0)
            {
                MessageBox.Show("Airport cannot be the same", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SetParameter();
            LoadFlights();
            if (dgFlights.Items.Count == 0)
                MessageBox.Show("No result", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void SetParameter()
        {
            from = cbDepatureAirport.Text;
            to = cbArrivalAirport.Text;
            sortBy = cbSorBy.SelectedIndex;
            date = dpOutbound.SelectedDate;
            flightNumber = txtFlightNumber.Text;
        }
        public void LoadFlights()
        {
            FilterFlights();
            SortFlights();
            DisplayFlights();
        }

        private void FilterFlights()
        {
            schedules = Db.Context.Schedules.ToList();
            if (from.Contains("All") == false)
                schedules = schedules.Where(t => t.Route.Airport.Name == from).ToList();
            if (to.Contains("All") == false)
                schedules = schedules.Where(t => t.Route.Airport1.Name == to).ToList();
            if (flightNumber != "")
                schedules = schedules.Where(t => t.FlightNumber == flightNumber).ToList();
            if (date != null)
                schedules = schedules.Where(t => t.Date == date.Value.Date).ToList();
        }

        private void btnCancelFlight_Click(object sender, RoutedEventArgs e)
        {
            if (currentFlight != null)
            {
                if (currentFlight.Schedule.Date + currentFlight.Schedule.Time < DateTime.Now)
                {
                    MessageBox.Show($"This flight cannot be canceled/Confirmed because it took off!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                currentFlight.Schedule.Confirmed = !currentFlight.Schedule.Confirmed;
                Db.Context.SaveChanges();
                LoadFlights();
                MessageBox.Show(btnCancelFlight.Content + " successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please choose a flight", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteFlight_Click(object sender, RoutedEventArgs e)
        {
            if (currentFlight != null)
            {
                if (currentFlight.Schedule.Tickets.Count == 0)
                {
                    if (MessageBox.Show("Do you want to delete this flight?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        Db.Context.Schedules.Remove(currentFlight.Schedule);
                        Db.Context.SaveChanges();
                        LoadFlights();
                        currentFlight = null;
                    }
                }
                else
                {
                    MessageBox.Show("This flight can not be deleted because it is related to tickets", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a flight!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddFlight_Click(object sender, RoutedEventArgs e)
        {
            AddFlightWindow wAddFlight = new AddFlightWindow();
            wAddFlight.ManageWindow = this;
            wAddFlight.ShowDialog();
        }

        private void btnEditFlight_Click(object sender, RoutedEventArgs e)
        {
            if (currentFlight != null)
            {
                try
                {
                    if (currentFlight.Schedule.Date + currentFlight.Schedule.Time < DateTime.Now)
                    {
                        MessageBox.Show("This flight cannot be changed because it took off!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    EditFlightWindow wEditFlight = new EditFlightWindow();
                    wEditFlight.Flight = currentFlight;
                    wEditFlight.ManageWindow = this;
                    wEditFlight.ShowDialog();
                    currentFlight = null;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show("Please choose a flight!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSetupCrew_Click(object sender, RoutedEventArgs e)
        {
            if (currentFlight != null)
            {
                try
                {
                    if (currentFlight.Schedule.Date + currentFlight.Schedule.Time < DateTime.Now)
                    {
                        MessageBox.Show("This flight cannot be changed because it took off!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    ChooseCrewWindow setUpCrewWindow = new ChooseCrewWindow();
                    setUpCrewWindow.Flight = currentFlight;
                    setUpCrewWindow.ShowDialog();
                    currentFlight = null;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show("Please choose a flight!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnImportChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SortFlights()
        {
            if (sortBy == 0)
                schedules = schedules.OrderByDescending(t => t.Date + t.Time).ToList();
            else if (sortBy == 1) schedules = schedules.OrderByDescending(t => t.EconomyPrice).ToList();
            else schedules = schedules.OrderByDescending(t => t.Confirmed).ToList();
        }
        private void DisplayFlights()
        {
            dgFlights.ItemsSource = null;

            flights = new List<NewFlight>();

            foreach (var item in schedules)
            {
                double price = (int)item.EconomyPrice;
                double bprice = Math.Floor(price * 1.35);
                double fprice = Math.Floor(bprice * 1.3);

                flights.Add(new NewFlight()
                {
                    EconomyPrice = price,
                    BusinessPrice = bprice,
                    FirstClassPrice = fprice,
                    Schedule = item,
                    Aircraft = item.Aircraft.Name + " " + item.Aircraft.MakeModel,
                    Crew = item.CrewId == null ? "None" : item.Crew.CrewName
                });
            }

            dgFlights.ItemsSource = flights;
        }
    }
}
