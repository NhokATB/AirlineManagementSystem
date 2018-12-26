using System;
using System.Collections.Generic;
using Android.Database;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace AmonicAirlineApp
{
    internal class AmenityAdapter<T> : IListAdapter
    {
        private GuestActivity context;
        private List<Amentiy> amenities;

        public AmenityAdapter(GuestActivity context, List<Amentiy> amenities)
        {
            this.context = context;
            this.amenities = amenities;
        }

        public int Count => throw new NotImplementedException();

        public bool HasStableIds => throw new NotImplementedException();

        public bool IsEmpty => throw new NotImplementedException();

        public int ViewTypeCount => throw new NotImplementedException();

        public IntPtr Handle => throw new NotImplementedException();

        public bool AreAllItemsEnabled()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public long GetItemId(int position)
        {
            throw new NotImplementedException();
        }

        public int GetItemViewType(int position)
        {
            throw new NotImplementedException();
        }

        public View GetView(int position, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(int position)
        {
            throw new NotImplementedException();
        }

        public void RegisterDataSetObserver(DataSetObserver observer)
        {
            throw new NotImplementedException();
        }

        public void UnregisterDataSetObserver(DataSetObserver observer)
        {
            throw new NotImplementedException();
        }
    }
}