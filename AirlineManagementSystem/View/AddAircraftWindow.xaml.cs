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
    /// Interaction logic for AddAircraftWindow.xaml
    /// </summary>
    public partial class AddAircraftWindow : Window
    {
        public AddAircraftWindow()
        {
            InitializeComponent();
        }

        public AircraftManagementWindow ManageWindow { get; internal set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int totalSeat, economySeat, businessSeat;

            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtMakeModel.Text.Trim() == "")
            {
                MessageBox.Show("Make model was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                totalSeat = int.Parse(txtTotalSeat.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Total seats must be digits!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                economySeat = int.Parse(txtEconomySeats.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Economy seats must be digits!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                businessSeat = int.Parse(txtBusinessSeats.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Business seats must be digits!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Db.Context.Aircrafts.ToList().Where(t => t.Name == txtName.Text).FirstOrDefault() != null)
            {
                MessageBox.Show("This aircraft was exists!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Aircraft aircraft = new Aircraft()
            {
                Name = txtName.Text,
                MakeModel = txtMakeModel.Text,
                TotalSeats = totalSeat,
                EconomySeats = economySeat,
                BusinessSeats = businessSeat
            };

            Db.Context.Aircrafts.Add(aircraft);
            Db.Context.SaveChanges();
            ManageWindow.LoadAircrafts();
            MessageBox.Show("Add aircraft successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
