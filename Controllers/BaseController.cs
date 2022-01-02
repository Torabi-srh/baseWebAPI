using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using baseWebAPI.DataLayer;
using System.Collections.Generic;

namespace baseWebAPI
{
    /// <summary>
    /// کنترل پایه برای ارث بری کنترلر ها
    /// </summary>
    [ApiController]

    public class BaseController : ControllerBase
    {
        private readonly IExternalDataResolver _externalDataResolver;
        /// <summary>
        /// سازنده
        /// </summary>
        public BaseController(IExternalDataResolver externalDataResolver)
        {
            _externalDataResolver = externalDataResolver;
        }
        /// <summary>
        /// بررسی وضعیت مدل و برگشت خطاهای احتمالی
        /// </summary>
        /// <param name="modelState">مدل وارد شده به سرویس</param>
        /// <returns>در صورت داشتن خطا این لیست پر می شود</returns>
        protected List<MethodError> GetModelStateErrors(ModelStateDictionary modelState)
        {
            List<MethodError> lstErrors = new List<MethodError>();
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var value = ModelState[key];
                    foreach (var error in value.Errors)
                    {
                        lstErrors.Add(new MethodError()
                        {
                            Code = -1,
                            Description = "مدل ارسال معتبر نمی باشد",
                            Title = "مدل ارسال معتبر نمی باشد",
                            FiledName = key
                        });
                    }
                }
            }
            return lstErrors;
        }
    }
}
