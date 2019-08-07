using System.IO;
using System.Net;
using System.Text;

namespace Core.Business
{
    public class HttpHelper
    {
        public static CookieContainer Post(string urlPath,string data)
        {
            var dataStream = Encoding.UTF8.GetBytes(data);
            var webRequest = WebRequest.Create(urlPath);
            webRequest.Method = "POST";
            ServicePointManager.ServerCertificateValidationCallback =
                new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = dataStream.Length;
            Stream newStream = webRequest.GetRequestStream();
            CookieContainer cookieContainer = new CookieContainer();
            if (webRequest is HttpWebRequest)
            {
                (webRequest as HttpWebRequest).CookieContainer = cookieContainer;
            }
            // Send the data.
            newStream.Write(dataStream, 0, dataStream.Length);
            newStream.Close();
            WebResponse response = webRequest.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            return (webRequest as HttpWebRequest).CookieContainer;
        }

        public string GetNetworkData(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Credentials = new System.Net.NetworkCredential("admin", "admin");
            request.AutomaticDecompression = DecompressionMethods.GZip;
            ServicePointManager.ServerCertificateValidationCallback =
                new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);


            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static bool AcceptAllCertifications(object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certification,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
