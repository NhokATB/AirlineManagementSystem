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
using AirportManagerSystem.Model;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for EditOfficeWindow.xaml
    /// </summary>
    public partial class EditOfficeWindow : Window
    {
        List<Country> countries;
        public EditOfficeWindow()
        {
            InitializeComponent();
            this.Loaded += EditOfficeWindow_Loaded;
        }

        private void EditOfficeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            countries = Db.Context.Countries.ToList();
            cbCountry.ItemsSource = countries;
            cbCountry.DisplayMemberPath = "Name";
            cbCountry.SelectedIndex = 0;

            txtTitle.Text = Office.Title;
            txtPhone.Text = Office.Phone.Replace("-", "");
            txtContact.Text = Office.Contact;
            cbCountry.SelectedItem = Office.Country;
        }

        public Office Office { get; internal set; }
        public OfficesManagementWindow ManageWindow { get; internal set; }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text.Trim() == "")
            {
                MessageBox.Show("Title was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtTitle.Text != Office.Title)
            {
                if (Db.Context.Offices.ToList().Where(t => t.Title == txtTitle.Text).FirstOrDefault() != null)
                {
                    MessageBox.Show("This office was existed!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
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

            if (txtPhone.Text != Office.Phone.Replace("-", ""))
            {
                if (Db.Context.Offices.ToList().Where(t => t.Phone.Replace("-", "") == txtPhone.Text).FirstOrDefault() != null)
                {
                    MessageBox.Show("This phone was used!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (txtContact.Text.Trim() == "")
            {
                MessageBox.Show("Contact was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Office.Title = txtTitle.Text;
            Office.Phone = txtPhone.Text;
            Office.Contact = txtContact.Text;
            Office.Country = countries[cbCountry.SelectedIndex];

            Db.Context.SaveChanges();
            ManageWindow.LoadOffices();

            MessageBox.Show("Edit office successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
