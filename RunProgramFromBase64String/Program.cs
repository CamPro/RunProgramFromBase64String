using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunProgramFromBase64String
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            WebClient client = new WebClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36");

            // get byte local
            byte[] bt1 = File.ReadAllBytes("..\\..\\..\\Empty\\bin\\Release\\Empty.exe");
            // get byte online
            bt1 = client.DownloadData("https://github.com/CamPro/RunProgramFromBase64String/raw/main/Empty/bin/Release/Empty.exe");

            // byte -> base64
            string strBase64 = Convert.ToBase64String(bt1);

            // base64 -> byte
            byte[] bt2 = Convert.FromBase64String(strBase64);

            // run program
            Assembly a = Assembly.Load(bt2);
            MethodInfo m = a.EntryPoint;
            object o = a.CreateInstance(m.Name);
            m.Invoke(o, null);

        }
    }
}
