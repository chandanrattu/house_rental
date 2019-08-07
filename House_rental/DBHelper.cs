using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace House_rental
{
    public class DBHelper : SQLiteOpenHelper
    {
      /*  public const string createTable = "create table " +
      table + "( " + id + " INTEGER PRIMARY KEY AUTOINCREMENT,"
          + fname + " text,"
          + lname + " TEXT,"
          + email + " TEXT,"
          + age + " INT,"
          + pass + " TEXT);";
          */


        //Step: 1 - 3:
        private static string _DatabaseName = "mydatabase.db";
        private const string TableName = "user_info";
        private const string Table_ads = "advertisement";
        private const string Table_fav_ads = "fav_advertisement";
        // user table
        private const string ColumnFirstName = "first_name";
        private const string ColumnLastName = "last_name";

        private const string ColumnEmail = "eMailsId";
        private const string ColumnAge = "user_age";
        private const string ColumnMobile = "user_mobile";
        private const string ColumnPassword = "user_password";
        private const string ColumnSecur_ques = "user_question";
        private const string ColumnSecur_answer = "user_answer";
        private const string Column_usertype = "user_type";

        // ads table

        private const string Column_id = "id";
        private const string Column_ad_title = "ad_title";
        private const string Column_ad_description = "ad_description";
        private const string Column_ad_price = "price";
        private const string Column_ad_region= "region";
        private const string Column_ad_email = "ad_email";

        //favads table
        private const string Column_f_id = "f_id";
        private const string Column_f_ad_title = "f_ad_title";
        private const string Column_f_ad_description = "f_ad_description";
        private const string Column_f_ad_price = "f_price";
        private const string Column_f_ad_region = "f_region";

       

        private const string Column_f_ad_email = "f_ad_email";

        // ads_insertion query
        public const string CreateAdsTableQuery= "create table " +
      Table_ads + "( " + Column_id + " INTEGER PRIMARY KEY AUTOINCREMENT,"
          + Column_ad_title + " text,"
          + Column_ad_description + " TEXT,"
          + Column_ad_price + " TEXT,"
            + Column_ad_email + " TEXT,"

          + Column_ad_region + " TEXT);";

        // fav_ads query
        public const string CreateFavAdsTableQuery = "create table " +
      Table_fav_ads + "( " + Column_f_id + " INTEGER,"
          + Column_f_ad_title + " text,"
          + Column_f_ad_description + " TEXT,"
          + Column_f_ad_price + " TEXT,"
            + Column_f_ad_email + " TEXT,"

          + Column_f_ad_region + " TEXT);";


        // user_insertion query
        public const string CreateUserTableQuery = "CREATE TABLE " +
       TableName + " ("
             + ColumnFirstName + " TEXT,"
            + ColumnLastName + " TEXT,"
           + ColumnEmail + " TEXT,"
            + ColumnAge + " TEXT,"
            + ColumnMobile + " TEXT,"
             + ColumnSecur_ques + " TEXT,"
             + ColumnSecur_answer + " TEXT,"
             + Column_usertype + " TEXT,"

           + ColumnPassword + " TEXT)";


        SQLiteDatabase myDBObj; // Step: 1 - 5
        Context myContext; // Step: 1 - 6

        string EmailfromDB = " ";
        string PasswordfromDB = " ";
        string FNamefromDB = " ";
        string LNamefromDB = " ";
        string AgeFromDB = " ";
        string MobileFromDB = " ";
        string Sec_qFromDB = " ";
        string Sec_aFromDB = " ";
        string userTypeFRomDB = " ";


        public DBHelper(Context context) : base(context, name: _DatabaseName, factory: null, version: 1) //Step 2;
        {
            myContext = context;
            myDBObj = WritableDatabase; // Step:3 create a DB objects
        }


        public override void OnCreate(SQLiteDatabase db)  // Step: 1 - 2:1
        {

            db.ExecSQL(CreateUserTableQuery);
            db.ExecSQL(CreateAdsTableQuery);
            db.ExecSQL(CreateFavAdsTableQuery);// Step: 4

        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) // Step: 1 - 2:2
        {
            throw new NotImplementedException();
        }


        public void insertMyValue(string fname_value, string lname_value, string email_Value,
            string age_value, string password_value, string type_of_user, 
            string mobile_of_user, string sec_q, string sec_a)
        {
            


            String insertSQL = "insert into " + TableName + " values (" + "'" + fname_value + "'" + "," + "'"
                + lname_value + "'" + "," + "'" + email_Value + "'" + "," + "'"
                + age_value + "'" + "," + "'" + mobile_of_user + "'" +
                "," + "'" + sec_q + "'" +
                "," + "'" + sec_a + "'" +
                "," + "'" + type_of_user + "'" +
                "," + "'" + password_value + "'" + ");";

            System.Console.WriteLine("Insert SQL " + insertSQL);
            myDBObj.ExecSQL(insertSQL);



        }

        public void insertMyadds(string vtitle, string vdescript, string vprice, string vregion,string vemail)
        {

            String insertSQL = "insert into " + Table_ads + "(" + Column_ad_title + "," + Column_ad_description +
                "," + Column_ad_price + ","  + Column_ad_email + "," + Column_ad_region + ") values ('" 
                + vtitle + "'" + "," + "'" + vdescript + "'," + "'" + vprice + "','"  +  vemail + "','" + vregion + "'" + ");";

            myDBObj.ExecSQL(insertSQL);


        }

        public void insertFavMyadds(int id ,string vtitle, string vdescript, string vprice, string vregion, string vemail)
        {

            String insertSQL = "insert into " + Table_fav_ads + "(" + Column_f_id + "," + Column_f_ad_title + "," + Column_f_ad_description +
                "," + Column_f_ad_price + "," + Column_f_ad_email + "," + Column_f_ad_region + ") values (" + id +  "," + "'" 
                + vtitle + "'" + "," + "'" + vdescript + "'," + "'" + vprice + "','" + vemail + "','" + vregion + "'" + ");";

            myDBObj.ExecSQL(insertSQL);

           
        }


        
        public ICursor showAllAds()
        {
            String selectAllSQL = "select * from " + Table_ads;
            return myDBObj.RawQuery(selectAllSQL, null);
        }

        public ICursor showAllfavAds(string email)
        {
            String selectAllAds = "select * from " + Table_fav_ads + " where " + Column_f_ad_email + "=" + "'"+ email + "'";
            return myDBObj.RawQuery(selectAllAds, null);
        }

        public void deleteFavAds(string name)
        {

            String myDelete = "delete from " + TableName + " where " + Column_f_ad_email + "=" + "'" + name + "'";
            myDBObj.ExecSQL(myDelete);

        }
        public bool selectMyValues(string eml, string pass)
        {
            

            String sqlQuery = "Select * from " + TableName + " where " + ColumnEmail + "=" + "'" + eml + "' and " + ColumnPassword + "= '" + pass + "'";

            ICursor result = myDBObj.RawQuery(sqlQuery, null);

            user userInfo = null;
            if (result.Count > 0)
            {
                while (result.MoveToNext())
                {
                    EmailfromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnEmail));
                    PasswordfromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnPassword));

                    FNamefromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnFirstName));


                    LNamefromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnLastName));

                    AgeFromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnAge));

                    MobileFromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnMobile));

                    Sec_qFromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnSecur_ques));

                    Sec_aFromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnSecur_answer));

                    userTypeFRomDB = result.GetString(result.GetColumnIndexOrThrow(Column_usertype));

                    System.Console.WriteLine("===" + EmailfromDB + PasswordfromDB + FNamefromDB + LNamefromDB + AgeFromDB);

                    userInfo = new user(FNamefromDB, LNamefromDB, EmailfromDB,
                        PasswordfromDB, AgeFromDB,MobileFromDB,userTypeFRomDB,Sec_qFromDB,Sec_aFromDB);



                }
                Toast.MakeText(myContext, "You are successfully logged in", ToastLength.Long).Show();




                return true;
            }


            else
            {
                Toast.MakeText(myContext, "Wrong Username and password", ToastLength.Long).Show();
                return false;
            }




        }
        public void updateData(string vfname, string vlname, string vage, string vmobile, string vemail, string paaswd, string update_user_type, string ques, string ansr)
        {
            String updateSQL = "update  " + TableName + " set " + ColumnFirstName + " = '" + vfname + "' , " + ColumnLastName
                + " ='" + vlname + "' ," + ColumnAge + " ='" + vage + "' , " + ColumnMobile + " ='" + vmobile + "' , "
                + ColumnEmail + " ='" + vemail + "' , "
                + ColumnPassword + " ='" + paaswd + "' , "
                + Column_usertype + " ='" + update_user_type +"' , "
                + ColumnSecur_ques + " ='" + ques + "' , "
                + ColumnSecur_answer + " ='" + ansr +
                "' where " + ColumnEmail + "=" + "'" + vemail + "' and " + ColumnPassword + "= '" + paaswd + "'";

            System.Console.WriteLine("Insert SQL " + updateSQL);
            myDBObj.ExecSQL(updateSQL);
        }

        public user selectInfo(string name, string password)
        {


            String sqlQuery = "Select * from " + TableName + " where " + ColumnEmail + "=" + "'" + name + "' and " + ColumnPassword + "= '" + password + "'";


            ICursor result = myDBObj.RawQuery(sqlQuery, null);

            user userInfo = null;

            while (result.MoveToNext())
            {


                EmailfromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnEmail));
                PasswordfromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnPassword));

                FNamefromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnFirstName));


                LNamefromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnLastName));

                AgeFromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnAge));

                MobileFromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnMobile));

                Sec_qFromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnSecur_ques));

                Sec_aFromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnSecur_answer));

                userTypeFRomDB = result.GetString(result.GetColumnIndexOrThrow(Column_usertype));

                System.Console.WriteLine("===" + EmailfromDB + PasswordfromDB + FNamefromDB + LNamefromDB + AgeFromDB);

                userInfo = new user(FNamefromDB, LNamefromDB, EmailfromDB,
                    PasswordfromDB, AgeFromDB, MobileFromDB, userTypeFRomDB, Sec_qFromDB, Sec_aFromDB);

            }
            return userInfo;
        }


    }
}
