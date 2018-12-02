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
    /// Interaction logic for SetUpCrewWindow.xaml
    /// </summary>
    public partial class SetUpCrewWindow : Window
    {
        NewFlight currentFlight;
        public SetUpCrewWindow()
        {
            InitializeComponent();
            dpOutbound.SelectedDate = DateTime.Now.Date;
            dgFlights.SelectedCellsChanged += DgFlights_SelectedCellsChanged;
        }

        private void DgFlights_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentFlight = dgFlights.SelectedItem as NewFlight;
            }
            catch (Exception)
            {
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (dpOutbound.SelectedDate == null)
            {
                MessageBox.Show("Date was required", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LoadFlights();
        }

        public void LoadFlights()
        {
            currentFlight = null;
            dgFlights.ItemsSource = null;

            var date = dpOutbound.SelectedDate.Value.Date;
            var flightNumber = txtFlightNumber.Text.Trim();
            var schedules = Db.Context.Schedules.Where(t => t.Date == date).ToList();
            if (txtFlightNumber.Text.Trim() != "")
            {
                schedules = schedules.Where(t => t.FlightNumber == flightNumber).ToList();
            }

            var flights = new List<NewFlight>();
            foreach (var item in schedules)
            {
                flights.Add(new NewFlight()
                {
                    Aircraft = item.Aircraft.Name + " " + item.Aircraft.MakeModel,
                    Schedule = item,
                    EconomyPrice = (int)item.EconomyPrice,
                    Crew = item.CrewId == null ? "None" : item.Crew.CrewName
                });
            }

            dgFlights.ItemsSource = flights;
        }

        private void btnSetupCrew_Click(object sender, RoutedEventArgs e)
        {
            if (currentFlight != null)
            {
                if ((currentFlight.Schedule.Date + currentFlight.Schedule.Time) < DateTime.Now)
                {
                    MessageBox.Show("This flight cannot be changed because it was took off", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (currentFlight.Schedule.CrewId != null)
                    {
                        if (MessageBox.Show("This flight was set up the crew. Do you want to change crew for it?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                        {
                            ChooseCrewWindow chooseCrewWindow = new ChooseCrewWindow();
                            chooseCrewWindow.Flight = currentFlight;
                            chooseCrewWindow.ManageWindow = this;
                            chooseCrewWindow.ShowDialog();
                        }
                    }
                    else
                    {
                        ChooseCrewWindow chooseCrewWindow = new ChooseCrewWindow();
                        chooseCrewWindow.Flight = currentFlight;
                        chooseCrewWindow.ManageWindow = this;
                        chooseCrewWindow.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose a flight", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
