using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;

namespace SMSbackup
{
    [Activity(Label = "SMSbackup", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button btnCheck = FindViewById<Button>(Resource.Id.btn_checkConnection);
            Button btnPush = FindViewById<Button>(Resource.Id.btn_pushDB);
            // input_url = FindViewById<TextAlignment> 
            EditText input_url = FindViewById<EditText>(Resource.Id.input_url);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://whatever.com");
            btnCheck.Click += delegate {
                req = GetHttpReq.Create(input_url.Text, "vinz", "");
                var conn=req.CheckConnection();
                btnCheck.Text = string.Format("{0} => {1}", conn.Connected, conn.Message);
                if (!conn.Connected)
                {
                    btnPush.Visibility = Android.Views.ViewStates.Visible;
                }
                else {
                    btnPush.Visibility = Android.Views.ViewStates.Invisible;
                }
            };
            btnPush.Click += delegate
            {
                req = GetHttpReq.Create(input_url.Text, "vinz", "ti1pfv@OC!");
                req.pushDB();
                
            };
        }
    }
}

