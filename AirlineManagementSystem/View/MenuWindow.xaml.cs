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
using System.Windows.Forms;
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
        public bool IsClosed { get; internal set; }

        public MenuWindow()
        {
            InitializeComponent();
            imgLogo.Source = new BitmapImage(new Uri(@"/AirportManagerSystem;component/Images/WSC2017_TP09_color@4x.png", UriKind.Relative));
            this.Closing += MenuWindow_Closing;
            this.Loaded += MenuWindow_Loaded;
        }

        private void MenuWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (User.Role.Title != "Administrator")
            {
                mnManagement.Items.Remove(mnAircrafts);
                mnManagement.Items.Remove(mnAirports);
                mnManagement.Items.Remove(mnOffices);
                mnManagement.Items.Remove(mnCabinTypes);
                mnManagement.Items.Remove(mnRoutes);

                mnReport.Items.Remove(mnShortSummaryReport);

                if (User.Role.Title == "User")
                {
                    mnManagement.Items.Remove(mnUsers);
                    mnManagement.Items.Remove(mnAmenities);
                    mnManagement.Items.Remove(mnFlights);
                    mnManagement.Items.Remove(mnLogingHistory);
                    mnManagement.Items.Remove(mnCrews);

                    mnReport.Items.Remove(mnSurveyReport);
                }
            }
        }

        private void MenuWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var log = Db.Context.LoginHistories.Where(t => t.UserId == User.ID).ToList().Last();
            log.LogoutTime = DateTime.Now;
            Db.Context.SaveChanges();
        }

        private void MnUsers_Click(object sender, RoutedEventArgs e)
        {
            UserManagementWindow wUsers = new UserManagementWindow();
            wUsers.User = User;
            ShowDialogWindow(wUsers);
        }

        private void mnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnLogingHistory_Click(object sender, RoutedEventArgs e)
        {
            LoginHistoryWindow wLoginHistory = new LoginHistoryWindow();
            wLoginHistory.User = User;
            ShowDialogWindow(wLoginHistory);
        }

        private void mnProfile_Click(object sender, RoutedEventArgs e)
        {
            EditProfileWindow wEditProfile = new EditProfileWindow();
            wEditProfile.LogonUser = User;
            wEditProfile.User = User;
            ShowDialogWindow(wEditProfile);
        }

        private void mnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow wChangePass = new ChangePasswordWindow();
            wChangePass.User = User;
            ShowDialogWindow(wChangePass);
        }

        private void mnPurchaseAmenities_Click(object sender, RoutedEventArgs e)
        {
            PurchaseAmenitiesWindow wPurchaseAmenities = new PurchaseAmenitiesWindow();
            ShowDialogWindow(wPurchaseAmenities);
        }

        private void mnBookFlight_Click(object sender, RoutedEventArgs e)
        {
            BookFlightWindow wBookFlight = new BookFlightWindow();
            wBookFlight.User = User;
            ShowDialogWindow(wBookFlight);
        }

        private void mnShortSummaryReport_Click(object sender, RoutedEventArgs e)
        {
            ShortSummaryWindow wShortSummary = new ShortSummaryWindow();
            ShowDialogWindow(wShortSummary);
        }

        private void mnAmenitiesReprot_Click(object sender, RoutedEventArgs e)
        {
            AmenitiesReportWindow wAmenitiesReport = new AmenitiesReportWindow();
            ShowDialogWindow(wAmenitiesReport);
        }

        private void mnSurveyReport_Click(object sender, RoutedEventArgs e)
        {
            SurveyReportWindow wSurveyReport = new SurveyReportWindow();
            ShowDialogWindow(wSurveyReport);
        }

        private void mnSurveyQuestionaire_Click(object sender, RoutedEventArgs e)
        {
            SurveyQuestionnaireWindow wSurveyQuestionnaire = new SurveyQuestionnaireWindow();
            ShowDialogWindow(wSurveyQuestionnaire);
        }

        private void mnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            CheckInWindow wCheckin = new CheckInWindow();
            ShowDialogWindow(wCheckin);
        }

        private void mnFlights_Click(object sender, RoutedEventArgs e)
        {
            FlightManagementWindow wFlightManagement = new FlightManagementWindow();
            ShowDialogWindow(wFlightManagement);
        }

        private void mnTickets_Click(object sender, RoutedEventArgs e)
        {
            TicketsManagementWindow wTicketsManagement = new TicketsManagementWindow();
            ShowDialogWindow(wTicketsManagement);
        }

        private void mnAmenities_Click(object sender, RoutedEventArgs e)
        {
            AmenitiesManagementWindow wAmenityManagement = new AmenitiesManagementWindow();
            ShowDialogWindow(wAmenityManagement);
        }

        private void mnRoutes_Click(object sender, RoutedEventArgs e)
        {
            RoutesManagementWindow wRouteManagement = new RoutesManagementWindow();
            ShowDialogWindow(wRouteManagement);
        }

        private void mnAircrafts_Click(object sender, RoutedEventArgs e)
        {
            AircraftManagementWindow wAircraftManagement = new AircraftManagementWindow();
            ShowDialogWindow(wAircraftManagement);
        }

        private void mnCabinTypes_Click(object sender, RoutedEventArgs e)
        {
            CabinTypesManagementWindow wCabinTypesManagement = new CabinTypesManagementWindow();
            ShowDialogWindow(wCabinTypesManagement);
        }

        private void mnLoginHistory_Click(object sender, RoutedEventArgs e)
        {
            LoginHistoryManagementWindow wLoginHistoryManagement = new LoginHistoryManagementWindow();
            ShowDialogWindow(wLoginHistoryManagement);
        }

        private void ShowDialogWindow(Window window)
        {
            this.Opacity = 0.5;
            window.ShowDialog();
            this.Opacity = 1;
        }
        private void ShowDialogWindow(Form window)
        {
            this.Opacity = 0.5;
            window.ShowDialog();
            this.Opacity = 1;
        }

        private void mnOffices_Click(object sender, RoutedEventArgs e)
        {
            OfficesManagementWindow officesManagementWindow = new OfficesManagementWindow();
            ShowDialogWindow(officesManagementWindow);
        }

        private void mnAirports_Click(object sender, RoutedEventArgs e)
        {
            AirportManagementWindow airportManagementWindow = new AirportManagementWindow();
            ShowDialogWindow(airportManagementWindow);
        }

        private void mnCrews_Click(object sender, RoutedEventArgs e)
        {
            CrewManagementWindow crewManagementWindow = new CrewManagementWindow()
            {
                LogonUser = User
            };
            ShowDialogWindow(crewManagementWindow);
        }

        private void mnMembers_Click(object sender, RoutedEventArgs e)
        {
            MemberManagementWindow memberManagementWindow = new MemberManagementWindow()
            {
                LogonUser = User
            };
            ShowDialogWindow(memberManagementWindow);
        }

        private void mnSeats_Click(object sender, RoutedEventArgs e)
        {
            SeatManagementWindow seatManagementWindow = new SeatManagementWindow();
            ShowDialogWindow(seatManagementWindow);
        }
    }
}
