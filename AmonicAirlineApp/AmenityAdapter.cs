﻿using System;
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
    internal class AmenityAdapter : BaseAdapter<Amentiy>
    {
        List<Amentiy> items;
        Activity context;
        public AmenityAdapter(Activity context, List<Amentiy> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override Amentiy this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.ListName_Value, null, false);

            view.FindViewById<TextView>(Resource.Id.tvObjectName).Text = items[position].Service;
            view.FindViewById<TextView>(Resource.Id.tvValue).Text = items[position].Price.ToString("C0");
            return view;
        }
    }
}