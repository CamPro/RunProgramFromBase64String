using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            byte[] bt1 = File.ReadAllBytes("..\\..\\..\\Empty\\bin\\Release\\Empty.exe");

            bt1 = new System.Net.WebClient().DownloadData();

            string strBase64 = Convert.ToBase64String(bt1);

            File.WriteAllText("bytestring.txt", strBase64);

            strBase64 = File.ReadAllText("bytestring.txt");

            byte[] bt2 = Convert.FromBase64String(strBase64);

            Assembly a = Assembly.Load(bt2);
            MethodInfo m = a.EntryPoint;
            object o = a.CreateInstance(m.Name);
            m.Invoke(o, null);

        }
    }
}
