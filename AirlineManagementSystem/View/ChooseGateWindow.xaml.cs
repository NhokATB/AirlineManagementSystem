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
using AirportManagerSystem.Model;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for ChooseGateWindow.xaml
    /// </summary>
    public partial class ChooseGateWindow : Window
    {
        internal NewFlight Flight { get; set; }
        public SetUpGateForFlightWindow ManageForm { get; internal set; }

        public ChooseGateWindow()
        {
            InitializeComponent();
            this.Loaded += ChooseGateWindow_Loaded;
        }

        private void ChooseGateWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var gates = Enumerable.Range(1, 20);
            cbGates.ItemsSource = gates;

            if (Flight.Schedule.Gate != null)
            {
                cbGates.SelectedItem = Flight.Schedule.Gate;
            }
            else
            {
                cbGates.SelectedIndex = 0;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Flight.Schedule.Gate = int.Parse(cbGates.Text);
            Db.Context.SaveChanges();
            ManageForm.LoadFlights();
            MessageBox.Show("Set up gate successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
