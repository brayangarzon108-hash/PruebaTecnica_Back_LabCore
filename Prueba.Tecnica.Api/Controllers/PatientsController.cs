using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sst_core.Core;
using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace services_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsCore _Core;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientsCore Core, ILogger<PatientsController> logger)
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
        public async Task<ActionResult<GeneralResponse>> GetAllServices(string document)
        {
            try
            {
                var data = await _Core.GetAll(document);
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
        public async Task<ActionResult<GeneralResponse>> UpsertServices([FromBody] PatientsRequest dataInfo)
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
