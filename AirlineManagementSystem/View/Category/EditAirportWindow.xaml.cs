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
using AirportManagerSystem.Model;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for EditAirportWindow.xaml
    /// </summary>
    public partial class EditAirportWindow : Window
    {
        List<Country> countries;
        public EditAirportWindow()
        {
            InitializeComponent();
            this.Loaded += EditAirportWindow_Loaded;
        }

        private void EditAirportWindow_Loaded(object sender, RoutedEventArgs e)
        {
            countries = Db.Context.Countries.ToList();
            cbCountry.ItemsSource = countries;
            cbCountry.DisplayMemberPath = "Name";
            cbCountry.SelectedIndex = 0;

            txtIATACode.Text = Airport.IATACode;
            txtName.Text = Airport.Name;
            cbCountry.SelectedItem = Airport.Country;
        }

        public AirportManagementWindow ManageWindow { get; internal set; }
        public Airport Airport { get; internal set; }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtIATACode.Text.Trim() == "")
            {
                MessageBox.Show("IATACode was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtIATACode.Text != Airport.IATACode)
            {
                if (Db.Context.Airports.ToList().Where(t => t.IATACode == txtIATACode.Text.ToUpper()).FirstOrDefault() != null)
                {
                    MessageBox.Show("This IATACode was existed!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Airport name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtName.Text != Airport.Name)
            {
                if (Db.Context.Airports.ToList().Where(t => t.Name.ToUpper() == txtName.Text.ToUpper()).FirstOrDefault() != null)
                {
                    MessageBox.Show("This airport name was used!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            Airport.IATACode = txtIATACode.Text.ToUpper();
            Airport.Name = txtName.Text;
            Airport.Country = countries[cbCountry.SelectedIndex];

            Db.Context.SaveChanges();
            ManageWindow.LoadAirports();

            MessageBox.Show("Edit airport successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
