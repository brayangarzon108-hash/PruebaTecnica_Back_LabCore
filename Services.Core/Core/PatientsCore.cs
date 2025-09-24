using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sst_database.sst_database.DbCore;
using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace sst_core.Core
{
    public class PatientsCore : IPatientsCore
    {
        private readonly IConfiguration _configuration;
        private readonly IPatientsRepository _companyData;
        private readonly ILogger<PatientsCore> _logger;

        public PatientsCore(
            ILogger<PatientsCore> logger,
            IConfiguration configuration,
            IPatientsRepository companyData
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
        public async Task<GeneralResponse> GetAll(string document)
        {
            var oReturn = new GeneralResponse();

            try
            {
                var services = new List<PatientsResponse>();

                var Dbdata = await _companyData.GetAll(document);

                if (Dbdata.Count() > 0)
                {
                    Dbdata?.All(x =>
                {
                    var servicesInfo = new PatientsResponse(x);
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
        public async Task<GeneralResponse> Upsert(PatientsRequest input)
        {
            var oReturn = new GeneralResponse();

            try
            {
                if (input.Id == 0)
                {
                    var data = await _companyData.GetSpecific(input.TypeDocument, input.Document);
                    if (!data)
                    {
                        input.UserId = this._configuration["User:User"];
                        int serviceId = _companyData.UpsertDynamic(input);
                    }
                    else
                    {
                        oReturn.Message = "El registro ya se encuentra dentro del sistema, por favor validar. ";
                        oReturn.Status = (int)Enumerations.enumTypeMessageResponse.BadRequest;
                    }
                }
                else
                {
                    input.UserId = this._configuration["User:User"];
                    int serviceId = _companyData.UpsertDynamic(input);
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
