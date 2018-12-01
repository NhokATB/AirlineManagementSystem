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
        List<Crew> crews;

        public FlightManagementWindow ManageWindow { get; internal set; }
        internal NewFlight Flight { get; set; }

        public SetUpCrewWindow()
        {
            InitializeComponent();
            this.Loaded += SetUpCrewWindow_Loaded;
            this.Title += $"{Flight.Schedule.FlightNumber} - {Flight.Schedule.Date.ToString("dd/MM/yyyy")} - {Flight.Schedule.Time.ToString(@"hh\:mm")} - {Flight.Schedule.Route.Airport.IATACode} to {Flight.Schedule.Route.Airport1.IATACode}";
        }

        private void SetUpCrewWindow_Loaded(object sender, RoutedEventArgs e)
        {
            crews = Db.Context.Crews.ToList();
            cbCrews.ItemsSource = crews;
            cbCrews.DisplayMemberPath = "CrewName";
            cbCrews.SelectedIndex = 0;

            if (Flight.Crew != "None")
            {
                cbCrews.SelectedItem = Flight.Schedule.Crew;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbCrews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCrewMembers();
        }

        private void LoadCrewMembers()
        {
            var crew = crews[cbCrews.SelectedIndex];
            dgMembers.ItemsSource = null;
            dgMembers.ItemsSource = crew.CrewMembers.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Flight.Schedule.Crew = crews[cbCrews.SelectedIndex];
            Db.Context.SaveChanges();
            ManageWindow.LoadFlights();
            MessageBox.Show("Set up crew successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
