using System;
using System.Collections.Generic;

namespace Core.Base
{
    public class Json
    {
        public bool success { get; set; }

        public object data { get; set; }

        public IEnumerable<string> error { get; set; }

        public IEnumerable<string> warning { get; set; }

        public IEnumerable<string> message { get; set; }

        public Json(bool success)
        {
            Initialize(null, success, null, null);
        }

        public Json(object data)
        {
            Initialize(data, true, null, null, null);
        }

        public Json(bool success, string text)
        {
            if (success)
                Initialize(null, true, null, null, new string[] { text });
            else
                Initialize(null, false, new string[] { text }, null, null);
        }

        public Json(bool success, IEnumerable<string> text)
        {
            if (success)
                Initialize(null, true, null, null, text);
            else
                Initialize(null, false, text, null, null);
        }

        public Json(object data, bool success, IEnumerable<string> error = null, IEnumerable<string> warning = null, IEnumerable<string> message = null)
        {
            Initialize(data, success, error, warning, message);
        }

        private void Initialize(object data, bool success, IEnumerable<string> error = null, IEnumerable<string> warning = null, IEnumerable<string> message = null)
        {
            if (data != null)
            {
                Type t = data.GetType();
                if (!t.IsGenericType)
                {
                    List<object> temp = new List<object>();
                    temp.Add(data);

                    data = temp;
                }
            }

            this.data = data;
            this.success = success;
            this.error = error ?? new string[] { };
            this.warning = warning ?? new string[] { };
            this.message = message ?? new string[] { };
        }
    }
}