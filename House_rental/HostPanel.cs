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
    [Activity(Label = "HostPanel")]
    public class HostPanel : Activity
    {
        string nm, pswd;
        EditText Title_ad, DEscription_ad, Price_ad;
        Spinner region_ad;

        DBHelper help;

        string[] myREgion = { "Mississauga", "Brampton", "Toronto" ,"Scarborough","North York"};

        string ur_ad_title,ur_ad_description,ur_Price_ad,ur_region;
        Button post_ad,view_post_ad;
        Android.App.AlertDialog.Builder myAlert;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.hostscreen);
            Title_ad = FindViewById<EditText>(Resource.Id.ad_title);
            DEscription_ad = FindViewById<EditText>(Resource.Id.ad_description);
            Price_ad = FindViewById<EditText>(Resource.Id.ad_price);
            region_ad = FindViewById<Spinner>(Resource.Id.region_id);
            post_ad= FindViewById<Button>(Resource.Id.ad_post);
            view_post_ad = FindViewById<Button>(Resource.Id.viewad_post);


            region_ad.Adapter = new ArrayAdapter
                (this, Android.Resource.Layout.SimpleListItem1, myREgion);

            myAlert = new Android.App.AlertDialog.Builder(this);

            nm = Intent.GetStringExtra("email");
            pswd = Intent.GetStringExtra("code");

            Toast.MakeText(this, nm, ToastLength.Short).Show();

            region_ad.ItemSelected += MyItemSelectedMethod;
            post_ad.Click += postAdmathod;
            view_post_ad.Click += view_postedasmethod;
           

        }

        private void view_postedasmethod(object sender, EventArgs e)
        {
            Intent view_ads = new Intent(this, typeof(ViewPostedads));
            view_ads.PutExtra("un", nm);
            StartActivity(view_ads);

        }

        private void MyItemSelectedMethod(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var index = e.Position;

          ur_region = myREgion[index];
            System.Console.WriteLine("value is " + ur_region);
        }

        private void postAdmathod(object sender, EventArgs e)
        {

            ur_ad_title = Title_ad.Text;
            ur_ad_description = DEscription_ad.Text;
            ur_Price_ad =Price_ad.Text;

            if (ur_ad_title == " " || ur_ad_title.Equals(""))

            {
                errorDialog("Please Enter a Title");
            }
            else if (ur_Price_ad == " " || ur_Price_ad.Equals(""))
            {
                errorDialog("Please Enter a price");
            }
             else if (ur_ad_description == " " || ur_ad_description.Equals(""))
            {
                errorDialog("Please Enter a ad description");
            }

            else {

                help = new DBHelper(this);
                
                help.insertMyadds(ur_ad_title, ur_ad_description, ur_Price_ad, ur_region,nm);
                Toast.MakeText(this, "You have successfully published ads", ToastLength.Long).Show();
            }

           


        }

        private void errorDialog(string msg)
        {
            myAlert.SetTitle("Error");
            myAlert.SetMessage(msg);
            myAlert.SetPositiveButton("OK", OkAction);
            Dialog myDialog = myAlert.Create();
            myDialog.Show();
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            System.Console.WriteLine("Ok button is clicked!!!");
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem1:
                    {
                        // add your code  
                        return true;
                    }
                case Resource.Id.menuItem2:
                    {
                        Intent loginPage = new Intent(this, typeof(Signin));

                        StartActivity(loginPage);
                        return true;
                    }

            }

            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.mainMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}