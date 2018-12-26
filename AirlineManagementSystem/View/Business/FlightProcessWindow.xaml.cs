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
        bool isDateTimeIncrease;
        bool isFlightTimeIncrease;
        public FlightProcessWindow()
        {
            InitializeComponent();
            this.Loaded += FlightProcessWindow_Loaded;
            cbSortBy.SelectionChanged += CbSortBy_SelectionChanged;

            this.StateChanged += FlightProcessWindow_StateChanged;

            this.WindowState = WindowState.Maximized;
        }

        private void FlightProcessWindow_StateChanged(object sender, EventArgs e)
        {
            scvFlightProcess.Height = this.WindowState == WindowState.Maximized ? 630 : 500;
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
            scvFlightProcess.Height = 630;
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
                if (isDateTimeIncrease)
                {
                    flights = flights.OrderByDescending(t => t.Date + t.Time).ToList();
                    isDateTimeIncrease = false;
                }
                else
                {
                    flights = flights.OrderBy(t => t.Date + t.Time).ToList();
                    isDateTimeIncrease = true;
                }
            }
            else
            {
                if (isFlightTimeIncrease)
                {
                    flights = flights.OrderByDescending(t => t.Route.FlightTime).ToList();
                    isFlightTimeIncrease = false;
                }
                else
                {
                    flights = flights.OrderBy(t => t.Route.FlightTime).ToList();
                    isFlightTimeIncrease = true;
                }
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
