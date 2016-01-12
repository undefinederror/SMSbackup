//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net;
//using System.IO;

//namespace SMSbackup
//{
//    public static class HttpWebRequestHelper
//    {
//        public static CheckConnectionResponse CheckConnection(this HttpWebRequest req)
//        {
//            var connectionResp = new CheckConnectionResponse();
//            var meth = req.Method;
//            req.Method = WebRequestMethods.Http.Get;
//            req.Headers.Add(@"Translate", "F");
            

//            try
//            {
//                var res = (HttpWebResponse)req.GetResponse();
//                if (res.StatusCode == HttpStatusCode.OK)
//                {
//                    connectionResp.Connected = true;
//                    connectionResp.Message = res.StatusDescription;
//                }
//                else {
//                    connectionResp.Connected = false;
//                    connectionResp.Message = res.StatusDescription;
//                }
               
//                res.Close();
 
//            }
//            catch (Exception ex) {
//                connectionResp.Connected = false;
//                connectionResp.Message = ex.Message;
//            }

//            req.Method = meth;
            
//            return connectionResp;
            
//        }
//        public static void pushDB(this HttpWebRequest req){
//            req.Method = WebRequestMethods.Http.Put;
//            req.Headers.Add(@"Overwrite", @"T");
//            //req.ContentLength = 3;
//            //var smsdbUri = Android.Provider.Telephony.MmsSms.ContentUri;
//            string smsdb = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "dev/Tmp/sms.txt");
//            //string smsdb ="/data/data/com.android.providers.telephony/databases/mmssms.db";
//            var sr = new StreamReader(smsdb,Encoding.UTF8);
//            string dbfile = sr.ReadToEnd();
//            req.ContentLength = dbfile.Length;

            
//            //req.SendChunked = true;

//            Stream reqStream = req.GetRequestStream();
//            reqStream.Write(Encoding.UTF8.GetBytes(dbfile.ToCharArray()),0,(int)sr.BaseStream.Length);

//            reqStream.Close();

//            HttpWebResponse reqResp = (HttpWebResponse)req.GetResponse();
            

            
        
        
        
//        }
//    }
//    public class CheckConnectionResponse 
//    {
//        private bool connected;
//        private string message;
//        public bool Connected
//        {
//            internal set;
//            get;
//        }
//        public string Message
//        {
//            internal set;
//            get;
//        }
//    }
//}
