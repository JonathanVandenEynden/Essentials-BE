using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class AdminsController : ControllerBase {

		private readonly IAdminRepository _adminRepo;

		public AdminsController(IAdminRepository adminRepo) {
			_adminRepo = adminRepo;
		}

		/// <summary>
		/// Get all admins
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Policy = "AdminAccess")]
		public IEnumerable<Admin> GetAllAdmins() {
			return _adminRepo.GetAll();
		}
		/// <summary>
		/// Get admin by a given Id
		/// </summary>
		/// <param name="adminId">the id of the admin</param>
		/// <returns>admin obj</returns>
		[HttpGet("{adminId}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Policy = "AdminAccess")]
		public ActionResult<Admin> GetAdminById(int adminId) {
			Admin a = _adminRepo.GetBy(adminId);

			if (a == null) {
				return NotFound("Admin not found");
			}

			return a;
		}

		/// <summary>
		/// Create a new admin
		/// </summary>
		/// <param name="dto"></param>
		// /// <returns></returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize(Policy = "AdminAccess")]
		public IActionResult PostAdmin(AdminDTO dto) {
			try {
				Admin newA = new Admin(dto.FirstName, dto.LastName, dto.Email);

				_adminRepo.Add(newA);

				_adminRepo.SaveChanges();

				return CreatedAtAction(nameof(GetAdminById), new { adminId = newA.Id }, newA);
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{adminId}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[Authorize(Policy = "AdminAccess")]
		public IActionResult DeleteAdmin(int adminId) {
			Admin a = _adminRepo.GetBy(adminId);

			if (a == null) {
				return NotFound("Admin not found");
			}

			_adminRepo.Delete(a);
			_adminRepo.SaveChanges();

			return NoContent();
		}

	}
}
