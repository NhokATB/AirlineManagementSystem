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
        private void AddUserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            offiices = Db.Context.Offices.ToList();
            cbOffice.ItemsSource = offiices;
            cbOffice.DisplayMemberPath = "Title";
            cbOffice.SelectedIndex = 0;
        }

        public UserManagementWindow ManageWindow { get; internal set; }

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

            if (txtFirstName.Text == "" || txtLastName.Text == "" || txtPassword.Password == "")
            {
                MessageBox.Show("Please fill all field", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (DateTime.Now.Year < 18 - dtpBirthdate.SelectedDate.Value.Year)
                {
                    MessageBox.Show("Age of user is at least 18", "Message");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please choose birthdate", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
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
                RoleID = 2,
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
