using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
		/// <returns>token</returns>
		[AllowAnonymous]
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> Login(LoginDTO dto) {
			try {
				var user = await _userManager.FindByNameAsync(dto.Email);

				if (user != null) {
					var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
					if (result.Succeeded) {
						IUser dbUser = _userRepo.GetByEmail(user.UserName);
						string token = await GetToken(user, dbUser.Id);
						return Ok(token);
					}
				}
				return BadRequest("Wrong credentials");
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}

		}

		/// <summary>
		///  Register a user
		/// </summary>
		/// <param name="dto">The user object to be created</param>
		/// <returns>token</returns>
		[AllowAnonymous]
		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> Register(RegisterDTO dto) {
			try {
				IUser checkedUser;
				checkedUser = new Employee(dto.FirstName, dto.LastName, dto.Email);
				IdentityUser user = new IdentityUser { UserName = checkedUser.Email, Email = checkedUser.Email };

				var result = await _userManager.CreateAsync(user, dto.Password);

				if (result.Succeeded) {
					_userRepo.Add(checkedUser);
					_userRepo.SaveChanges();
					string token = await GetToken(user, checkedUser.Id);
					return Created("", token);
				}
				else
					return BadRequest("Could not register");
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// checks if an email is available
		/// </summary>
		/// <param name="email"></param>
		/// <returns>True when email is available (not in use), false when it is not</returns>
		[AllowAnonymous]
		[HttpPost("checkemail")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<bool>> checkUsername(string email) {
			try {
				var user = await _userManager.FindByNameAsync(email);
				return user == null;
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// generates a Bearer-token for the user
		/// </summary>
		/// <param name="user"></param>
		/// <param name="id"></param>
		/// <returns>a valid Bearer-token</returns>
		private async Task<string> GetToken(IdentityUser user, int id) {
			var roleClaims = await _userManager.GetClaimsAsync(user);
			var claims = new List<Claim> {
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
				new Claim(JwtRegisteredClaimNames.NameId, id.ToString())
			};
			claims.AddRange(roleClaims);
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(null, null, claims, expires: DateTime.Now.AddDays(14), signingCredentials: creds);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

	}
}
