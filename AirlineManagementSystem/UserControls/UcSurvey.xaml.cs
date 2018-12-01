using AirportManagerSystem.HelperClass;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirportManagerSystem.UserControls
{
    /// <summary>
    /// Interaction logic for UcSurvey.xaml
    /// </summary>
    public partial class UcSurvey : UserControl
    {
        public Question Question { get; set; }
        public int AnswerId = -1;
        public UcSurvey()
        {
            InitializeComponent();

            SetColorlegendForAnswer();
            rdbOutstanding.Checked += AnwserChecked;
            rdbVeryGood.Checked += AnwserChecked;
            rdbGood.Checked += AnwserChecked;
            rdbNeedImprovement.Checked += AnwserChecked;
            rdbNotKnow.Checked += AnwserChecked;
            rdbPoor.Checked += AnwserChecked;
            rdbAdequate.Checked += AnwserChecked;

            this.Loaded += UcSurvey_Loaded;
        }

        private void UcSurvey_Loaded(object sender, RoutedEventArgs e)
        {
            tblQuestion.Text = $"{Question.QuestionId}. {Question.Text}";
        }

        private void AnwserChecked(object sender, RoutedEventArgs e)
        {
            var rdb = sender as RadioButton;
            AnswerId = GetAnswerId(rdb.Name.Substring(3));
        }

        private int GetAnswerId(string answer)
        {
            switch (answer)
            {
                case "OutStanding": return 1;
                case "VeryGood": return 2;
                case "Good": return 3;
                case "Adequate": return 4;
                case "NeedImprovement": return 5;
                case "Poor": return 6;
                default: return 7;
            }
        }

        private void SetColorlegendForAnswer()
        {
            rdbOutstanding.Background = new SolidColorBrush(AMONICColor.DarkBlue);
            rdbVeryGood.Background = new SolidColorBrush(AMONICColor.LightGold);
            rdbGood.Background = new SolidColorBrush(AMONICColor.DarkPurple);
            rdbAdequate.Background = new SolidColorBrush(AMONICColor.DarkGold);
            rdbNeedImprovement.Background = new SolidColorBrush(AMONICColor.MainGold);
            rdbPoor.Background = new SolidColorBrush(AMONICColor.DarkGreen);
            rdbNotKnow.Background = new SolidColorBrush(AMONICColor.MainOrange);
        }
    }
}
