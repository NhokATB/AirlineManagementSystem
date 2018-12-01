using AirportManagerSystem.HelperClass;
using AirportManagerSystem.Model;
using AirportManagerSystem.UserControls;
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
    /// Interaction logic for TestSeatWindow.xaml
    /// </summary>
    public partial class SeatModelWindow : Window
    {
        UcSeat selectedSeat;
        string directionOfSelectedSeat = "";
        public List<Ticket> Tickets { get; internal set; }

        public SeatModelWindow()
        {
            InitializeComponent();
            this.Loaded += SeatModelWindow_Loaded;

            dpnCheckedInSeat.Background = new SolidColorBrush(AMONICColor.CheckedIn);
            dpnEmptySeat.Background = new SolidColorBrush(AMONICColor.Empty);
            dpnSelectedSeat.Background = new SolidColorBrush(AMONICColor.Selected);
            dpnDualEmptySeat.Background = new SolidColorBrush(AMONICColor.Dual);
        }

        private void SeatModelWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = $"Seat model for flight: {Tickets[0].Schedule.FlightNumber} - {Tickets[0].Schedule.Date.ToString("dd/MM/yyyy")} - {Tickets[0].Schedule.Time.ToString(@"hh\:mm")} - {Tickets[0].Schedule.Route.Airport.IATACode} to {Tickets[0].Schedule.Route.Airport1.IATACode}";

            LoadSeat();

            if (Tickets.Count == 2)
            {
                ShowDualSeat();
            }
            else
            {
                stpNote.Children.Remove(tblDualEmptySeat);
                stpNote.Children.Remove(dpnDualEmptySeat);
            }
        }

        private void ShowDualSeat()
        {
            var controls = wpSeats.Children;

            for (int i = 0; i < controls.Count - 1; i++)
            {
                var uc1 = (UcSeat)controls[i];
                var uc2 = (UcSeat)controls[i + 1];

                uc1.btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);

                if (uc1.CabinId == 2)
                {
                    if (uc1.Ticket == null && uc2.Ticket == null && (uc1.Seat.Contains("C") || uc1.Seat.Contains("A")))
                    {
                        uc1.btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);
                        uc2.btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);

                        uc1.IsDual = true;
                        uc2.IsDual = true;
                    }
                }
                else if (uc1.CabinId == 1)
                {
                    if (uc1.Ticket == null && uc2.Ticket == null && (uc1.Seat.Contains("A") || uc1.Seat.Contains("B") || uc1.Seat.Contains("D") || uc1.Seat.Contains("E")))
                    {
                        uc1.btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);
                        uc2.btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);

                        uc1.IsDual = true;
                        uc2.IsDual = true;
                    }
                }

                if (uc1.Ticket != null) uc1.btnSeat.Background = new SolidColorBrush(AMONICColor.CheckedIn);
                if (uc2.Ticket != null) uc2.btnSeat.Background = new SolidColorBrush(AMONICColor.CheckedIn);
            }
        }

        private void LoadSeat()
        {
            wpSeats.Children.Clear();

            var flight = Tickets[0].Schedule;

            var numE = flight.Aircraft.EconomySeats;
            var numB = flight.Aircraft.BusinessSeats;
            var numF = flight.Aircraft.TotalSeats - numE - numB;

            var dayF = numF / 2;
            var dayB = numB / 4;
            var dayE = numE / 6;

            AddSeat(flight, 1, dayF, 3);
            AddSeat(flight, dayF + 1, dayF + dayB, 2);
            AddSeat(flight, dayF + dayB + 1, dayF + dayB + dayE, 1);
        }
        private void AddSeat(Schedule flight, int from, int to, int cabinId)
        {
            string seatName1 = "AB";
            string seatName2 = "ABCD";
            string seatName3 = "ABCDEF";
            var seatName = cabinId == 3 ? seatName1 : (cabinId == 2 ? seatName2 : seatName3);

            UcSeat previous = new UcSeat();

            for (int i = from; i <= to; i++)
            {
                foreach (var item in seatName)
                {
                    UcSeat uc = new UcSeat();
                    uc.Seat = i + item.ToString();
                    uc.Ticket = flight.Tickets.Where(t => t.Seat == uc.Seat).FirstOrDefault();
                    uc.Flight = flight;
                    uc.CabinId = cabinId;

                    if (cabinId == 2)
                    {
                        if (uc.Seat.Contains("A")) { previous = uc; }
                        else if (uc.Seat.Contains("B")) { uc.Previous = previous; previous.After = uc; }
                        else if (uc.Seat.Contains("C")) { previous = uc; }
                        else if (uc.Seat.Contains("D")) { uc.Previous = previous; previous.After = uc; }
                    }
                    else if (cabinId == 1)
                    {
                        if (uc.Seat.Contains("A")) { previous = uc; }
                        else if (uc.Seat.Contains("B")) { uc.Previous = previous; previous.After = uc; previous = uc; }
                        else if (uc.Seat.Contains("C")) { uc.Previous = previous; previous.After = uc; }
                        else if (uc.Seat.Contains("D")) { previous = uc; }
                        else if (uc.Seat.Contains("E")) { uc.Previous = previous; previous.After = uc; previous = uc; }
                        else if (uc.Seat.Contains("F")) { uc.Previous = previous; previous.After = uc; }
                    }

                    if (cabinId == 3)
                    {
                        if (uc.Seat.Contains("A")) uc.Width = 175;
                    }
                    else if (cabinId == 2)
                    {
                        if (uc.Seat.Contains("B")) uc.Width = 175;
                    }
                    else if (uc.Seat.Contains("C")) uc.Width = 175;

                    uc.btnSeat.Click += BtnSeat_Click;

                    wpSeats.Children.Add(uc);
                }
            }
        }
        private void BtnSeat_Click(object sender, EventArgs e)
        {
            var ucSeat = (sender as Button).Parent as UcSeat;

            if (ucSeat.Ticket == null)
            {
                if (Tickets.Count == 1)
                {
                    if (Tickets[0].CabinTypeID != ucSeat.CabinId)
                    {
                        MessageBox.Show("Your ticket is not in this cabin", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        ResetColorBeforeChooseDualSeat();

                        selectedSeat = ucSeat;
                        ucSeat.btnSeat.Background = new SolidColorBrush(AMONICColor.Selected);
                    }
                }
                else
                {
                    if (((SolidColorBrush)ucSeat.btnSeat.Background).Color != AMONICColor.Selected)
                    {
                        if (((SolidColorBrush)ucSeat.btnSeat.Background).Color == AMONICColor.Dual)
                        {
                            if (Tickets[0].CabinTypeID != ucSeat.CabinId)
                            {
                                MessageBox.Show("Your tickets is not in this cabin", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                ResetColorBeforeChooseDualSeat();

                                selectedSeat = ucSeat;
                                ucSeat.btnSeat.Background = new SolidColorBrush(AMONICColor.Selected);
                                if (ucSeat.Previous != null && ucSeat.Previous.Ticket == null)
                                {
                                    ucSeat.Previous.btnSeat.Background = new SolidColorBrush(AMONICColor.Selected);
                                    directionOfSelectedSeat = "left";
                                }
                                else
                                {
                                    ucSeat.After.btnSeat.Background = new SolidColorBrush(AMONICColor.Selected);
                                    directionOfSelectedSeat = "right";
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("This seat isn't dual seat", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This seat was checked in", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetColorBeforeChooseDualSeat()
        {
            try
            {
                if (Tickets.Count == 1)
                {
                    selectedSeat.btnSeat.Background = new SolidColorBrush(AMONICColor.Empty);
                }
                else
                {
                    selectedSeat.btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);
                    if (directionOfSelectedSeat == "left")
                    {
                        selectedSeat.Previous.btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);
                    }
                    else if (directionOfSelectedSeat == "right")
                    {
                        selectedSeat.After.btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSeat != null)
            {
                if (Tickets.Count == 1)
                {
                    Tickets[0].Seat = selectedSeat.Seat;
                    Db.Context.SaveChanges();
                    MessageBox.Show("Check in successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Tickets[0].Seat = selectedSeat.Seat;
                    selectedSeat.btnSeat.Background = new SolidColorBrush(AMONICColor.CheckedIn);

                    if (directionOfSelectedSeat == "left")
                    {
                        Tickets[1].Seat = selectedSeat.Previous.Seat;
                        selectedSeat.Previous.btnSeat.Background = new SolidColorBrush(AMONICColor.CheckedIn);
                    }
                    else if (directionOfSelectedSeat == "right")
                    {
                        Tickets[1].Seat = selectedSeat.After.Seat;
                        selectedSeat.After.btnSeat.Background = new SolidColorBrush(AMONICColor.CheckedIn);
                    }

                    Db.Context.SaveChanges();
                    MessageBox.Show("Check in successful", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                btnReCheckIn.IsEnabled = true;
                btnOk.IsEnabled = false;
            }
        }

        private void btnReCheckIn_Click(object sender, RoutedEventArgs e)
        {
            if (Tickets.Count == 1)
            {
                Tickets[0].Seat = null;
                Db.Context.SaveChanges();
                selectedSeat.btnSeat.Background = new SolidColorBrush(AMONICColor.Empty);
            }
            else
            {
                Tickets[0].Seat = null;
                Tickets[1].Seat = null;
                Db.Context.SaveChanges();

                selectedSeat.btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);

                if (directionOfSelectedSeat == "left")
                {
                    selectedSeat.Previous.btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);
                }
                else if (directionOfSelectedSeat == "right")
                {
                    selectedSeat.After.btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);
                }
            }

            btnReCheckIn.IsEnabled = false;
            btnOk.IsEnabled = true;
        }
    }
}
