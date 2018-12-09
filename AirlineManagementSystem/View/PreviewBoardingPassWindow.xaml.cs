using AirportManagerSystem.Model;
using Microsoft.Reporting.WinForms;
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
    /// Interaction logic for PreviewBoardingPassWindow.xaml
    /// </summary>
    public partial class PreviewBoardingPassWindow : Window
    {
        public PreviewBoardingPassWindow()
        {
            InitializeComponent();

            this.Loaded += PreviewBoardingPassWindow_Loaded;

            this.rvBoardingPassPreview.LocalReport.ReportPath = "../../Model/TicketInformationReport.rdlc";

            this.rvBoardingPassPreview.ShowToolBar = false;

            this.rvBoardingPassPreview.ZoomMode = ZoomMode.PageWidth;
        }

        public int TicketId { get; internal set; }

        private void PreviewBoardingPassWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var ticket = Db.Context.Tickets.Find(TicketId);

            rvBoardingPassPreview.LocalReport.SetParameters(new ReportParameter("CabinType", ticket.CabinType.Name));
            rvBoardingPassPreview.LocalReport.SetParameters(new ReportParameter("Name", ticket.Firstname + " " + ticket.Lastname));
            rvBoardingPassPreview.LocalReport.SetParameters(new ReportParameter("From", ticket.Schedule.Route.Airport.Name));
            rvBoardingPassPreview.LocalReport.SetParameters(new ReportParameter("To", ticket.Schedule.Route.Airport1.Name));
            rvBoardingPassPreview.LocalReport.SetParameters(new ReportParameter("Flight", ticket.Schedule.ID.ToString()));
            rvBoardingPassPreview.LocalReport.SetParameters(new ReportParameter("Gate", (ticket.Schedule.Gate == null ? "None" : ticket.Schedule.Gate.ToString())));
            rvBoardingPassPreview.LocalReport.SetParameters(new ReportParameter("Date", ticket.Schedule.Date.ToString("dd/MM/yyyy")));
            rvBoardingPassPreview.LocalReport.SetParameters(new ReportParameter("Time", ticket.Schedule.Time.ToString(@"hh\:mm")));
            rvBoardingPassPreview.LocalReport.SetParameters(new ReportParameter("BoardingTime", (ticket.Schedule.Date + ticket.Schedule.Time).AddMinutes(-30).ToString(@"hh\:mm")));
            rvBoardingPassPreview.LocalReport.SetParameters(new ReportParameter("Seat", ticket.Seat));

            rvBoardingPassPreview.RefreshReport();
        }
    }
}
