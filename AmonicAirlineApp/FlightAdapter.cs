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
    internal class FlightAdapter : BaseAdapter<Flight>
    {
        List<Flight> items;
        Activity context;
        public FlightAdapter(Activity context, List<Flight> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override Flight this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.ListFlight, null, false);

            view.FindViewById<TextView>(Resource.Id.tvFlightOutbound).Text = items[position].Outbound.ToString("dd/MM/yyyy");
            view.FindViewById<TextView>(Resource.Id.tvFlightTime).Text = items[position].Time.ToString(@"hh\:mm");
            view.FindViewById<TextView>(Resource.Id.tvFlightPrice).Text = items[position].Price.ToString("C0");
            view.FindViewById<TextView>(Resource.Id.tvFlightNumberOfStop).Text = items[position].NumberOfStop.ToString();
            return view;
        }
    }
}