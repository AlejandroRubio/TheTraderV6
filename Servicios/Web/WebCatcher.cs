using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace TheTrader.Controles.Web
{
    public class WebCatcher
    {

        public static string CapturaValores(String urlAddress, int reintento=0)
        {

            string s = String.Empty;

            try
            {
                WebClient client = new WebClient();

                // Add a user agent header in case the 
                // requested URI contains a query.
                //client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");


                client.Headers.Add("user-agent", "Mozilla /5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)");

                Stream data = client.OpenRead(urlAddress);
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();
            }
            catch  {
                //politica de 5 reintentos
                reintento++;
                if (reintento < 5)
                {
                    s = CapturaValores(urlAddress, reintento);
                }
                else {
                    throw new Exception("ERROR ATACANDO A LA WEB");
                }
               
            }
           



            return s;

        }


        static string URLRequest(string url)
        {
            // Prepare the Request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Set method to GET to retrieve data
            request.Method = "GET";
            request.Timeout = 6000; //60 second timeout
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)";

            string responseContent = null;

            // Get the Response
            using (WebResponse response = request.GetResponse())
            {
                // Retrieve a handle to the Stream
                using (Stream stream = response.GetResponseStream())
                {
                    // Begin reading the Stream
                    using (StreamReader streamreader = new StreamReader(stream))
                    {
                        // Read the Response Stream to the end
                        responseContent = streamreader.ReadToEnd();
                    }
                }
            }

            return (responseContent);
        }


    }
}
