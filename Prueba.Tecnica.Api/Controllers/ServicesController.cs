using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sst_core.Core;
using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace services_backend.Controllers
{
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceCore _Core;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IServiceCore Core, ILogger<ServicesController> logger)
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
        [Route("GetAllServices")]
        public async Task<ActionResult<GeneralResponse>> GetAllServices(int providerId)
        {
            try
            {
                var data = await _Core.GetAll(providerId);
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
        public async Task<ActionResult<GeneralResponse>> UpsertServices([FromBody] ServiceRequest dataInfo)
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
