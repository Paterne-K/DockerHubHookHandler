using DockerHubHookHandler.Models;
using DockerHubHookHandler.Refit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DockerHubHookHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DockerHookController : ControllerBase
    {
        private readonly IConfiguration _config;
        private static ITelegramBotClient botClient;
        private readonly ILogger<DockerHookController> _logger;
        public DockerHookController(IConfiguration config, ILogger<DockerHookController> logger)
        {
            _config = config;
            _logger = logger;
        }

        // GET: api/<DockerHookController>
        [HttpPost]
        public async Task<ActionResult> HandleDockerHubWebHook([FromQuery] string chatId,[FromQuery] string key, [FromBody] Payload payload)
        {
            if (!key.Equals(_config["HUB_KEY"])) return StatusCode(401, "Unauthorized");

            string token = _config["TELEGRAM_TOKEN"];
            botClient = new TelegramBotClient(token);

            string msg = $"Le BUILD du repos {payload.repository.name} a été effectué avec succès.\nDate: {DateTime.Now:dd MM yyyy, HH:mm:ss}";
            msg = $"{msg}\nDescription: {payload.repository.full_description}";

            bool flag = true;
            try
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: msg
                );
            }catch(Exception ex)
            {
                _logger.LogError("An error occured while sending telegram message. Description: {msg}", ex.Message);
                flag = false;
            }

            var status = flag ? "Telegram message SENT" : "Telegram message NOT SENT";
            //validating callback
            var callback = new Callback() { state = "success", description = status };
            var callbackApi = RestService.For<ICallbackApi>(payload.callback_url);
            await callbackApi.SendCallback(callback);

            return Ok();
        }

        
    }
}
