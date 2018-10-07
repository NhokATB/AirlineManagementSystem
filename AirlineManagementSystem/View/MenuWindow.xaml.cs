using AirportManagerSystem.Model;
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

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public User User { get; internal set; }
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void usersMenu_Click(object sender, RoutedEventArgs e)
        {
            UserManagementWindow wUsers = new UserManagementWindow();
            wUsers.ShowDialog();
        }
    }
}
