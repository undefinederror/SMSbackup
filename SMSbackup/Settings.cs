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
        EditText inputServer; 
        EditText inputUser;
        EditText inputPass;
        Button btn_checkConnection;
        ProgressBar loader;
        TextView lbl_connectionResult;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Settings);
            getSettings();
            
            // setting views
            this.inputServer = FindViewById<EditText>(Resource.Id.input_serverRoot);
            this.inputUser = FindViewById<EditText>(Resource.Id.input_user);
            this.inputPass = FindViewById<EditText>(Resource.Id.input_pass);
            this.btn_checkConnection = FindViewById<Button>(Resource.Id.btn_checkConnection);
            this.loader = FindViewById<ProgressBar>(Resource.Id.loader_checkConnection);
            this.lbl_connectionResult = FindViewById<TextView>(Resource.Id.lbl_connectionResult);
            

            btn_checkConnection.Click += async delegate
            {
                Util.Do.hideSoftKeyboard(this);
                loader.Visibility = Android.Views.ViewStates.Visible;
                lbl_connectionResult.Visibility = Android.Views.ViewStates.Invisible;
                //var res = new Util.ConnectionResponse(true, "yo");
                var res = await Util.Do.CheckConnection(inputServer.Text, inputUser.Text, inputPass.Text);
                loader.Visibility = Android.Views.ViewStates.Gone;
                lbl_connectionResult.Text = String.Format("{0} => {1}", res.Connected, res.Message);
                lbl_connectionResult.Visibility = Android.Views.ViewStates.Visible;
                OnSuccessfulConnect();
            };
            
        }
        protected void OnSuccessfulConnect()
        { 
            //save settings
            //show input filepath with root preentered(file manager to pick location?)
        
        }
        protected void checkConnection(){
    
        }
        protected void saveSettings() {
            var editor = getPreferences().Edit();
            editor.PutString("inputServer", inputServer.Text);
            editor.PutString("inputUser", inputUser.Text);
            editor.PutString("inputPass", inputPass.Text);
            editor.Apply();
        }
        protected void getSettings()
        {
            var prefs = getPreferences();
            inputServer.Text= prefs.GetString("inputServer","");
            inputUser.Text = prefs.GetString("inputUser","");
            inputPass.Text = prefs.GetString("inputPass","");

            
        }
        protected ISharedPreferences getPreferences() {
            return this.GetSharedPreferences("thisAppPrefs", 0);
        }
        protected override void OnPause()
        {
            base.OnPause();
            saveSettings();
        }
        protected override void OnResume()
        {
            base.OnResume();
            getSettings();
        }
    }
    
}