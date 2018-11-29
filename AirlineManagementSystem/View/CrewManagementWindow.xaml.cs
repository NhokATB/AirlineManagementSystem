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
    /// Interaction logic for CrewManagementWindow.xaml
    /// </summary>
    public partial class CrewManagementWindow : Window
    {
        List<Office> offices;
        Crew currentCrew;
        public CrewManagementWindow()
        {
            InitializeComponent();
            this.Loaded += CrewManagementWindow_Loaded;
            dgCrews.SelectedCellsChanged += DgCrews_SelectedCellsChanged;
            dgCrews.LoadingRow += DgCrews_LoadingRow;
        }

        private void DgCrews_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var row = e.Row;
            var crew = e.Row.Item as Crew;
            if (crew.CrewMembers.Count() < crew.NumberOfMembers)
            {
                row.Background = new SolidColorBrush(Color.FromRgb(247, 148, 32));
            }
            else
            {
                row.Background = new SolidColorBrush(Colors.White);
            }
        }

        private void DgCrews_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentCrew = dgCrews.SelectedItem as Crew;
            }
            catch (Exception)
            {
            }
        }

        private void CrewManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            offices = Db.Context.Offices.ToList();
            offices.Insert(0, new Office() { Title = "All" });
            cbOffice.ItemsSource = offices;
            cbOffice.DisplayMemberPath = "Title";
            cbOffice.SelectedIndex = 0;

            if (LogonUser.Role.Title == "Manager")
            {
                cbOffice.SelectedItem = LogonUser.Office;
                cbOffice.IsEnabled = false;
            }

            LoadCrews();
        }

        public User LogonUser { get; internal set; }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCrews();
        }

        public void LoadCrews()
        {
            dgCrews.ItemsSource = null;
            var crews = Db.Context.Crews.ToList();
            if (cbOffice.SelectedIndex != 0)
            {
                crews = crews.Where(t => t.Office.Title == offices[cbOffice.SelectedIndex].Title).ToList();
            }
            dgCrews.ItemsSource = crews;
        }

        private void btnEditCrew_Click(object sender, RoutedEventArgs e)
        {
            if (currentCrew != null)
            {
                EditCrewWindow editCrewWindow = new EditCrewWindow
                {
                    Crew = currentCrew,
                    ManageWindow = this
                };
                editCrewWindow.ShowDialog();
                editCrewWindow = null;
            }
            else
            {
                MessageBox.Show("Please choose a crew!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteCrew_Click(object sender, RoutedEventArgs e)
        {
            if (currentCrew != null)
            {
                if (currentCrew.Schedules.Count == 0)
                {
                    if (MessageBox.Show("Do you want to delete this crew?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        currentCrew.CrewMembers.Clear();
                        Db.Context.Crews.Remove(currentCrew);
                        Db.Context.SaveChanges();
                        LoadCrews();
                        currentCrew = null;
                    }
                }
                else
                {
                    MessageBox.Show("This crew can not be deleted because it is related to schedules", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a crew!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMemberList_Click(object sender, RoutedEventArgs e)
        {
            if (currentCrew != null)
            {
                MemberListWindow memberListWindow = new MemberListWindow
                {
                    Crew = currentCrew
                };
                memberListWindow.ShowDialog();
                LoadCrews();
            }
            else
            {
                MessageBox.Show("Please choose a crew!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddCrew_Click(object sender, RoutedEventArgs e)
        {
            AddCrewWindow addCrewWindow = new AddCrewWindow();
            addCrewWindow.LogonUser = LogonUser;
            addCrewWindow.ManageWindow = this;
            addCrewWindow.ShowDialog();
        }
    }
}
