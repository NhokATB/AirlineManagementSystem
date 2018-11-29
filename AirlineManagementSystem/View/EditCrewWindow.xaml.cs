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
    /// Interaction logic for EditCrewWindow.xaml
    /// </summary>
    public partial class EditCrewWindow : Window
    {
        public CrewManagementWindow ManageWindow { get; internal set; }
        public Crew Crew { get; internal set; }
        public EditCrewWindow()
        {
            InitializeComponent();
            this.Loaded += EditCrewWindow_Loaded;
        }

        private void EditCrewWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var offices = Db.Context.Offices.ToList();
            cbOffice.ItemsSource = offices;
            cbOffice.DisplayMemberPath = "Title";

            txtCrewName.Text = Crew.CrewName;
            txtNumberOfMembers.Text = Crew.NumberOfMembers.ToString();
            cbOffice.SelectedItem = Crew.Office;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtCrewName.Text.Trim() == "")
            {
                MessageBox.Show("Crew name was required!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtCrewName.Text.Trim() != Crew.CrewName)
            {
                if (Db.Context.Crews.ToList().FirstOrDefault(t => t.CrewName == txtCrewName.Text) != null)
                {
                    MessageBox.Show("This crew name was used", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (txtNumberOfMembers.Text.Trim() == "")
            {
                MessageBox.Show("Number of member was required", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var number = int.Parse(txtNumberOfMembers.Text.Trim());
                if (number < Crew.CrewMembers.Count())
                {
                    MessageBox.Show("The number of member must be greater than or equal to the current membes", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Number of members must be integer", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Crew.CrewName = txtCrewName.Text.Trim();
            Crew.NumberOfMembers = int.Parse(txtNumberOfMembers.Text.Trim());
            Crew.Office = cbOffice.SelectedItem as Office;

            Db.Context.SaveChanges();
            ManageWindow.LoadCrews();
            MessageBox.Show("Edit crew successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
