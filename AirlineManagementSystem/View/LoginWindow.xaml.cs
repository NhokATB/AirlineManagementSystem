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
using AirportManagerSystem.HelperClass;
using System.IO;

namespace AirportManagerSystem
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        int times = 0, tick = 10;
        DispatcherTimer timer = new DispatcherTimer();
        private string GetBr()
        {
            while (true)
            {
                var br = System.IO.Path.GetRandomFileName().Substring(0, 6).ToUpper();
                if (Db.Context.Tickets.Select(t => t.BookingReference).Contains(br) == false)
                    return br;
            }
        }

        public LoginWindow()
        {
            InitializeComponent();
            imgLogo.Source = new BitmapImage(new Uri(@"/AirportManagerSystem;component/Images/WSC2017_TP09_color.png", UriKind.Relative));
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);

            txtUsername.Focus();
        }

        private void AddAmenitiesForTicket()
        {
            Random r = new Random();
            var flights = Db.Context.Schedules.ToList();
            var amenitiesFirst = Db.Context.CabinTypes.Where(t => t.ID == 3).SelectMany(t => t.Amenities).ToList();
            var amenitiesBusiness = Db.Context.CabinTypes.Where(t => t.ID == 2).SelectMany(t => t.Amenities).ToList();

            foreach (var item in flights)
            {
                var ticketsF = item.Tickets.Where(t => t.CabinTypeID == 3).ToList();
                var ticketsB = item.Tickets.Where(t => t.CabinTypeID == 2).ToList();

                try
                {
                    ticketsF[0].AmenitiesTickets.Add(new AmenitiesTicket()
                    {
                        AmenityID = amenitiesFirst[r.Next(0, amenitiesFirst.Count)].ID,
                        Price = amenitiesFirst[r.Next(0, amenitiesFirst.Count)].Price,
                    });
                    ticketsF[1].AmenitiesTickets.Add(new AmenitiesTicket()
                    {
                        AmenityID = amenitiesFirst[r.Next(0, amenitiesFirst.Count)].ID,
                        Price = amenitiesFirst[r.Next(0, amenitiesFirst.Count)].Price,
                    });

                    ticketsB[0].AmenitiesTickets.Add(new AmenitiesTicket()
                    {
                        AmenityID = amenitiesBusiness[r.Next(0, amenitiesBusiness.Count)].ID,
                        Price = amenitiesBusiness[r.Next(0, amenitiesBusiness.Count)].Price,
                    });
                    ticketsB[1].AmenitiesTickets.Add(new AmenitiesTicket()
                    {
                        AmenityID = amenitiesBusiness[r.Next(0, amenitiesBusiness.Count)].ID,
                        Price = amenitiesBusiness[r.Next(0, amenitiesBusiness.Count)].Price,
                    });
                }
                catch (Exception)
                {
                }

                Db.Context.SaveChanges();
            }
        }

        private void AddCrew()
        {
            Random r = new Random();
            var genders = new List<string>() { "Male", "Female" };
            var dates = new List<DateTime>() { new DateTime(2017, 12, 12), new DateTime(2017, 11, 17), new DateTime(2017, 12, 25), new DateTime(2018, 4, 15), new DateTime(2018, 8, 8), new DateTime(2018, 7, 15), new DateTime(2018, 6, 19) };

            //foreach (var item in Db.Context.Offices.ToList())
            //{
            //    for (int i = 1; i < 3; i++)
            //    {
            //        Crew crew = new Crew()
            //        {
            //            CrewName = "Crew of " + item.Title + i,
            //            Office = item,
            //            NumberOfMembers = 11,
            //        };
            //        Db.Context.Crews.Add(crew);
            //        Db.Context.SaveChanges();
            //    }
            //}

            var tickets = Db.Context.Tickets.ToList().Skip(1000).Take(1000).ToList();

            for (int i = 1; i < 11; i++)
            {
                for (int j = 0; j < 7; j++)
                //foreach (var item in Db.Context.Positions.Where(t => t.PositionId != 5).ToList())
                {
                    CrewMember crewMember = new CrewMember()
                    {
                        //FirstName = tickets[i + item.PositionId * 10].Firstname,
                        //LastName = tickets[i + item.PositionId * 10].Lastname,
                        //PositionId = item.PositionId,
                        //Phone = tickets[i + item.PositionId * 10].Phone,
                        //Gender = genders[r.Next(0, 2)],
                        Gender = "Female",
                        FirstName = tickets[i + j * 10].Firstname,
                        LastName = tickets[i + j * 10].Lastname,
                        PositionId = 5,
                        Phone = tickets[i + j * 10].Phone,
                        HireDate = dates[r.Next(0, dates.Count)],
                        CrewId = i
                    };
                    Db.Context.CrewMembers.Add(crewMember);
                }
            }
            Db.Context.SaveChanges();
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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Db.Context = new AirlineManagementSystemEntities();

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
