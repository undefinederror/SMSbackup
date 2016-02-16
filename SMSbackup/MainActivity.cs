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
using Android.Content.PM;

namespace SMSbackup
{
    [Activity(Label = "SMSbackup", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Activity
    {
        string rootUrl;
        string dbFile;
        string remotePath;
        string user;
        string pass;
        string prefs_key;

        Button btnPush;
        Button btnSettings;
        LinearLayout layout_settingsOK;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // set views
            btnPush = FindViewById<Button>(Resource.Id.btn_pushDB);
            btnSettings = FindViewById<Button>(Resource.Id.btn_settings);
            layout_settingsOK = FindViewById<LinearLayout>(Resource.Id.layout_settingsOK);
            
           
            btnPush.Click += async delegate
            {
                //btnPush.Text = "";
                string smsdb = "/data/data/com.android.providers.telephony/databases/mmssms.db";
                //btnPush.Text = "copying db to sdcard";
                var p = Java.Lang.Runtime.GetRuntime().Exec("su");
                var os = new Java.IO.DataOutputStream(p.OutputStream);
                var osRes = new Java.IO.DataInputStream(p.InputStream);
                var cmd = "cp " + smsdb + " /sdcard/dev/tmp/";
                os.WriteBytes(cmd + '\n');
                os.Flush();
                //string msg = osRes.ReadLine();
                //btnPush.Text = "maybe copied to sdcard";

                HttpResponseMessage res;
                bool isGood;
                string msg;
                string localsmsdb = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "dev/tmp/mmssms.db");
                using (var stream = File.OpenRead(localsmsdb))
                {
                    using (HttpClient httpClient = new HttpClient(
                        new HttpClientHandler()
                        {
                            Credentials = new NetworkCredential(user,pass),
                            PreAuthenticate = true
                        })
                    )
                    {
                        try
                        {
                            isGood = true;
                            res = await httpClient.PutAsync(remotePath,new StreamContent(stream));
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
            

            btnSettings.Click += (sender, e)=>
            {
                var intent = new Intent(this, typeof(Settings));
                StartActivity(intent);

            };
            

        }
        protected void getSettings() {
            var prefs= Util.Do.GetPreferences(ApplicationContext, GetString(Resource.String.prefs_key));
            rootUrl = prefs.GetString("inputServer", "");
            user = prefs.GetString("inputUser", "");
            pass = prefs.GetString("inputPass", "");
            dbFile = prefs.GetString("inputFilePath", "");
            remotePath = Path.Combine(rootUrl, dbFile);
        }
        protected override void OnResume()
        {
            base.OnResume();

            // set visibility
            layout_settingsOK.Visibility = ViewStates.Gone;
            // get prefs
            getSettings();
            if (!string.IsNullOrEmpty(dbFile))
            {
                layout_settingsOK.Visibility = ViewStates.Visible;
            }
        } 
    }
}

