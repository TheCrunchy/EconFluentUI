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

namespace FluentUI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class KeenStoresController : ControllerBase
    {
        public KeenStoresController(LiveStoreDataService storeDataService, ILogger<KeenStoresController> logger)
        {
            _storeDataService = storeDataService;
            this.logger = logger;
        }

        private LiveStoreDataService _storeDataService { get; set; }
        private ILogger<KeenStoresController> logger { get; set; }

        [HttpPost]
        public async Task<IActionResult> PostKeenStores([FromBody] object jsonMessage)
        {
            var message = JsonConvert.DeserializeObject<APIMessage>(jsonMessage.ToString());

            //if (message.APIKEY != Program.APIKEY)
            //{
            //    return Unauthorized("API KEY IS NOT VALID");
            //}
            var storeData = JsonConvert.DeserializeObject<List<KeenNPCStoreEntry>>(message.JsonMessage.ToString());
            logger.Log(LogLevel.Information, $"Processing event : {storeData}");
            _storeDataService.AddKeenStoreData(message.ServerId, storeData);
            return Ok();
        }
    }
}
