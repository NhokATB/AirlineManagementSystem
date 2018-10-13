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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class LoginHistoryWindow : Window
    {
        public LoginHistoryWindow()
        {
            InitializeComponent();
            this.Loaded += LoginHistoryWindow_Loaded;
            dgLogs.LoadingRow += DgLogs_LoadingRow;
        }

        private void DgLogs_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var log = e.Row.Item as LoginHistory;
            if (log.LogoutTime == null)
            {
                e.Row.Background = new SolidColorBrush(Color.FromRgb(247, 148, 32));
            }
        }

        private void LoginHistoryWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tblMessage.Text = $"Hi {User.FirstName} {User.LastName}, Welcome to AMONIC airlines automation system";
            LoadLoginHistory();
        }

        private void LoadLoginHistory()
        {
            var logs = Db.Context.LoginHistories.Where(t => t.UserId == User.ID).OrderByDescending(t=>t.LoginTime).ToList();
            dgLogs.ItemsSource = logs.Where(t => t != logs.First()).ToList();
            tblNumberOfCrash.Text = (logs.Count(t => t.LogoutTime == null) - 1).ToString();
        }

        public User User { get; internal set; }
    }
}
