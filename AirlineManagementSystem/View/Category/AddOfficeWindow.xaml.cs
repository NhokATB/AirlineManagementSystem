using AirportManagerSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddOfficeWindow.xaml
    /// </summary>
    public partial class AddOfficeWindow : Window
    {
        List<Country> countries;
        public AddOfficeWindow()
        {
            InitializeComponent();
            this.Loaded += AddOfficeWindow_Loaded;
        }

        private void AddOfficeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            countries = Db.Context.Countries.ToList();
            cbCountry.ItemsSource = countries;
            cbCountry.DisplayMemberPath = "Name";
            cbCountry.SelectedIndex = 0;
        }

        public OfficesManagementWindow ManageWindow { get; internal set; }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text.Trim() == "")
            {
                MessageBox.Show("Title was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Db.Context.Offices.ToList().Where(t => t.Title == txtTitle.Text).FirstOrDefault() != null)
            {
                MessageBox.Show("This office was existed!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Phone was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Regex.IsMatch(txtPhone.Text, @"\D"))
            {
                MessageBox.Show("Phone must be digits", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Db.Context.Offices.ToList().Where(t => t.Phone.Replace("-","") == txtPhone.Text).FirstOrDefault() != null)
            {
                MessageBox.Show("This phone was used!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtContact.Text.Trim() == "")
            {
                MessageBox.Show("Contact was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Office office = new Office()
            {
                Title = txtTitle.Text,
                Phone = txtPhone.Text,
                Contact = txtContact.Text,
                Country = countries[cbCountry.SelectedIndex]
            };

            Db.Context.Offices.Add(office);
            Db.Context.SaveChanges();
            ManageWindow.LoadOffices();

            MessageBox.Show("Add office successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
