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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AirportManagerSystem.Model;

namespace AirportManagerSystem.UserControls
{
    /// <summary>
    /// Interaction logic for UcFlightProcess.xaml
    /// </summary>
    public partial class UcFlightProcess : UserControl
    {
        DispatcherTimer timer = new DispatcherTimer();
        DateTime Now;

        public UcFlightProcess()
        {
            InitializeComponent();
            imgFlight.Source = new BitmapImage(new Uri(@"/AirportManagerSystem;component/Images/WSC2017_TP09_Airplane-Amonic-Single.png", UriKind.Relative));
            this.Loaded += UcFlightProcess_Loaded;
            timer.Interval = new TimeSpan(0, 0, 0, 2);
            timer.Tick += Timer_Tick;
        }

        public Schedule Flight { get; internal set; }

        private void UcFlightProcess_Loaded(object sender, RoutedEventArgs e)
        {
            grImageContainer.Background = new SolidColorBrush(Colors.White);

            tblFlightInfomation.Text = $"Flight infomation: {Flight.Date.ToString("dd/MM/yyyy")} - {Flight.Time.ToString(@"hh\:mm")} - {Flight.Aircraft.Name} - {Flight.FlightNumber}";
            tblFlightTime.Text = "Flight time: " + Flight.Route.FlightTime.ToString() + " minutes";
            tblDistance.Text = "Distance: " + Flight.Route.Distance.ToString() + " kilometers";

            tblDeparture.Text = Flight.Route.Airport.IATACode;
            tblArrival.Text = Flight.Route.Airport1.IATACode;

            LoadProcess();
        }

        private void LoadProcess()
        {
            imgFlight.Margin = new Thickness(10, 0, 0, 0);
            Now = (Flight.Date + Flight.Time).AddMinutes(Flight.Route.FlightTime - 1);
            Now = DateTime.Now;

            if (Flight.Date + Flight.Time > Now)
            {
                tblFlightStatus.Text = "Aircraft Not Fly";
                tblTimeFly.Text = "";

                timer.Start();
            }
            else
            {
                if ((Flight.Date + Flight.Time).AddMinutes(Flight.Route.FlightTime) < Now)
                {
                    imgFlight.HorizontalAlignment = HorizontalAlignment.Right;
                    imgFlight.Margin = new Thickness(0, 0, 10, 0);
                    tblFlightStatus.Text = "Aircraft ARRIVED";
                    tblTimeFly.Text = "";
                }
                else
                {
                    tblFlightStatus.Text = "Aircraft Flying - ";
                    var timeFlied = Now - (Flight.Date + Flight.Time);
                    tblTimeFly.Text = $"Time flied: {(timeFlied.Minutes + timeFlied.Hours * 60)} minutes {(Now - (Flight.Date + Flight.Time)).Seconds} seconds";

                    var seconds = (Now - (Flight.Date + Flight.Time)).TotalSeconds / 60;
                    var width = seconds * tblProcess.Width / Flight.Route.FlightTime;

                    imgFlight.Margin = new Thickness(10 + width, 0, 0, 0);

                    timer.Start();
                }
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Now = Now.AddSeconds(10);
            if (Flight != null)
            {
                if (Flight.Date + Flight.Time < Now)
                {
                    if ((Flight.Date + Flight.Time).AddMinutes(Flight.Route.FlightTime) < Now)
                    {
                        imgFlight.HorizontalAlignment = HorizontalAlignment.Right;
                        imgFlight.Margin = new Thickness(0, 0, 10, 0);
                        tblFlightStatus.Text = "Aircraft ARRIVED";
                        tblTimeFly.Text = "";
                        timer.Stop();
                    }
                    else
                    {
                        tblFlightStatus.Text = "Aircraft Flying - ";
                        var timeFlied = Now - (Flight.Date + Flight.Time);
                        tblTimeFly.Text = $"Time flied: {(timeFlied.Minutes + timeFlied.Hours * 60)} minutes {(Now - (Flight.Date + Flight.Time)).Seconds} seconds";

                        var seconds = (Now - (Flight.Date + Flight.Time)).TotalSeconds / 60;
                        var width = seconds * tblProcess.Width / Flight.Route.FlightTime;

                        imgFlight.Margin = new Thickness(10 + width, 0, 0, 0);
                    }
                }
            }
        }
    }
}
