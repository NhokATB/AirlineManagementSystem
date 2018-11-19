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
    /// Interaction logic for AddCabinTypeWindow.xaml
    /// </summary>
    public partial class AddCabinTypeWindow : Window
    {
        List<Amenity> amenities = new List<Amenity>();
        public AddCabinTypeWindow()
        {
            InitializeComponent();
            this.Loaded += AddCabinTypeWindow_Loaded;
        }

        private void AddCabinTypeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var id = Db.Context.CabinTypes.Max(t => t.ID) + 1;
            txtId.Text = id.ToString();
            LoadAmenities();
        }

        public CabinTypesManagementWindow ManageWindow { get; internal set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtCabinName.Text.Trim() == "")
            {
                MessageBox.Show("Cabin name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Db.Context.CabinTypes.ToList().Where(t => t.Name == txtCabinName.Text).FirstOrDefault() != null)
            {
                MessageBox.Show("This cabin name was exists!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (amenities.Count == 0)
            {
                MessageBox.Show("Please choose amenites for this cabin type", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CabinType cabinType = new CabinType()
            {
                ID = int.Parse(txtId.Text),
                Name = txtCabinName.Text,
                Amenities = amenities
            };

            Db.Context.CabinTypes.Add(cabinType);
            Db.Context.SaveChanges();
            ManageWindow.LoadCabinTypes();
            MessageBox.Show("Add cabin type successfull!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
        private void LoadAmenities()
        {
            var amenities = Db.Context.Amenities.ToList();
            foreach (var item in amenities)
            {
                CheckBox c = new CheckBox()
                {
                    Content = item.Price == 0 ? $"{item.Service} (Free)" : $"{item.Service} ({item.Price.ToString("C0")})",
                    Width = 270,
                    Tag = item,
                    Margin = new Thickness(5)
                };

                c.Checked += C_Checked;
                c.Unchecked += C_Unchecked;

                wpAmenities.Children.Add(c);
            }
        }

        private void C_Unchecked(object sender, RoutedEventArgs e)
        {
            var chb = sender as CheckBox;
            amenities.Remove((chb.Tag as Amenity));
        }

        private void C_Checked(object sender, RoutedEventArgs e)
        {
            var chb = sender as CheckBox;
            amenities.Add((chb.Tag as Amenity));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
