using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
//using Excel = Microsoft.Office.Interop.Excel;


namespace Core.Base
{
   
        public static class Extensions
    {
        //public static void ExportToExcel(this List<object> lstExport)
        //{

        //}

        //private static Microsoft.Office.Interop.Excel.Application excel;

        //public static void ToExcel<T>(this IList<T> list, string include = "", string exclude = "", Expression<Func<T, object>> c = null)
        //{
        //    excel = new Microsoft.Office.Interop.Excel.Application();
        //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        //    System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
        //    System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

        //    //Get property collection and set selected property list
        //    PropertyInfo[] props = typeof(T).GetProperties();
        //    List<PropertyInfo> propList = GetSelectedProperties(props, include, exclude);

        //    //Get simple type name
        //    string typeName = GetSimpleTypeName(list);

        //    //Convert list to array for selected properties
        //    object[,] listArray = new object[list.Count + 1, propList.Count];


        //    int colIdx = 0;
        //    int realIndex = 0;

        //    List<int> lstIndexVisible = new List<int>();
        //    foreach (PropertyInfo prop in props)
        //    {
        //        object[] attrs = prop.GetCustomAttributes(true);


        //        foreach (object attr in attrs)
        //        {
        //            FieldInfoAttribute authAttr = attr as FieldInfoAttribute;
        //            if (authAttr != null)
        //            {
        //                if (!authAttr.Visible)
        //                {
        //                    realIndex++;
        //                    continue;
        //                }

        //                lstIndexVisible.Add(realIndex);
        //                listArray[0, colIdx] = authAttr.Title;
        //                colIdx++;
        //                realIndex++;

        //            }
        //        }
        //    }


        //    //Add property name to array as the first row

        //    //foreach (var prop in propList)
        //    //{
        //    //    listArray[0, colIdx] = prop.Name;
        //    //    colIdx++;
        //    //}
        //    //Iterate through data list collection for rows

        //    int rowIdx = 1;

        //    foreach (var item in list)
        //    {
        //        colIdx = 0;
        //        //Iterate through property collection for columns
        //        int realIndx = 0;
        //        foreach (var prop in propList)
        //        {
        //            if (!lstIndexVisible.Any(a => a == realIndx))
        //            {
        //                realIndx++;
        //                continue;
        //            }
        //            //Do property value
        //            listArray[rowIdx, colIdx] = "\t " + prop.GetValue(item, null).ToString();
        //            colIdx++;
        //            realIndx++;
        //        }
        //        rowIdx++;
        //    }
        //    //Processing for Excel
        //    object oOpt = System.Reflection.Missing.Value;
        //    Excel.Application oXL = new Excel.Application();
        //    Excel.Workbooks oWBs = oXL.Workbooks;
        //    //Excel.Workbook oWB = excel.Workbooks.Add();
        //    Excel.Workbook oWB = oWBs.Add(Excel.XlWBATemplate.xlWBATWorksheet);
        //    Excel.Worksheet oSheet = (Excel.Worksheet)oWB.ActiveSheet;
        //    oSheet.Name = typeName;
        //    Excel.Range oRng = oSheet.get_Range("A1", oOpt).get_Resize(list.Count + 1, propList.Count);
        //    oRng.set_Value(oOpt, listArray);
        //    //Open Excel
        //    oXL.Visible = true;
        //}
        private static string GetSimpleTypeName<T>(IList<T> list)
        {
            string typeName = list.GetType().ToString();
            int pos = typeName.IndexOf("[") + 1;
            typeName = typeName.Substring(pos, typeName.LastIndexOf("]") - pos);
            typeName = typeName.Substring(typeName.LastIndexOf(".") + 1);
            return typeName;
        }

        public static string ToJson<T>(this List<T> list)
        {
           return  new JavaScriptSerializer().Serialize(list).Replace(@"\", "");
        }
        
        private static List<PropertyInfo> GetSelectedProperties(PropertyInfo[] props, string include, string exclude)
        {
            List<PropertyInfo> propList = new List<PropertyInfo>();
            if (include != "") //Do include first
            {
                var includeProps = include.ToLower().Split(',').ToList();
                foreach (var item in props)
                {
                    var propName = includeProps.Where(a => a == item.Name.ToLower()).FirstOrDefault();
                    if (!string.IsNullOrEmpty(propName))
                        propList.Add(item);
                }
            }
            else if (exclude != "") //Then do exclude
            {
                var excludeProps = exclude.ToLower().Split(',');
                foreach (var item in props)
                {
                    var propName = excludeProps.Where(a => a == item.Name.ToLower()).FirstOrDefault();
                    if (string.IsNullOrEmpty(propName))
                        propList.Add(item);
                }
            }
            else //Default
            {
                propList.AddRange(props.ToList());
            }
            return propList;
        }
        public static DataTable AsDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        public static List<T> ConvertToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(this DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static DateTime PersianDateToMiladi(this string date)
        {

            int year = Convert.ToInt16( date.Substring(0,4));
            int month = Convert.ToInt16(date.Substring(5, 2));
            int  day = Convert.ToInt16(date.Substring(8, 2));
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(year, month, day, pc);
            return dt;
        }
    


        public static string ToPersianShortDate(this DateTime date)
        {
            PersianCalendar persian = new PersianCalendar();

            string day = persian.GetDayOfMonth(date).ToString("D2");
            string month = persian.GetMonth(date).ToString("D2");
            string year = persian.GetYear(date).ToString("D4");

            return string.Format("{0}/{1}/{2}", year, month, day);
        }
        public static string GetEnumValue(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public static T ComputeFormula<T>(this string strFormula)
        {
            object value = new DataTable()
                .Compute(strFormula, string.Empty);
            T result = (T)Convert.ChangeType(value, typeof(T));
            return result;
        }
       
        
    }
}
