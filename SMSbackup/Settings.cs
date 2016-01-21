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

namespace SMSbackup
{
    [Activity(Label = "@string/btn_settings", Icon = "@drawable/icon")]
    public class Settings : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Settings);
            // Create your application here

            EditText inputServer = FindViewById<EditText>(Resource.Id.input_serverRoot);
            EditText inputUser = FindViewById<EditText>(Resource.Id.input_user);
            EditText inputPass = FindViewById<EditText>(Resource.Id.input_pass);
            Button btn_checkConnection = FindViewById<Button>(Resource.Id.btn_checkConnection);
            ProgressBar loader = FindViewById<ProgressBar>(Resource.Id.loader_checkConnection);
            TextView lbl_connectionResult = FindViewById<TextView>(Resource.Id.lbl_connectionResult);

            btn_checkConnection.Click += async delegate
            {
                var texts = ((Android.Widget)CurrentFocus.is
                loader.Visibility = Android.Views.ViewStates.Visible;
                lbl_connectionResult.Visibility = Android.Views.ViewStates.Invisible;
                //var res = new Util.ConnectionResponse(true, "yo");
                var res = await Util.Do.CheckConnection(inputServer.Text, inputUser.Text, inputPass.Text);
                loader.Visibility = Android.Views.ViewStates.Gone;
                lbl_connectionResult.Text = String.Format("{0} => {1}", res.Connected, res.Message);
                lbl_connectionResult.Visibility = Android.Views.ViewStates.Visible;
            };
            
        }
    }
    
}