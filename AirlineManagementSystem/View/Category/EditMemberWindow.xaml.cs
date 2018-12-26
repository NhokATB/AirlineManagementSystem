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
    /// Interaction logic for EditMemberWindow.xaml
    /// </summary>
    public partial class EditMemberWindow : Window
    {

        public MemberManagementWindow ManageWindow { get; internal set; }
        public CrewMember Member { get; internal set; }
        public User LogonUser { get; internal set; }
        public EditMemberWindow()
        {
            InitializeComponent();
            this.Loaded += EditMemberWindow_Loaded;
        }

        private void EditMemberWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var genders = new List<string>() { "Male", "Female" };
            var offices = Db.Context.Offices.ToList();
            var positions = Db.Context.Positions.ToList();
            var countries = Db.Context.Countries.ToList();

            cbGender.ItemsSource = genders;

            cbPosition.ItemsSource = positions;
            cbPosition.DisplayMemberPath = "PositionName";

            cbOffice.ItemsSource = offices;
            cbOffice.DisplayMemberPath = "Title";

            cbCountry.ItemsSource = countries;
            cbCountry.DisplayMemberPath = "Name";

            cbGender.Text = Member.Gender;
            cbPosition.SelectedItem = Member.Position;
            cbCountry.SelectedItem = Member.Country;
            cbOffice.SelectedItem = Member.Office;

            txtFirstName.Text = Member.FirstName;
            txtLastName.Text = Member.LastName;
            txtPhone.Text = Member.Phone;
            dtpHireDate.SelectedDate = Member.HireDate;

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

            if (txtPhone.Text != Member.Phone)
            {
                if (Db.Context.CrewMembers.FirstOrDefault(t => t.Phone == txtPhone.Text) != null)
                {
                    MessageBox.Show("This phone was used", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            Member.FirstName = txtFirstName.Text.Trim();
            Member.LastName = txtLastName.Text.Trim();
            Member.Office = cbCountry.SelectedItem as Office;
            Member.Country = cbCountry.SelectedItem as Country;
            Member.Gender = cbGender.Text;
            Member.Position = cbPosition.SelectedItem as Position;
            Member.Phone = txtPhone.Text;
            Member.HireDate = dtpHireDate.SelectedDate.Value.Date;

            Db.Context.SaveChanges();
            ManageWindow.LoadMembers();
            MessageBox.Show("Edit member successful", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            this.Close();
        }
    }
}
