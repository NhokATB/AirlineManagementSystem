using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace AmonicAirlineApp
{
    [Activity(Label = "ManagerActivity")]
    public class ManagerActivity : Activity
    {
        private Button btnRevenueFromTickets;
        private Button btnEmptySeat;
        private Button btnApply;
        private TextView tvDate;
        private ListView lvRevenues;
        private List<Revenue> revenues;
        private Button btnBack;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Manager);

            btnRevenueFromTickets = this.FindViewById<Button>(Resource.Id.btnRevenueFromTickets);
            btnEmptySeat = this.FindViewById<Button>(Resource.Id.btnEmptySeat);
            tvDate = this.FindViewById<TextView>(Resource.Id.tvDateRevenueTicket);
            btnApply = this.FindViewById<Button>(Resource.Id.btnApplyRevenueTicket);
            lvRevenues = this.FindViewById<ListView>(Resource.Id.lvRevenues);
            btnBack = this.FindViewById<Button>(Resource.Id.btnBack);

            btnRevenueFromTickets.Click += BtnRevenueFromTickets_Click;
            btnEmptySeat.Click += BtnEmptySeat_Click;
            btnApply.Click += BtnApply_Click;

            tvDate.Click += TvDate_Click;
            btnBack.Click += BtnBack_Click;
        }
        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
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

            LoadRevenue();
        }

        private void LoadRevenue()
        {
            var date = DateTime.Parse(tvDate.Text);
            if (date.DayOfYear % 4 == 0)
            {
                revenues = new List<Revenue>()
                {
                    new Revenue (){ ObjectName= "Economy Class", Value = 522782 },
                    new Revenue (){ ObjectName= "Business Class", Value = 223528 },
                    new Revenue (){ ObjectName= "First Class", Value = 327492 },
                };
            }
            else if (date.DayOfYear % 2 == 0)
            {
                revenues = new List<Revenue>()
                {
                    new Revenue (){ ObjectName= "Economy Class", Value = 452282 },
                    new Revenue (){ ObjectName= "Business Class", Value = 203725 },
                    new Revenue (){ ObjectName= "First Class", Value = 328496 },
                };
            }
            else if (date.DayOfYear % 3 == 0)
            {
                revenues = new List<Revenue>()
                {
                    new Revenue (){ ObjectName= "Economy Class", Value = 722782 },
                    new Revenue (){ ObjectName= "Business Class", Value = 413543 },
                    new Revenue (){ ObjectName= "First Class", Value = 427498 },
                };
            }
            else
            {
                revenues = new List<Revenue>()
                {
                    new Revenue (){ ObjectName= "Economy Class", Value = 622786 },
                    new Revenue (){ ObjectName= "Business Class", Value = 413568 },
                    new Revenue (){ ObjectName= "First Class", Value = 427491 },
                };
            }

            lvRevenues.Adapter = new RevenueAdapter(this, revenues);
        }

        private void BtnEmptySeat_Click(object sender, EventArgs e)
        {
            //btnEmptySeat.SetBackgroundColor(Color.Rgb(237,214,136));
            //btnRevenueFromTickets.SetBackgroundColor(Color.Rgb(0, 160, 187));

            Intent intent = new Intent(this, typeof(EmptySeatActivity));
            StartActivity(intent);
        }

        private void BtnRevenueFromTickets_Click(object sender, EventArgs e)
        {
            btnEmptySeat.SetBackgroundColor(Color.Rgb(0, 160, 187));
            btnRevenueFromTickets.SetBackgroundColor(Color.Rgb(237, 214, 136));
        }

    }
}