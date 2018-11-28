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
    /// Interaction logic for AddRouteWindow.xaml
    /// </summary>
    public partial class AddRouteWindow : Window
    {
        List<Airport> arrivalAirports;
        List<Airport> departureAirports;
        public AddRouteWindow()
        {
            InitializeComponent();
            this.Loaded += AddRouteWindow_Loaded;
        }

        public RoutesManagementWindow ManageWindow { get; internal set; }

        private void AddRouteWindow_Loaded(object sender, RoutedEventArgs e)
        {
            arrivalAirports = Db.Context.Airports.ToList();
            cbArrivalAirport.ItemsSource = arrivalAirports;
            cbArrivalAirport.DisplayMemberPath = "Name";
            cbArrivalAirport.SelectedIndex = 0;

            departureAirports = Db.Context.Airports.ToList();
            cbDepatureAirport.ItemsSource = departureAirports;
            cbDepatureAirport.DisplayMemberPath = "Name";
            cbDepatureAirport.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbArrivalAirport.Text == cbDepatureAirport.Text)
            {
                MessageBox.Show("Arrival airport and departure airport cannot be the same!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                double.Parse(txtDistance.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Distance must be digits!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                double.Parse(txtFlightTime.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Flight time must be digits!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var flightTime = double.Parse(txtFlightTime.Text);
            var distance = double.Parse(txtDistance.Text);

            if (Db.Context.Routes.ToList().Where(t => t.Airport.Name == cbDepatureAirport.Text && t.Airport1.Name == cbArrivalAirport.Name && t.Distance == distance && t.FlightTime == flightTime).FirstOrDefault() != null)
            {
                MessageBox.Show("This route was exists!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Route route = new Route()
            {
                Airport = departureAirports[cbDepatureAirport.SelectedIndex],
                Airport1 = arrivalAirports[cbArrivalAirport.SelectedIndex],
                Distance = (int)distance,
                FlightTime = (int)flightTime
            };

            Db.Context.Routes.Add(route);
            Db.Context.SaveChanges();
            ManageWindow.LoadRoutes();
            MessageBox.Show("Add route successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void cbDepatureAirport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var from = departureAirports[cbDepatureAirport.SelectedIndex].Name;
            var to = arrivalAirports[cbArrivalAirport.SelectedIndex].Name;
            Route route;
            if ((route = Db.Context.Routes.Where(t => t.Airport.Name == from && t.Airport1.Name == to).FirstOrDefault()) != null)
            {
                txtDistance.Text = route.Distance.ToString();
                txtDistance.IsEnabled = false;
            }
            else
            {
                txtDistance.Text = "";
                txtDistance.IsEnabled = true;
            }
        }

        private void cbArrivalAirport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var from = departureAirports[cbDepatureAirport.SelectedIndex].Name;
            var to = arrivalAirports[cbArrivalAirport.SelectedIndex].Name;
            Route route;
            if ((route = Db.Context.Routes.Where(t => t.Airport.Name == from && t.Airport1.Name == to).FirstOrDefault()) != null)
            {
                txtDistance.Text = route.Distance.ToString();
                txtDistance.IsEnabled = false;
            }
            else
            {
                txtDistance.Text = "";
                txtDistance.IsEnabled = true;
            }
        }
    }
}
