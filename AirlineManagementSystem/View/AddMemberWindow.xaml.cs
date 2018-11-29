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
    /// Interaction logic for AddMemberWindow.xaml
    /// </summary>
    public partial class AddMemberWindow : Window
    {
        public MemberManagementWindow ManageWindow { get; internal set; }
        public User LogonUser { get; internal set; }

        public AddMemberWindow()
        {
            InitializeComponent();
            this.Loaded += AddMemberWindow_Loaded;
        }

        private void AddMemberWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var genders = new List<string>() { "Male", "Female" };
            var offices = Db.Context.Offices.ToList();
            var positions = Db.Context.Positions.ToList();
            var countries = Db.Context.Countries.ToList();

            cbGender.ItemsSource = genders;
            cbGender.SelectedIndex = 0;

            cbPosition.ItemsSource = positions;
            cbPosition.DisplayMemberPath = "PositionName";
            cbPosition.SelectedIndex = 0;

            cbOffice.ItemsSource = offices;
            cbOffice.DisplayMemberPath = "Title";
            cbOffice.SelectedIndex = 0;

            cbCountry.ItemsSource = countries;
            cbCountry.DisplayMemberPath = "Name";
            cbCountry.SelectedIndex = 0;

            if (LogonUser.Role.Title == "Manager")
            {
                cbOffice.SelectedItem = LogonUser.Office;
                cbOffice.IsEnabled = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text.Trim() == "")
            {
                MessageBox.Show("First name was required", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtLastName.Text.Trim() == "")
            {
                MessageBox.Show("Last name was required", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dtpHireDate.SelectedDate == null)
            {
                MessageBox.Show("Hire date was required", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dtpHireDate.SelectedDate.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Hire date must be <= Now", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Phone was required", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Regex.IsMatch(txtPhone.Text, @"\D"))
            {
                MessageBox.Show("Phone must be digits", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CrewMember crewMember = new CrewMember()
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Office = cbCountry.SelectedItem as Office,
                Country = cbCountry.SelectedItem as Country,
                Gender = cbGender.Text,
                Position = cbPosition.SelectedItem as Position,
                Phone = txtPhone.Text,
                HireDate = dtpHireDate.SelectedDate.Value.Date
            };

            Db.Context.CrewMembers.Add(crewMember);
            Db.Context.SaveChanges();
            ManageWindow.LoadMembers();
            MessageBox.Show("Add member successful", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            this.Close();
        }
    }
}
