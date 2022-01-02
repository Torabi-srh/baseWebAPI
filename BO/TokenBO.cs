using baseWebAPI.ViewModels.DTO.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace baseWebAPI.BO
{
    //https://www.blinkingcaret.com/2018/05/30/refresh-tokens-in-asp-net-core-web-api/
    /// <summary>
    /// سازنده توکن
    /// </summary>
    public class TokenBO
    {
        /// <summary>
        /// کلید توکن
        /// </summary>
        public static string BWATokenSecretKey = "B#$FE3f2fv";
        /// <summary>
        /// کلید رمزنگاری
        /// </summary>
        public static string BWATokenEncryptionKey = "B#$FE3f2fv";
        /// <summary>
        /// عنوان سفارش دهنده
        /// </summary>
        public static string AudienceTitle = "BWAJwt.Security.Bearer";
        /// <summary>
        /// عنوان
        /// </summary>
        public static string IssuerTitle = "BWAJwt.Security.Bearer";
        /// <summary>
        /// تاریخ انقضا
        /// </summary>
        public static int ExpireInMinute = (24 * 60);
        /// <summary>
        /// سازنده توکن
        /// </summary>
        public TokenBO()
        {
        }
        /// <summary>
        /// ایجاد توکن اعتبار سنجی بر اساس مشخصات کاربر
        /// </summary>
        /// <param name="userData">اطلاعات کاربر</param>
        /// <returns></returns>
        public string GenerateAccessToken(LoginInput userData)
        {
            return new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create(BWATokenSecretKey))
                .AddEncryptionKey(BWATokenEncryptionKey)
                .AddIssuer(IssuerTitle)
                .AddAudience(AudienceTitle)
                .AddSubject(userData.IMEI)
                .AddClaim(ClaimTypes.UserData, userData.Password)
                .AddClaim(ClaimTypes.Name, userData.UserName)
                .AddClaim(ClaimTypes.SerialNumber, userData.IMEI)
                .AddClaim(ClaimTypes.Role, userData.DeviceType.ToString())
                .AddExpiry(ExpireInMinute)
                .Build();
        }
    }
}