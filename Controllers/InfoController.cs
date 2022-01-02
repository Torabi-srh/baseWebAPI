using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using baseWebAPI.BO;
using baseWebAPI.ViewModels.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace baseWebAPI.Controllers
{
    /// <summary>
    /// سرویس مدیریت اطلاعات بانک اطلاعتی
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class InfoController : baseWebAPI.BaseController
    {
        private readonly IExternalDataResolver _externalDataResolver;
        private readonly InfoBO _infoBO;
        /// <summary>
        /// سازنده سرویس دهنده اطلاعات سیستم
        /// </summary>
        /// <param name="externalDataResolver"></param>
        public InfoController(IExternalDataResolver externalDataResolver):base(externalDataResolver)
        { 
            _externalDataResolver = externalDataResolver;
            _infoBO = new InfoBO(_externalDataResolver);
        }
        /// <summary>
        /// دریافت آدرس بانک اطلاعاتی
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public SqlConnectionInfo ConnectionInfo()
        {
            
            Console.WriteLine(_externalDataResolver.ConnectionString);
            return _infoBO.ConnectionInfo(_externalDataResolver.ConnectionString);
        }
        /// <summary>
        /// تست ارتباط با سرور
        /// </summary>
        /// <returns>درصورتیکه اتصال برقرار باشد مقدار true و در غیر اینصورت مقدار false بر میگرداند</returns>
        [HttpGet("[action]")]
        public bool TestConnection()
        {
            return _infoBO.TestConnection();
        }

        /// <summary>
        /// پسورد دیتابیس رو بصورت رمز شده بر میگرداند
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public string EncryptDatabasePassword([FromBody] string password)
        {
            return PersianUtilCore.Security.Cryptography.EncryptByMD5(password, baseWebAPI.Startup.EncriptionKey);
        }

        /// <summary>
        /// پسورد کاربر رو بصورت رمز شده بر میگرداند
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public string EncryptUserPassword([FromBody] string password)
        { 
            return Resolvers.Cryptography.Encrypt(password, baseWebAPI.Startup.UEncriptionKey);
        }
        /// <summary>
        /// دریافت زمان سرور
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public DateTime GetServerTime()
        {
            return DateTime.Now;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        } 
    }
}