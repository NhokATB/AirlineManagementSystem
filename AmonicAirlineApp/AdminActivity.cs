using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmonicAirlineApp
{
    [Activity(Label = "AdminActivity")]
    public class AdminActivity : Activity
    {
        private Button btnApplyAdmin;
        private TextView tvDate;
        private List<Revenue> revenues;
        private ListView lvRevenueOffice;
        private Button btnBack;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Admin);

            lvRevenueOffice = this.FindViewById<ListView>(Resource.Id.lvRevenueOffice);

            tvDate = this.FindViewById<TextView>(Resource.Id.tvDate);
            tvDate.Click += TvDate_Click;

            btnApplyAdmin = this.FindViewById<Button>(Resource.Id.btnApplyAdmin);
            btnApplyAdmin.Click += BtnApply_Click;

            btnBack = this.FindViewById<Button>(Resource.Id.btnBack);
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

            LoadRevenueOffice();
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
      
        private void LoadRevenueOffice()
        {
            var date = DateTime.Parse(tvDate.Text);

            if (date.DayOfYear % 4 == 0)
            {
                revenues = new List<Revenue>()
                {
                    new Revenue (){ ObjectName= "Abu dhabi", Value = 782098 },
                    new Revenue (){ ObjectName= "Cairo", Value = 323528 },
                    new Revenue (){ ObjectName= "Bahrain", Value = 233492 },
                    new Revenue (){ ObjectName= "Doha", Value = 437492 },
                    new Revenue (){ ObjectName= "Riyadh", Value = 537492 },
                };
            }
            else if (date.DayOfYear % 2 == 0)
            {
                revenues = new List<Revenue>()
                {
                    new Revenue (){ ObjectName= "Abu dhabi", Value = 592098 },
                    new Revenue (){ ObjectName= "Cairo", Value = 233528 },
                    new Revenue (){ ObjectName= "Bahrain", Value = 337492 },
                    new Revenue (){ ObjectName= "Doha", Value = 637492 },
                    new Revenue (){ ObjectName= "Riyadh", Value = 293792 },
                };
            }
            else if (date.DayOfYear % 3 == 0)
            {
                revenues = new List<Revenue>()
                {
                    new Revenue (){ ObjectName= "Abu dhabi", Value = 382098 },
                    new Revenue (){ ObjectName= "Cairo", Value = 623528 },
                    new Revenue (){ ObjectName= "Bahrain", Value = 337492 },
                    new Revenue (){ ObjectName= "Doha", Value = 327492 },
                    new Revenue (){ ObjectName= "Riyadh", Value = 417492 },
                };
            }
            else
            {
                revenues = new List<Revenue>()
                {
                    new Revenue (){ ObjectName= "Abu dhabi", Value = 682098 },
                    new Revenue (){ ObjectName= "Cairo", Value = 223528 },
                    new Revenue (){ ObjectName= "Bahrain", Value = 7537492 },
                    new Revenue (){ ObjectName= "Doha", Value = 123492 },
                    new Revenue (){ ObjectName= "Riyadh", Value = 337492 },
                };
            }

            revenues = revenues.OrderByDescending(t => t.Value).ToList();
            lvRevenueOffice.Adapter = new RevenueAdapter(this, revenues);
        }
    }
}