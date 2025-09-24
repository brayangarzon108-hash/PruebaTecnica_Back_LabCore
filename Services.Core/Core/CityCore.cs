using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sst_database.sst_database.DbCore;
using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;
using System.Text.Json;

namespace sst_core.Core
{
    public class CityCore : ICityCore
    {
        private readonly IConfiguration _configuration;
        private readonly ICityRepository _companyData;
        private readonly ILogger<CityCore> _logger;

        public CityCore(
            ILogger<CityCore> logger,
            IConfiguration configuration,
            ICityRepository companyData
            )
        {
            _configuration = configuration;
            _logger = logger;
            _companyData = companyData;
        }

        #region Get

        /// <summary>
        /// Get list
        /// </summary>
        /// <returns>List<UserDto> </returns>
        public async Task<GeneralResponse> GetAll()
        {
            var oReturn = new GeneralResponse();

            try
            {
                var users = new List<CityResponse>();

                var userDb = await _companyData.GetAll();

                userDb?.All(x =>
                {
                    users.Add(new CityResponse(x));
                    return true;
                });

                oReturn.Data = users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                oReturn.Message = "Error Procesando Solicitud";
                oReturn.Status = (int)Enumerations.enumTypeMessageResponse.Error;
            }

            return oReturn;
        }


        /// <summary>
        /// Create or Update
        /// </summary>
        /// <param name="input">UserDto</param>
        /// <returns> List<UserDto></returns>
        public async Task<GeneralResponse> Upsert()
        {
            var oReturn = new GeneralResponse();

            try
            {
                // Extrac Info Service Country
                using var client = new HttpClient();
                var response = await client.GetStringAsync(this._configuration["ParametersGeneral:UrlCountry"].ToString());

                var json = JsonDocument.Parse(response);

                var paises = json.RootElement.EnumerateArray()
                    .Select(p => new CityRequest
                    {
                        IsoCode = p.GetProperty("cca3").GetString(),
                        Name = p.GetProperty("name").GetProperty("common").GetString(),
                        Enabled = true,
                        UserId = this._configuration["User:User"]
                    })
                    .ToList();

                foreach (var pais in paises)
                {
                    var data = await _companyData.GetAllAny(pais.IsoCode);
                    if (!data)
                    {
                        _companyData.UpsertDynamic(pais);
                    }
                }
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
