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
    /// Interaction logic for AddAirportWindow.xaml
    /// </summary>
    public partial class AddAirportWindow : Window
    {
        List<Country> countries;
        public AddAirportWindow()
        {
            InitializeComponent();
            this.Loaded += AddAirportWindow_Loaded;
        }

        private void AddAirportWindow_Loaded(object sender, RoutedEventArgs e)
        {
            countries = Db.Context.Countries.ToList();
            cbCountry.ItemsSource = countries;
            cbCountry.DisplayMemberPath = "Name";
            cbCountry.SelectedIndex = 0;
        }

        public AirportManagementWindow ManageWindow { get; internal set; }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtIATACode.Text.Trim() == "")
            {
                MessageBox.Show("IATACode was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Db.Context.Airports.ToList().Where(t => t.IATACode == txtIATACode.Text).FirstOrDefault() != null)
            {
                MessageBox.Show("IATACode wasn't duplicated!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Airport name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Db.Context.Airports.ToList().Where(t => t.Name == txtName.Text).FirstOrDefault() != null)
            {
                MessageBox.Show("Airport name was used!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Airport airport = new Airport()
            {
                IATACode = txtIATACode.Text,
                Name = txtName.Text,
                Country = countries[cbCountry.SelectedIndex]
            };

            Db.Context.Airports.Add(airport);
            Db.Context.SaveChanges();
            ManageWindow.LoadAirports();

            MessageBox.Show("Add airport successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
