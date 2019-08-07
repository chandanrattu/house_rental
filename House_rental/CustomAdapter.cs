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

namespace House_rental
{
    class CustomAdapter: BaseAdapter<user>
    {

        Activity myContext;
        List<user> myListArray;

        public CustomAdapter(Activity context, List<user> myUserList)
        {
            this.myContext = context;
            this.myListArray = myUserList;
        }

        public override user this[int position]
        {
            get { return myListArray[position]; }
        }

        public override int Count
        {
            get { return myListArray.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View myView = convertView;

            user usersObj = myListArray[position];

            if (myView == null)
            {
                myView = myContext.LayoutInflater.Inflate(Resource.Layout.customList, null);

                myView.FindViewById<ImageView>(Resource.Id.image).SetImageResource(Resource.Drawable.inside);
                myView.FindViewById<TextView>(Resource.Id.title_ofad).Text = usersObj.vad_titleFRomDb;
                myView.FindViewById<TextView>(Resource.Id.price_ofad).Text = "Price" + usersObj.vpriceFromDb;
                myView.FindViewById<TextView>(Resource.Id.regio_ofad).Text = "Region " + usersObj.vregionFromDb;
            }

            return myView;
        }
    }
}