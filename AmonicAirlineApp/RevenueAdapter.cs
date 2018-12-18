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
    internal class RevenueAdapter : BaseAdapter<Revenue>
    {
        List<Revenue> items;
        Activity context;
        public RevenueAdapter(Activity context, List<Revenue> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override Revenue this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.ListRevenue, null, false);

            view.FindViewById<TextView>(Resource.Id.tvOfficeName).Text = items[position].ObjectName;
            view.FindViewById<TextView>(Resource.Id.tvRevenueOffice).Text = items[position].Value.ToString("C0");

            return view;
        }
    }
}