using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public static class CommonExtensions
    {
        #region String Extensions

        public static String Fill(this String text, params object[] objects)
        {
            return String.Format(text, objects);
        }

        public static bool IsNullOrBlank(this String text)
        {
            return String.IsNullOrWhiteSpace(text);
        }

        public static bool IsNotNullOrBlank(this String text)
        {
            return !IsNullOrBlank(text);
        }

        #endregion

        public static string JsEscape(this string str)
        {
            if (str == null)
            {
                return null;
            }
            else
            {
                str = str.Replace("\'", "\\\'").Replace("\"", "\\\"");
                return str;
            }
        }

        public static string ConvertToCommaDelimitedString<T>(this IEnumerable<T> list)
        {
            string str = list.Aggregate<T, string, string>("", (a, b) => a.ToString() + b + ",", (a) => a.ToString().TrimEnd(',') + "");
            return str;
        }

        public static string ConvertToDelimitedString<T>(this IEnumerable<T> list, string delimiter)
        {
            string str = list.Aggregate<T, string, string>("", (a, b) => a.ToString() + b + delimiter, (a) => a.ToString().TrimEnd(delimiter.ToCharArray()) + "");
            return str;
        }

        public static string Join<T>(this IEnumerable<T> list, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object o in list)
                sb.AppendFormat("{0}{1}", o, separator);
            if (sb.Length > 0)
                sb.Length -= separator.Length;
            return sb.ToString();
        }

        public static List<T> Top<T>(this List<T> list, int topNum)
        {
            if (list.Count > topNum)
            {
                List<T> returnList = list.GetRange(0, topNum);
                return returnList;
            }
            else
            {
                return new List<T>(list);
            }

            //int numItemsToSplitOn = (listCount < i + numItemsPerList) ? listCount - i : numItemsPerList;
            //returnList.Add(list.GetRange(i, numItemsToSplitOn));
        }

        public static List<List<T>> Split<T>(this List<T> list, int numItemsPerList)
        {
            List<List<T>> returnList = new List<List<T>>();
            int listCount = list.Count();
            for (int i = 0; i < listCount; i += numItemsPerList)
            {
                int numItemsToSplitOn = (listCount < i + numItemsPerList) ? listCount - i : numItemsPerList;
                returnList.Add(list.GetRange(i, numItemsToSplitOn));
            }
            return returnList;
        }

        /// <summary>
        /// Determines whether a sequence contains any of the specified elements by using the default equality comparer.        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool ContainsAny<T>(this IEnumerable<T> list, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                if (list.Contains(item))
                    return true;
            }

            return false;
        }
    }
}
