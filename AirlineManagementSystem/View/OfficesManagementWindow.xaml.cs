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
    /// Interaction logic for OfficesManagementWindow.xaml
    /// </summary>
    public partial class OfficesManagementWindow : Window
    {
        Office currentOffice;
        public OfficesManagementWindow()
        {
            InitializeComponent();
            this.Loaded += OfficesManagementWindow_Loaded;
            dgOffices.SelectedCellsChanged += DgOffices_SelectedCellsChanged;
        }

        private void DgOffices_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentOffice = dgOffices.SelectedItem as Office;
            }
            catch (Exception)
            {
            }
        }

        private void OfficesManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOffices();
        }

        public void LoadOffices()
        {
            dgOffices.ItemsSource = null;
            var offices = Db.Context.Offices.ToList();
            dgOffices.ItemsSource = offices;
        }

        private void btnAddOffice_Click(object sender, RoutedEventArgs e)
        {
            AddOfficeWindow addOfficeWindow = new AddOfficeWindow();
            addOfficeWindow.ManageWindow = this;
            addOfficeWindow.ShowDialog();
        }

        private void btnEditOffice_Click(object sender, RoutedEventArgs e)
        {
            if (currentOffice == null)
            {
                MessageBox.Show("Please choose a office!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                EditOfficeWindow editOfficeWindow = new EditOfficeWindow();
                editOfficeWindow.ManageWindow = this;
                editOfficeWindow.Office = currentOffice;
                editOfficeWindow.ShowDialog();
                currentOffice = null;
            }
        }

        private void btnDeleteOffice_Click(object sender, RoutedEventArgs e)
        {
            if (currentOffice != null)
            {
                if (currentOffice.Users.Count == 0)
                {
                    if (MessageBox.Show("Do you want to delete this office?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        Db.Context.Offices.Remove(currentOffice);
                        Db.Context.SaveChanges();
                        LoadOffices();
                        currentOffice = null;
                    }
                }
                else
                {
                    MessageBox.Show("This office can not be deleted because it was related users", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a office!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
