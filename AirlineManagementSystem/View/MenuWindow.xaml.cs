using AirportManagerSystem.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

            this.WindowState = WindowState.Maximized;
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

                if (User.Role.Title == "Manager")
                {
                    mnSystem.Items.Remove(mnMyCommission);
                }
                else
                {
                    mnManagement.Items.Remove(mnUsers);
                    mnManagement.Items.Remove(mnAmenities);
                    mnManagement.Items.Remove(mnFlights);
                    mnManagement.Items.Remove(mnLogingHistory);
                    mnManagement.Items.Remove(mnCrews);
                    mnManagement.Items.Remove(mnMembers);

                    mnBusiness.Items.Remove(mnSetUpCrew);
                    mnBusiness.Items.Remove(mnSetUpGate);
                    mnBusiness.Items.Remove(mnFlightProcess);

                    mnReport.Items.Remove(mnSurveyReport);
                    mnReport.Items.Remove(mnRevenueReport);

                    if (User.Role.Title == "Agent")
                    {
                        mnBusiness.Items.Remove(mnCheckIn);
                        mnBusiness.Items.Remove(mnTicketControl);
                        mnBusiness.Items.Remove(mnSurveyQuestionaire);

                        mnManagement.Items.Remove(mnTickets);
                        mnManagement.Items.Remove(mnSeats);

                        mnReport.Items.Remove(mnAmenitiesReprot);

                        menu.Items.Remove(mnManagement);
                        menu.Items.Remove(mnReport);
                    }
                    else
                    {
                        mnReport.Items.Remove(mnCommisstion);
                    }
                }
            }
            else
            {
                mnSystem.Items.Remove(mnMyCommission);
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
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            SurveyReportWindow surveyReportWindow = new SurveyReportWindow();
            ShowDialogWindow(surveyReportWindow);
            Mouse.OverrideCursor = null;
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

            if (wTicketsManagement.IsAddTicket)
            {
                BookFlightWindow wBookFlight = new BookFlightWindow();
                wBookFlight.User = User;
                ShowDialogWindow(wBookFlight);
            }
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
            Mouse.OverrideCursor = null;
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

        private void mnChangeTicket_Click(object sender, RoutedEventArgs e)
        {
            ChangeTicketWindow changeTicketWindow = new ChangeTicketWindow();
            ShowDialogWindow(changeTicketWindow);
        }

        private void mnCancelTicket_Click(object sender, RoutedEventArgs e)
        {
            CancelTicketWindow cancelTicketWindow = new CancelTicketWindow();
            ShowDialogWindow(cancelTicketWindow);
        }

        private void mnTicketControl_Click(object sender, RoutedEventArgs e)
        {
            TicketControlWindow ticketControlWindow = new TicketControlWindow();
            ShowDialogWindow(ticketControlWindow);
        }

        private void mnSetUpCrew_Click(object sender, RoutedEventArgs e)
        {
            SetUpCrewWindow setUpCrewWindow = new SetUpCrewWindow();
            ShowDialogWindow(setUpCrewWindow);
        }

        private void mnChangeTicketPolicy_Click(object sender, RoutedEventArgs e)
        {
            ChangeTicketPolicyWindow changeTicketPolicyWindow = new ChangeTicketPolicyWindow();
            ShowDialogWindow(changeTicketPolicyWindow);
        }

        private void mnCancelTicketPolicy_Click(object sender, RoutedEventArgs e)
        {
            CancelTicketPolicyWindow cancelTicketPolicyWindow = new CancelTicketPolicyWindow();
            ShowDialogWindow(cancelTicketPolicyWindow);
        }

        private void mnAutomationSystem_Click(object sender, RoutedEventArgs e)
        {
            AboutAirlineManagementSystemWindow aboutAirlineManagementSystemWindow = new AboutAirlineManagementSystemWindow();
            ShowDialogWindow(aboutAirlineManagementSystemWindow);
        }

        private void mnAmonicAirline_Click(object sender, RoutedEventArgs e)
        {
            AboutAMONICAirlineWindow aboutAMONICAirlineWindow = new AboutAMONICAirlineWindow();
            ShowDialogWindow(aboutAMONICAirlineWindow);
        }

        private void mnFlightProcess_Click(object sender, RoutedEventArgs e)
        {
            FlightProcessWindow flightProcessWindow = new FlightProcessWindow();
            ShowDialogWindow(flightProcessWindow);
        }

        private void mnCommisstion_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            CommissionReportWindow commissionReportWindow = new CommissionReportWindow();
            ShowDialogWindow(commissionReportWindow);
            Mouse.OverrideCursor = null;
        }

        private void mnMyCommission_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            MyCommissionWindow myCommissionWindow = new MyCommissionWindow
            {
                User = User.FirstName + " " + User.LastName
            };
            ShowDialogWindow(myCommissionWindow);
            Mouse.OverrideCursor = null;
        }

        private void mnRevenueReport_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            RevenueReportWindow revenueReportWindow = new RevenueReportWindow();
            ShowDialogWindow(revenueReportWindow);
            Mouse.OverrideCursor = null;
        }

        private void mnSetUpGate_Click(object sender, RoutedEventArgs e)
        {
            SetUpGateForFlightWindow setUpGateForFlightWindow = new SetUpGateForFlightWindow();
            ShowDialogWindow(setUpGateForFlightWindow);
        }

        private void MnUserGuide_Click(object sender, RoutedEventArgs e)
        {
            //open pdf File
        }
    }
}
