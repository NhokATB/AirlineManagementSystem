using AirportManagerSystem.HelperClass;
using AirportManagerSystem.Model;
using AirportManagerSystem.UserControls;
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
    /// Interaction logic for FlightProcessWindow.xaml
    /// </summary>
    public partial class FlightProcessWindow : Window
    {
        List<string> criterias = new List<string>() { "Date - time", "Flight time" };
        public FlightProcessWindow()
        {
            InitializeComponent();
            this.Loaded += FlightProcessWindow_Loaded;
            cbSortBy.SelectionChanged += CbSortBy_SelectionChanged;
        }

        private void CbSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LoadFlightProcess();
            }
            catch (Exception)
            {
            }
        }

        private void FlightProcessWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cbSortBy.ItemsSource = criterias;
            cbSortBy.SelectedIndex = 0;
        }

        private void LoadFlightProcess()
        {
            stpFlights.Children.Clear();

            var today = DateTime.Now.Date;
            var tomorow = today.AddDays(1);
            var flights = Db.Context.Schedules.Where(t => t.Date == today || t.Date == tomorow).ToList();

            if (criterias[cbSortBy.SelectedIndex] == "Date - time")
            {
                flights = flights.OrderByDescending(t => t.Date + t.Time).ToList();
            }
            else
            {
                flights = flights.OrderByDescending(t => t.Route.FlightTime).ToList();
            }

            int i = 0;
            foreach (var item in flights)
            {
                UcFlightProcess ucFlightProcess = new UcFlightProcess();
                ucFlightProcess.Flight = item;
                ucFlightProcess.Background = i % 2 == 0 ? new SolidColorBrush(AMONICColor.LightBlue) : new SolidColorBrush(AMONICColor.DarkGreen); ;
                stpFlights.Children.Add(ucFlightProcess);
                i++;
            }
        }
    }
}
