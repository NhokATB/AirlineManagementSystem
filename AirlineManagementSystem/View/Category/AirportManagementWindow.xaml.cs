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
    /// Interaction logic for AirportManagementWindow.xaml
    /// </summary>
    public partial class AirportManagementWindow : Window
    {
        Airport currentAirport;
        public AirportManagementWindow()
        {
            InitializeComponent();
            this.Loaded += AirportManagementWindow_Loaded;
            dgAirports.SelectedCellsChanged += DgAirports_SelectedCellsChanged;
        }

        private void DgAirports_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentAirport = dgAirports.SelectedItem as Airport;
            }
            catch (Exception)
            {
            }
        }

        private void AirportManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAirports();
        }

        public void LoadAirports()
        {
            dgAirports.ItemsSource = null;
            var airports = Db.Context.Airports.ToList();
            dgAirports.ItemsSource = airports;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEditAirport_Click(object sender, RoutedEventArgs e)
        {
            if (currentAirport == null)
            {
                MessageBox.Show("Please choose a airport!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                EditAirportWindow editAirportWindow = new EditAirportWindow();
                editAirportWindow.ManageWindow = this;
                editAirportWindow.Airport = currentAirport;
                editAirportWindow.ShowDialog();
                currentAirport = null;
            }
        }

        private void btnAddAirport_Click(object sender, RoutedEventArgs e)
        {
            AddAirportWindow addAirportWindow = new AddAirportWindow();
            addAirportWindow.ManageWindow = this;
            addAirportWindow.ShowDialog();
        }

        private void btnDeleteAirport_Click(object sender, RoutedEventArgs e)
        {
            if (currentAirport != null)
            {
                if (currentAirport.Routes.Count == 0 || currentAirport.Routes1.Count == 0)
                {
                    if (MessageBox.Show("Do you want to delete this airport?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        Db.Context.Airports.Remove(currentAirport);
                        Db.Context.SaveChanges();
                        LoadAirports();
                        currentAirport = null;
                    }
                }
                else
                {
                    MessageBox.Show("This airport can not be deleted because it was related route", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a airport!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
