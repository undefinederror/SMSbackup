using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SMSbackup
{
    public static class HttpWebRequestHelper
    {
        public static CheckConnectionResponse CheckConnection(this HttpWebRequest req)
        {
            var connectionResp = new CheckConnectionResponse();
            var meth = req.Method;
            req.Method = WebRequestMethods.Http.Head;

            try
            {
                var res = (HttpWebResponse)req.GetResponse();
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    connectionResp.Connected = true;
                    connectionResp.Message = res.StatusDescription;
                }
                else {
                    connectionResp.Connected = false;
                    connectionResp.Message = res.StatusDescription;
                }
 
            }
            catch (Exception ex) {
                connectionResp.Connected = false;
                connectionResp.Message = ex.Message;
            }

            req.Method = meth;
            return connectionResp;
            
        }
    }
    public class CheckConnectionResponse 
    {
        private bool connected;
        private string message;
        public bool Connected
        {
            internal set;
            get;
        }
        public string Message
        {
            internal set;
            get;
        }
    }
}
