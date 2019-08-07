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
    [Activity(Label = "User_tab_bar")]
    public class User_tab_bar : Activity
    {

        string nm, pswd;
        Fragment[] _fragmentsArray;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            RequestWindowFeature(Android.Views.WindowFeatures.ActionBar);
            //enable navigation mode to support tab layout
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.SetDisplayShowTitleEnabled(false);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.tab_bar_user);
            // Create your application here


            nm = Intent.GetStringExtra("email");
            pswd = Intent.GetStringExtra("code");

            _fragmentsArray = new Fragment[]
           {
            new Fragment_ads(nm),
            new Fragment_fav_ads(nm),
             new Fragment_edit(nm,pswd),
           };

            AddTabToActionBar("Ads"); //First Tab
            AddTabToActionBar("Favourites"); //Second Tab
            AddTabToActionBar("Edit Profile"); //third Tab
        }

        void AddTabToActionBar(string tabTitle)
        {
            Android.App.ActionBar.Tab tab = ActionBar.NewTab();
            tab.SetText(tabTitle);
            


            tab.SetIcon(Android.Resource.Drawable.IcInputAdd);

            tab.TabSelected += TabOnTabSelected;

            ActionBar.AddTab(tab);
        }

        private void TabOnTabSelected(object sender, ActionBar.TabEventArgs e)
        {
            ActionBar.Tab tab = (ActionBar.Tab)sender;

            //Log.Debug(Tag, "The tab {0} has been selected.", tab.Text);
            Fragment frag = _fragmentsArray[tab.Position];

            e.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
        }
    }
}