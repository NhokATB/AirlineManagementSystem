using AirportManagerSystem.HelperClass;
using AirportManagerSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
            this.Loaded += AddUserWindow_Loaded;
        }
        private List<Office> offiices;
        private List<Role> roles;
        private void AddUserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            offiices = Db.Context.Offices.ToList();
            cbOffice.ItemsSource = offiices;
            cbOffice.DisplayMemberPath = "Title";
            cbOffice.SelectedIndex = 0;

            roles = Db.Context.Roles.Where(t => t.Title != "Administrator").ToList();
            cbUserRole.ItemsSource = roles;
            cbUserRole.DisplayMemberPath = "Title";
            cbUserRole.SelectedIndex = 0;

            if (LogonUser.Role.Title == "Manager")
            {
                roles.RemoveAt(1);
                cbUserRole.ItemsSource = roles;
            }
        }

        public UserManagementWindow ManageWindow { get; internal set; }
        public User LogonUser { get; internal set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Email is required", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Regex.IsMatch(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                MessageBox.Show("Email is invalid", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = Db.Context.Users.Where(t => t.Email == txtEmail.Text).FirstOrDefault();
            if (user != null)
            {
                MessageBox.Show("This email is used", "Message");
                return;
            }

            if (txtFirstName.Text.Trim() == "")
            {
                MessageBox.Show("First name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtLastName.Text.Trim() == "")
            {
                MessageBox.Show("Last name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (dtpBirthdate.SelectedDate != null)
            {
                if (DateTime.Now.Year < 18 - dtpBirthdate.SelectedDate.Value.Year)
                {
                    MessageBox.Show("Age of user is at least 18", "Message");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please choose birthdate", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPassword.Password.Trim() == "")
            {
                MessageBox.Show("Password was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            user = new User()
            {
                Email = txtEmail.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Birthdate = dtpBirthdate.SelectedDate,
                Office = offiices[cbOffice.SelectedIndex],
                Active = true,
                Password = Md5Helper.GetMd5(txtPassword.Password),
                RoleID = roles[cbUserRole.SelectedIndex].ID
            };

            Db.Context.Users.Add(user);
            Db.Context.SaveChanges();
            this.ManageWindow.LoadUsers();
            MessageBox.Show("Add user successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
