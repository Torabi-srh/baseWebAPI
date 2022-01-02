using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace baseWebAPI
{
    /// <summary>
    ///  
    /// </summary> 
    /// <returns></returns>
    public static class Helpers
    {
        /// <summary>
        /// Check if text is numeric
        /// </summary>
        /// <param text="string">text</param>
        /// <returns></returns>
        public static bool IsNumeric(this string text)
        {
            if (text == "")
            {
                return true;
            }
            double test;
            return double.TryParse(text, out test);
        }
        /// <summary>
        /// Convert object to json 
        /// </summary>
        /// <param obj="object">obj</param>
        /// <returns>string</returns>
        public static string OTJ(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// Convert json to object
        /// </summary>
        /// <param str="string">str</param>
        /// <returns>object</returns>
        public static object JTO(string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(str);
        }
    }
}
