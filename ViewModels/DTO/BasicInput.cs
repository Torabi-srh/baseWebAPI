using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace baseWebAPI.ViewModels.DTO
{
    public class BasicInput : ISqlServerParameterInterface
    {
        /// <summary>
        /// کد
        /// </summary>
        [Required]
        [Display(Name = "کد")]
        public string Code { get; set; }

        /// <summary>
        /// تبدیل به پارام اس کیو ال
        /// </summary>
        public List<SqlParameter> GetSqlParameters()
        {
            return new List<SqlParameter>()
            {
new SqlParameter(nameof(Code),Code),
            };
        }
    }
}
