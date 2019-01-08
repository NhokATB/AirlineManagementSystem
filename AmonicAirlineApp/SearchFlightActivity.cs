using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmonicAirlineApp
{
    [Activity(Label = "SearchFlight")]
    public class SearchFlightActivity : Activity
    {
        private Button btnSearchFlight;
        private Button btnAmenities;
        private Button btnApplySearch;
        private TextView tvDateForSearch;
        private ListView lvFlights;
        private Spinner spinnerFrom;
        private Spinner spinnerTo;
        private List<Flight> flights;
        private Button btnBack;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SearchFlight);

            lvFlights = this.FindViewById<ListView>(Resource.Id.lvFlights);
            btnAmenities = this.FindViewById<Button>(Resource.Id.btnAmenities);
            btnSearchFlight = this.FindViewById<Button>(Resource.Id.btnSearchFlight);
            btnApplySearch = this.FindViewById<Button>(Resource.Id.btnApplySearch);
            tvDateForSearch = this.FindViewById<TextView>(Resource.Id.tvDateForSearch);
            spinnerTo = this.FindViewById<Spinner>(Resource.Id.spinnerTo);
            spinnerFrom = this.FindViewById<Spinner>(Resource.Id.spinnerFrom);
            btnBack = this.FindViewById<Button>(Resource.Id.btnBack);

            btnAmenities.Click += BtnAmenities_Click;
            tvDateForSearch.Click += TvDateForSearch_Click;
            btnApplySearch.Click += BtnApplySearch_Click;

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.airports, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            spinnerFrom.Adapter = adapter;
            spinnerTo.Adapter = adapter;
            btnBack.Click += BtnBack_Click;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void BtnApplySearch_Click(object sender, EventArgs e)
        {
            lvFlights.Adapter = new FlightAdapter(this, new List<Flight>());

            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();

            if (spinnerTo.SelectedItem.ToString() == spinnerFrom.SelectedItem.ToString())
            {
                alert.SetTitle("Message");
                alert.SetMessage("Departure and arrival airport can not be the same");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
                return;
            }

            if (tvDateForSearch.Text == "")
            {
                alert.SetTitle("Message");
                alert.SetMessage("Please choose date before apply");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
                return;
            }

            if (tvDateForSearch.Text == "Select Date")
            {
                alert.SetTitle("Message");
                alert.SetMessage("Please choose date before apply");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
                return;
            }

            if (DateTime.Parse(tvDateForSearch.Text).Date < DateTime.Now.Date)
            {
                alert.SetTitle("Message");
                alert.SetMessage("Date must be >= today");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
                return;
            }

            if (DateTime.Parse(tvDateForSearch.Text).Date >= new DateTime(2019, 4, 1))
            {
                alert.SetTitle("Message");
                alert.SetMessage("No data");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
                return;
            }

            LoadFlights();
        }
        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void LoadFlights()
        {
            var date = DateTime.Parse(tvDateForSearch.Text);
            if (date.DayOfYear % 4 == 0)
            {
                flights = new List<Flight>()
                {
                    new Flight(){ Outbound = date, Time = new TimeSpan (6, 0, 0), Price = 2030, NumberOfStop = 0},
                    new Flight(){ Outbound = date, Time = new TimeSpan (10, 0, 0), Price = 2340, NumberOfStop = 0},
                    new Flight(){ Outbound = date, Time = new TimeSpan (12, 0, 0), Price = 2242, NumberOfStop = 0},
                    new Flight(){ Outbound = date, Time = new TimeSpan (17, 0, 0), Price = 2690, NumberOfStop = 1},
                    new Flight(){ Outbound = date, Time = new TimeSpan (22, 0, 0), Price = 2760, NumberOfStop = 1},
                    new Flight(){ Outbound = date, Time = new TimeSpan (9, 0, 0), Price = 2140, NumberOfStop = 0},
                };
            }
            else if (date.DayOfYear % 2 == 0)
            {
                flights = new List<Flight>()
                {
                    new Flight(){ Outbound = date, Time = new TimeSpan (6, 0, 0), Price = 2150, NumberOfStop = 0},
                    new Flight(){ Outbound = date, Time = new TimeSpan (9, 0, 0), Price = 2236, NumberOfStop = 0},
                    new Flight(){ Outbound = date, Time = new TimeSpan (14, 0, 0), Price = 2442, NumberOfStop = 1},
                    new Flight(){ Outbound = date, Time = new TimeSpan (12, 0, 0), Price = 2730, NumberOfStop = 0},
                    new Flight(){ Outbound = date, Time = new TimeSpan (20, 0, 0), Price = 2742, NumberOfStop = 1},
                    new Flight(){ Outbound = date, Time = new TimeSpan (17, 0, 0), Price = 2690, NumberOfStop = 1},
                    new Flight(){ Outbound = date, Time = new TimeSpan (22, 0, 0), Price = 2460, NumberOfStop = 0},
                };
            }
            else if (date.DayOfYear % 3 == 0)
            {
                flights = new List<Flight>()
                {
                    new Flight(){ Outbound = date, Time = new TimeSpan (7, 0, 0), Price = 3545, NumberOfStop = 1},
                    new Flight(){ Outbound = date, Time = new TimeSpan (10, 0, 0), Price = 2740, NumberOfStop = 0},
                    new Flight(){ Outbound = date, Time = new TimeSpan (12, 0, 0), Price = 2942, NumberOfStop = 0},
                    new Flight(){ Outbound = date, Time = new TimeSpan (17, 0, 0), Price = 3210, NumberOfStop = 1},
                    new Flight(){ Outbound = date, Time = new TimeSpan (22, 0, 0), Price = 3415, NumberOfStop = 1},
                    new Flight(){ Outbound = date, Time = new TimeSpan (20, 0, 0), Price = 3514, NumberOfStop = 1},
                };
            }
            else
            {
                flights = new List<Flight>()
                {
                    new Flight(){ Outbound = date, Time = new TimeSpan (6, 0, 0), Price = 3030, NumberOfStop = 0},
                    new Flight(){ Outbound = date, Time = new TimeSpan (12, 0, 0), Price = 2840, NumberOfStop = 1},
                    new Flight(){ Outbound = date, Time = new TimeSpan (17, 0, 0), Price = 2942, NumberOfStop = 0},
                    new Flight(){ Outbound = date, Time = new TimeSpan (21, 0, 0), Price = 2990, NumberOfStop = 1},
                    new Flight(){ Outbound = date, Time = new TimeSpan (22, 0, 0), Price = 2660, NumberOfStop = 0},
                };
            }

            flights = flights.OrderBy(t => t.Time).ToList();
            lvFlights.Adapter = new FlightAdapter(this, flights);
        }

        private void BtnAmenities_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GuestActivity));
            StartActivity(intent);
        }

        private void TvDateForSearch_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                tvDateForSearch.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);

            lvFlights.Adapter = new FlightAdapter(this, new List<Flight>());
        }
    }
}