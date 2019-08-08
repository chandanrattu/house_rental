using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace House_rental
{
    public class Fragment_edit : Fragment
    {
        private string vnm;
        private string vpswd;

        EditText first_nm, last_nm, _age, mob_no, _email, _password, sec_ans;
        RadioButton u, h,rb;
        Button update,delete;
        string update_user_type,ques;
        string user_type = " ";
        DBHelper help;
        Android.App.AlertDialog.Builder myAlert;


        Spinner edit_spinner;
        public Fragment_edit(string nm, string pswd)
        {
            this.vnm = nm;
            this.vpswd = pswd;
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
            View myView = inflater.Inflate(Resource.Layout.edit_profile, container, false);

            first_nm = myView.FindViewById<EditText>(Resource.Id.edit_fn);
            last_nm = myView.FindViewById<EditText>(Resource.Id.edit_ln);
            _age = myView.FindViewById<EditText>(Resource.Id.edit_age);
            mob_no = myView.FindViewById<EditText>(Resource.Id.edit_mobile_no);
            _email = myView.FindViewById<EditText>(Resource.Id.edit_email);
            _password = myView.FindViewById<EditText>(Resource.Id.edit_passwd);
            sec_ans = myView.FindViewById<EditText>(Resource.Id.edit_security_ans);

            u= myView.FindViewById<RadioButton>(Resource.Id.edit_radio_user);
            h = myView.FindViewById<RadioButton>(Resource.Id.edit_radio_hoster);
            edit_spinner = myView.FindViewById<Spinner>(Resource.Id.edit_securityques);
            update = myView.FindViewById<Button>(Resource.Id.edit_update);
            delete = myView.FindViewById<Button>(Resource.Id.edit_delete);
            // set dATA TO FIELDS
            help = new DBHelper(Activity);

            user userInfo = help.selectInfo(vnm, vpswd);
            first_nm.Text = userInfo.fname;
            last_nm.Text = userInfo.lname;
            _age.Text = userInfo.age;
            mob_no.Text = userInfo.mobile;
            _email.Text = userInfo.email;
            _password.Text = userInfo.password;
            ques = userInfo.security_q;

            string[] questt = { ques };

            edit_spinner.Adapter = new ArrayAdapter
                (Activity, Android.Resource.Layout.SimpleListItem1, questt);

            sec_ans.Text = userInfo.security_a;
           

            edit_spinner.ItemSelected += MyItemSelectedMethod;

            u.Click += RadioButtonClick;
            h.Click += RadioButtonClick;
            delete.Click += Delete_user;
            update.Click += updateDetails;
            return myView;
        }

        private void Delete_user(object sender, EventArgs e)
        {

            help = new DBHelper(Activity);
            help.delete_user(vnm);
            Toast.MakeText(Activity, "Deletion successful", ToastLength.Long).Show();

            Intent i = new Intent(Activity, typeof(Signin));
            StartActivity(i);
        }

        private void RadioButtonClick(object sender, EventArgs e)
        {
            rb = (RadioButton)sender;
            Toast.MakeText(Activity, rb.Text, ToastLength.Short).Show();
            user_type = rb.Text;
        }

        private void updateDetails(object sender, EventArgs e)
        {


            if(user_type == " "|| user_type=="")
            {
                myAlert = new Android.App.AlertDialog.Builder(Activity);
                myAlert.SetTitle("Error");
                myAlert.SetMessage("plaese select user type");
                myAlert.SetPositiveButton("OK", OkAction);
                Dialog myDialog = myAlert.Create();
                myDialog.Show();
            }
            else
            { 
            var f_name = first_nm.Text;
            var l_name = last_nm.Text;
            var age = _age.Text;
            var mobile = mob_no.Text;
            var email = _email.Text;
            var paaswd = _password.Text;

            var ansr = sec_ans.Text;


            help = new DBHelper(Activity);

            help.updateData(f_name, l_name, age, mobile, email,paaswd,user_type,ques,ansr);
            myAlert = new Android.App.AlertDialog.Builder(Activity);
            myAlert.SetTitle("Success");
            myAlert.SetMessage("information updated plesase login again ");
            myAlert.SetPositiveButton("OK", OkAction);
            Dialog myDialog = myAlert.Create();
            myDialog.Show();

            }

        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            System.Console.WriteLine("Ok button is clicked!!!");
        }

        private void MyItemSelectedMethod(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var index = e.Position;
           
        }
    }
}