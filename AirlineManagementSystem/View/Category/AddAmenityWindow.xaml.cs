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
    /// Interaction logic for AddAmenityWindow.xaml
    /// </summary>
    public partial class AddAmenityWindow : Window
    {
        public AddAmenityWindow()
        {
            InitializeComponent();
        }

        public AmenitiesManagementWindow ManageWindow { get; internal set; }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtAmenityName.Text.Trim() == "")
            {
                MessageBox.Show("Amenity name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtPrice.Text.Trim() == "")
            {
                MessageBox.Show("Amenity price was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                try
                {
                    double.Parse(txtPrice.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Amenity price must be digits", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (Db.Context.Amenities.ToList().Where(t => t.Service == txtAmenityName.Text).FirstOrDefault() != null)
            {
                MessageBox.Show("This amenity was exitst!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Amenity amenity = new Amenity()
            {
                Service = txtAmenityName.Text,
                Price = decimal.Parse(txtPrice.Text)
            };

            Db.Context.Amenities.Add(amenity);
            Db.Context.SaveChanges();
            ManageWindow.LoadAmenities();
            MessageBox.Show("Add amenity successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
