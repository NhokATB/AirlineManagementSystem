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
    /// Interaction logic for EditAmenityWindow.xaml
    /// </summary>
    public partial class EditAmenityWindow : Window
    {
        public EditAmenityWindow()
        {
            InitializeComponent();
            this.Loaded += EditAmenityWindow_Loaded;
        }

        private void EditAmenityWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtAmenityName.Text = Amenity.Service;
            txtPrice.Text = Amenity.Price.ToString();
        }

        public Amenity Amenity { get; internal set; }
        public AmenitiesManagementWindow ManageWindow { get; internal set; }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
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

            if (txtAmenityName.Text != Amenity.Service && Db.Context.Amenities.ToList().Where(t => t.Service == txtAmenityName.Text).FirstOrDefault() != null)
            {
                MessageBox.Show("This amenity was exitst!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Amenity.Service = txtAmenityName.Text;
            Amenity.Price = decimal.Parse(txtPrice.Text);

            Db.Context.SaveChanges();
            ManageWindow.LoadAmenities();
            MessageBox.Show("Edit amenity successful!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
