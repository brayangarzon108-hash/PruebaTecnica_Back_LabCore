using Microsoft.AspNetCore.Mvc;
using StoreSample.Domain.Model.General;
using Tenkuscore.Core;

namespace tenkus_services_backend.Controllers
{
    [Route("api/[controller]")]

    public class PublicController : ControllerBase
    {
        private readonly IManagementCore _managementCore;
        private readonly ILogger<PublicController> _logger;

        public PublicController(IManagementCore managementCore, ILogger<PublicController> logger)
        {
            _managementCore = managementCore;
            _logger = logger;
        }

        /// <summary>
        /// Generate token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        [HttpGet]
        [Route("GenerateToken")]
        public async Task<ActionResult<GeneralResponse>> GenerateToken(string user)
        {

            return Ok(_managementCore.GenerateToken(user));
        }
    }
}
