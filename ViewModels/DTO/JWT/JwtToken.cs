using System;
using System.IdentityModel.Tokens.Jwt;

namespace baseWebAPI
{
    /// <summary>
    /// Create JWT Token
    /// </summary>
    public sealed class JwtToken
    {
        private JwtSecurityToken token;

        internal JwtToken(JwtSecurityToken token)
        {
            this.token = token;
        }
        /// <summary>
        /// Validate to
        /// </summary>
        public DateTime ValidTo => token.ValidTo;
        /// <summary>
        /// مقدار
        /// </summary>
        public string Value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}
