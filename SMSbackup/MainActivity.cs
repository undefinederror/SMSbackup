using System;
using System.Net;
using System.Net.Http;
using System.IO;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


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
            Button btnPushOnly = FindViewById<Button>(Resource.Id.btn_pushOnly);
            Button btnSettings = FindViewById<Button>(Resource.Id.btn_settings);
            // input_url = FindViewById<TextAlignment> 
            EditText input_url = FindViewById<EditText>(Resource.Id.input_url);
            
            btnCheck.Click += async delegate {
                btnCheck.Text = "";
                HttpResponseMessage res;
                string msg;
                bool isGood;
                using (HttpClient httpClient = new HttpClient(
                    new HttpClientHandler()
                    {
                        Credentials = new NetworkCredential("vinz", @""),
                        PreAuthenticate = true
                    })
                ) {
                    try
                    {
                        isGood = true;
                        res = await httpClient.GetAsync(input_url.Text);
                        res.EnsureSuccessStatusCode();
                        msg = "OK";
                    }
                    catch (Exception ex)
                    {
                        isGood = false;
                        msg = ex.Message;
                    }
                }
                
                btnCheck.Text = string.Format("{0} => {1}", isGood, msg);
                if (isGood)
                {
                    btnPush.Visibility = Android.Views.ViewStates.Visible;
                }
                else {
                    btnPush.Visibility = Android.Views.ViewStates.Invisible;
                }
            };
            btnPush.Click += async delegate
            {
                btnPush.Text = "";
                string smsdb = "/data/data/com.android.providers.telephony/databases/mmssms.db";
                btnPush.Text = "copying db to sdcard";
                var p = Java.Lang.Runtime.GetRuntime().Exec("su");
                var os = new Java.IO.DataOutputStream(p.OutputStream);
                var osRes = new Java.IO.DataInputStream(p.InputStream);
                var cmd = "cp " + smsdb + " /sdcard/dev/tmp/";
                os.WriteBytes(cmd + '\n');
                os.Flush();
                //string msg = osRes.ReadLine();
                btnPush.Text = "maybe copied to sdcard";

                HttpResponseMessage res;
                bool isGood;
                string msg;
                string localsmsdb = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "dev/tmp/mmssms.db");
                using (var stream = File.OpenRead(localsmsdb))
                {
                    using (HttpClient httpClient = new HttpClient(
                        new HttpClientHandler()
                        {
                            Credentials = new NetworkCredential("vinz",@""),
                            PreAuthenticate = true
                        })
                    )
                    {
                        try
                        {
                            isGood = true;
                            res = await httpClient.PutAsync(input_url.Text,new StreamContent(stream));
                            res.EnsureSuccessStatusCode();
                            msg="OK";
                        }
                        catch (Exception ex)
                        {
                            isGood = false;
                            msg = ex.Message;
                        }

                    }
                }
                btnPush.Text = string.Format("{0} => {1}", isGood, msg);
            };
            btnPushOnly.Click += async delegate
            {
                HttpResponseMessage res;
                bool isGood;
                string msg;
                string localsmsdb = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "dev/tmp/mmssms.db");
                using (var stream = File.OpenRead(localsmsdb))
                {
                    using (HttpClient httpClient = new HttpClient(
                        new HttpClientHandler()
                        {
                            Credentials = new NetworkCredential("vinz", @""),
                            PreAuthenticate = true
                        })
                    )
                    {
                        try
                        {
                            isGood = true;
                            res = await httpClient.PutAsync(input_url.Text, new StreamContent(stream));
                            res.EnsureSuccessStatusCode();
                            msg = "OK";
                        }
                        catch (Exception ex)
                        {
                            isGood = false;
                            msg = ex.Message;
                        }

                    }
                }
                btnPushOnly.Text = string.Format("{0} => {1}", isGood, msg);

            };

            btnSettings.Click += (sender, e)=>
            {
                var intent = new Intent(this, typeof(Settings));
                StartActivity(intent);

            };
            // coze I can't be bothered
            input_url.Text = @"https://vinznet.net/owncloud/remote.php/webdav/Tmp/x.db";

        }
    }
}

