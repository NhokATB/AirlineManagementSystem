using AirportManagerSystem.ChartControls;
using AirportManagerSystem.HelperClass;
using AirportManagerSystem.Model;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for SurveyReportWindow.xaml
    /// </summary>
    public partial class SurveyReportWindow : Window
    {
        List<string> genders = new List<string>() { "All genders", "Male", "Female" };
        List<string> chartTypes = new List<string>() { "Column", "Line" };
        List<string> ages = new List<string>() { "All ages", "18 - 24", "25 - 39", "40 - 59", "60+" };
        List<string> years;
        List<Survey> surveys;
        List<string> times;
        int male, female, age18, age25, age40, age60;

        public SurveyReportWindow()
        {
            InitializeComponent();
            rvSummaryReport.ZoomMode = ZoomMode.FullPage;
            rvDetailReport.ZoomMode = ZoomMode.PageWidth;
            this.Loaded += SurveyReportWindow_Loaded;
            dpDateOfFlight.SelectedDateChanged += DpDateOfFlight_SelectedDateChanged;
        }

        private void DpDateOfFlight_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var date = dpDateOfFlight.SelectedDate.Value.Date;

            var flightNumbers = Db.Context.Schedules.Where(t=>t.Date == date).Select(t => t.FlightNumber).Distinct().ToList();
            flightNumbers.Insert(0, "All");
            cbFlightNumber.ItemsSource = flightNumbers;
            cbFlightNumber.SelectedIndex = 0;
        }

        private void SurveyReportWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetColorlegendForAnswer();

            LoaddataForTabSummary();

            LoadDataForTabDetail();

            LoadDataForTabFilterSurvey();
        }

        private void LoaddataForTabSummary()
        {
            years = Db.Context.Surveys.Select(t => t.Respondent.Schedule.Date.Year.ToString()).Distinct().OrderByDescending(t => t).ToList();
            years.Insert(0, "All times");
            cbYear.ItemsSource = years;
            cbYear.SelectedIndex = 0;
        }

        private void LoadDataForTabDetail()
        {
            times = Db.Context.Respondents.Select(t => t.Schedule.Date).Distinct().OrderByDescending(t => t).ToList().Select(t => t.ToString("MMMM yyyy")).Distinct().ToList();
            times.Insert(0, "All times");
            cbTimePeriod.ItemsSource = times;
            cbTimePeriod.SelectedIndex = 0;

            cbGender.ItemsSource = genders;
            cbGender.SelectedIndex = 0;

            cbAge.ItemsSource = ages;
            cbAge.SelectedIndex = 0;

            ResetFilterSurvey();
        }

        private void LoadDataForTabFilterSurvey()
        {
            var flightNumbers = Db.Context.Schedules.Select(t => t.FlightNumber).Distinct().ToList();
            flightNumbers.Insert(0, "All");
            cbFlightNumber.ItemsSource = flightNumbers;
            cbFlightNumber.SelectedIndex = 0;

            var airports = Db.Context.Airports.ToList();
            airports.Insert(0, new Airport() { Name = "All" });
            cbArrivalAirport.ItemsSource = airports;
            cbArrivalAirport.DisplayMemberPath = "Name";
            cbArrivalAirport.SelectedIndex = 0;

            var cabins = Db.Context.CabinTypes.ToList();
            cabins.Insert(0, new CabinType() { Name = "All" });
            cbCabinType.ItemsSource = cabins;
            cbCabinType.DisplayMemberPath = "Name";
            cbCabinType.SelectedIndex = 0;

            cbChartType.ItemsSource = chartTypes;
            cbChartType.SelectedIndex = 0;
        }

        private void LoadChartSurvey()
        {
            grdChartContainer.Children.Clear();
            int i = 0;
            if (chartTypes[cbChartType.SelectedIndex] == "Line")
            {
                UcSurveyLineChart lineChart = new UcSurveyLineChart();
                foreach (var ans in Db.Context.Answers.Where(t => t.AnswerId != 0).ToList())
                {
                    List<KeyValuePair<string, int>> sources = new List<KeyValuePair<string, int>>();
                    foreach (var ques in Db.Context.Questions.ToList())
                    {
                        sources.Add(new KeyValuePair<string, int>(ques.Text, surveys.Count(t => t.AnswerId == ans.AnswerId && t.QuestionId == ques.QuestionId)));
                    }
                    ((LineSeries)(lineChart.mcChart).Series[i]).ItemsSource = sources;
                    i++;
                }
                grdChartContainer.Children.Add(lineChart);
            }
            else
            {
                UcSurveyColumnChart columnChart = new UcSurveyColumnChart();
                foreach (var ans in Db.Context.Answers.Where(t => t.AnswerId != 0).ToList())
                {
                    List<KeyValuePair<string, int>> sources = new List<KeyValuePair<string, int>>();
                    foreach (var ques in Db.Context.Questions.ToList())
                    {
                        sources.Add(new KeyValuePair<string, int>(ques.Text, surveys.Count(t => t.AnswerId == ans.AnswerId && t.QuestionId == ques.QuestionId)));
                    }
                   ((ColumnSeries)(columnChart.mcChart).Series[i]).ItemsSource = sources;
                    i++;
                }
                grdChartContainer.Children.Add(columnChart);
            }
        }

        private void ResetFilterSurvey()
        {
            dgSurveys.ItemsSource = null;
            grdChartContainer.Children.Clear();
        }

        private void SetColorlegendForAnswer()
        {
            lblOutstanding.Background = new SolidColorBrush(AMONICColor.DarkBlue);
            lblVeryGood.Background = new SolidColorBrush(AMONICColor.LightGold);
            lblGood.Background = new SolidColorBrush(AMONICColor.DarkPurple);
            lblAdequate.Background = new SolidColorBrush(AMONICColor.DarkGold);
            lblNeedImprovement.Background = new SolidColorBrush(AMONICColor.MainGold);
            lblPoor.Background = new SolidColorBrush(AMONICColor.DarkGreen);
            lblNotKnow.Background = new SolidColorBrush(AMONICColor.MainOrange);
        }

        private void LoadDetailReport()
        {
            SetParameterForDetailReport();

            var surveys = Db.Context.Surveys.ToList();
            if (cbTimePeriod.SelectedIndex != 0)
            {
                var date = DateTime.Parse(times[cbTimePeriod.SelectedIndex]);
                surveys = surveys.Where(t => t.Respondent.Schedule.Date.Month == date.Month && t.Respondent.Schedule.Date.Year == date.Year).ToList();
            }

            if (chbGender.IsChecked.Value && cbGender.SelectedIndex != 0)
                surveys = surveys.Where(t => t.Respondent.Gender != null).ToList();
            if (chbAge.IsChecked.Value && cbAge.SelectedIndex != 0)
                surveys = surveys.Where(t => t.Respondent.Age >= 18).ToList();

            if (chbGender.IsChecked.Value && male == 0)
                surveys = surveys.Where(t => !(t.Respondent.Gender == "M")).ToList();
            if (chbGender.IsChecked.Value && female == 0)
                surveys = surveys.Where(t => !(t.Respondent.Gender == "F")).ToList();

            if (chbAge.IsChecked.Value && age18 == 0)
                surveys = surveys.Where(t => !(t.Respondent.Age >= 18 && t.Respondent.Age <= 24)).ToList();
            if (chbAge.IsChecked.Value && age25 == 0)
                surveys = surveys.Where(t => !(t.Respondent.Age >= 25 && t.Respondent.Age <= 39)).ToList();
            if (chbAge.IsChecked.Value && age40 == 0)
                surveys = surveys.Where(t => !(t.Respondent.Age >= 40 && t.Respondent.Age <= 59)).ToList();
            if (chbAge.IsChecked.Value && age60 == 0)
                surveys = surveys.Where(t => !(t.Respondent.Age >= 60)).ToList();

            SurveyDataSet.SurveyDataTable dt = new SurveyDataSet.SurveyDataTable();
            foreach (var ques in Db.Context.Questions.ToList())
            {
                foreach (var ans in Db.Context.Answers.Where(t => t.AnswerId != 0).ToList())
                {
                    var report = surveys.Where(t => t.AnswerId == ans.AnswerId && t.QuestionId == ques.QuestionId).ToList();
                    var items = new List<int>()
                    {
                        report.Count(t=>t.Respondent.Gender == "M") * male,
                        report.Count(t=>t.Respondent.Gender == "F") * female,
                        report.Count(t=>t.Respondent.Age >= 18 && t.Respondent.Age <= 24) * age18,
                        report.Count(t=>t.Respondent.Age >= 25 && t.Respondent.Age <= 39) * age25,
                        report.Count(t=>t.Respondent.Age >= 40 && t.Respondent.Age <= 59) * age40,
                        report.Count(t=>t.Respondent.Age >= 60) * age60,
                        report.Count(t=>t.Respondent.CabinType == "Economy"),
                        report.Count(t=>t.Respondent.CabinType == "Business"),
                        report.Count(t=>t.Respondent.CabinType == "First Class"),
                        report.Count(t=>t.Respondent.Arrival == "AUH"),
                        report.Count(t=>t.Respondent.Arrival == "BAH"),
                        report.Count(t=>t.Respondent.Arrival == "DOH"),
                        report.Count(t=>t.Respondent.Arrival == "RUH"),
                        report.Count(t=>t.Respondent.Arrival == "CAI")
                    };

                    dt.AddSurveyRow(ques.Text, ans.Text, ans.AnswerId, items.Sum(), items[0], items[1], items[2], items[3], items[4], items[5], items[6], items[7], items[8], items[9], items[10], items[11], items[12], items[13]);
                }
            }

            ReportDataSource rdsDetail = new ReportDataSource();
            SurveyDataSet dataset = new SurveyDataSet();

            dataset.BeginInit();

            rdsDetail.Name = "DataSet1";
            rdsDetail.Value = dt;
            this.rvDetailReport.LocalReport.DataSources.Clear();
            this.rvDetailReport.LocalReport.DataSources.Add(rdsDetail);
            this.rvDetailReport.LocalReport.ReportPath = "../../Model/SurveyDetailReport.rdlc";

            dataset.EndInit();

            rvDetailReport.LocalReport.SetParameters(new ReportParameter("Male", male.ToString()));
            rvDetailReport.LocalReport.SetParameters(new ReportParameter("Female", female.ToString()));
            rvDetailReport.LocalReport.SetParameters(new ReportParameter("Age18", age18.ToString()));
            rvDetailReport.LocalReport.SetParameters(new ReportParameter("Age25", age25.ToString()));
            rvDetailReport.LocalReport.SetParameters(new ReportParameter("Age40", age40.ToString()));
            rvDetailReport.LocalReport.SetParameters(new ReportParameter("Age60", age60.ToString()));

            this.rvDetailReport.RefreshReport();
        }

        private void SetParameterForDetailReport()
        {
            if (chbGender.IsChecked.Value)
            {
                cbGender.IsEnabled = true;
                switch (cbGender.SelectedIndex)
                {
                    case 0:
                        male = 1;
                        female = 1;
                        break;
                    case 1:
                        male = 1;
                        female = 0;
                        break;
                    case 2:
                        male = 0;
                        female = 1;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                cbGender.IsEnabled = false;
                male = 0;
                female = 0;
                cbGender.SelectedIndex = 0;
            }

            if (chbAge.IsChecked.Value)
            {
                cbAge.IsEnabled = true;
                switch (cbAge.SelectedIndex)
                {
                    case 0:
                        age18 = 1;
                        age25 = 1;
                        age40 = 1;
                        age60 = 1;
                        break;
                    case 1:
                        age18 = 1;
                        age25 = 0;
                        age40 = 0;
                        age60 = 0;
                        break;
                    case 2:
                        age18 = 0;
                        age25 = 1;
                        age40 = 0;
                        age60 = 0;
                        break;
                    case 3:
                        age18 = 0;
                        age25 = 0;
                        age40 = 1;
                        age60 = 0;
                        break;
                    case 4:
                        age18 = 0;
                        age25 = 0;
                        age40 = 0;
                        age60 = 1;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                cbAge.IsEnabled = false;
                age18 = 0;
                age25 = 0;
                age40 = 0;
                age60 = 0;
                cbAge.SelectedIndex = 0;
            }
        }

        private void LoadSummaryReport()
        {
            var report = Db.Context.Respondents.ToList();
            if (cbYear.SelectedIndex != 0)
            {
                var year = int.Parse(years[cbYear.SelectedIndex]);
                report = report.Where(t => t.Schedule.Date.Year == year).ToList();
            }

            var items = new List<int>()
            {
                report.Count(t=>t.Gender == "M"),
                report.Count(t=>t.Gender == "F"),
                report.Count(t=>t.Age >= 18 && t.Age <= 24),
                report.Count(t=>t.Age >= 25 && t.Age <= 39),
                report.Count(t=>t.Age >= 40 && t.Age <= 59),
                report.Count(t=>t.Age >= 60),
                report.Count(t=>t.CabinType == "Economy"),
                report.Count(t=>t.CabinType == "Business"),
                report.Count(t=>t.CabinType == "First Class"),
                report.Count(t=>t.Arrival == "AUH"),
                report.Count(t=>t.Arrival == "BAH"),
                report.Count(t=>t.Arrival == "DOH"),
                report.Count(t=>t.Arrival == "RUH"),
                report.Count(t=>t.Arrival == "CAI")
            };

            SurveyDataSet.SurveyDataTable dt = new SurveyDataSet.SurveyDataTable();
            dt.AddSurveyRow("", "", 0, items.Sum(), items[0], items[1], items[2], items[3], items[4], items[5], items[6], items[7], items[8], items[9], items[10], items[11], items[12], items[13]);

            ReportDataSource rdsByDate = new ReportDataSource();
            SurveyDataSet dataset = new SurveyDataSet();

            dataset.BeginInit();

            rdsByDate.Name = "DataSet1";
            rdsByDate.Value = dt;
            this.rvSummaryReport.LocalReport.DataSources.Clear();
            this.rvSummaryReport.LocalReport.DataSources.Add(rdsByDate);
            this.rvSummaryReport.LocalReport.ReportPath = "../../Model/SurveySummaryReport.rdlc";

            dataset.EndInit();

            rvSummaryReport.LocalReport.SetParameters(new ReportParameter("SampleSize", $"Sample size: {report.Count}"));
            rvSummaryReport.LocalReport.SetParameters(new ReportParameter("Year", $"Time: {years[cbYear.SelectedIndex]}"));

            rvSummaryReport.RefreshReport();
        }

        private void cbTimePeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDetailReport();
        }

        private void cbArrivalAirport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetFilterSurvey();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            ResetFilterSurvey();
            FilterSurvey();

            List<NewSurvey> newSurveys = new List<NewSurvey>();
            foreach (var ques in Db.Context.Questions.ToList())
            {
                NewSurvey ns = new NewSurvey() { Question = ques.Text };
                ns.Outstanding = surveys.Count(t => t.AnswerId == 1 && t.QuestionId == ques.QuestionId);
                ns.VeryGood = surveys.Count(t => t.AnswerId == 2 && t.QuestionId == ques.QuestionId);
                ns.Good = surveys.Count(t => t.AnswerId == 3 && t.QuestionId == ques.QuestionId);
                ns.Adequate = surveys.Count(t => t.AnswerId == 4 && t.QuestionId == ques.QuestionId);
                ns.NeedImprovement = surveys.Count(t => t.AnswerId == 5 && t.QuestionId == ques.QuestionId);
                ns.Poor = surveys.Count(t => t.AnswerId == 6 && t.QuestionId == ques.QuestionId);
                ns.NotKnow = surveys.Count(t => t.AnswerId == 7 && t.QuestionId == ques.QuestionId);

                newSurveys.Add(ns);
            }

            LoadChartSurvey();
            dgSurveys.ItemsSource = newSurveys;
        }

        private void FilterSurvey()
        {
            surveys = Db.Context.Surveys.ToList();
            if (cbArrivalAirport.SelectedIndex != 0)
                surveys = surveys.Where(t => t.Respondent.Arrival == (cbArrivalAirport.SelectedItem as Airport).IATACode).ToList();
            if (cbCabinType.SelectedIndex != 0)
                surveys = surveys.Where(t => t.Respondent.CabinType == cbCabinType.Text).ToList();
            if (cbFlightNumber.SelectedIndex != 0)
                surveys = surveys.Where(t => t.Respondent.Schedule.FlightNumber == cbFlightNumber.Text).ToList();
            if (dpDateOfFlight.SelectedDate != null)
                surveys = surveys.Where(t => t.Respondent.Schedule.Date == dpDateOfFlight.SelectedDate.Value.Date).ToList();
        }

        private void cbChartType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (surveys != null)
            {
                grdChartContainer.Children.Clear();
                LoadChartSurvey();
            }
        }

        private void cbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadSummaryReport();
        }

        private void chbGender_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadDetailReport();
            }
            catch (Exception)
            {
            }
        }

        private void cbFlightNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetFilterSurvey();
        }

        private void cbCabinType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetFilterSurvey();
        }

        private void chbGender_Unchecked(object sender, RoutedEventArgs e)
        {
            LoadDetailReport();
        }

        private void chbAge_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadDetailReport();
            }
            catch (Exception)
            {
            }
        }

        private void chbAge_Unchecked(object sender, RoutedEventArgs e)
        {
            LoadDetailReport();
        }

        private void cbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDetailReport();
        }

        private void cbAge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDetailReport();
        }
    }
}
