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
using AirportManagerSystem.HelperClass;
using AirportManagerSystem.Model;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public User User { get; internal set; }

        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (pwbPassword.Password.Trim() == "")
            {
                MessageBox.Show("Please enter password", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Md5Helper.GetMd5(pwbPassword.Password) != User.Password.ToUpper())
            {
                MessageBox.Show("Password not correct, please check again", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (pwbNewPassword.Password.Trim() == "")
                {
                    MessageBox.Show("Please enter new password", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (pwbPasswordAgain.Password.Trim() == "")
                {
                    MessageBox.Show("Please reenter new password", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (pwbPasswordAgain.Password != pwbNewPassword.Password)
                {
                    MessageBox.Show("Password again not correct! Please check again", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                User.Password = Md5Helper.GetMd5(pwbNewPassword.Password);
                Db.Context.SaveChanges();
                MessageBox.Show("Change password successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
