using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirportManagerSystem.Model;
using AirportManagerSystem.HelperClass;

namespace AirportManagerSystem.UserControls
{
    public partial class UcSeatForm : UserControl
    {
        public UcSeatForm()
        {
            InitializeComponent();
        }

        public int CabinId { get; internal set; }
        public Schedule Flight { get; internal set; }
        public string NearSeat { get; internal set; }
        public string Seat { get; internal set; }
        public Ticket Ticket { get; internal set; }

        private void UcSeat_Load(object sender, EventArgs e)
        {
            btnSeat.Text = Seat;
            if (Ticket != null)
            {
                ToolTip t = new ToolTip();
                t.SetToolTip(btnSeat, $"{Ticket.ID}\n{Ticket.Firstname} {Ticket.Lastname}\n{Ticket.PassportNumber}");

                btnSeat.BackColor = MyColor.CheckedIn;
            }
            else
            {
                btnSeat.BackColor = MyColor.Empty;
            }

            if (CabinId == 3)
                BackColor = MyColor.First;
            else if (CabinId == 2)
                BackColor = MyColor.Business;
        }
    }
}

