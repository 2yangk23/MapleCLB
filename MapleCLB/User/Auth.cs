using System;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;

namespace MapleCLB.User
{
    class Auth
    {
        byte maxRetry = 10;

        public string loginAuth(string user, string Pass)
        {
            string authCode = getAuth(user, Pass);

            for (int i = 0; i < maxRetry && authCode == "error"; ++i)
            {
                Program.writeLog(("Retrying auth in 2 minutes..."));
                Thread.Sleep(120000);

                authCode = getAuth(user, Pass);
            }

            return authCode; //if successful authCode wont be "error"
        }


        public string getAuth(string LoginID, string LoginPW)
        {
            HttpWebResponse get = Post("http://www.nexon.net/api/v001/account/login", "userID=" + LoginID + "&password=" + LoginPW);
            if (get == null) {
                return "error";
            }
            string a = get.Headers.ToString();
            string b = a;
            a = a.Remove(0, a.IndexOf("NP12"));
            a = a.Remove(a.IndexOf(";"));
            b = b.Remove(0, b.IndexOf("authToken=") + 10);
            b = b.Remove(b.IndexOf(";"));
            get.Close();

            return a;
        }

        public static HttpWebResponse Post(string url, string data)
        {
            HttpWebResponse WebResp = null;
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(url);
                string[] list = { "108.62.137.45:60265", "108.62.142.240:60265", "108.62.148.135:60265", "108.62.148.204:60265", "108.62.164.232:60265", "108.62.169.29:60265", "108.62.183.206:60265", "108.62.193.25:60265", "108.62.229.133:60265", "108.62.229.140:60265", "108.62.232.206:60265", "108.62.232.229:60265", "108.62.241.50:60265", "108.62.246.122:60265", "108.62.246.139:60265", "108.62.53.59:60265", "108.62.72.20:60265", "108.62.94.140:60265", "173.208.9.180:60265", "173.208.9.47:60265", "173.234.117.188:60265", "23.19.108.165:60265", "23.19.108.180:60265", "23.19.109.172:60265", "23.19.109.4:60265", "23.19.213.145:60265", "23.19.213.166:60265", "23.19.224.226:60265", "23.19.36.112:60265", "23.19.36.190:60265", "23.19.37.101:60265", "23.19.37.147:60265", "23.19.4.111:60265", "23.19.4.76:60265", "23.19.5.245:60265", "23.19.5.77:60265", "23.19.74.160:60265", "23.19.74.184:60265", "23.19.96.157:60265", "31.214.150.153:60265", "31.214.151.118:60265", "46.251.230.80:60265", "46.251.231.79:60265", "46.251.232.107:60265", "46.251.232.59:60265", "46.251.233.110:60265", "46.251.233.239:60265", "64.120.85.7:60265" };
                string toproxy = list[new Random().Next(list.Length) - 1];
                IWebProxy z = new WebProxy(toproxy);
                z.Credentials = new NetworkCredential("inzems", "MzRsrgwE");
                //WebReq.Proxy = z;
                WebReq.Method = "POST";
                WebReq.ContentType = "application/x-www-form-urlencoded";
                WebReq.ContentLength = buffer.Length;
                Stream PostData = WebReq.GetRequestStream();
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();
                WebResp = (HttpWebResponse)WebReq.GetResponse();
            }
            catch (Exception)
            {
                return null;
            }
            return WebResp;
        }
    }
}