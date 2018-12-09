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
    /// Interaction logic for SetUpGateForFlightWindow.xaml
    /// </summary>
    public partial class SetUpGateForFlightWindow : Window
    {
        List<Schedule> schedules;
        List<NewFlight> flights;
        NewFlight currentFlight;

        string from, to, flightNumber;
        DateTime? date;

        public SetUpGateForFlightWindow()
        {
            InitializeComponent();
            this.Loaded += SetUpGateForFlightWindow_Loaded;
            this.dgFlights.LoadingRow += DgFlights_LoadingRow;
        }

        private void DgFlights_LoadingRow(object sender, DataGridRowEventArgs e)
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

        private void SetUpGateForFlightWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dpOutbound.SelectedDate = DateTime.Now.Date;

            var arrivalAirports = Db.Context.Airports.ToList();
            arrivalAirports.Insert(0, new Airport() { Name = "All airports" });
            cbArrivalAirport.ItemsSource = arrivalAirports;
            cbArrivalAirport.DisplayMemberPath = "Name";
            cbArrivalAirport.SelectedIndex = 0;

            var departureAirports = Db.Context.Airports.ToList();
            departureAirports.Insert(0, new Airport() { Name = "All airports" });
            cbDepatureAirport.ItemsSource = departureAirports;
            cbDepatureAirport.DisplayMemberPath = "Name";
            cbDepatureAirport.SelectedIndex = 0;

            SetParameter();
            LoadFlights();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (dpOutbound.SelectedDate == null)
            {
                MessageBox.Show("Outbound was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
            date = dpOutbound.SelectedDate;
            flightNumber = txtFlightNumber.Text;
        }

        public void LoadFlights()
        {
            FilterFlights();
            DisplayFlights();
        }

        private void SetUpGate(object sender, RoutedEventArgs e)
        {
            var flight = dgFlights.SelectedItem as NewFlight;

            if (flight.Schedule.Confirmed == false)
            {
                MessageBox.Show("Cann't set up gate for this flight because it was canceled", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((flight.Schedule.Date + flight.Schedule.Time) < DateTime.Now)
            {
                MessageBox.Show("Cann't set up gate for this flight because it was took off", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ChooseGateWindow chooseGateWindow = new ChooseGateWindow();
            chooseGateWindow.Flight = flight;
            chooseGateWindow.ManageForm = this;
            chooseGateWindow.ShowDialog();
        }

        private void FilterFlights()
        {
            try
            {
                schedules = Db.Context.Schedules.Where(t => t.Date == date.Value).ToList().OrderBy(t => t.Date + t.Time).ToList();

                if (from.Contains("All") == false)
                    schedules = schedules.Where(t => t.Route.Airport.Name == from).ToList();
                if (to.Contains("All") == false)
                    schedules = schedules.Where(t => t.Route.Airport1.Name == to).ToList();
                if (flightNumber != "")
                    schedules = schedules.Where(t => t.FlightNumber == flightNumber).ToList();
            }
            catch (Exception)
            {
            }
        }

        private void DisplayFlights()
        {
            dgFlights.ItemsSource = null;

            flights = new List<NewFlight>();

            foreach (var item in schedules)
            {
                flights.Add(new NewFlight()
                {
                    Schedule = item,
                    Status = item.Gate == null ? "Set Up Gate" : "Change gate",
                    Aircraft = item.Aircraft.Name + " " + item.Aircraft.MakeModel,
                    Gate = item.Gate == null ? "None" : item.Gate.ToString()
                });
            }

            dgFlights.ItemsSource = flights;
        }
    }
}
