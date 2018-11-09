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
        private string GetBr()
        {
            while (true)
            {
                var br = System.IO.Path.GetRandomFileName().Substring(0, 6).ToUpper();
                if (Db.Context.Tickets.Select(t => t.BookingReference).Contains(br) == false)
                    return br;
            }
        }

        Random r = new Random();
        private void SetScheduleIdForRespondent(List<Schedule> schedules)
        {
            var total = 0;
            var respondents = Db.Context.Respondents.Where(t => t.ScheduleId == null).ToList();
            foreach (var item in schedules)
            {
                var count = item.Tickets.Count / 2;
                var newRespondents = respondents.Skip(total).Take(count).ToList();
                foreach (var re in newRespondents)
                {
                    re.ScheduleId = item.ID;
                }
                total += count;
                Db.Context.SaveChanges();
            }
        }
        private void SearRespondentNotHaveEnoughQuestion()
        {
            var res1 = Db.Context.Respondents.Where(t => t.Surveys.Where(k => k.QuestionId == 1).Count() == 0).ToList();

            var res2 = Db.Context.Respondents.Where(t => t.Surveys.Where(k => k.QuestionId == 2).Count() == 0).ToList();
            var res3 = Db.Context.Respondents.Where(t => t.Surveys.Where(k => k.QuestionId == 3).Count() == 0).ToList();
            var res4 = Db.Context.Respondents.Where(t => t.Surveys.Where(k => k.QuestionId == 4).Count() == 0).ToList();

            FileStream fs1 = new FileStream("../../res1.txt", FileMode.OpenOrCreate);
            FileStream fs2 = new FileStream("../../res2.txt", FileMode.OpenOrCreate);
            FileStream fs3 = new FileStream("../../res3.txt", FileMode.OpenOrCreate);
            FileStream fs4 = new FileStream("../../res4.txt", FileMode.OpenOrCreate);
            StreamWriter sw1 = new StreamWriter(fs1);
            StreamWriter sw2 = new StreamWriter(fs2);
            StreamWriter sw3 = new StreamWriter(fs3);
            StreamWriter sw4 = new StreamWriter(fs4);

            foreach (var res in res1)
            {
                sw1.WriteLine(res.RespondentId);
            }

            foreach (var res in res2)
            {
                sw2.WriteLine(res.RespondentId);
            }

            foreach (var res in res3)
            {
                sw3.WriteLine(res.RespondentId);
            }

            foreach (var res in res4)
            {
                sw4.WriteLine(res.RespondentId);
            }

            sw1.Close();
            fs1.Close();
            sw2.Close();
            fs2.Close();
            sw3.Close();
            fs3.Close();
            sw4.Close();
            fs4.Close();
        }

        public LoginWindow()
        {
            InitializeComponent();
            imgLogo.Source = new BitmapImage(new Uri(@"/AirportManagerSystem;component/Images/WSC2017_TP09_color.png", UriKind.Relative));
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);

            var schedules = Db.Context.Schedules.Where(t => t.Tickets.Count > 10 && t.Date.Year == 2019).ToList();
            var date = new DateTime(2019, 1, 25);
            var re = Db.Context.Respondents.Where(t => t.Schedule.Date > date).ToList();
            foreach (var item in re)
            {
                var su = item.Surveys.ToList();
                Db.Context.Surveys.RemoveRange(su);
            }
            Db.Context.SaveChanges();
            Db.Context.Respondents.RemoveRange(re);
            Db.Context.SaveChanges();

            foreach (var item in schedules)
            {
                var respond = item.Respondents.Take(10).ToList();

                //foreach (var re in respond)
                //{
                //    re.Surveys.Clear();
                //}
                //Db.Context.SaveChanges();

                //Db.Context.Respondents.RemoveRange(respond);
            }
            //Db.Context.Schedules.RemoveRange(schedules);
            Db.Context.SaveChanges();


            //var s = Db.Context.Schedules.ToList().Where(t => t.Surveys.Count == 0).ToList();

            //var date1 = new DateTime(2017, 9, 1);
            //var date2 = new DateTime(2018, 1, 1);
            //var date3 = new DateTime(2018, 4, 1);
            //var date4 = new DateTime(2018, 9, 1);
            //var date5 = new DateTime(2019, 1, 1);
            //var date6 = new DateTime(2019, 4, 1);

            //var schedules = Db.Context.Schedules.Where(t => t.Date >= date1 && t.Date < date2).ToList();
            //var schedules1 = Db.Context.Schedules.Where(t => t.Date >= date2 && t.Date < date3).ToList();
            //var schedules2 = Db.Context.Schedules.Where(t => t.Date >= date4 && t.Date < date5).ToList();
            //var schedules3 = Db.Context.Schedules.Where(t => t.Date >= date5 && t.Date < date6).ToList();
            //var schedules = Db.Context.Schedules.Where(t => t.Date.Month == 8 && t.Date.Year == 2018).ToList();

            //var cabins = Db.Context.CabinTypes.ToList();
            //var respondents = Db.Context.Respondents.ToList();
            //foreach (var item in respondents)
            //{
            //    item.CabinType = cabins[r.Next(0, 3)].Name;
            //    if (item.RespondentId % 2 == 0)
            //        item.CabinType = cabins[r.Next(0, 3)].Name;
            //    else item.CabinType = cabins[r.Next(0, 2)].Name;
            //}
            //Db.Context.SaveChanges();


            //var tickets1 = schedules.SelectMany(t => t.Tickets).ToList();
            //var tickets2 = schedules1.SelectMany(t => t.Tickets).ToList();
            //var tickets3 = schedules2.SelectMany(t => t.Tickets).ToList();
            //var tickets4 = schedules3.SelectMany(t => t.Tickets).ToList();
            //var atb = (tickets1.Count + tickets2.Count + tickets3.Count + tickets4.Count) / 2;

            //var surveys = Db.Context.Surveys.Where(t => t.Schedule.Date > date2).ToList();

            //var schedules = Db.Context.Schedules.ToList().Where(t => t.Surveys.Count == 0 && t.Tickets.Count > 0).ToList();
            //var scheduleId = schedules.Select(t => t.ID).ToList();
            //schedules = Db.Context.Schedules.ToList().Where(t => t.Surveys.Count == 0).ToList();
            //var schedules = Db.Context.Schedules.ToList().Where(t => t.Tickets.Count > 0 && t.Surveys.Count == 0 && t.Date > date2 && t.Date < date1).ToList();
            //var respontdents = Db.Context.Respondents.ToList();

            //SetScheduleIdForRespondent(schedules);
            //SetScheduleIdForRespondent(schedules1);
            //SetScheduleIdForRespondent(schedules2);
            //SetScheduleIdForRespondent(schedules3);

            //var surveys = Db.Context.Surveys.Where(t => t.ScheduleId == null).ToList();

            //foreach (var item in respondent)
            //{
            //    var srs = item.Surveys.ToList();
            //    try
            //    {
            //        srs[0].QuestionId = 1;
            //        srs[1].QuestionId = 2;
            //        srs[2].QuestionId = 3;
            //        srs[3].QuestionId = 4;
            //    }
            //    catch (Exception)
            //    {
            //    }

            //    Db.Context.SaveChanges();
            //}

            //var schedules = Db.Context.Schedules.Where(t => t.Date >= date1 && t.Date < date2).Where(t => t.Surveys.Where(k => k.QuestionId == 4).Count() == 0 && t.Tickets.Count > 0).ToList();

            //foreach (var item in schedules)
            //{
            //    //var surveys = Db.Context.Surveys.Where(t => t.ScheduleId == null && t.QuestionId == 4).ToList();
            //    for (int j = 1; j < 5; j++)
            //    {
            //        surveys = surveys.Where(t => t.QuestionId == j).ToList();
            //        int i = 0;
            //        foreach (var sv in surveys)
            //        {
            //            if (i < item.Tickets.Count - 1)
            //                sv.ScheduleId = item.ID;
            //            i++;
            //        }

            //        Db.Context.SaveChanges();
            //    }

            //}
            //var a = "ahihi";

            ////var surveys1 = Db.Context.Surveys.Where(t => t.ScheduleId == null).ToList();
            //foreach (var sv in surveys1)
            //{
            //    sv.ScheduleId = scheduleId[r.Next(1, 116)];
            //}
            //Db.Context.SaveChanges();


            //var j = Db.Context.Respondents.Count() + 1;
            //foreach (var item in schedules)
            //{
            //    var number = r.Next(7, 11);
            //    for (int i = 0; i < number; i++)
            //    {
            //        var respont = respontdents[r.Next(1, 2298)];
            //        var survey = surveys[r.Next(1, 1000)];
            //        //try
            //        {
            //            Respondent rs = new Respondent()
            //            {
            //                Age = respont.Age,
            //                Arrival = respont.Arrival,
            //                Gender = respont.Gender,
            //                Departure = respont.Departure,
            //                CabinType = respont.CabinType,
            //                RespondentId = j
            //            };

            //            Survey s = new Survey()
            //            {
            //                AnswerId = r.Next(0,8),
            //                QuestionId = r.Next(1,5),
            //                Respondent = rs,
            //                Schedule = item,
            //            };

            //            Db.Context.Surveys.Add(s);

            //            j++;
            //        }

            //        //catch (Exception)
            //        //{
            //        //    i--;
            //        //    continue;
            //        //}
            //    }

            //    Db.Context.SaveChanges();
            //}


            //schedules = Db.Context.Schedules.ToList().Where(t => t.Tickets.Count() == 0).ToList();

            //foreach (var item in schedules)
            //{
            //    var number = r.Next(10, 15);
            //    if (item.Tickets.Count < 15) number = 20 - item.Tickets.Count();
            //    for (int i = 0; i < number; i++)
            //    {
            //        var id = r.Next(437, 20000);
            //        var tick = Db.Context.Tickets.Find(id);
            //        try
            //        {
            //            Ticket t = new Ticket()
            //            {
            //                PassportCountryID = tick.PassportCountryID,
            //                Birthdate = tick.Birthdate,
            //                Firstname = tick.Firstname,
            //                Lastname = tick.Lastname,
            //                Confirmed = tick.Confirmed,
            //                BookingReference = GetBr(),
            //                CabinType = tick.CabinType,
            //                PassportNumber = tick.PassportNumber,
            //                Phone = tick.Phone,
            //                ScheduleID = item.ID,
            //                UserID = r.Next(0, 10),
            //            };

            //            Db.Context.Tickets.Add(t);
            //        }
            //        catch (Exception)
            //        {
            //            i--;
            //            continue;
            //        }
            //    }
            //    Db.Context.SaveChanges();
            //}
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
