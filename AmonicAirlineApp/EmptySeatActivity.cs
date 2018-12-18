using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AmonicAirlineApp
{
    [Activity(Label = "EmptySeat")]
    public class EmptySeatActivity : Activity
    {
        Button btnEmptySeat;
        Button btnRevenue;
        Button btnApply;
        TextView tvDate;
        ListView lvEmptySeat;
        List<SeatReport> seatReports;
        Button btnBack;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EmptySeat);

            btnApply = this.FindViewById<Button>(Resource.Id.btnApplyEmptySeat);
            btnEmptySeat = this.FindViewById<Button>(Resource.Id.btnEmptySeat);
            btnRevenue = this.FindViewById<Button>(Resource.Id.btnRevenueFromTickets);
            tvDate = this.FindViewById<TextView>(Resource.Id.tvDateForEmptySeat);
            lvEmptySeat = this.FindViewById<ListView>(Resource.Id.lvEmptySeat);
            btnBack = this.FindViewById<Button>(Resource.Id.btnBack);

            btnApply.Click += BtnApply_Click;
            btnEmptySeat.Click += BtnEmptySeat_Click;
            btnRevenue.Click += BtnRevenue_Click;
            tvDate.Click += TvDate_Click;
            btnBack.Click += BtnBack_Click;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void TvDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                tvDate.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void BtnRevenue_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ManagerActivity));
            StartActivity(intent);
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
        private void BtnEmptySeat_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();

            if (tvDate.Text == "Select Date")
            {
                alert.SetTitle("Message");
                alert.SetMessage("Please choose date before apply");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
                return;
            }

            if (DateTime.Parse(tvDate.Text).Date >= new DateTime(2019, 4, 1))
            {
                alert.SetTitle("Message");
                alert.SetMessage("No data");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
                return;
            }

            LoadEmptySeat();
        }

        private void LoadEmptySeat()
        {
            var date = DateTime.Parse(tvDate.Text);

            if(date.DayOfYear % 4 == 0)
            {
                seatReports = new List<SeatReport>()
                {
                    new SeatReport (){ From = "AUH", To = "BAH", Time = new TimeSpan(17,0,0), EconomySeat = 160, EconomyTicket = 45, BusinessSeat = 20, BusinessTicket = 12, FirstClassSeat = 8, FirstClassTicket = 4},
                    new SeatReport (){ From = "RUH", To = "AUH", Time = new TimeSpan(21,0,0), EconomySeat = 154, EconomyTicket = 123, BusinessSeat = 16, BusinessTicket = 10, FirstClassSeat = 8, FirstClassTicket = 7},
                    new SeatReport (){ From = "ADE", To = "CAI", Time = new TimeSpan(8,0,0), EconomySeat = 154, EconomyTicket = 105, BusinessSeat = 16, BusinessTicket = 7, FirstClassSeat = 8, FirstClassTicket = 2},
                    new SeatReport (){ From = "AUH", To = "CAI", Time = new TimeSpan(15,0,0), EconomySeat = 160, EconomyTicket = 55, BusinessSeat = 20, BusinessTicket = 5, FirstClassSeat = 8, FirstClassTicket = 3},
                    new SeatReport (){ From = "CAI", To = "BAH", Time = new TimeSpan(22,0,0), EconomySeat = 160, EconomyTicket = 142, BusinessSeat = 16, BusinessTicket = 12, FirstClassSeat = 6, FirstClassTicket = 4},
                    new SeatReport (){ From = "BAH", To = "RUH", Time = new TimeSpan(6,0,0), EconomySeat = 160, EconomyTicket = 66, BusinessSeat = 20, BusinessTicket = 15, FirstClassSeat = 6, FirstClassTicket = 5},
                    new SeatReport (){ From = "AUH", To = "ADE", Time = new TimeSpan(10,0,0), EconomySeat = 154, EconomyTicket = 43, BusinessSeat = 20, BusinessTicket = 12, FirstClassSeat = 8, FirstClassTicket = 6},
                };
            }
            else if (date.DayOfYear % 2 == 0)
            {
                seatReports = new List<SeatReport>()
                {
                    new SeatReport (){ From = "RUH", To = "AUH", Time = new TimeSpan(8,0,0), EconomySeat = 154, EconomyTicket = 123, BusinessSeat = 16, BusinessTicket = 10, FirstClassSeat = 8, FirstClassTicket = 7},
                    new SeatReport (){ From = "AUH", To = "BAH", Time = new TimeSpan(17,0,0), EconomySeat = 160, EconomyTicket = 45, BusinessSeat = 20, BusinessTicket = 12, FirstClassSeat = 8, FirstClassTicket = 4},
                    new SeatReport (){ From = "AUH", To = "CAI", Time = new TimeSpan(15,0,0), EconomySeat = 160, EconomyTicket = 55, BusinessSeat = 20, BusinessTicket = 5, FirstClassSeat = 8, FirstClassTicket = 3},
                    new SeatReport (){ From = "ADE", To = "CAI", Time = new TimeSpan(8,0,0), EconomySeat = 154, EconomyTicket = 105, BusinessSeat = 16, BusinessTicket = 7, FirstClassSeat = 8, FirstClassTicket = 2},
                    new SeatReport (){ From = "BAH", To = "RUH", Time = new TimeSpan(6,0,0), EconomySeat = 160, EconomyTicket = 66, BusinessSeat = 20, BusinessTicket = 15, FirstClassSeat = 6, FirstClassTicket = 5},
                    new SeatReport (){ From = "ADE", To = "BAH", Time = new TimeSpan(12,0,0), EconomySeat = 160, EconomyTicket = 55, BusinessSeat = 20, BusinessTicket = 5, FirstClassSeat = 8, FirstClassTicket = 3},
                    new SeatReport (){ From = "CAI", To = "BAH", Time = new TimeSpan(17,0,0), EconomySeat = 160, EconomyTicket = 142, BusinessSeat = 16, BusinessTicket = 12, FirstClassSeat = 6, FirstClassTicket = 4},
                    new SeatReport (){ From = "AUH", To = "ADE", Time = new TimeSpan(10,0,0), EconomySeat = 154, EconomyTicket = 43, BusinessSeat = 20, BusinessTicket = 12, FirstClassSeat = 8, FirstClassTicket = 6},
                    new SeatReport (){ From = "AUH", To = "RUH", Time = new TimeSpan(15,0,0), EconomySeat = 160, EconomyTicket = 55, BusinessSeat = 20, BusinessTicket = 5, FirstClassSeat = 8, FirstClassTicket = 3},
                };
            }
            else if (date.DayOfYear % 3 == 0)
            {
                seatReports = new List<SeatReport>()
                {
                    new SeatReport (){ From = "AUH", To = "CAI", Time = new TimeSpan(15,0,0), EconomySeat = 160, EconomyTicket = 55, BusinessSeat = 20, BusinessTicket = 5, FirstClassSeat = 8, FirstClassTicket = 3},
                    new SeatReport (){ From = "AUH", To = "BAH", Time = new TimeSpan(17,0,0), EconomySeat = 160, EconomyTicket = 45, BusinessSeat = 20, BusinessTicket = 12, FirstClassSeat = 8, FirstClassTicket = 4},
                    new SeatReport (){ From = "CAI", To = "BAH", Time = new TimeSpan(21,0,0), EconomySeat = 160, EconomyTicket = 142, BusinessSeat = 16, BusinessTicket = 12, FirstClassSeat = 6, FirstClassTicket = 4},
                    new SeatReport (){ From = "RUH", To = "AUH", Time = new TimeSpan(21,0,0), EconomySeat = 154, EconomyTicket = 123, BusinessSeat = 16, BusinessTicket = 10, FirstClassSeat = 8, FirstClassTicket = 7},
                    new SeatReport (){ From = "AUH", To = "ADE", Time = new TimeSpan(10,0,0), EconomySeat = 154, EconomyTicket = 43, BusinessSeat = 20, BusinessTicket = 12, FirstClassSeat = 8, FirstClassTicket = 6},
                    new SeatReport (){ From = "BAH", To = "RUH", Time = new TimeSpan(6,0,0), EconomySeat = 160, EconomyTicket = 66, BusinessSeat = 20, BusinessTicket = 15, FirstClassSeat = 6, FirstClassTicket = 5},
                    new SeatReport (){ From = "ADE", To = "CAI", Time = new TimeSpan(6,0,0), EconomySeat = 154, EconomyTicket = 105, BusinessSeat = 16, BusinessTicket = 7, FirstClassSeat = 8, FirstClassTicket = 2},
                    new SeatReport (){ From = "CAI", To = "AUH", Time = new TimeSpan(17,0,0), EconomySeat = 160, EconomyTicket = 142, BusinessSeat = 16, BusinessTicket = 12, FirstClassSeat = 6, FirstClassTicket = 4},
                };
            }
            else 
            {
                seatReports = new List<SeatReport>()
                {
                    new SeatReport (){ From = "AUH", To = "RUH", Time = new TimeSpan(10,0,0), EconomySeat = 160, EconomyTicket = 55, BusinessSeat = 20, BusinessTicket = 5, FirstClassSeat = 8, FirstClassTicket = 3},
                    new SeatReport (){ From = "ADE", To = "CAI", Time = new TimeSpan(8,0,0), EconomySeat = 154, EconomyTicket = 105, BusinessSeat = 16, BusinessTicket = 7, FirstClassSeat = 8, FirstClassTicket = 2},
                    new SeatReport (){ From = "CAI", To = "BAH", Time = new TimeSpan(22,0,0), EconomySeat = 160, EconomyTicket = 142, BusinessSeat = 16, BusinessTicket = 12, FirstClassSeat = 6, FirstClassTicket = 4},
                    new SeatReport (){ From = "AUH", To = "CAI", Time = new TimeSpan(21,0,0), EconomySeat = 160, EconomyTicket = 55, BusinessSeat = 20, BusinessTicket = 5, FirstClassSeat = 8, FirstClassTicket = 3},
                    new SeatReport (){ From = "RUH", To = "AUH", Time = new TimeSpan(21,0,0), EconomySeat = 154, EconomyTicket = 123, BusinessSeat = 16, BusinessTicket = 10, FirstClassSeat = 8, FirstClassTicket = 7},
                    new SeatReport (){ From = "AUH", To = "BAH", Time = new TimeSpan(17,0,0), EconomySeat = 160, EconomyTicket = 45, BusinessSeat = 20, BusinessTicket = 12, FirstClassSeat = 8, FirstClassTicket = 4},
                    new SeatReport (){ From = "AUH", To = "ADE", Time = new TimeSpan(10,0,0), EconomySeat = 154, EconomyTicket = 43, BusinessSeat = 20, BusinessTicket = 12, FirstClassSeat = 8, FirstClassTicket = 6},
                    new SeatReport (){ From = "BAH", To = "RUH", Time = new TimeSpan(6,0,0), EconomySeat = 160, EconomyTicket = 66, BusinessSeat = 20, BusinessTicket = 15, FirstClassSeat = 6, FirstClassTicket = 5},
                };
            }

            lvEmptySeat.Adapter = new SeatReportAdapter(this, seatReports);
        }
    }
}