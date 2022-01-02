using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace baseWebAPI.ViewModels.Enums
{
    /// <summary>
    /// نوع حقیقی و حقوقی 
    /// </summary>
    public enum CustomerType
    {
        /// <summary>
        /// حقیقی
        /// </summary>
        [Display(Name = "حقیقی")]
        Natural = 2,
        /// <summary>
        /// حقوقی
        /// </summary>
        [Display(Name = "حقوقی")]
        Legal = 1
    }
}
