using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AmonicAirlineApp
{
    [Activity(Label = "GuestActivity")]
    public class GuestActivity : Activity
    {
        List<Amentiy> amenities;
        Button btnSearchFlight;
        Button btnAmenities;
        ListView lvAmenities;
        Button btnBack;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Guest);

            lvAmenities = this.FindViewById<ListView>(Resource.Id.lvAmenities);
            btnAmenities = this.FindViewById<Button>(Resource.Id.btnAmenities);
            btnSearchFlight = this.FindViewById<Button>(Resource.Id.btnSearchFlight);
            btnBack = this.FindViewById<Button>(Resource.Id.btnBack);

            btnAmenities.Click += BtnAmenities_Click;
            btnSearchFlight.Click += BtnSearchFlight_Click;

            amenities = new List<Amentiy>
            {
                new Amentiy() { Service = "Extra Blanket", Price = 10},
                new Amentiy() { Service = "Next Seat Free", Price = 30 },
                new Amentiy() { Service = "Two Neighboring Seats Free", Price = 50 },
                new Amentiy() { Service = "Tablet Rental", Price = 12 },
                new Amentiy() { Service = "Laptop Rental", Price = 15 },
                new Amentiy() { Service = "Lounge Access", Price = 25 },
                new Amentiy() { Service = "Soft Drinks", Price = 0 },
                new Amentiy() { Service = "Premium Headphones Rental", Price = 5 },
                new Amentiy() { Service = "Extra Bag", Price = 15 },
                new Amentiy() { Service = "Fast Checkin Lane", Price = 10 },
                new Amentiy() { Service = "Wi-Fi 50 mb", Price = 0 },
                new Amentiy() { Service = "Wi-Fi 250 mb", Price = 25 },
            };

            lvAmenities.Adapter = new AmenityAdapter(this, amenities);

            btnBack.Click += BtnBack_Click;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void BtnSearchFlight_Click(object sender, EventArgs e)
        {
            //btnSearchFlight.SetBackgroundColor(Color.Rgb(237, 214, 136));
            //btnAmenities.SetBackgroundColor(Color.Rgb(0, 160, 187));

            Intent intent = new Intent(this, typeof(SearchFlightActivity));
            StartActivity(intent);
        }

        private void BtnAmenities_Click(object sender, EventArgs e)
        {
            btnSearchFlight.SetBackgroundColor(Color.Rgb(0, 160, 187));
            btnAmenities.SetBackgroundColor(Color.Rgb(237, 214, 136));
        }
    }
}