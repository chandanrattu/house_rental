using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace House_rental
{
    [Activity(Label = "ViewPostedads")]
    public class ViewPostedads : Activity
    {

        ListView myListView;
        string nm;

        DBHelper help;
        int vid;
        Android.App.AlertDialog.Builder myAlert;
        List<user> my_ads_list = new List<user>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout1);

            myListView = FindViewById<ListView>(Resource.Id.list_posted_ad);
            nm = Intent.GetStringExtra("un");

           
            help = new DBHelper(this);
            ICursor result = help.showAll_postedAds(nm);

            while (result.MoveToNext())
            {
                /*
                 * 
                 * 
                 * private const string Column_id = "id";
        private const string Column_ad_title = "ad_title";
        private const string Column_ad_description = "ad_description";
        private const string Column_ad_price = "price";
        private const string Column_ad_region= "region";
        private const string Column_ad_email = "ad_email";
                 * */

                var titleFromDb = result.GetString(result.GetColumnIndexOrThrow("ad_title"));
                var descFRomdb = result.GetString(result.GetColumnIndexOrThrow("ad_description"));
                var priceFromDb = result.GetString(result.GetColumnIndexOrThrow("price"));
                var regionFromDb = result.GetString(result.GetColumnIndexOrThrow("region"));
                var idFromDb = result.GetString(result.GetColumnIndexOrThrow("id"));


                my_ads_list.Add(new user(idFromDb,titleFromDb, descFRomdb, priceFromDb, regionFromDb));
            }


            var myAdapter = new CustomAdapter(this, my_ads_list);
            myAdapter.NotifyDataSetChanged();

            myListView.Adapter = myAdapter;


            myListView.ItemClick += MyListView_ItemClick;
        }

        private void MyListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var index = e.Position;

            var value = my_ads_list[index];
            string id = value.vidFromDb;
            Toast.MakeText(this, id , ToastLength.Short).Show();

            Console.Write("==================================="+id);

            vid = Convert.ToInt32(id);

            myAlert = new Android.App.AlertDialog.Builder(this);
            myAlert.SetTitle("Favourties");


            myAlert.SetMessage("Do you want to delete this AD!!!");
            myAlert.SetPositiveButton("yes", delegate {
                help.deleteAds(nm,vid);
                Toast.MakeText(this,"ad deleted", ToastLength.Short).Show();
            });

            myAlert.SetNegativeButton("Cancel", delegate {
                Console.Write("Cancelled");
            });
            Dialog myDialog = myAlert.Create();
            myDialog.Show();


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