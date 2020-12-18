using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.Questions;
using P3Backend.Model.RepoInterfaces;

namespace P3Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DeviceTokensController : ControllerBase
    {
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
		[Authorize(Policy = "ChangeManagerAccess")]
		public IEnumerable<string> GetAll() {
			return _deviceTokensRepository.Get().getAllTokens();
		}

		/// <summary>
		/// Return all device tokens for certain users
		/// </summary>
		/// <param name="userids">List of user ids seperated by ","</param>
		/// <returns>IEnumerable of tokens (string)</returns>
		[HttpGet("GetByIds")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Policy = "ChangeManagerAccess")]
		public IEnumerable<string> GetByIds(string userids)
		{
			List<string> users = userids.Split(",").ToList();
			return _deviceTokensRepository.Get().getTokensByIds(users);
		}


		/// <summary>
		/// Posts a new devicetoken
		/// </summary>
		/// <param name="userid">id of user on logged in on the device</param>
		/// <param name="token">unique token of the device</param>
		/// <exception cref="Exception"></exception>
		[HttpPost("{userid})")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
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