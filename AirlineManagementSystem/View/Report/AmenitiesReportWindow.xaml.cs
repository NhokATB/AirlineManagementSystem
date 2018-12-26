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
    /// Interaction logic for AmenitiesReport.xaml
    /// </summary>
    public partial class AmenitiesReportWindow : Window
    {
        public AmenitiesReportWindow()
        {
            InitializeComponent();
        }

        string information;
        string flightNumber;
        string time;
        private void btnMakeReport_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            var tickets = Db.Context.Tickets.ToList();
            if (txtFlightNumber.Text.Trim() != "")
            {
                if (Db.Context.Schedules.Select(t => t.FlightNumber).Contains(txtFlightNumber.Text.Trim()) == false)
                {
                    MessageBox.Show("This flight number not exists", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                flightNumber = txtFlightNumber.Text.Trim();
                tickets = tickets.Where(t => t.Schedule.FlightNumber == flightNumber).ToList();
            }
            else flightNumber = "0";

            if (dpFrom.SelectedDate == null)
            {
                MessageBox.Show("Please choose from date", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (dpTo.SelectedDate == null)
                {
                    tickets = tickets.Where(t => t.Schedule.Date == dpFrom.SelectedDate.Value.Date).ToList();
                    time = "Date: " + dpFrom.SelectedDate.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    if (dpTo.SelectedDate.Value.Date < dpFrom.SelectedDate.Value.Date)
                    {
                        MessageBox.Show("From date must <= to date", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        tickets = tickets.Where(t => t.Schedule.Date >= dpFrom.SelectedDate.Value.Date && t.Schedule.Date <= dpTo.SelectedDate.Value.Date).ToList();
                        time = "From: " + dpFrom.SelectedDate.Value.ToString("dd/MM/yyyy") + "\t\tTo: " + dpTo.SelectedDate.Value.ToString("dd/MM/yyyy");
                    }
                }
            }

            AmenitiesDataSet.AmenitiesReportDataTable dt = new AmenitiesDataSet.AmenitiesReportDataTable();
            foreach (var amen in Db.Context.Amenities.ToList())
            {
                List<int> numTick = new List<int>();
                foreach (var cabin in Db.Context.CabinTypes.ToList())
                {
                    var tickByCabin = tickets.Where(t => t.CabinTypeID == cabin.ID).ToList();
                    if (cabin.Amenities.Contains(amen))
                    {
                        if (amen.Price == 0) numTick.Add(tickByCabin.Count);
                        else numTick.Add(tickByCabin.Count(k => k.AmenitiesTickets.Select(t => t.AmenityID).Contains(amen.ID)));
                    }
                    else numTick.Add(0);
                }
                dt.AddAmenitiesReportRow(amen.Service, numTick[0].ToString(), numTick[1].ToString(), numTick[2].ToString());
            }

            ReportDataSource rdsByDate = new ReportDataSource();
            AmenitiesDataSet dataset = new AmenitiesDataSet();

            dataset.BeginInit();

            rdsByDate.Name = "DataSet1";
            rdsByDate.Value = dt;
            this.rvAmenitiesReport.LocalReport.DataSources.Clear();
            this.rvAmenitiesReport.LocalReport.DataSources.Add(rdsByDate);
            this.rvAmenitiesReport.LocalReport.ReportPath = "../../Model/AmenitiesReport.rdlc";

            dataset.EndInit();

            information = flightNumber == "0" ? time : $"Flight number: {flightNumber}\t\t" + time;
            rvAmenitiesReport.LocalReport.SetParameters(new ReportParameter("information", information));

            rvAmenitiesReport.RefreshReport();
            wfAmenitiesReport.Visibility = Visibility.Visible;

            this.Cursor = Cursors.Arrow;
        }
       
        private void ResetAll()
        {
            try
            {
                wfAmenitiesReport.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
            }
        }
    }
}
