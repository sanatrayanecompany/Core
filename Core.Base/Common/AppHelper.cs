using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Base
{
    public class AppHelper
    {
        public static string CreateTrackingCharacters(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        public string PasswordKey { get; internal set; }



        public byte[] GetMD5HashData(string data, ref int index)
        {
            index = 0;
            try
            {
                index = 1;
                MD5 md5 = MD5.Create();
                index = 2;
                byte[] bytes = Encoding.Default.GetBytes(data);
                index = 3;
                byte[] resultHashData = md5.ComputeHash(bytes);
                index = 4;
                return resultHashData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public byte[] GetMD5HashData(string data)
        {
           
            try
            {
                
                MD5 md5 = MD5.Create();
               
                byte[] bytes = Encoding.Default.GetBytes(data);
             
                byte[] resultHashData = md5.ComputeHash(bytes);
               
                return resultHashData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static byte[] GetMD5HashData2(string data)
        {
            MD5 md5 = MD5.Create();

            byte[] resultHashData = md5.ComputeHash(Encoding.Default.GetBytes(data));

            return resultHashData;
        }

        public string GetSHA1HashData(string data)
        {
            //create new instance of md5
            SHA1 sha1 = SHA1.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }

        private bool ValidateMD5HashData(string inputData, string storedHashData)
        {
            //hash input text and save it string variable
            string getHashInputData = inputData;

            if (string.Compare(getHashInputData, storedHashData) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateSHA1HashData(string inputData, string storedHashData)
        {
            //hash input text and save it string variable
            string getHashInputData = GetSHA1HashData(inputData);

            if (string.Compare(getHashInputData, storedHashData) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //public static ModelDbAttribute GetModelDbAttr<T>()
        //{
        //    var mdAttribute = typeof(T).GetCustomAttributes(
        //        typeof(ModelDbAttribute), true
        //    ).FirstOrDefault() as ModelDbAttribute;
        //    if (mdAttribute != null)
        //    {
        //        return mdAttribute;
        //    }
        //    return null;
        //}


        public static List<SpParametersInfo> GetParametersInfo(object parameters)
        {
            List<SpParametersInfo> result = new List<SpParametersInfo>();



            Type mytype = parameters.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(mytype.GetProperties());

            var lstFieldName = props.Select(p => p.Name).ToList();

            Type type = parameters.GetType();

            FieldInfo[] fields = type.GetFields();
            foreach (var p in props)
            {
                object value = null;
                string name = p.Name;
                object temp = p.GetValue(parameters);
                if (temp is int)
                {
                    value = (int)temp;
                }
                else if (temp is string)
                {
                    value = temp as string;
                }
                result.Add(new SpParametersInfo()
                {
                    Name = name,
                    Value = value
                });
            }
            return result;
        }

        public AppHelper()
        {
            PasswordKey = GetPasswordKey();
        }
        public bool ComparerPassword(byte[] dbPass, byte[] entryPass)
        {
            HashAlgorithm ha = HashAlgorithm.Create();
            string strDbPass = BitConverter.ToString(dbPass);
            string strEntryPass = BitConverter.ToString(entryPass);
            if (strDbPass == strEntryPass)
                return true;
            return false;
        }

        public byte[] StrToMD5Hash(string strKey, string password)
        {
            var byteKey = System.Text.Encoding.UTF8.GetBytes(strKey);
            var md5 = new HMACMD5(byteKey);
            var bytePass = System.Text.Encoding.UTF8.GetBytes(password);
            var PassHash = md5.ComputeHash(bytePass);
            return PassHash;
        }

        private string GetPasswordKey()
        {
            //read db
            return "HOOO";
        }

    }
}
