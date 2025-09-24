using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sst_database.sst_database.DbCore;
using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace Tenkus_core.Core
{
    public class ProviderCustomFieldCore : IProviderCustomFieldCore
    {
        private readonly IConfiguration _configuration;
        private readonly IProviderCustomFieldRepository _companyData;
        private readonly ILogger<ProviderCustomFieldCore> _logger;

        public ProviderCustomFieldCore(
            ILogger<ProviderCustomFieldCore> logger,
            IConfiguration configuration,
            IProviderCustomFieldRepository companyData
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
        public async Task<GeneralResponse> GetAll(int providerId)
        {
            var oReturn = new GeneralResponse();

            try
            {
                var users = new List<ProviderCustomFieldResponse>();

                var userDb = await _companyData.GetAll(providerId);

                userDb?.All(x =>
                {
                    users.Add(new ProviderCustomFieldResponse(x));
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
        public async Task<GeneralResponse> Upsert(ProviderCustomFieldRequest input)
        {
            var oReturn = new GeneralResponse();

            try
            {
                _companyData.UpsertDynamic(input);
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
