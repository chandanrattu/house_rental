using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace House_rental
{
    public class Fragment_ads : Fragment
    {

        ListView myListView;
        SearchView mySearchView;

        DBHelper help;
        List<user> myUserList = new List<user>();
        CustomAdapter myAdapter;
        private string nm;

        public Fragment_ads(string nm)
        {
            this.nm = nm;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
           
            View myView = inflater.Inflate(Resource.Layout.ads_fragment, container, false);

            help = new DBHelper(Activity);
            myListView = myView.FindViewById<ListView>(Resource.Id.listView1);
            mySearchView = myView.FindViewById<SearchView>(Resource.Id.searchView1);


           
            ICursor result = help.showAllAds();

            myUserList.Clear();
            while (result.MoveToNext())
            {
                
                /*
                 private const string Column_id = "id";
        private const string Column_ad_title = "ad_title";
        private const string Column_ad_description = "ad_description";
        private const string Column_ad_price = "price";
        private const string Column_ad_region= "region";
        private const string Column_ad_email = "ad_email"; */

                var idFromDb = result.GetString(result.GetColumnIndexOrThrow("id"));
                var ad_titleFRomDb = result.GetString(result.GetColumnIndexOrThrow("ad_title"));
                var ad_DescFRomDb = result.GetString(result.GetColumnIndexOrThrow("ad_description"));
                var priceFromDb = result.GetString(result.GetColumnIndexOrThrow("price"));
                var regionFromDb = result.GetString(result.GetColumnIndexOrThrow("region"));

               
                myUserList.Add(new user(idFromDb, ad_titleFRomDb, ad_DescFRomDb, priceFromDb, regionFromDb));
            }
        
            myAdapter = new CustomAdapter(Activity, myUserList);
            myAdapter.NotifyDataSetChanged();

            myListView.Adapter = myAdapter;
            myListView.ItemClick += MyListView_ItemClick;
            mySearchView.QueryTextChange += MySearchView_QueryTextChange;

            return myView;


        }

        private void MySearchView_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            string searchValue = e.NewText;
            System.Console.WriteLine("value is: " + searchValue);

            
            List<user> newHouses = new List<user>();

           

            foreach (user userObj in myUserList)
            {
                if (userObj.vad_titleFRomDb.Contains(searchValue))
                {
                    newHouses.Add(userObj);
                }
            }

            //myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, newStringArray);

            var myAdapter = new CustomAdapter(Activity, newHouses);
            myListView.Adapter = myAdapter;
        }

        private void MyListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var index = e.Position;

            var value = myUserList[index];
            string id = value.vidFromDb;
            string title_h = value.vad_titleFRomDb;
            string description_h = value.vad_DescFRomDb;
            string price_h = value.vpriceFromDb;
            string refion_h = value.vregionFromDb;

            Intent new_house = new Intent(Activity, typeof(View_House));
            new_house.PutExtra("h_nm", nm);
            new_house.PutExtra("h_id", id);
            new_house.PutExtra("h_t", title_h);
            new_house.PutExtra("h_d", description_h);
            new_house.PutExtra("h_p", price_h);
            new_house.PutExtra("h_r", refion_h);

            StartActivity(new_house);
        }
    }
}