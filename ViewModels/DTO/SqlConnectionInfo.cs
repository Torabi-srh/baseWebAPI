using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace baseWebAPI.ViewModels.DTO
{
    /// <summary>
    /// اطلاعات مورد نظر برای دریافت اطلاعات از بانک اطلاعاتی
    /// </summary>
    public class SqlConnectionInfo
    {
        /// <summary>
        /// آدرس سرور وصل شدن به بانک اطلاعاتی
        /// </summary>
        public string ServerAddress { get; set; }
        /// <summary>
        /// عنوان دیتا بیس
        /// </summary>
        public string DatabaseName { get; set; }
        /// <summary>
        /// نام کاربری دیتا بیس
        /// </summary>
        public string DatabaseUsername { get; set; }
    }
}
