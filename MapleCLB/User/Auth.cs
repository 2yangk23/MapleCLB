using System;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace MapleCLB.User
{
    class Auth
    {
        byte maxRetry = 10;

        public string loginAuth(string user, string Pass)
        {
            string authCode = RetrieveWebAuthToken(user, Pass);

            for (int i = 0; i < maxRetry && authCode == "error"; ++i)
            {
                Program.WriteLog(("Retrying auth in 2 minutes..."));
                Thread.Sleep(120000);

                authCode = RetrieveWebAuthToken(user, Pass);
            }

            return authCode; //if successful authCode wont be "error"
        }

        private string RetrieveWebAuthToken(string username, string password)
        {
            using (WebClient wc = new WebClient())
            {
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("userID", username);
                nvc.Add("password", password);
                nvc.Add("device_id", Guid.NewGuid().ToString()); //Make a random device id

                try
                {
                    wc.UploadValues("https://nexon.net/api/v001/account/login", "POST", nvc);

                    CookieContainer cookies = new CookieContainer();
                    cookies.SetCookies(new Uri("https://nexon.net/api/v001/account/login"), wc.ResponseHeaders["Set-Cookie"]);
                    return cookies.GetCookies(new Uri("https://nexon.net/api/v001/account/login"))["NPPv2"].Value;
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                    return "error";
                }
            }
        }

        public string UpdateCookie(string username, string password) //still needs to be added
        {
            using (var wc = new WebClient())
            {

                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                string data = string.Concat("userID=", username, "&password=", password);

                try
                {
                    wc.UploadString("http://www.nexon.net/api/v001/account/login", data);
                }
                catch (WebException we)
                {
                    if (we.Message.Contains("401"))
                        return "401: WrongInfo";

                    if (we.Message.Contains("429"))
                        return "429: Banned";

                    return "WebError";
                }

                const string SearchKey = "NPPv2=";

                string cookies = wc.ResponseHeaders["Set-Cookie"];
                int pos = cookies.IndexOf(SearchKey);



                if (pos == -1)
                {
                    return "Deactivated";
                }
                else
                {
                    cookies = cookies.Remove(0, pos + SearchKey.Length);
                   return cookies.Substring(0, cookies.IndexOf(";")); //idek
                }
            }

            //return "Success";
        }
    }
}