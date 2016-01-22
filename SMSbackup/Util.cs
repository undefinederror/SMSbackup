using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;


namespace SMSbackup.Util
{
    public class ConnectionResponse
    {
        private bool connected;
        private string message;
        public bool Connected { get { return connected; } }
        public string Message { get { return message; } }
        public ConnectionResponse(bool connected, string message) {
            this.connected = connected;
            this.message = message;

        }
    }
    public abstract class Do {
        public async static Task<ConnectionResponse> CheckConnection(string url, string user, string pass)
        {
            var handler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential(user, pass),
                PreAuthenticate = true
            };
            using (var client = new HttpClient(handler))
            {
                try
                {
                    var res = await client.GetAsync(url);
                    res.EnsureSuccessStatusCode();
                    return new ConnectionResponse(true, "connected");
                }
                catch (Exception ex)
                {
                    return new ConnectionResponse(false, ex.Message);
                }
            }
        }
        public static void hideSoftKeyboard(Activity activity)
        {
            InputMethodManager inputMethodManager = (InputMethodManager)activity.GetSystemService(Activity.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus.WindowToken, 0);
        }
    }
    
}