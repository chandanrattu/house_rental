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
    [Activity(Label = "View_House")]
    public class View_House : Activity
    {
        string nm,vid,vt,vd,vp,vr;
        int f_id;

        bool st ;

        TextView h_title, h_description, h_price, h_region;
        Button calling,add_to_fav,next_img,prev_img;
        ImageView myImageView;

        DBHelper help;
        int image_no = 0;
        int[] myImageList = new int[] { Resource.Drawable.house,Resource.Drawable.inside,
        Resource.Drawable.inside2,Resource.Drawable.side,Resource.Drawable.stairs,Resource.Drawable.backyard};

        Android.App.AlertDialog.Builder myAlert;

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
            myImageView = FindViewById<ImageView>(Resource.Id.house_view);
            next_img = FindViewById<Button>(Resource.Id.nxt);
            prev_img = FindViewById<Button>(Resource.Id.prev);
            
            myImageView.SetImageResource(Resource.Drawable.house);

            next_img.Click += nextimgMethod;
            prev_img.Click += prevImagemethod;
           
            f_id = Convert.ToInt32(vid);
            h_title.Text = vt;
            h_description.Text = vd;
            h_price.Text = "Price $"+vp;
            h_region.Text = "Location :" + vr;

            help = new DBHelper(this);
            add_to_fav.Click += addtofav_Mathod;
            calling.Click += making_callmathod;
            st = help.check_fav_ad(nm, f_id);
            if (st == false)
            {
                add_to_fav.Text = "Add to favourites";
            }
            else
            {
                add_to_fav.Text = "Remove from favourites";
            }



            Toast.MakeText(this, nm, ToastLength.Long).Show();
        }

        private void prevImagemethod(object sender, EventArgs e)
        {
            if (image_no > 0)
            {
                image_no--;
                myImageView.SetImageResource(myImageList[image_no]);
            }
            else
            {
                Toast.MakeText(this, "this is 1st image", ToastLength.Long).Show();
            }
        }

        private void nextimgMethod(object sender, EventArgs e)
        {
           if(image_no<5)
            {
                image_no++;
                myImageView.SetImageResource(myImageList[image_no]);
            }
           else
            {
                Toast.MakeText(this, "this is last image", ToastLength.Long).Show();
            }
        }

       

        private void addtofav_Mathod(object sender, EventArgs e)
        {
            myAlert = new Android.App.AlertDialog.Builder(this);
            myAlert.SetTitle("Favourties");

            st = help.check_fav_ad(nm, f_id);

            if (st == false)
            {
                myAlert.SetMessage("Do you want to add this item to favorites!!!");
                myAlert.SetPositiveButton("Add", delegate {
                    help.insertFavMyadds(f_id,vt,vd,vp,vr,nm);
                    add_to_fav.Text = "Remove from favourites";
                });
            }
            else
            {
                myAlert.SetMessage("Do you want to remove this item from favorites!!!");
                myAlert.SetPositiveButton("Remove", delegate {
                    help.deleteFavAds(nm, f_id);
                    add_to_fav.Text = "Add to favourites";
                });

            }

            myAlert.SetNegativeButton("Cancel", delegate {
                Console.Write("Cancelled");
            });
            Dialog myDialog = myAlert.Create();
            myDialog.Show();
        
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