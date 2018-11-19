using AirportManagerSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UserManagementWindow.xaml
    /// </summary>
    public partial class UserManagementWindow : Window
    {
        public UserManagementWindow()
        {
            InitializeComponent();
            this.Loaded += UserManagementWindow_Loaded;

            dgUsers.SelectedCellsChanged += DgUsers_SelectedCellsChanged;
            dgUsers.LoadingRow += DgUsers_LoadingRow;
        }
        private User currentUser;
        private List<Office> offices;
        private List<Role> roles;

        public User User { get; internal set; }

        private void DgUsers_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var row = e.Row;
            var user = e.Row.Item as User;
            if (user.RoleID == 1)
            {
                if (user.Active.Value == false)
                {
                    row.Background = new SolidColorBrush(Color.FromRgb(247, 148, 32));
                }
                else
                {
                    row.Background = new SolidColorBrush(Color.FromRgb(25, 106, 166));
                }
            }
            else
            {
                if (user.Active.Value == false)
                {
                    row.Background = new SolidColorBrush(Color.FromRgb(247, 148, 32));
                }
                else
                {
                    row.Background = new SolidColorBrush(Colors.White);
                }
            }
        }

        private void DgUsers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentUser = dgUsers.CurrentItem as User;

                if (currentUser.Active.Value)
                {
                    btnDisableAccount.Content = "Disable account";
                    btnDisableAccount.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    btnDisableAccount.Content = "Enable account";
                    btnDisableAccount.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            catch (Exception)
            {
            }
        }
        private void UserManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            offices = Db.Context.Offices.ToList();
            offices.Insert(0, new Office() { Title = "All offices" });
            cbOffice.ItemsSource = offices;
            cbOffice.DisplayMemberPath = "Title";

            roles = Db.Context.Roles.ToList();
            roles.Insert(0, new Role() { Title = "All roles" });
            cbRole.ItemsSource = roles;
            cbRole.DisplayMemberPath = "Title";

            cbRole.SelectedIndex = 0;
            cbOffice.SelectedIndex = 0;
        }

        public void LoadUsers()
        {
            dgUsers.ItemsSource = null;

            var users = Db.Context.Users.Where(t => t.ID != User.ID).ToList();

            try
            {
                var officeName = offices[cbOffice.SelectedIndex].Title;
                if (officeName != "All offices")
                {
                    users = users.Where(t => t.Office.Title == officeName).ToList();
                }
            }
            catch (Exception)
            {
            }

            var roleName = roles[cbRole.SelectedIndex].Title;
            if (roleName != "All roles")
            {
                users = users.Where(t => t.Role.Title == roleName).ToList();
            }

            dgUsers.ItemsSource = users;
        }

        private void addUserMenu_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow wAddUser = new AddUserWindow();
            wAddUser.ManageWindow = this;
            wAddUser.ShowDialog();
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser != null)
            {
                try
                {
                    EditProfileWindow wEditProfile = new EditProfileWindow();
                    wEditProfile.User = currentUser;
                    wEditProfile.ManageWindow = this;
                    wEditProfile.ShowDialog();
                    currentUser = null;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show("Please choose a user!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDisableAccount_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser != null)
            {
                try
                {
                    currentUser.Active = currentUser.Active.Value ? false : true;
                    Db.Context.SaveChanges();
                    LoadUsers();
                    currentUser = null;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show("Please choose a user!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadUsers();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow wAddUser = new AddUserWindow();
            wAddUser.ManageWindow = this;
            wAddUser.ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser != null)
            {
                if (currentUser.Tickets.Count == 0)
                {
                    if (MessageBox.Show("Do you want to delete this user?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        Db.Context.LoginHistories.RemoveRange(currentUser.LoginHistories);
                        Db.Context.Users.Remove(currentUser);
                        Db.Context.SaveChanges();
                        LoadUsers();
                        currentUser = null;
                    }
                }
                else
                {
                    MessageBox.Show("This user can not be deleted because it is related to tickets", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a user!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadUsers();
        }
    }
}

