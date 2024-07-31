using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Threading;
using System.Text.Json;
using Newtonsoft.Json;
using Econ.Models;
using Econ.Models.Stores;
using Econ.Services;
using Econ.Models.Config;

namespace FluentUI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class KeenStoresController : ControllerBase
    {
        public KeenStoresController(SpaceEngineersServerService serverService, LiveStoreDataService storeDataService, ILogger<KeenStoresController> logger)
        {
            this.serverService = serverService;
            _storeDataService = storeDataService;
            this.logger = logger;
        }

        private SpaceEngineersServerService serverService { get; set; }
        private LiveStoreDataService _storeDataService { get; set; }
        private ILogger<KeenStoresController> logger { get; set; }

        [HttpPost]
        public async Task<IActionResult> PostKeenStores([FromBody] object jsonMessage)
        {
            var message = JsonConvert.DeserializeObject<APIMessage>(jsonMessage.ToString());
            var config = new ServerConfig()
            {
                ServerName = message.ServerName,
                ServerId = message.ServerId,
                AuthKey = message.ServerName
            };

            if (serverService.AddServer(config))
            {
                var storeData = JsonConvert.DeserializeObject<List<KeenNPCStoreEntry>>(message.JsonMessage.ToString());
                logger.Log(LogLevel.Information, $"Processing event : {storeData}");
                _storeDataService.AddKeenStoreData(message.ServerId, storeData);
                return Ok();
            }

            return Unauthorized();
            }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }
    }
}
