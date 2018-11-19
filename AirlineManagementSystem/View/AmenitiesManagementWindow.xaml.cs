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
    /// Interaction logic for AmenitiesManagementWindow.xaml
    /// </summary>
    public partial class AmenitiesManagementWindow : Window
    {
        Amenity currentAmenity;
        public AmenitiesManagementWindow()
        {
            InitializeComponent();
            this.Loaded += AmenitiesManagementWindow_Loaded;
            dgAmenities.SelectedCellsChanged += DgAmenities_SelectedCellsChanged;
        }

        private void DgAmenities_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentAmenity = dgAmenities.CurrentItem as Amenity;
            }
            catch (Exception)
            {
            }
        }

        private void AmenitiesManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAmenities();
        }

        public void LoadAmenities()
        {
            dgAmenities.ItemsSource = null;
            var amenities = Db.Context.Amenities.ToList();
            dgAmenities.ItemsSource = amenities;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddAmenity_Click(object sender, RoutedEventArgs e)
        {
            AddAmenityWindow wAddAmenity = new AddAmenityWindow();
            wAddAmenity.ManageWindow = this;
            wAddAmenity.ShowDialog();
        }

        private void btnEditAmenity_Click(object sender, RoutedEventArgs e)
        {
            if (currentAmenity != null)
            {
                try
                {
                    EditAmenityWindow wEditAmenity = new EditAmenityWindow();
                    wEditAmenity.Amenity = currentAmenity;
                    wEditAmenity.ManageWindow = this;
                    wEditAmenity.ShowDialog();
                    currentAmenity = null;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show("Please choose a amenity!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteAmenity_Click(object sender, RoutedEventArgs e)
        {
            if (currentAmenity != null)
            {
                if (currentAmenity.AmenitiesTickets.Count == 0)
                {
                    if (MessageBox.Show("Do you want to delete this amenity?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        Db.Context.Amenities.Remove(currentAmenity);
                        Db.Context.SaveChanges();
                        LoadAmenities();
                        currentAmenity = null;
                    }
                }
                else
                {
                    MessageBox.Show("This amenity can not be deleted because it was purchased", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a amenity!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
