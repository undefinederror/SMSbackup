using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace SMSbackup
{
    public static class GetHttpReq 
    {
        public static HttpWebRequest Create(string url, string user, string pass) {

            // Create an HTTP request for the URL.
            HttpWebRequest httpGetRequest =
               (HttpWebRequest)WebRequest.Create(url);
            // Set up new credentials.
            httpGetRequest.Credentials =
               new NetworkCredential(user, pass);
            // Pre-authenticate the request.
            httpGetRequest.PreAuthenticate = true;
            // Define the HTTP method.
            httpGetRequest.Method = WebRequestMethods.Http.Get;
            // Specify the request for source code.
            httpGetRequest.Headers.Add(@"Translate", "F");

            return httpGetRequest;
        }
        //public static void Init() {
        //    // --------------- GET REQUEST --------------- //
        //    string url = @"https://192.168.0.102/owncloud/remote.php/webdav/Tmp/";
        //    //string filename = "yo.txt";
        //    string filename = "test.txt";
        //    string user = @"vinz";
        //    string pass = @"ti1pfv@OC!";
            

            

            

        //    // Retrieve the response.
        //    var connected = httpGetRequest.CheckConnection();

        //    HttpWebResponse httpGetResponse =
        //       (HttpWebResponse)httpGetRequest.GetResponse();
            
        //    // Retrieve the response stream.
        //    Stream responseStream =
        //       httpGetResponse.GetResponseStream();
            
        //    // Retrieve the response length.
        //    long responseLength =
        //       httpGetResponse.ContentLength;

        //    // Create a stream reader for the response.
        //    StreamReader streamReader =
        //       new StreamReader(responseStream, Encoding.UTF8);

        //    // Write the response status to the console.
        //    Console.WriteLine(
        //       @"GET Response: {0}",
        //       httpGetResponse.StatusDescription);
        //    Console.WriteLine(
        //       @"  Response Length: {0}",
        //       responseLength);
        //    Console.WriteLine(
        //       @"  Response Text: {0}",
        //       streamReader.ReadToEnd());

        //    // Close the response streams.
        //    streamReader.Close();
        //    responseStream.Close();
        
        
        //}
    }
}
