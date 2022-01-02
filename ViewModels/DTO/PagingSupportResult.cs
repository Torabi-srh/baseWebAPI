using System.Collections.Generic;

namespace baseWebAPI
{
    /// <summary>
    /// پشتیبانی از صفحه بندی
    /// </summary>
    /// <typeparam name="T">نوع خروجی داده</typeparam>
    public class PagingSupportResult<T> where T : class
    {
        /// <summary>
        /// کل رکورد های موجودی
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// ردیف های داده ها
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// سازنده
        /// </summary>
        public PagingSupportResult()
        {
            Items = new List<T>();
        }
    }
}
