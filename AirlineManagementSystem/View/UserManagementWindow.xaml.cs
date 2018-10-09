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
        private void DgUsers_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var row = e.Row;
            var user = e.Row.Item as User;
            if (user.RoleID == 1)
            {
                row.Background = new SolidColorBrush(Color.FromRgb(25, 106, 166));
            }
            if (user.Active.Value == false)
            {
                row.Background = new SolidColorBrush(Color.FromRgb(247, 148, 32));
            }
        }

        private void DgUsers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var user = dgUsers.CurrentItem as User;

                if (user.Active.Value)
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
            var offices = Db.Context.Offices.ToList();
            offices.Insert(0, new Office() { Title = "All offices" });
            cbOffice.ItemsSource = offices;
            cbOffice.DisplayMemberPath = "Title";
            cbOffice.SelectedIndex = 0;
        }

        public void LoadUsers()
        {
            dgUsers.Items.Clear();
            var users = Db.Context.Users.ToList();
            if (cbOffice.SelectedIndex != 0)
            {
                users = users.Where(t => t.Office.Title == cbOffice.Text).ToList();
            }

            dgUsers.ItemsSource = users;
        }

        private void addUserMenu_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow wAddUser = new AddUserWindow();
            wAddUser.ManageWindow = this;
            wAddUser.ShowDialog();
        }

        private void btnChangeRole_Click(object sender, RoutedEventArgs e)
        {
            ChangeRoleWindow wChangeRole = new ChangeRoleWindow();
            var user = dgUsers.CurrentItem as User;
            wChangeRole.User = user;
            wChangeRole.ManageWindow = this;
            wChangeRole.ShowDialog();
        }

        private void btnDisableAccount_Click(object sender, RoutedEventArgs e)
        {
            var user = dgUsers.CurrentItem as User;
            user.Active = user.Active.Value ? false : true;
            Db.Context.SaveChanges();
            LoadUsers();
        }

        private void cbOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadUsers();
        }
    }
}
