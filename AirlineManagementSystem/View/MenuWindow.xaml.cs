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
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public User User { get; internal set; }
        public MenuWindow()
        {
            InitializeComponent();
            imgLogo.Source = new BitmapImage(new Uri(@"/AirportManagerSystem;component/Images/WSC2017_TP09_color@4x.png", UriKind.Relative));
            this.Closing += MenuWindow_Closing;
            this.Loaded += MenuWindow_Loaded;
        }

        private void MenuWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Process Menu
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
            this.Opacity = 0.9;
            wUsers.ShowDialog();
            this.Opacity = 1;
        }

        private void mnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnLogingHistory_Click(object sender, RoutedEventArgs e)
        {
            LoginHistoryWindow wLoginHistory = new LoginHistoryWindow();
            wLoginHistory.User = User;
            wLoginHistory.ShowDialog();
        }

        private void mnProfile_Click(object sender, RoutedEventArgs e)
        {
            EditProfileWindow wEditProfile = new EditProfileWindow();
            wEditProfile.LogonUser = User;
            wEditProfile.User = User;
            wEditProfile.ShowDialog();
        }

        private void mnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow wChangePass = new ChangePasswordWindow();
            wChangePass.User = User;
            wChangePass.ShowDialog();
        }

        private void mnPurchaseAmenities_Click(object sender, RoutedEventArgs e)
        {
            PurchaseAmenitiesWindow wPurchaseAmenities = new PurchaseAmenitiesWindow();
            wPurchaseAmenities.ShowDialog();
        }

        private void mnBookFlight_Click(object sender, RoutedEventArgs e)
        {
            BookFlightWindow wBookFlight = new BookFlightWindow();
            wBookFlight.User = User;
            wBookFlight.ShowDialog();
        }

        private void mnShortSummaryReport_Click(object sender, RoutedEventArgs e)
        {
            ShortSummaryWindow wShortSummary = new ShortSummaryWindow();
            wShortSummary.ShowDialog();
        }

        private void mnAmenitiesReprot_Click(object sender, RoutedEventArgs e)
        {
            AmenitiesReportWindow wAmenitiesReport = new AmenitiesReportWindow();
            wAmenitiesReport.ShowDialog();
        }

        private void mnSurveyReport_Click(object sender, RoutedEventArgs e)
        {
            SurveyReportWindow wSurveyReport = new SurveyReportWindow();
            wSurveyReport.ShowDialog();
        }

        private void mnSurveyQuestionaire_Click(object sender, RoutedEventArgs e)
        {
            SurveyQuestionnaireWindow wSurveyQuestionnaire = new SurveyQuestionnaireWindow();
            wSurveyQuestionnaire.ShowDialog();
        }

        private void mnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            CheckInWindow wCheckin = new CheckInWindow();
            wCheckin.ShowDialog();
        }

        private void mnFlights_Click(object sender, RoutedEventArgs e)
        {
            FlightManagementWindow wFlightManagement = new FlightManagementWindow();
            wFlightManagement.ShowDialog();
        }

        private void mnTickets_Click(object sender, RoutedEventArgs e)
        {
            TicketsManagementWindow wTicketsManagement = new TicketsManagementWindow();
            wTicketsManagement.ShowDialog();
        }

        private void mnAmenities_Click(object sender, RoutedEventArgs e)
        {
            AmenitiesManagementWindow wAmenityManagement = new AmenitiesManagementWindow();
            wAmenityManagement.ShowDialog();
        }

        private void mnRoutes_Click(object sender, RoutedEventArgs e)
        {
            RoutesManagementWindow wRouteManagement = new RoutesManagementWindow();
            wRouteManagement.ShowDialog();
        }

        private void mnAircrafts_Click(object sender, RoutedEventArgs e)
        {
            AircraftManagementWindow wAircraftManagement = new AircraftManagementWindow();
            wAircraftManagement.ShowDialog();
        }

        private void mnCabinTypes_Click(object sender, RoutedEventArgs e)
        {
            CabinTypesManagementWindow wCabinTypesManagement = new CabinTypesManagementWindow();
            wCabinTypesManagement.ShowDialog();
        }
    }
}
