using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sst_core.Core;
using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace services_backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly ICountryCore _Core;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryCore Core, ILogger<CountryController> logger)
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
        [Route("GetAllCountry")]
        public async Task<ActionResult<GeneralResponse>> GetAllCountry()
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
        [HttpGet]
        [Route("UpsertCountry")]
        public async Task<ActionResult<GeneralResponse>> UpsertCountry()
        {
            try
            {
                var data = await _Core.Upsert();
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
