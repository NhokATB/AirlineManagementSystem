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
    /// Interaction logic for MemberListWindow.xaml
    /// </summary>
    public partial class MemberListWindow : Window
    {
        CrewMember currentCrewMember;

        public Crew Crew { get; internal set; }

        public MemberListWindow()
        {
            InitializeComponent();
            this.Loaded += MemberListWindow_Loaded;
            dgMembers.SelectedCellsChanged += DgMembers_SelectedCellsChanged;

            this.StateChanged += MemberListWindow_StateChanged;
            this.WindowState = WindowState.Maximized;
        }

        private void MemberListWindow_StateChanged(object sender, EventArgs e)
        {
            if(this.WindowState == WindowState.Maximized)
            {
                dgMembers.Height = 420;
            }
            else
            {
                dgMembers.Height = 320; 
            }
        }

        private void DgMembers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentCrewMember = dgMembers.SelectedItem as CrewMember;
            }
            catch (Exception)
            {
            }
        }

        private void MemberListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dgMembers.Height = 420;
            this.Title = this.Title + " " + Crew.CrewName;
            LoadCrewMembers();
            LoadMemberOfPosition();
        }
        private List<Member> CreateMemberList(List<CrewMember> crewMembers)
        {
            List<Member> members = new List<Member>();
            foreach (var item in crewMembers)
            {
                members.Add(new Member()
                {
                    CrewMember = item,
                    FullName = item.FirstName + " " + item.LastName
                });
            }
            return members;
        }

        private void LoadMemberOfPosition()
        {
            var captains = CreateMemberList(Db.Context.CrewMembers.Where(t => t.Position.PositionName == "Captain" && t.CrewId == null).ToList());
            var firstOfficers = CreateMemberList(Db.Context.CrewMembers.Where(t => t.Position.PositionName == "First Officer" && t.CrewId == null).ToList());
            var secondOfficers = CreateMemberList(Db.Context.CrewMembers.Where(t => t.Position.PositionName == "Second Officer" && t.CrewId == null).ToList());
            var pursures = CreateMemberList(Db.Context.CrewMembers.Where(t => t.Position.PositionName == "Purser" && t.CrewId == null).ToList());
            var attendants = CreateMemberList(Db.Context.CrewMembers.Where(t => t.Position.PositionName == "Flight Attendant" && t.CrewId == null).ToList());

            cbCaptain.ItemsSource = captains;
            cbCaptain.DisplayMemberPath = "FullName";
            cbCaptain.SelectedIndex = 0;

            cbFirstOfficer.ItemsSource = firstOfficers;
            cbFirstOfficer.DisplayMemberPath = "FullName";
            cbFirstOfficer.SelectedIndex = 0;

            cbSecondOfficer.ItemsSource = secondOfficers;
            cbSecondOfficer.DisplayMemberPath = "FullName";
            cbSecondOfficer.SelectedIndex = 0;

            cbPurser.ItemsSource = pursures;
            cbPurser.DisplayMemberPath = "FullName";
            cbPurser.SelectedIndex = 0;

            cbAttendant.ItemsSource = attendants;
            cbAttendant.DisplayMemberPath = "FullName";
            cbAttendant.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (currentCrewMember != null)
            {
                if (MessageBox.Show("Do you want to delete this member from this crew?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Crew.CrewMembers.Remove(currentCrewMember);
                    Db.Context.SaveChanges();
                    LoadCrewMembers();
                    LoadMemberOfPosition();
                    currentCrewMember = null;
                }
            }
            else
            {
                MessageBox.Show("Please choose a member!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadCrewMembers()
        {
            dgMembers.ItemsSource = null;
            dgMembers.ItemsSource = Crew.CrewMembers.ToList();
        }

        private void btnAddCaptain_Click(object sender, RoutedEventArgs e)
        {
            if (cbCaptain.SelectedIndex != -1)
            {
                if (Crew.CrewMembers.FirstOrDefault(t => t.Position.PositionName == "Captain") == null)
                {
                    Crew.CrewMembers.Add((cbCaptain.SelectedItem as Member).CrewMember);
                    Db.Context.SaveChanges();
                    LoadCrewMembers();
                    LoadMemberOfPosition();
                }
                else
                {
                    MessageBox.Show("This crew had captain", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Captain not available", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddFirstOfficer_Click(object sender, RoutedEventArgs e)
        {
            if (cbFirstOfficer.SelectedIndex != -1)
            {
                if (Crew.CrewMembers.FirstOrDefault(t => t.Position.PositionName == "First Officer") == null)
                {
                    Crew.CrewMembers.Add((cbFirstOfficer.SelectedItem as Member).CrewMember);
                    Db.Context.SaveChanges();
                    LoadCrewMembers();
                    LoadMemberOfPosition();
                }
                else
                {
                    MessageBox.Show("This crew had First Officer", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("First officer not available", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddSecondOfficer_Click(object sender, RoutedEventArgs e)
        {
            if (cbSecondOfficer.SelectedIndex != -1)
            {
                if (Crew.CrewMembers.FirstOrDefault(t => t.Position.PositionName == "Second Officer") == null)
                {
                    Crew.CrewMembers.Add((cbSecondOfficer.SelectedItem as Member).CrewMember);
                    Db.Context.SaveChanges();
                    LoadCrewMembers();
                    LoadMemberOfPosition();
                }
                else
                {
                    MessageBox.Show("This crew had Second Officer", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Second officer not available", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddPurser_Click(object sender, RoutedEventArgs e)
        {
            if (cbPurser.SelectedIndex != -1)
            {
                if (Crew.CrewMembers.FirstOrDefault(t => t.Position.PositionName == "Purser") == null)
                {
                    Crew.CrewMembers.Add((cbPurser.SelectedItem as Member).CrewMember);
                    Db.Context.SaveChanges();
                    LoadCrewMembers();
                    LoadMemberOfPosition();
                }
                else
                {
                    MessageBox.Show("This crew had Purser", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Puser not available", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddAttendant_Click(object sender, RoutedEventArgs e)
        {
            if (cbAttendant.SelectedIndex != -1)
            {
                if (Crew.CrewMembers.Count(t => t.Position.PositionName == "Flight Attendant") < Crew.NumberOfMembers - 4)
                {
                    Crew.CrewMembers.Add((cbAttendant.SelectedItem as Member).CrewMember);
                    Db.Context.SaveChanges();
                    LoadCrewMembers();
                    LoadMemberOfPosition();
                }
                else
                {
                    MessageBox.Show("This crew had enough Flight attendants", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Attendant not available", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
