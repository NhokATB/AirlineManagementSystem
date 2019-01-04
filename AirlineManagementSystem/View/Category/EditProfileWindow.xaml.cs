using AirportManagerSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for ChangeRoleWindow.xaml
    /// </summary>
    public partial class EditProfileWindow : Window
    {
        public User User { get; internal set; }
        public UserManagementWindow ManageWindow { get; internal set; }
        public User LogonUser { get; internal set; }
        private List<Office> offices;
        public EditProfileWindow()
        {
            InitializeComponent();
            this.Loaded += EditProfileWindow_Loaded;
        }

        private void EditProfileWindow_Loaded(object sender, RoutedEventArgs e)
        {
            offices = Db.Context.Offices.ToList();
            cbOffice.ItemsSource = offices;
            cbOffice.DisplayMemberPath = "Title";

            var roles = Db.Context.Roles.ToList();
            cbUserRole.ItemsSource = roles;
            cbUserRole.DisplayMemberPath = "Title";

            txtEmail.Text = User.Email;
            txtFirstName.Text = User.FirstName;
            txtLastName.Text = User.LastName;
            cbOffice.SelectedItem = User.Office;
            cbUserRole.SelectedItem = User.Role;
            dtpBirthdate.SelectedDate = User.Birthdate;

            if (ManageWindow == null) //Navigate from Edit profile
            {
                if (User.Role.Title == "Administrator")
                {

                }
                else
                {
                    cbUserRole.IsEnabled = false;
                    cbOffice.IsEnabled = false;
                }
            }
            else //Nagigate from UserManagement
            {
                roles.RemoveAt(0);
                if (LogonUser.Role.Title == "Manager")
                {
                    roles.RemoveAt(1);
                }
                cbUserRole.ItemsSource = roles;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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

            if (txtEmail.Text != User.Email)
            {
                var user = Db.Context.Users.Where(t => t.Email == txtEmail.Text).FirstOrDefault();
                if (user != null)
                {
                    MessageBox.Show("This email is used", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
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

            try
            {
                if (DateTime.Now.Year < 18 - dtpBirthdate.SelectedDate.Value.Year)
                {
                    MessageBox.Show("Age of user is at least 18", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please choose birthdate", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User.Email = txtEmail.Text;
            User.FirstName = txtFirstName.Text;
            User.LastName = txtLastName.Text;
            User.Birthdate = dtpBirthdate.SelectedDate;
            User.Office = offices[cbOffice.SelectedIndex];
            User.RoleID = (cbUserRole.SelectedItem as Role).ID;

            Db.Context.SaveChanges();
            if (ManageWindow != null)
            {
                this.ManageWindow.LoadUsers();
            }

            MessageBox.Show("Edit user successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
