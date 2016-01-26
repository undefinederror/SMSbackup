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
using Android.Graphics;

namespace SMSbackup
{
    [Activity(Label = "@string/btn_settings", Icon = "@drawable/icon")]
    public class Settings : Activity
    {
        EditText input_server; 
        EditText input_user;
        EditText input_pass;
        EditText input_filePath;
        Button btn_checkConnection;
        Button btn_saveSettings;
        ProgressBar loader;
        TextView lbl_connectionResult;
        TextView lbl_saveResult;
        LinearLayout layout_success;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Settings);
            
            
            // setting views
            input_server = FindViewById<EditText>(Resource.Id.input_serverRoot);
            input_user = FindViewById<EditText>(Resource.Id.input_user);
            input_pass = FindViewById<EditText>(Resource.Id.input_pass);
            input_filePath = FindViewById<EditText>(Resource.Id.input_filepath);
            btn_checkConnection = FindViewById<Button>(Resource.Id.btn_checkConnection);
            btn_saveSettings= FindViewById<Button>(Resource.Id.btn_saveSetting);
            loader = FindViewById<ProgressBar>(Resource.Id.loader_checkConnection);
            lbl_connectionResult = FindViewById<TextView>(Resource.Id.lbl_connectionResult);
            lbl_saveResult = FindViewById<TextView>(Resource.Id.lbl_saveResult);
            layout_success = FindViewById<LinearLayout>(Resource.Id.layout_success);

            // set visibility
            layout_success.Visibility = Android.Views.ViewStates.Gone;

            // getting preferences
            getSettings();

            // bindings
            btn_checkConnection.Click += async delegate
            {
                Util.Do.HideSoftKeyboard(this);
                loader.Visibility = Android.Views.ViewStates.Visible;
                lbl_connectionResult.Visibility = Android.Views.ViewStates.Invisible;
                layout_success.Visibility = Android.Views.ViewStates.Gone;
                var res = await Util.Do.CheckConnection(input_server.Text, input_user.Text, input_pass.Text);
                loader.Visibility = Android.Views.ViewStates.Gone;
                lbl_connectionResult.Text = res.Message;
                lbl_connectionResult.SetTextColor(res.Connected ? Color.LightGreen : Color.IndianRed);
                lbl_connectionResult.Visibility = Android.Views.ViewStates.Visible;
                if(res.Connected) OnSuccessfulConnect();
            };
            btn_saveSettings.Click += delegate
            {
                lbl_saveResult.Visibility = ViewStates.Gone;
                try
                {
                    saveSettings();
                    lbl_saveResult.SetTextColor(Color.LightGreen);
                    lbl_saveResult.Text = "saved!";
                }
                catch (Exception err)
                {
                    lbl_saveResult.SetTextColor(Color.IndianRed);
                    lbl_saveResult.Text = err.Message;
                }
                lbl_saveResult.Visibility = ViewStates.Visible;

            };

        }
        protected void OnSuccessfulConnect()
        { 
            //show input filepath with root preentered(file manager to pick location?)
            layout_success.Visibility = Android.Views.ViewStates.Visible;
            
        
        }
        protected void checkConnection(){
    
        }
        protected void saveSettings() {
            var editor = Util.Do.GetPreferences(ApplicationContext, GetString(Resource.String.prefs_key)).Edit();
            editor.PutString("inputServer", input_server.Text);
            editor.PutString("inputUser", input_user.Text);
            editor.PutString("inputPass", input_pass.Text);
            editor.PutString("inputFilePath", input_filePath.Text);
            editor.Apply();
        }
        protected void getSettings()
        {
            var prefs = Util.Do.GetPreferences(ApplicationContext, GetString(Resource.String.prefs_key));
            input_server.Text = prefs.GetString("inputServer", "");
            input_user.Text = prefs.GetString("inputUser", "");
            input_pass.Text = prefs.GetString("inputPass", "");
            input_filePath.Text = prefs.GetString("inputFilePath", "");
        }
       
        protected override void OnPause()
        {
            base.OnPause();
            //saveSettings();
        }
        protected override void OnResume()
        {
            base.OnResume();
            getSettings();
        }
    }
    
}