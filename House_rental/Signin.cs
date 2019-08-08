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
    [Activity(Label = "Signin")]
    public class Signin : Activity
    {
        EditText name, pwd;
        Button logBtn, reg;
        Android.App.AlertDialog.Builder myAlert;
        DBHelper help;
       
     
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.signin);

            name = FindViewById<EditText>(Resource.Id.userName);
            pwd = FindViewById<EditText>(Resource.Id.password);
            
            logBtn = FindViewById<Button>(Resource.Id.logIn);
            reg = FindViewById<Button>(Resource.Id.sign_Up2);


            reg.Click += signUpmathod;
            logBtn.Click += loginMathod;

            

            myAlert = new Android.App.AlertDialog.Builder(this);

        }

       


        // go to sign up
        private void signUpmathod(object sender, EventArgs e)
        {
            Intent registerPage = new Intent(this, typeof(Sign_up));

            StartActivity(registerPage);
        }

        // LOg In Process
        private void loginMathod(object sender, EventArgs e)
        {
            var nm = name.Text;
            var pswd = pwd.Text;


            if (nm == " " || nm.Equals(""))

            {
                errorDialog("Please Enter a Username");
            }
            else if (pswd == " " || pswd.Equals(""))
            {
                errorDialog("Please Enter a Password");
            }

           

            else
            {
                help = new DBHelper(this);
                bool condtion = help.selectMyValues(nm, pswd);


                if (condtion == true)
                {
                    user userInfo = help.selectInfo(nm, pswd);

                    string type = userInfo.usr_type;

                    if(type=="User")
                    {
                        Intent userPage = new Intent(this, typeof(User_tab_bar));
                        userPage.PutExtra("email", nm);
                        userPage.PutExtra("code", pswd);
                        StartActivity(userPage);
                    }

                    else
                    {


                        Intent hostPage = new Intent(this, typeof(HostPanel));
                        hostPage.PutExtra("email", nm);
                        hostPage.PutExtra("code", pswd);

                        StartActivity(hostPage);
                    }


                    Toast.MakeText(this, "you are signed in ", ToastLength.Long).Show();
                }
                else
                {
                    myAlert.SetTitle("Error");
                    myAlert.SetMessage("Wrong userName , Password or User Type");
                    myAlert.SetPositiveButton("OK", OkAction);

                    Dialog myDialog = myAlert.Create();
                    myDialog.Show();
                }
            }
        }

        // mathod for error Dialog
        private void errorDialog(string msg)
        {
            myAlert.SetTitle("Error");
            myAlert.SetMessage(msg);
            myAlert.SetPositiveButton("OK", OkAction);
            Dialog myDialog = myAlert.Create();
            myDialog.Show();
        }


        // pk nutton mathod
        private void OkAction(object sender, DialogClickEventArgs e)
        {
            System.Console.WriteLine("Ok button is clicked!!!");
        }
    }
}