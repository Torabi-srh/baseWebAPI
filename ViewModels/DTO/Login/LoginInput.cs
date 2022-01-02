using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace baseWebAPI.ViewModels.DTO.Login
{
    /// <summary>
    /// اطلاعات مربوط به ورود به سیستم
    /// </summary>
    public class LoginInput
    {
        /// <summary>
        /// IMEI
        /// </summary>
        [Required]
        [Display(Name = "IMEI")]
        public string IMEI { get; set; }
        /// <summary>
        /// 1: موبایل    2: تبلت
        /// </summary>
        [Required]
        [Display(Name = "نوع وسيله")]
        public int DeviceType { get; set; }

        /// <summary>
        /// Encrypted UserName
        /// </summary>
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        /// <summary>
        /// Encrypted Password
        /// </summary>
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public List<SqlParameter> GetSqlParameters()
        {
            return new List<SqlParameter>()
            {
                new SqlParameter(nameof(IMEI),IMEI),
                new SqlParameter(nameof(DeviceType),DeviceType),
                new SqlParameter(nameof(UserName),UserName),
                new SqlParameter(nameof(Password),Password),
            };
        }
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            return Helpers.OTJ(this);
        }
    }
}