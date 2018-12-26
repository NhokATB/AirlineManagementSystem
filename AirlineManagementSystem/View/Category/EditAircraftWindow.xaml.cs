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
    /// Interaction logic for EditAircraftWindow.xaml
    /// </summary>
    public partial class EditAircraftWindow : Window
    {
        public EditAircraftWindow()
        {
            InitializeComponent();
            this.Loaded += EditAircraftWindow_Loaded;
        }

        private void EditAircraftWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Text = Aircraft.Name;
            txtMakeModel.Text = Aircraft.MakeModel;
            txtTotalSeat.Text = Aircraft.TotalSeats.ToString();
            txtEconomySeats.Text = Aircraft.EconomySeats.ToString();
            txtBusinessSeats.Text = Aircraft.BusinessSeats.ToString();
        }

        public Aircraft Aircraft { get; internal set; }
        public AircraftManagementWindow ManageWindow { get; internal set; }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
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
            if (txtName.Text != Aircraft.Name)
            {
                if (Db.Context.Aircrafts.ToList().Where(t => t.Name == txtName.Text).FirstOrDefault() != null)
                {
                    MessageBox.Show("This aircraft was exists!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            Aircraft.Name = txtName.Text;
            Aircraft.MakeModel = txtMakeModel.Text;
            Aircraft.TotalSeats = totalSeat;
            Aircraft.EconomySeats = economySeat;
            Aircraft.BusinessSeats = businessSeat;

            Db.Context.SaveChanges();
            ManageWindow.LoadAircrafts();
            MessageBox.Show("Edit aircraft successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
