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
    /// Interaction logic for CrashWindow.xaml
    /// </summary>
    public partial class CrashWindow : Window
    {
        public CrashWindow()
        {
            InitializeComponent();
            this.Loaded += CrashWindow_Loaded;
        }

        private void CrashWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tblMessage.Text = $"No logout detected for your last login on {Log.LoginTime.ToString("dd/MM/yyyy")} at {Log.LoginTime.ToString("HH:/mm")}";
        }

        public LoginHistory Log { get; internal set; }
        public bool IsConfirmed { get; internal set; }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (txtReason.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the reason!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Log.Reason = txtReason.Text;
            Log.CrashId = rdbSystemCrash.IsChecked.Value ? 1 : 2;

            Db.Context.SaveChanges();
            this.IsConfirmed = true;
            this.Close();
        }
    }
}
