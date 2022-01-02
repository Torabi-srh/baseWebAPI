using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using baseWebAPI.BO;
using baseWebAPI.ViewModels.DTO;
using baseWebAPI.ViewModels.DTO.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace baseWebAPI.Controllers
{
    /// <summary>
    /// سرویس مدیریت اطلاعات بانک اطلاعتی
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SecurityController : baseWebAPI.BaseController
    {
        private readonly IExternalDataResolver _externalDataResolver;
        private readonly SecurityBO _securityBO;
        IOptionsMonitor<baseWebAPI.Option.BridgeOption> _optionsAccessor;

        /// <summary>
        /// سازنده سرویس دهنده اطلاعات سیستم
        /// </summary>
        /// <param name="externalDataResolver"></param>
        /// <param name="options">تنظیمات سیستم</param>
        public SecurityController(IExternalDataResolver externalDataResolver, IOptionsMonitor<baseWebAPI.Option.BridgeOption> options) :base(externalDataResolver)
        {
            _externalDataResolver = externalDataResolver;
            _optionsAccessor = options;
            _securityBO = new SecurityBO(_externalDataResolver,_optionsAccessor);
        }
        /// <summary>
        /// دریافت توکن اعتبار سنجی
        /// </summary>
        /// <param name="login">اطلاعات کاربری</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public UserDataDTO GetToken([FromBody] LoginInput login)
        {
            var token = _securityBO.GetToken(login);
            return token;
        }
    }
}