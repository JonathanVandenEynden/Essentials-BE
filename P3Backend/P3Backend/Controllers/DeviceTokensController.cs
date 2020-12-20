using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model.RepoInterfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Backend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DeviceTokensController : ControllerBase {
        private readonly IDeviceTokensRepository _deviceTokensRepository;

        public DeviceTokensController(IDeviceTokensRepository deviceTokensRepository) {
            _deviceTokensRepository = deviceTokensRepository;
        }

        /// <summary>
        /// Return all device tokens for all users
        /// </summary>
        /// <returns>IEnumerable of tokens (string)</returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = "ChangeManagerAccess")]
        public IEnumerable<string> GetAll() {
            return _deviceTokensRepository.Get().getAllTokens();
        }

        /// <summary>
        /// Send notification to certain users
        /// </summary>
        /// <param name="userids">List of user ids seperated by ","</param>
        /// <param name="title">Title of notification</param>
        /// <param name="message">Body of notification</param>
        [HttpGet("sendNotifications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = "ChangeManagerAccess")]
        public IActionResult SendNotifications(string userids, string title, string message) {
            var users = userids.Split(",").ToList();
            var tokens = _deviceTokensRepository.Get().getTokensByIds(users);

            foreach (var token in tokens) {
                Console.Out.Write(token);
                var client = new RestClient("https://fcm.googleapis.com/fcm/send");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "key=AAAAjQbE4JE:APA91bG6xmBINuyMRO0CIE6IUYW2wT38l3By12RkIcC17sqEznr2yBgZ035VimzzxPWaKMNopW8MS4yH84F6GpVDaOaJZJkhKFFEabGO_YwOGx2kTA39M7bYz3Nae2lr_NWxdcFWi008");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\n \"to\" : \"" + token + "\",\n \"notification\" : {\n     \"body\" : \"" + message + "\",\n     \"title\": \"" + title + "\"\n }\n}", ParameterType.RequestBody);
                var response = client.Execute(request);
                Console.WriteLine(response.Content);
            }
            return NoContent();
        }


        /// <summary>
        /// Posts a new devicetoken
        /// </summary>
        /// <param name="userid">id of user on logged in on the device</param>
        /// <param name="token">unique token of the device</param>
        /// <exception cref="Exception"></exception>
        [HttpPost("{userid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = "employeeAccess")]
        public IActionResult PostPresetSurvey(string userid, string token) {
            var dt = _deviceTokensRepository.Get();
            dt.addToken(userid, token);
            _deviceTokensRepository.Update(dt);
            _deviceTokensRepository.SaveChanges();
            return NoContent();
        }
    }
}