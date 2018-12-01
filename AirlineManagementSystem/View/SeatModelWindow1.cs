using AirportManagerSystem.HelperClass;
using AirportManagerSystem.Model;
using AirportManagerSystem.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirportManagerSystem.View
{
    public partial class SeatModelWindow1 : Form
    {
        List<Schedule> schedules;
        Ticket updatedTicket;
        bool isChangeSeat;

        public SeatModelWindow1()
        {
            InitializeComponent();

            pictureBox1.BackColor = MyColor.CheckedIn;
            pictureBox2.BackColor = MyColor.Empty;
            pictureBox3.BackColor = MyColor.Dual;

            foreach (var item in Db.Context.Tickets)
            {
                item.Seat = null;
            }
            Db.Context.SaveChanges();
        }
     
        private void FrmCheckInSystem_Load(object sender, EventArgs e)
        {
            ResetAll();

            schedules = Db.Context.Schedules.ToList();
            foreach (var item in schedules)
            {
                comboBox1.Items.Add($"{item.Date.ToString("dd/MM/yyyy")}, {item.Time.ToString(@"hh\:mm")}, {item.Route.Airport.IATACode} - {item.Route.Airport1.IATACode}");
            }
            comboBox1.SelectedIndex = 0;
        }

        private void ResetAll()
        {
            chart1.Visible = false;
            flowLayoutPanel1.Controls.Clear();
            label12.Text = label13.Text = label14.Text = label15.Text = label16.Text = label17.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetAll();
            LoadSeat();
        }

        private void LoadSeat()
        {
            flowLayoutPanel1.Controls.Clear();
            var flight = schedules[comboBox1.SelectedIndex];

            var numE = flight.Aircraft.EconomySeats;
            var numB = flight.Aircraft.BusinessSeats;
            var numF = flight.Aircraft.TotalSeats - numE - numB;

            var dayF = (numF + (4 - (numF % 4))) / 4;
            var dayB = (numB + (4 - (numB % 4))) / 4;
            var dayE = (numE + (4 - (numE % 4))) / 4;

            AddSeat(flight, 1, dayF, 3, numF % 4);
            AddSeat(flight, dayF + 1, dayF + dayB, 2, numB % 4);
            AddSeat(flight, dayF + dayB + 1, dayF + dayB + dayE, 1, numE % 4);

            SearhPairseat();
            EmptySeat();
            DualSeat();
            LoadChart();
        }
        private void AddSeat(Schedule flight, int from, int to, int cabinId, int soDu)
        {
            string str = "ABCD";

            for (int i = from; i <= to; i++)
            {
                foreach (var item in str)
                {
                    UcSeatForm uc = new UcSeatForm();
                    uc.Seat = i + item.ToString();
                    uc.Ticket = flight.Tickets.Where(t => t.Seat == uc.Seat).FirstOrDefault();
                    uc.Flight = flight;
                    uc.CabinId = cabinId;

                    var near = ' ';
                    if (uc.Seat.Contains("A")) near = 'B';
                    else if (uc.Seat.Contains("B")) near = 'A';
                    else if (uc.Seat.Contains("C")) near = 'D';
                    else if (uc.Seat.Contains("D")) near = 'C';

                    uc.NearSeat = i + near.ToString();

                    if (uc.Seat.Contains("B")) uc.Width = 175;

                    uc.btnSeat.Click += Button1_Click;

                    flowLayoutPanel1.Controls.Add(uc);

                    if (soDu == 1 && i == from)
                    {
                        if (uc.Seat.Contains("B") == false)
                        {
                            uc.Ticket = new Ticket();
                            uc.btnSeat.Visible = false;
                        }
                    }
                    else if (soDu == 2 && i == from)
                    {
                        if (uc.Seat.Contains("B") == false && uc.Seat.Contains("C") == false)
                        {
                            uc.Ticket = new Ticket();
                            uc.btnSeat.Visible = false;
                        }
                    }
                    else if (soDu == 3 && i == from)
                    {
                        if (uc.Seat.Contains("D"))
                        {
                            uc.Ticket = new Ticket();
                            uc.btnSeat.Visible = false;
                        }
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var uc = (sender as Button).Parent as UcSeatForm;

            if (radioButton3.Checked)
            {
                if (isChangeSeat == false)
                {
                    if (uc.btnSeat.BackColor == MyColor.Empty)
                    {
                        MessageBox.Show("This seat wasn't checked in");
                        return;
                    }

                    isChangeSeat = true;
                    updatedTicket = uc.Ticket;
                    uc.btnSeat.BackColor = MyColor.Chaning;
                }
                else
                {
                    if (uc.btnSeat.BackColor == MyColor.Chaning)
                    {
                        uc.btnSeat.BackColor = MyColor.CheckedIn;
                        isChangeSeat = false;
                        updatedTicket = null;
                        return;
                    }
                    else
                    {
                        if (uc.CabinId != updatedTicket.CabinTypeID)
                        {
                            MessageBox.Show($"Seat {updatedTicket.Seat} and {uc.Seat} are not the same cabin");
                            return;
                        }

                        if (uc.btnSeat.BackColor == MyColor.CheckedIn)
                        {
                            MessageBox.Show("This seat was checked in");
                            return;
                        }
                    }

                    updatedTicket.Seat = uc.Seat;
                    isChangeSeat = false;
                    updatedTicket = null;
                    Db.Context.SaveChanges();
                    LoadSeat();
                }
            }
            else
            {
                var singleCheckin = false;
                if (radioButton1.Checked) singleCheckin = true;

                if (uc.btnSeat.BackColor == MyColor.CheckedIn)
                {
                    MessageBox.Show("This seat was checked in");
                    return;
                }

                if (singleCheckin == false)
                {
                    if (uc.btnSeat.BackColor == MyColor.Empty)
                    {
                        MessageBox.Show("Please choose in dual seat");
                        return;
                    }
                }

                //FrmCheckIn f = new FrmCheckIn();
                //f.Uc = uc;
                //f.Flight = uc.Flight;
                //f.SingleCheck = singleCheckin;

                //f.ShowDialog();
                //if (f.IsConfirmed)
                //    LoadSeat();
            }
        }

        private void SearhPairseat()
        {
            updatedTicket = null;
            isChangeSeat = false;
            var controls = flowLayoutPanel1.Controls;

            if (radioButton2.Checked)
            {
                label3.Visible = true;
                pictureBox3.Visible = true;

                for (int i = 0; i < controls.Count - 1; i++)
                {
                    var uc1 = (UcSeatForm)controls[i];
                    var uc2 = (UcSeatForm)controls[i + 1];

                    if (uc1.Ticket == null && uc2.Ticket == null && (uc1.Seat.Contains("C") || uc1.Seat.Contains("A")))
                    {
                        uc1.btnSeat.BackColor = MyColor.Dual;
                        uc2.btnSeat.BackColor = MyColor.Dual;
                    }

                    if (uc1.Ticket != null) uc1.btnSeat.BackColor = MyColor.CheckedIn;
                    if (uc2.Ticket != null) uc2.btnSeat.BackColor = MyColor.CheckedIn;
                }
            }
            else
            {
                label3.Visible = false;
                pictureBox3.Visible = false;

                foreach (UcSeatForm item in controls)
                {
                    if (item.Ticket != null)
                    {
                        item.btnSeat.BackColor = MyColor.CheckedIn;
                    }
                    else item.btnSeat.BackColor = MyColor.Empty;
                }
            }
        }

        private void EmptySeat()
        {
            var flight = schedules[comboBox1.SelectedIndex];

            var eSeat = flight.Aircraft.EconomySeats;
            var bSeat = flight.Aircraft.BusinessSeats;
            var fSeat = flight.Aircraft.TotalSeats - eSeat - bSeat;

            var numE = flight.Tickets.Where(t => t.Seat != null && t.CabinTypeID == 1).Count();
            var numB = flight.Tickets.Where(t => t.Seat != null && t.CabinTypeID == 2).Count();
            var numF = flight.Tickets.Where(t => t.Seat != null && t.CabinTypeID == 3).Count();

            label12.Text = (fSeat - numF).ToString();
            label13.Text = (bSeat - numB).ToString();
            label14.Text = (eSeat - numE).ToString();
        }

        private void DualSeat()
        {
            var flight = schedules[comboBox1.SelectedIndex];

            var dualE = 0;
            var dualB = 0;
            var dualF = 0;

            var controls = flowLayoutPanel1.Controls;

            for (int i = 0; i < controls.Count - 1; i++)
            {
                var uc1 = (UcSeatForm)controls[i];
                var uc2 = (UcSeatForm)controls[i + 1];

                if (uc1.Ticket == null && uc2.Ticket == null && (uc1.Seat.Contains("C") || uc1.Seat.Contains("A")))
                {
                    if (uc1.CabinId == 1) dualE++;
                    else if (uc1.CabinId == 2) dualB++;
                    else dualF++;
                }
            }

            label17.Text = dualF.ToString();
            label16.Text = dualB.ToString();
            label15.Text = dualE.ToString();
        }

        private void LoadChart()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            var flight = schedules[comboBox1.SelectedIndex];

            var left = flight.Tickets.ToList().Where(t => t.Seat != null && (t.Seat.Contains("A") || t.Seat.Contains("B"))).Count();
            var right = flight.Tickets.ToList().Where(t => t.Seat != null && (t.Seat.Contains("D") || t.Seat.Contains("C"))).Count();

            chart1.Series[0].Points.AddXY("Left", left);
            chart1.Series[0].Points[0].Label = left.ToString();
            chart1.Series[1].Points.AddXY("Right", right);
            chart1.Series[1].Points[0].Label = right.ToString();

            chart1.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SearhPairseat();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SearhPairseat();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SearhPairseat();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetAll();
        }
    }
}

