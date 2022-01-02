using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace baseWebAPI
{
    /// <summary>
    /// Token security key
    /// </summary>
    public static class JwtSecurityKey
    {
        /// <summary>
        /// Create token
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
