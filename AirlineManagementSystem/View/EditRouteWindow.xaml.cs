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
    /// Interaction logic for EditRouteWindow.xaml
    /// </summary>
    public partial class EditRouteWindow : Window
    {
        List<Airport> arrivalAirports;
        List<Airport> departureAirports;

        public RoutesManagementWindow ManageWindow { get; internal set; }
        public Route Route { get; internal set; }

        public EditRouteWindow()
        {
            InitializeComponent();
            this.Loaded += EditRouteWindow_Loaded;
        }

        private void EditRouteWindow_Loaded(object sender, RoutedEventArgs e)
        {
            arrivalAirports = Db.Context.Airports.ToList();
            arrivalAirports.Insert(0, new Airport() { Name = "All airports" });
            cbArrivalAirport.ItemsSource = arrivalAirports;
            cbArrivalAirport.DisplayMemberPath = "Name";

            departureAirports = Db.Context.Airports.ToList();
            departureAirports.Insert(0, new Airport() { Name = "All airports" });
            cbDepatureAirport.ItemsSource = departureAirports;
            cbDepatureAirport.DisplayMemberPath = "Name";

            cbDepatureAirport.SelectedItem = Route.Airport;
            cbArrivalAirport.SelectedItem = Route.Airport1;

            txtDistance.Text = Route.Distance.ToString();
            txtFlightTime.Text = Route.FlightTime.ToString();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
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

            Route.Distance = (int)distance;
            Route.FlightTime = (int)flightTime;

            Db.Context.SaveChanges();
            ManageWindow.LoadRoutes();
            MessageBox.Show("Edit route successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
