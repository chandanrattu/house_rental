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
    public class user
    {
        public string fname;
        public string lname;
        public string email;
        public string password;
        public string age;
        public string mobile;
        public string usr_type;
        public string security_q;
        public string security_a;

        Context context;
        public string vidFromDb;
        public string vad_titleFRomDb;
        public string vad_DescFRomDb;
        public string vpriceFromDb;
        public string vregionFromDb;

        public user(Context myContext)
        {
            this.context = myContext;
        }

        public user(string idFromDb, string ad_titleFRomDb, string ad_DescFRomDb, string priceFromDb, string regionFromDb)
        {
            this.vidFromDb = idFromDb;
            this.vad_titleFRomDb = ad_titleFRomDb;
            this.vad_DescFRomDb = ad_DescFRomDb;
            this.vpriceFromDb = priceFromDb;
            this.vregionFromDb = regionFromDb;
        }

        public user(string fNameC, string lNameC, string emailC, string passwordC, string ageC, 
            string mobileC, string usertypeC, string sec_qC, string sec_aC)
        {
            this.fname = fNameC;
            this.lname = lNameC;
            this.email = emailC;
            this.password = passwordC;
            this.age = ageC;
            this.mobile = mobileC;
            this.usr_type = usertypeC;
            this.security_q = sec_qC;
            this.security_a = sec_aC;
        }
    }
}