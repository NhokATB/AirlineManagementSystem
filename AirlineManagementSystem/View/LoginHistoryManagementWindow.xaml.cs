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
    /// Interaction logic for LoginHistoryManagementWindow.xaml
    /// </summary>
    public partial class LoginHistoryManagementWindow : Window
    {
        List<Crash> crashTypes;
        public LoginHistoryManagementWindow()
        {
            InitializeComponent();
            this.Loaded += LoginHistoryManagementWindow_Loaded;
            dgLogs.LoadingRow += DgLogs_LoadingRow;
            dpDate.SelectedDate = DateTime.Now.Date;
        }

        private void DgLogs_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var log = e.Row.Item as NewLog;
            if (log.Log.LogoutTime == null)
            {
                e.Row.Background = new SolidColorBrush(Color.FromRgb(247, 148, 32));
            }
            else
            {
                e.Row.Background = new SolidColorBrush(Colors.White);
            }
        }

        private void LoginHistoryManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var hoursFrom = Enumerable.Range(0, 24).ToList();
            var hoursTo = Enumerable.Range(0, 25).ToList();

            cbFrom.ItemsSource = hoursFrom;
            cbFrom.SelectedIndex = 0;
            cbTo.ItemsSource = hoursTo;
            cbTo.SelectedIndex = hoursTo.Count - 1;

            crashTypes = Db.Context.Crashes.ToList();
            crashTypes.Insert(0, new Crash() { Name = "Not crashed" });
            crashTypes.Insert(0, new Crash() { Name = "All" });
            cbCrashType.ItemsSource = crashTypes;
            cbCrashType.DisplayMemberPath = "Name";
            cbCrashType.SelectedIndex = 0;

            LoadLoginHistory();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            LoadLoginHistory();
        }

        private void LoadLoginHistory()
        {
            dgLogs.ItemsSource = null;

            var date = dpDate.SelectedDate.Value.Date;
            var logs = Db.Context.LoginHistories.ToList().Where(t => t.LoginTime.Date == date).OrderByDescending(t => t.LoginTime).Skip(1).ToList();
            var from = int.Parse(cbFrom.Text);
            var to = int.Parse(cbTo.Text);

            logs = logs.Where(t => t.LoginTime.Hour >= from && t.LoginTime.Hour <= to).ToList();
            var crashId = crashTypes[cbCrashType.SelectedIndex].CrashId;
            if (cbCrashType.SelectedIndex == 1)
            {
                logs = logs.Where(t => t.LogoutTime != null).ToList();
            }
            else if (cbCrashType.SelectedIndex != 0)
                logs = logs.Where(t => t.CrashId == crashId).ToList();

            var newLogs = new List<NewLog>();
            foreach (var item in logs)
            {
                newLogs.Add(new NewLog()
                {
                    Log = item,
                    User = item.User.FirstName + " " + item.User.LastName
                });
            }

            dgLogs.ItemsSource = newLogs;
        }
    }
}
