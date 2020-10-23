using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class AccountController : ControllerBase {

		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IConfiguration _config;
		private readonly IUserRepository _userRepo;

		public AccountController(IUserRepository urepo,
			SignInManager<IdentityUser> SignInManager,
			UserManager<IdentityUser> userManager,
			IConfiguration config) {
			_signInManager = SignInManager;
			_userManager = userManager;
			_config = config;
			_userRepo = urepo;
		}
		/// <summary>
		/// Login a user
		/// </summary>
		/// <param name="dto">the email and password of an existing user</param>
		/// <returns>message</returns>
		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(LoginDTO dto) {
			var user = await _userManager.FindByNameAsync(dto.Email);

			if (user != null) {

				var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
				if (result.Succeeded) {
					// TODO AuthO implementeren
					//string token = GetToken(user);
					return Created("", "User ingelogd");
				}

			}
			return BadRequest("Wrong credentials");
		}

		/// <summary>
		///  Register a user
		/// </summary>
		/// <param name="dto">The user object to be created</param>
		/// <returns>message</returns>
		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<ActionResult<string>> Register(RegisterDTO dto) {

			try {
				IUser checkedUser;
				checkedUser = new Employee(dto.FirstName, dto.LastName, dto.Email);
				IdentityUser user = new IdentityUser { UserName = checkedUser.Email, Email = checkedUser.Email };

				var result = await _userManager.CreateAsync(user, dto.Password);

				if (result.Succeeded) {
					_userRepo.Add(checkedUser);
					_userRepo.SaveChanges();
					// TODO AuthO implementer
					//string token = GetToken(user);
					return Created("", "User aangemaakt");
				}
			}
			catch (Exception e) {
				return BadRequest(e);
			}

			return BadRequest("Something went wrong");

		}

		///// <summary>
		///// checks if an email is available
		///// </summary>
		///// <param name="email"></param>
		///// <returns></returns>
		//[AllowAnonymous]
		//[HttpGet("checkemail")]
		//public async Task<ActionResult<Boolean>> checkUsername(string email) {

		//	var user = await _userManager.FindByNameAsync(email);
		//	return user == null;
		//}

		//private String GetToken(IdentityUser user) { // header.payload.signature
		//											 // Create the token
		//	var claims = new[]
		//	{
		//	  new Claim(JwtRegisteredClaimNames.Sub, user.Email),
		//	  new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
		//	};

		//	var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

		//	var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		//	var token = new JwtSecurityToken(
		//	  null, null,
		//	  claims,
		//	  expires: DateTime.Now.AddMinutes(30),
		//	  signingCredentials: creds);

		//	return new JwtSecurityTokenHandler().WriteToken(token);
		//}

	}
}
