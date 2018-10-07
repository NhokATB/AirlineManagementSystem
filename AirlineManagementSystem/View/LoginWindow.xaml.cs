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
using AirportManagerSystem.View;
using System.Windows.Threading;

namespace AirportManagerSystem
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            imgLogo.Source = new BitmapImage(new Uri(@"/AirportManagerSystem;component/Images/WSC2017_TP09_color.png", UriKind.Relative));
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            btnLogin.Content = "Next login: " + tick.ToString();
            tick--;
            if (tick < 0)
            {
                btnLogin.Content = "Login";
                btnLogin.IsEnabled = true;
                tick = 10;
                times = 0;
                timer.Stop();
            }
        }

        int times = 0, tick = 10;
        DispatcherTimer timer = new DispatcherTimer();
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "" || pwbPassword.Password == "")
            {
                MessageBox.Show("Enter username or password");
                return;
            }

            var pass = Md5Helper.GetMd5(pwbPassword.Password);

            var user = Db.Context.Users.Where(t => t.Email == txtUsername.Text && t.Password == pass).FirstOrDefault();

            if (user != null)
            {
                times = 0;
                if (user.Active.Value)
                {
                    var lgs = user.LoginHistories.ToList();
                    if (lgs.Count > 0 && lgs.Last().LogoutTime == null)
                    {
                        CrashWindow crashWindow = new CrashWindow();
                        crashWindow.Log = lgs.Last();

                        this.Hide();
                        crashWindow.ShowDialog();
                        if (crashWindow.IsConfirmed == false)
                        {
                            this.Show();
                            return;
                        }
                    }

                    var now = DateTime.Now;
                    var log = new LoginHistory()
                    {
                        UserId = user.ID,
                        LoginTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, 0)
                    };

                    user.LoginHistories.Add(log);
                    Db.Context.SaveChanges();

                    MenuWindow menuWindow = new MenuWindow();
                    menuWindow.User = user;
                    this.Hide();
                    menuWindow.ShowDialog();
                    this.Show();

                    txtUsername.Text = pwbPassword.Password = "";
                }
                else
                {
                    MessageBox.Show("Your account was disabled");
                }
            }
            else
            {
                times++;
                if (times > 3)
                {
                    MessageBox.Show("Username or password not correct! You must wait 10s for the next lgoin time.", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    btnLogin.IsEnabled = false;
                    timer.Start();
                }
                else
                {
                    MessageBox.Show("Username or password not correct", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
