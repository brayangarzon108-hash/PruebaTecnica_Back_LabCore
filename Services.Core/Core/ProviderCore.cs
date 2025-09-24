using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sst_database.sst_database.DbCore;
using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace sst_core.Core
{
    public class ProviderCore : IProviderCore
    {
        private readonly IConfiguration _configuration;
        private readonly IProviderRepository _companyData;
        private readonly IProviderCustomFieldRepository _fieldsData;
        private readonly ILogger<ProviderCore> _logger;

        public ProviderCore(
            ILogger<ProviderCore> logger,
            IConfiguration configuration,
            IProviderRepository companyData,
            IProviderCustomFieldRepository fieldsData
            )
        {
            _configuration = configuration;
            _logger = logger;
            _companyData = companyData;
            _fieldsData = fieldsData;
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
                var dbdata = new List<ProviderResponse>();

                var Dbase = await _companyData.GetAll();

                if (Dbase.Count() > 0)
                {
                    Dbase?.All(x =>
                {
                    var dataInfo = new ProviderResponse(x);
                    dataInfo.CustomFields = new List<ProviderCustomFieldResponse>();

                    var dataFields = _fieldsData.GetAll(x.ProviderId);
                    if (dataFields.Result.Count() > 0)
                    {
                        dataInfo.CustomFields = dataFields.Result.Select(x => new ProviderCustomFieldResponse
                        {
                            CustomFieldId = x.CustomFieldId,
                            FieldName = x.FieldName,
                            FieldValue = x.FieldValue,
                            Enabled = x.Enabled
                        }).ToList();
                    }

                    dbdata.Add(dataInfo);
                    return true;
                });
                    oReturn.Data = dbdata;
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
        public async Task<GeneralResponse> Upsert(ProviderRequest input)
        {
            var oReturn = new GeneralResponse();

            try
            {
                var data = await _companyData.GetAll();
                if (!data.Any(x => x.TaxId == input.TaxId))
                {
                    int providerId = _companyData.UpsertDynamic(input);

                    // Creacion de campos automáticos
                    if (providerId > 0 && input.dynamicFields != null && input.dynamicFields.Count() > 0)
                    {
                        foreach (var field in input.dynamicFields)
                        {
                            field.ProviderId = providerId;
                            _fieldsData.UpsertDynamic(field);
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
