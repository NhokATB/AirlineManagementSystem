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
    /// Interaction logic for EditCabinTypeWindow.xaml
    /// </summary>
    public partial class EditCabinTypeWindow : Window
    {
        private List<int> amenitiesId;

        public CabinType Cabin { get; internal set; }
        public CabinTypesManagementWindow ManageWindow { get; internal set; }

        public EditCabinTypeWindow()
        {
            InitializeComponent();
            this.Loaded += EditCabinTypeWindow_Loaded;
        }

        private void EditCabinTypeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            amenitiesId = Cabin.Amenities.Select(t => t.ID).ToList();
            LoadAmenities();

            txtCabinName.Text = Cabin.Name;
            txtId.Text = Cabin.ID.ToString();
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

                c.IsChecked = amenitiesId.Contains(item.ID);

                wpAmenities.Children.Add(c);
            }
        }

        private void C_Unchecked(object sender, RoutedEventArgs e)
        {
            var chb = sender as CheckBox;
            amenitiesId.Remove((chb.Tag as Amenity).ID);
        }

        private void C_Checked(object sender, RoutedEventArgs e)
        {
            var amen = (sender as CheckBox).Tag as Amenity;
            if (amenitiesId.Contains(amen.ID) == false)
                amenitiesId.Add((amen.ID));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtCabinName.Text.Trim() == "")
            {
                MessageBox.Show("Cabin name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtCabinName.Text != Cabin.Name)
            {
                if (Db.Context.CabinTypes.ToList().Where(t => t.Name == txtCabinName.Text).FirstOrDefault() != null)
                {
                    MessageBox.Show("This cabin name was exists!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (amenitiesId.Count == 0)
            {
                MessageBox.Show("Please choose amenites for this cabin type", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Cabin.Name = txtCabinName.Text;
            Cabin.Amenities.Clear();
            foreach (var item in amenitiesId)
            {
                Cabin.Amenities.Add(Db.Context.Amenities.Find(item));
            }

            Db.Context.SaveChanges();
            ManageWindow.LoadCabinTypes();
            MessageBox.Show("Edit cabin type successfull!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
