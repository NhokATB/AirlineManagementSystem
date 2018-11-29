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
    /// Interaction logic for AddCrewWindow.xaml
    /// </summary>
    public partial class AddCrewWindow : Window
    {
        public CrewManagementWindow ManageWindow { get; internal set; }
        public User LogonUser { get; internal set; }
        public AddCrewWindow()
        {
            InitializeComponent();
            this.Loaded += AddCrewWindow_Loaded;
        }

        private void AddCrewWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var offices = Db.Context.Offices.ToList();
            cbOffice.ItemsSource = offices;
            cbOffice.DisplayMemberPath = "Title";
            cbOffice.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtCrewName.Text.Trim() == "")
            {
                MessageBox.Show("Crew name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Db.Context.Crews.ToList().FirstOrDefault(t => t.CrewName == txtCrewName.Text) != null)
            {
                MessageBox.Show("This crew name was used", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtNumberOfMembers.Text.Trim() == "")
            {
                MessageBox.Show("Number of member was required", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                int.Parse(txtNumberOfMembers.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("Number of members must be integer", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Crew crew = new Crew()
            {
                CrewName = txtCrewName.Text.Trim(),
                NumberOfMembers = int.Parse(txtNumberOfMembers.Text.Trim()),
                Office = cbOffice.SelectedItem as Office
            };

            Db.Context.Crews.Add(crew);
            Db.Context.SaveChanges();
            ManageWindow.LoadCrews();
            MessageBox.Show("Add crew successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }
    }
}
