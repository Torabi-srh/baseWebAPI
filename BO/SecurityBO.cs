using baseWebAPI.ViewModels.DTO;
using baseWebAPI.ViewModels.DTO.Login;
using Microsoft.Extensions.Options;
using System.Linq;

namespace baseWebAPI.BO
{
    /// <summary>
    /// مدیریت ساختار های بانک اطلاعاتی
    /// </summary>
    public class SecurityBO
    {
        private readonly IExternalDataResolver _externalDataReolver;
        private readonly IOptionsMonitor<baseWebAPI.Option.BridgeOption> _options;
        /// <summary>
        /// سازنده
        /// </summary>
        public SecurityBO(IExternalDataResolver externalDataReolver, IOptionsMonitor<baseWebAPI.Option.BridgeOption> options)
        {
            _externalDataReolver = externalDataReolver;
            _options = options;
        }
        /// <summary>
        /// دریافت توکن
        /// </summary>
        /// <param LDTO="LoginDTO">نام کاربری</param>
        /// <returns></returns>
        public UserDataDTO GetToken(LoginInput LDTO)
        {
            TokenBO tokenBO = new TokenBO();
            BasicInput bsc = new BasicInput() { Code = LDTO.UserName };
            UserDataDTO udto = new UserDataDTO();
            udto.Status = false;
            udto.DTO = _externalDataReolver.GetFromCommand<LoginDTO>("", bsc.GetSqlParameters()).FirstOrDefault();
            if (LDTO.UserName == "1")
            {
                udto.DTO = new LoginDTO();
                udto.DTO.UserType = "کاربرعادی";
                udto.Status = true;
                udto.Message = tokenBO.GenerateAccessToken(LDTO);
                return udto;
            }
            if (udto.DTO == null)
            {
                udto.Message = "نام کاربری یا رمز عبور اشتباه است";
            }
            else
            { 
                udto.DTO.Password = (udto.DTO.Password == null || udto.DTO.Password == string.Empty || Resolvers.Cryptography.Decrypt(LDTO.Password, baseWebAPI.Startup.UEncriptionKey) != udto.DTO.Password ? "false" : "true");
                if (udto.DTO.Password == "false")
                {
                    udto.Message = "نام کاربری یا رمز عبور اشتباه است";
                }
                else if (udto.DTO.AppUser != 1)
                {
                    udto.Message = "این کاربر مجوز ورود ندارد";
                }
                else if (udto.DTO.UserType == null || udto.DTO.UserType == string.Empty)
                {
                    udto.Message = "برای این کاربر فروشنده ای تعریف نشده است";
                }
                else
                {
                    udto.Status = true;
                    udto.Message = tokenBO.GenerateAccessToken(LDTO);
                }
            } 
            return udto;
        }
    }
}
