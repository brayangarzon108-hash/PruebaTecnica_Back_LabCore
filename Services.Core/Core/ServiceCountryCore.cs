using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sst_database.sst_database.DbCore;
using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace sst_core.Core
{
    public class ServiceCountryCore : IServiceCountryCore
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceCountryRepository _companyData;
        private readonly ILogger<ServiceCountryCore> _logger;

        public ServiceCountryCore(
            ILogger<ServiceCountryCore> logger,
            IConfiguration configuration,
            IServiceCountryRepository companyData
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
        public async Task<GeneralResponse> GetAll(int serviceId)
        {
            var oReturn = new GeneralResponse();

            try
            {
                var users = new List<ServiceCountryResponse>();

                var userDb = await _companyData.GetAllServicesCountry(serviceId);

                userDb?.All(x =>
                {
                    users.Add(new ServiceCountryResponse(x));
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
        /// Get list
        /// </summary>
        /// <returns>List<UserDto> </returns>
        public async Task<GeneralResponse> GetAllSummary()
        {
            var oReturn = new GeneralResponse();

            try
            {
                var users = new List<ServiceCountryResponse>();

                var userDb = await _companyData.GetAllServicesCountry(0);

                // Indicador 1: Cantidad de proveedores (clientes) por país
                var clientsByCountry = userDb
                    .GroupBy(sc => sc.CountryId)
                    .Select(g => new ClientsByCountryDto
                    {
                        CountryId = g.Key,
                        CountryName = g.First().Country.Name,
                        ClientsCount = g
                            .Select(sc => sc.Service.ProviderId)
                            .Distinct()
                            .Count()
                    })
                    .ToList();

                // Indicador 2: Cantidad de servicios por país
                var servicesByCountry = userDb
                    .GroupBy(sc => sc.CountryId)
                    .Select(g => new ServicesByCountryDto
                    {
                        CountryId = g.Key,
                        CountryName = g.First().Country.Name,
                        ServicesCount = g
                            .Select(sc => sc.ServiceId)
                            .Distinct()
                            .Count()
                    })
                    .ToList();

                var summary = new ReportSummaryDto
                {
                    ClientsByCountry = clientsByCountry,
                    ServicesByCountry = servicesByCountry
                };

                oReturn.Data = summary;
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
        public async Task<GeneralResponse> Upsert(ServiceCountryRequest input)
        {
            var oReturn = new GeneralResponse();

            try
            {
                var data = await _companyData.GetAllServicesCountry(input.ServiceId);
                if (!data.Any(x => x.ServiceId == input.ServiceId && x.CountryId == input.CountryId))
                {
                    _companyData.UpsertDynamic(input);
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
