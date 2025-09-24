using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sst_core.Core;
using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace services_backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ServiceCountryController : ControllerBase
    {
        private readonly IServiceCountryCore _Core;
        private readonly ILogger<ServiceCountryController> _logger;

        public ServiceCountryController(IServiceCountryCore Core, ILogger<ServiceCountryController> logger)
        {
            _Core = Core;
            _logger = logger;
        }

        /// <summary>
        /// Get result 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>List<DynamicFieldModel></returns>
        [HttpGet]
        [Route("GetAllServicesCountry")]
        public async Task<ActionResult<GeneralResponse>> GetAllServices(int serviceId)
        {
            try
            {
                var data = await _Core.GetAll(serviceId);
                return StatusCode(data.Status, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error [500]: No se logro realizar la petición del servicio de manera correcta");
            }
        }


        /// <summary>
        /// Get result 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>List<DynamicFieldModel></returns>
        [HttpGet]
        [Route("GetAllServicesCountrySummary")]
        public async Task<ActionResult<GeneralResponse>> GetAllServicesCountrySummary()
        {
            try
            {
                var data = await _Core.GetAllSummary();
                return StatusCode(data.Status, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error [500]: No se logro realizar la petición del servicio de manera correcta");
            }
        }

        /// <summary>
        /// Upsert Data
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns>List<DynamicFieldModel></returns>
        [HttpPost]
        [Route("UpsertServices")]
        public async Task<ActionResult<GeneralResponse>> UpsertServices([FromBody] ServiceCountryRequest dataInfo)
        {
            try
            {
                var data = await _Core.Upsert(dataInfo);
                return StatusCode(data.Status, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error [500]: No se logro realizar la petición del servicio de manera correcta");
            }
        }
    }
}
