using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;

namespace House_rental
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button signup, signin;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

           signup= FindViewById<Button>(Resource.Id.signUp);
           signin = FindViewById<Button>(Resource.Id.signIn);

            signup.Click += Signup_act;


        }

        private void Signup_act(object sender, EventArgs e)
        {
            Intent registerPage = new Intent(this, typeof(Sign_up));

            StartActivity(registerPage);
        }
    }
}