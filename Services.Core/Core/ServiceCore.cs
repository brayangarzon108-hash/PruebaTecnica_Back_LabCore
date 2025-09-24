using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sst_database.sst_database.DbCore;
using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace sst_core.Core
{
    public class ServiceCore : IServiceCore
    {
        private readonly IConfiguration _configuration;
        private readonly IPatientsRepository _companyData;
        private readonly IServiceCountryRepository _servivecData;
        private readonly ILogger<ServiceCore> _logger;

        public ServiceCore(
            ILogger<ServiceCore> logger,
            IConfiguration configuration,
            IPatientsRepository companyData,
            IServiceCountryRepository servivecData
            )
        {
            _configuration = configuration;
            _logger = logger;
            _companyData = companyData;
            _servivecData = servivecData;
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
                var services = new List<ServiceResponse>();

                var Dbdata = await _companyData.GetAll(providerId);

                if (Dbdata.Count() > 0)
                {
                    Dbdata?.All(x =>
                {
                    var servicesInfo = new ServiceResponse(x);
                    var dbCountry = _servivecData.GetAllServicesCountry(x.ServiceId);

                    // All Country
                    if (dbCountry.Result.Count() > 0)
                    {
                        servicesInfo.Countries = dbCountry.Result.Select(x => x.Country.Name).ToList();
                    }

                    services.Add(servicesInfo);
                    return true;
                });

                    oReturn.Data = services;
                }
                else
                {
                    oReturn.Message = "No se encontraron registros seleccionados";
                    oReturn.Status = (int)Enumerations.enumTypeMessageResponse.NotFound;
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

        /// <summary>
        /// Create or Update
        /// </summary>
        /// <param name="input">UserDto</param>
        /// <returns> List<UserDto></returns>
        public async Task<GeneralResponse> Upsert(ServiceRequest input)
        {
            var oReturn = new GeneralResponse();

            try
            {
                var data = await _companyData.GetAll(input.ProviderId);
                if (!data.Any(x => x.ServiceId == input.ServiceId))
                {
                    input.UserId = this._configuration["User:User"];
                    int serviceId = _companyData.UpsertDynamic(input);

                    if (serviceId > 0 && input.Countries.Count() > 0)
                    {
                        foreach (var country in input.Countries)
                        {
                            var servicecountry = new ServiceCountryRequest();
                            servicecountry.ServiceId = serviceId;
                            servicecountry.CountryId = country;
                            servicecountry.UserId = input.UserId;
                            _servivecData.UpsertDynamic(servicecountry);
                        }
                    }
                }
                else
                {
                    oReturn.Message = "El registro ya se encuentra dentro del sistema, por favor validar. ";
                    oReturn.Status = (int)Enumerations.enumTypeMessageResponse.BadRequest;
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
