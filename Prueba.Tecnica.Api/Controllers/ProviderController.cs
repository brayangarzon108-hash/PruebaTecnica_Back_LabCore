using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sst_core.Core;
using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace services_backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderCore _Core;
        private readonly ILogger<ProviderController> _logger;

        public ProviderController(IProviderCore Core, ILogger<ProviderController> logger)
        {
            _Core = Core;
            _logger = logger;
        }

        /// <summary>
        /// Get Result All Customer Order
        /// </summary>
        /// <param name="idCustomer"></param>
        /// <returns>List<DynamicFieldModel></returns>
        [HttpGet]
        [Route("GetAllProvider")]
        public async Task<ActionResult<GeneralResponse>> GetAllProvider()
        {
            try
            {
                var data = await _Core.GetAll();
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
        [Route("UpsertProvider")]
        public async Task<ActionResult<GeneralResponse>> UpsertProvider([FromBody] ProviderRequest dataInfo)
        {
            try
            {
                var data = await _Core.Upsert(dataInfo);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error [500]: No se logro realizar la petición del servicio de manera correcta");
            }
        }
    }
}
