using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business
{
    public class Constants
    {
        public static AuthInfo AuthInfo
        {
            get
            {
                return new AuthInfo() { username = "FANAVA", password = "123" };
            }

        }

        public static string LocalIP
        {
            get { return "172.20.10.2"; }
        }

        public static string serial
        {
            get { return "139710221001"; }
        }
        public static string invoiceNo
        {
            get { return "10001"; }
        }
        public static string STAN
        {
            get {
                Random generator = new Random();
                String value = generator.Next(1, 99999).ToString("D5");
                return value;
            }
        }
        public static string merchantID
        {
            get { return "71098358"; }
        }
        public static string terminalID
        {
            get { return "71104976"; }
        }
        public static string mobile
        {
            get { return  "09123206492";}
        }
        public static string date
        {
            get
            {
                return DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            }
        }
        public static string time
        {
            get
            {
                return DateTime.Now.Hour.ToString() +
                       DateTime.Now.Minute.ToString() +
                       DateTime.Now.Second.ToString();
            }
        }

    }
}

