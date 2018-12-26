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
    /// Interaction logic for RoutesManagementWindow.xaml
    /// </summary>
    public partial class RoutesManagementWindow : Window
    {
        List<string> criterias = new List<string>() { "Distance", "Flight Time" };
        List<Airport> arrivalAirports;
        List<Airport> departureAirports;
        Route currentRoute;

        public RoutesManagementWindow()
        {
            InitializeComponent();
            this.Loaded += RoutesManagementWindow_Loaded;
            dgRoutes.SelectedCellsChanged += DgRoutes_SelectedCellsChanged;
        }

        private void DgRoutes_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentRoute = dgRoutes.SelectedItem as Route;
            }
            catch (Exception)
            {
            }
        }

        private void RoutesManagementWindow_Loaded(object sender, RoutedEventArgs e)
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

            cbSortBy.ItemsSource = criterias;
            cbSortBy.SelectedIndex = 0;

            LoadRoutes();
        }

        public void LoadRoutes()
        {
            dgRoutes.ItemsSource = null;

            var routes = Db.Context.Routes.ToList();
            if (cbArrivalAirport.SelectedIndex != 0)
                routes = routes.Where(t => t.Airport1.Name == cbArrivalAirport.Text).ToList();
            if (cbDepatureAirport.SelectedIndex != 0)
                routes = routes.Where(t => t.Airport.Name == cbDepatureAirport.Text).ToList();
            if (cbSortBy.SelectedIndex == 0)
                routes = routes.OrderByDescending(t => t.Distance).ToList();
            else routes = routes.OrderByDescending(t => t.FlightTime).ToList();

            dgRoutes.ItemsSource = routes;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            LoadRoutes();
        }

        private void btnAddRoute_Click(object sender, RoutedEventArgs e)
        {
            AddRouteWindow wAddRoute = new AddRouteWindow();
            wAddRoute.ManageWindow = this;
            wAddRoute.ShowDialog();
        }

        private void btnEditRoute_Click(object sender, RoutedEventArgs e)
        {
            if (currentRoute != null)
            {
                try
                {
                    EditRouteWindow wEditRoute = new EditRouteWindow();
                    wEditRoute.Route = currentRoute;
                    wEditRoute.ManageWindow = this;
                    wEditRoute.ShowDialog();
                    currentRoute = null;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show("Please choose a route!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteRoute_Click(object sender, RoutedEventArgs e)
        {
            if (currentRoute != null)
            {
                if (currentRoute.Schedules.Count == 0)
                {
                    if (MessageBox.Show("Do you want to delete this route?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        Db.Context.Routes.Remove(currentRoute);
                        Db.Context.SaveChanges();
                        LoadRoutes();
                        currentRoute = null;
                    }
                }
                else
                {
                    MessageBox.Show("This route can not be deleted because it had schedules", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a route!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
