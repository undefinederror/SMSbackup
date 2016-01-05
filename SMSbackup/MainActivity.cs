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
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.btn_checkConnection);
            // input_url = FindViewById<TextAlignment> 
            EditText input_url = FindViewById<EditText>(Resource.Id.input_url);

            button.Click += delegate {
                HttpWebRequest req = GetHttpReq.Create(input_url.Text, "vinz", "ti1pfv@OC!");
                var conn=req.CheckConnection();
                button.Text = string.Format("{0} => {1}",conn.Connected,conn.Message);
            };
        }
    }
}

