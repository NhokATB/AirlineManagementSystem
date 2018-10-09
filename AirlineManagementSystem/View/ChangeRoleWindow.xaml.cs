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
using AirportManagerSystem.Model;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for ChangeRoleWindow.xaml
    /// </summary>
    public partial class ChangeRoleWindow : Window
    {
        public ChangeRoleWindow()
        {
            InitializeComponent();
        }

        public User User { get; internal set; }
        public UserManagementWindow ManageWindow { get; internal set; }
    }
}
