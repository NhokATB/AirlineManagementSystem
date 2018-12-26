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
    /// Interaction logic for MemberManagementWindow.xaml
    /// </summary>
    public partial class MemberManagementWindow : Window
    {
        CrewMember currentMember;

        public User LogonUser { get; internal set; }

        public MemberManagementWindow()
        {
            InitializeComponent();
            this.Loaded += MemberManagementWindow_Loaded;
            dgMembers.SelectedCellsChanged += DgMembers_SelectedCellsChanged;

            this.StateChanged += MemberManagementWindow_StateChanged;

            this.WindowState = WindowState.Maximized;
        }

        private void MemberManagementWindow_StateChanged(object sender, EventArgs e)
        {
            if(this.WindowState == WindowState.Maximized)
            {
                dgMembers.Height = 550;
            }
            else
            {
                dgMembers.Height = 450;
            }
        }

        private void DgMembers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                currentMember = dgMembers.SelectedItem as CrewMember;
            }
            catch (Exception)
            {
            }
        }

        private void MemberManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dgMembers.Height = 550;

            var genders = new List<string>() { "All", "Male", "Female" };
            var crews = Db.Context.Crews.ToList();
            var positions = Db.Context.Positions.ToList();

            crews.Insert(0, new Crew() { CrewName = "All" });
            positions.Insert(0, new Position() { PositionName = "All" });

            cbCrews.ItemsSource = crews;
            cbCrews.DisplayMemberPath = "CrewName";
            cbCrews.SelectedIndex = 0;

            cbPosition.ItemsSource = positions;
            cbPosition.DisplayMemberPath = "PositionName";
            cbPosition.SelectedIndex = 0;

            cbGender.ItemsSource = genders;
            cbGender.SelectedIndex = 0;

            LoadMembers();
        }

        public void LoadMembers()
        {
            dgMembers.ItemsSource = null;

            var members = Db.Context.CrewMembers.ToList();

            if (cbCrews.SelectedIndex != 0)
                members = members.Where(t => t.CrewId == (cbCrews.SelectedItem as Crew).CrewId).ToList();
            if (cbPosition.SelectedIndex != 0)
                members = members.Where(t => t.PositionId == (cbPosition.SelectedItem as Position).PositionId).ToList();
            if (cbGender.SelectedIndex != 0)
                members = members.Where(t => t.Gender == cbGender.Text).ToList();

            if (txtPhone.Text.Trim() != "")
                members = members.Where(t => t.Phone.Contains(txtPhone.Text)).ToList();

            dgMembers.ItemsSource = members;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadMembers();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            LoadMembers();
        }

        private void btnEditMember_Click(object sender, RoutedEventArgs e)
        {
            if (currentMember != null)
            {
                EditMemberWindow editMemberWindow = new EditMemberWindow();
                editMemberWindow.LogonUser = LogonUser;
                editMemberWindow.Member = currentMember;
                editMemberWindow.ManageWindow = this;
                editMemberWindow.ShowDialog();
                currentMember = null;
            }
            else
            {
                MessageBox.Show("Please choose a member!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (currentMember != null)
            {
                if (currentMember.Crew == null)
                {
                    if (MessageBox.Show("Do you want to delete this member?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        Db.Context.CrewMembers.Remove(currentMember);
                        Db.Context.SaveChanges();
                        LoadMembers();
                        currentMember = null;
                    }
                }
                else
                {
                    MessageBox.Show("This member can not be deleted because it is a member of a crew!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a member!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddMember_Click(object sender, RoutedEventArgs e)
        {
            AddMemberWindow addMemberWindow = new AddMemberWindow();
            addMemberWindow.LogonUser = LogonUser;
            addMemberWindow.ManageWindow = this;
            addMemberWindow.ShowDialog();
        }
    }
}
