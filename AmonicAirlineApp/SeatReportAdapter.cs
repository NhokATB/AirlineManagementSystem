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
    internal class SeatReportAdapter : BaseAdapter<SeatReport>
    {
        List<SeatReport> items;
        Activity context;
        public SeatReportAdapter(Activity context, List<SeatReport> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override SeatReport this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.ListSeatReport, null, false);

            view.FindViewById<TextView>(Resource.Id.tvSeatReportFrom).Text = items[position].From;
            view.FindViewById<TextView>(Resource.Id.tvSeatReportTo).Text = items[position].To;
            view.FindViewById<TextView>(Resource.Id.tvSeatReportTime).Text = items[position].Time.ToString(@"hh\:mm");
            view.FindViewById<TextView>(Resource.Id.tvSeatReportEconomy).Text = $"{items[position].EconomyTicket}/{items[position].EconomySeat}";
            view.FindViewById<TextView>(Resource.Id.tvSeatReportBusiness).Text = $"{items[position].BusinessTicket}/{items[position].BusinessSeat}";
            view.FindViewById<TextView>(Resource.Id.tvSeatReportFirst).Text = $"{items[position].FirstClassTicket}/{items[position].FirstClassSeat}";
            return view;
        }
    }
}