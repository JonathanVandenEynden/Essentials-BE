using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ChangeManagersController : ControllerBase {

		private readonly IChangeManagerRepository _changeManagerRepo;
		private readonly IOrganizationRepository _organizationRepo;
		private readonly IEmployeeRepository _employeeRepo;

		public ChangeManagersController(IChangeManagerRepository changeManagerRepo,
			IOrganizationRepository organizationRepo,
			IEmployeeRepository employeeRepo) {
			_changeManagerRepo = changeManagerRepo;
			_organizationRepo = organizationRepo;
			_employeeRepo = employeeRepo;
		}

		/// <summary>
		/// Get the changeManagers of an organization
		/// </summary>
		/// <returns></returns>
		[Route("[action]")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Policy = "ChangeManagerAccess")]
		public ActionResult<IEnumerable<ChangeManager>> GetChangeManagersFromOrganization() {
			try {
				Employee loggedInEmployee = _employeeRepo.GetByEmail(User.Identity.Name);

				Organization o = _organizationRepo.GetAll().FirstOrDefault(o => o.ChangeManagers.Any(cm => cm.Id == loggedInEmployee.Id) || o.Employees.Any(e => e.Id == loggedInEmployee.Id));

				return o.ChangeManagers;
			}
			catch {
				return NotFound("Organization not found");
			}
		}

		/// <summary>
		/// Get a changeManager with a given Id
		/// </summary>
		/// <param name="changeManagerId"></param>
		/// <returns></returns>
		[HttpGet("{changeManagerId}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Policy = "AdminAccess")]
		public ActionResult<ChangeManager> GetChangeManagerById(int changeManagerId) {
			ChangeManager cm = _changeManagerRepo.GetBy(changeManagerId);

			if (cm == null) {
				return NotFound("Change manager not found");
			}

			return cm;
		}

		/// <summary>
		/// Get changemanager by a given email
		/// </summary>
		/// <param name="email">the email of the changemanager</param>
		/// <returns>changemanager obj</returns>
		[HttpGet("[action]/{email}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Policy = "AdminAccess")]
		public ActionResult<ChangeManager> GetChangeManagerByEmail(string email) {
			ChangeManager e = _changeManagerRepo.GetByEmail(email);

			if (e == null) {
				return NotFound("Employee not found");
			}

			return e;
		}

		/// <summary>
		/// Upgrade an employee to a changeManager
		/// </summary>
		/// <param name="employeeId"></param>
		/// <returns></returns>
		[HttpPost("{employeeId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize(Policy = "ChangeManagerAccess")]
		public IActionResult UpgradeEmployeeToChangeManager(int employeeId) {
			try {

				Employee empl = _employeeRepo.GetBy(employeeId);

				if (empl == null) {
					return NotFound("Employee not found");
				}

				Organization o = _organizationRepo.GetAll().Where(o => o.Employees.Any(e => e.Id == empl.Id)).FirstOrDefault();

				if (o == null) {
					return NotFound("There is no organization with this employee");
				}

				ChangeManager newCm = new ChangeManager(empl);

				_employeeRepo.Delete(empl);
				_changeManagerRepo.Update(newCm);
				o.ChangeManagers.Add(newCm);
				_organizationRepo.SaveChanges();

				return NoContent();
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}


		}

	}
}
