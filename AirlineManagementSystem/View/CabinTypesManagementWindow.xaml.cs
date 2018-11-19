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
    /// Interaction logic for CabinTypesManagementWindow.xaml
    /// </summary>
    public partial class CabinTypesManagementWindow : Window
    {
        CabinType currentCabin;
        public CabinTypesManagementWindow()
        {
            InitializeComponent();
            this.Loaded += CabinTypesManagementWindow_Loaded;
            dgCabinTypes.SelectedCellsChanged += DgCabinTypes_SelectedCellsChanged;
        }

        private void DgCabinTypes_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentCabin = dgCabinTypes.SelectedItem as CabinType;
            }
            catch (Exception)
            {
            }
        }

        private void CabinTypesManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCabinTypes();
        }

        public void LoadCabinTypes()
        {
            dgCabinTypes.ItemsSource = null;
            var cabinTypes = Db.Context.CabinTypes.ToList();
            dgCabinTypes.ItemsSource = cabinTypes;
        }

        private void btnEditCabinType_Click(object sender, RoutedEventArgs e)
        {
            if (currentCabin != null)
            {
                try
                {
                    EditCabinTypeWindow wEditCabinType = new EditCabinTypeWindow();
                    wEditCabinType.Cabin = currentCabin;
                    wEditCabinType.ManageWindow = this;
                    wEditCabinType.ShowDialog();
                    currentCabin = null;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show("Please choose a cabin type!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteCabinType_Click(object sender, RoutedEventArgs e)
        {
            if (currentCabin != null)
            {
                if (currentCabin.Tickets.Count == 0)
                {
                    if (MessageBox.Show("Do you want to delete this cabin type?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        Db.Context.CabinTypes.Remove(currentCabin);
                        Db.Context.SaveChanges();
                        LoadCabinTypes();
                        currentCabin = null;
                    }
                }
                else
                {
                    MessageBox.Show("This cabin type can not be deleted because it was related to tickets", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a cabin type!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddCabinType_Click(object sender, RoutedEventArgs e)
        {
            AddCabinTypeWindow wAddCabinType = new AddCabinTypeWindow();
            wAddCabinType.ManageWindow = this;
            wAddCabinType.ShowDialog();
        }

        private void AmenitiesList(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    var cabin = row.Item as CabinType;
                    string str = "";
                    int i = 1;

                    foreach (var item in cabin.Amenities.ToList())
                    {
                        str += i + ". " + item.Service + "\n";
                        i++;
                    }

                    MessageBox.Show(str, $"Amenities of cabin: {cabin.Name}", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
