using AirportManagerSystem.HelperClass;
using AirportManagerSystem.Model;
using AirportManagerSystem.UserControls;
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
    /// Interaction logic for SurveyQuestionnaireWindow.xaml
    /// </summary>
    public partial class SurveyQuestionnaireWindow : Window
    {
        public SurveyQuestionnaireWindow()
        {
            InitializeComponent();
            this.Loaded += SurveyQuestionnaireWindow_Loaded;
        }

        private void SurveyQuestionnaireWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetColorlegendForAnswer();

            LoadInformation();

            var questions = Db.Context.Questions.ToList();
            foreach (var ques in questions)
            {
                stpQuestions.Children.Add(new UcSurvey() { Question = ques });
            }
        }

        private void LoadInformation()
        {
            var today = DateTime.Now.Date;
            var flightIds = Db.Context.Schedules.Where(t => t.Date == today).Select(t => t.ID).ToList();
            cbFlightId.ItemsSource = flightIds;
            cbFlightId.SelectedIndex = 0;

            var cabins = Db.Context.CabinTypes.ToList();
            cbCabinType.ItemsSource = cabins;
            cbCabinType.DisplayMemberPath = "Name";
            cbCabinType.SelectedIndex = 0;

            var ages = Enumerable.Range(10, 200);
            cbAge.ItemsSource = ages;
            cbAge.SelectedIndex = 0;
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

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var surveys = stpQuestions.Children;
            int flightId = int.Parse(cbFlightId.Text);

            Respondent rsd = new Respondent()
            {
                Age = int.Parse(cbAge.Text),
                Arrival = Db.Context.Schedules.Find(flightId).Route.Airport1.IATACode,
                CabinType = cbCabinType.Text,
                Gender = rdbFemale.IsChecked.Value ? "F" : "M",
                ScheduleId = int.Parse(cbFlightId.Text),
            };

            foreach (UcSurvey item in surveys)
            {
                if (item.AnswerId == -1)
                {
                    MessageBox.Show("Please choose answer for all question!!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                rsd.Surveys.Add(new Survey()
                {
                    AnswerId = item.AnswerId,
                    QuestionId = item.Question.QuestionId
                });
            }

            Db.Context.Respondents.Add(rsd);
            Db.Context.SaveChanges();

            MessageBox.Show("Thank for your feedback and have a nice day!!", "Suceessful", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
