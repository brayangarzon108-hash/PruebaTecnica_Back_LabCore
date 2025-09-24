using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StoreSample.Domain.Model.General;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tenkuscore.Core
{
    public class ManagementCore : IManagementCore
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ManagementCore> _logger;

        public ManagementCore(
            ILogger<ManagementCore> logger,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        #region Token

        /// <summary>
        /// Generate token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        public GeneralResponse GenerateToken(string user)
        {
            var oReturn = new GeneralResponse();

            try
            {
                var userDb = this._configuration["User:User"];

                if (userDb != user)
                {
                    oReturn.Message = "Invalid User";
                    oReturn.Status = (int)Enumerations.enumTypeMessageResponse.NotFound;
                    return oReturn;
                }


                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWT:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[] {
                        new Claim("UserName", user)
                    };

                var currentDate = DateTime.UtcNow;

                var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                    claims: claims,
                    expires: currentDate.AddMinutes(int.Parse(this._configuration["JWT:Expire"])),
                    signingCredentials: credentials);


                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                oReturn.Data = tokenString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                oReturn.Message = "Error Procesando Solicitud";
                oReturn.Status = (int)Enumerations.enumTypeMessageResponse.Error;
            }

            return oReturn;
        }

        #endregion

    }
}
