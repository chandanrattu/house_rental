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
    [Activity(Label = "Sign_up")]
    public class Sign_up : Activity
    {

        Boolean condition = false;

        EditText Fname, Lname, Mobileno, email, in_pass, in_age,in_answer;
        
        Spinner spinnerView;
        string First_nm, Last_nm, ur_email, ur_password, ur_age,ur_mobile, ur_question,ur_answer;
        string type_user = " ";
        string[] security_Question = { "You 1st pet name?", "your best friend name?"
                , "Which is your favourite book" };

        Button sign_up;
        RadioButton rb_user, rb_hoster,rb;
        Android.App.AlertDialog.Builder myAlert;
        DBHelper help;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            // Create your application hkllere
            SetContentView(Resource.Layout.signup);
            Fname = FindViewById<EditText>(Resource.Id.fn);
            Lname = FindViewById<EditText>(Resource.Id.ln);
            email = FindViewById<EditText>(Resource.Id.email);
            in_pass = FindViewById<EditText>(Resource.Id.passwd);
            sign_up = FindViewById<Button>(Resource.Id.register);
            in_age = FindViewById<EditText>(Resource.Id.age);
            Mobileno = FindViewById<EditText>(Resource.Id.mobile_no);
            spinnerView = FindViewById<Spinner>(Resource.Id.securityques);
            in_answer = FindViewById<EditText>(Resource.Id.security_ans);
            rb_user = FindViewById<RadioButton>(Resource.Id.radio_user);
            rb_hoster = FindViewById<RadioButton>(Resource.Id.radio_hoster);

           
            spinnerView.Adapter = new ArrayAdapter
                (this, Android.Resource.Layout.SimpleListItem1, security_Question);

            spinnerView.ItemSelected += MyItemSelectedMethod;
            rb_hoster.Click += RadioButtonClick;
            rb_user.Click += RadioButtonClick;

            sign_up.Click += registerMe;
            myAlert = new Android.App.AlertDialog.Builder(this);
        }

        private void RadioButtonClick(object sender, EventArgs e)
        {
             rb = (RadioButton)sender;
            Toast.MakeText(this, rb.Text, ToastLength.Short).Show();
            type_user = rb.Text;
        }

        private void registerMe(object sender, EventArgs e)
        {
            First_nm = Fname.Text;
            Last_nm = Lname.Text;
            ur_email = email.Text;
            ur_password = in_pass.Text;
            ur_age = in_age.Text;
            ur_mobile = Mobileno.Text;
            ur_answer = in_answer.Text;


           
// radio button selection code
            
            

            
            // validation are impelmented

            if (First_nm == " " || First_nm.Equals(""))

            {
                errorDialog("Please Enter a Fisrt Name");
            }
            else if (Last_nm == " " || Last_nm.Equals(""))
            {
                errorDialog("Please Enter a Last Name");
            }
           
           
            
            else if (ur_email == " " || ur_email.Equals(""))
            {
                errorDialog("Please Enter a Email");
            }
            else if (ur_password == " " || ur_password.Equals(""))
            {
                errorDialog("Please Enter a Password");
            }
            else if (ur_age == " " || ur_age.Equals(""))
            {
                errorDialog("Please Enter a Age");
            }

            else if (ur_mobile == " " || ur_mobile.Equals(""))
            {
                errorDialog("Please Enter a MObile Number");
            }
            else if (type_user == " " || type_user.Equals(""))
            {
                errorDialog("Please select a user type");
            }
            else if (ur_question == " " || ur_question.Equals(""))
            {
                errorDialog("Please select a Security Question");
            }
            else if (ur_answer == " " || ur_answer.Equals(""))
            {
                errorDialog("Please enter a  answer for security question");
            }
            else
            {
               

                help = new DBHelper(this);
                help.insertMyValue(First_nm, Last_nm, ur_email, ur_age, 
                    ur_password,type_user,ur_mobile,ur_question,ur_answer);

                condition = true;

                myAlert.SetTitle("|Success");
                myAlert.SetMessage("You are Succesfully Registered ");
                myAlert.SetPositiveButton("OK", OkAction);
                Dialog myDialog = myAlert.Create();
                myDialog.Show();


                /*  public void insertMyValue(string fname_value, string lname_value, string email_Value,
             string age_value, string password_value, string type_of_user,
             string mobile_of_user, string sec_q, string sec_a)*/
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
            if(condition==true)
            {
                  Intent loginPage = new Intent(this, typeof(Signin));

            StartActivity(loginPage);
            }
            else {
                System.Console.WriteLine("Ok button is clicked!!!");
            }
            
        }

        private void MyItemSelectedMethod(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var index = e.Position;

            ur_question = security_Question[index];
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}