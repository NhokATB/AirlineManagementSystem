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
    /// Interaction logic for AircraftManagementWindow.xaml
    /// </summary>
    public partial class AircraftManagementWindow : Window
    {
        Aircraft currentAircraft;
        public AircraftManagementWindow()
        {
            InitializeComponent();
            this.Loaded += AircraftManagementWindow_Loaded;
            dgAircrafts.SelectedCellsChanged += DgAircrafts_SelectedCellsChanged;
        }

        private void DgAircrafts_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentAircraft = dgAircrafts.SelectedItem as Aircraft;
            }
            catch (Exception)
            {
            }
        }

        private void AircraftManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAircrafts();
        }

        public void LoadAircrafts()
        {
            dgAircrafts.ItemsSource = null;
            var aircrafts = Db.Context.Aircrafts.ToList();
            dgAircrafts.ItemsSource = aircrafts;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDeleteAircraft_Click(object sender, RoutedEventArgs e)
        {
            if (currentAircraft != null)
            {
                if (currentAircraft.Schedules.Count == 0)
                {
                    if (MessageBox.Show("Do you want to delete this aircraft?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        Db.Context.Aircrafts.Remove(currentAircraft);
                        Db.Context.SaveChanges();
                        LoadAircrafts();
                        currentAircraft = null;
                    }
                }
                else
                {
                    MessageBox.Show("This aircraft can not be deleted because it had schedules", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a aircraft!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditAircraft_Click(object sender, RoutedEventArgs e)
        {
            if (currentAircraft != null)
            {
                try
                {
                    EditAircraftWindow wEditAircraft = new EditAircraftWindow();
                    wEditAircraft.Aircraft = currentAircraft;
                    wEditAircraft.ManageWindow = this;
                    wEditAircraft.ShowDialog();
                    currentAircraft = null;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show("Please choose a aircraft!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddAircraft_Click(object sender, RoutedEventArgs e)
        {
            AddAircraftWindow wAddAircraft = new AddAircraftWindow();
            wAddAircraft.ManageWindow = this;
            wAddAircraft.ShowDialog();
        }
    }
}
