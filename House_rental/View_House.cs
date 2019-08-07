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
using Xamarin.Essentials;

namespace House_rental
{
    [Activity(Label = "View_House", Theme = "@style/AppTheme")]
    public class View_House : Activity
    {
        string nm,vid,vt,vd,vp,vr;
        int f_id;

        bool st = true;

        TextView h_title, h_description, h_price, h_region;
        Button calling,add_to_fav;

        DBHelper help;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.viewHouse);

            nm = Intent.GetStringExtra("h_nm");
            vid = Intent.GetStringExtra("h_id");
            vt = Intent.GetStringExtra("h_t");
            vd = Intent.GetStringExtra("h_d");
            vp = Intent.GetStringExtra("h_p");
            vr = Intent.GetStringExtra("h_r");
            h_title = FindViewById<TextView>(Resource.Id.house_title);
            h_description = FindViewById<TextView>(Resource.Id.house_description);
            h_price = FindViewById<TextView>(Resource.Id.house_price);
            h_region = FindViewById<TextView>(Resource.Id.house_region);
            calling = FindViewById<Button>(Resource.Id.make_call);
            add_to_fav = FindViewById<Button>(Resource.Id.fav);
            f_id = Convert.ToInt32(vid);
            h_title.Text = vt;
            h_description.Text = vd;
            h_price.Text = vp;
            h_region.Text = vr;
           
            calling.Click += making_callmathod;
            if(st == true)
            {
                add_to_fav.Click += addtofav_Mathod;
            }
            else
            {
                add_to_fav.Click += removefav_method;
            }



            Toast.MakeText(this, nm, ToastLength.Long).Show();
        }

        private void removefav_method(object sender, EventArgs e)
        {
            help = new DBHelper(this);
            help.deleteFavAds(nm);
            add_to_fav.Text = "Remove from favourites";
            st = true ;
        }

        private void addtofav_Mathod(object sender, EventArgs e)
        {
            help = new DBHelper(this);
          help.insertFavMyadds(f_id, vt, vd, vp, vr,nm);
            add_to_fav.Text = "Remove from favourites";
            st = false;

            Toast.MakeText(this, "added to fav ", ToastLength.Long).Show();
        }

        private void making_callmathod(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open("+14372889895");
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}