using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
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
		/// <param name="organizationId"></param>
		/// <returns></returns>
		[Route("[action]/{organizationId}")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<ChangeManager>> GetChangeManagersFromOrganization(int organizationId) {
			Organization o = _organizationRepo.GetBy(organizationId);

			if (o == null) {
				return NotFound();
			}

			return o.ChangeManagers;
		}

		/// <summary>
		/// Get a changeManager with a given Id
		/// </summary>
		/// <param name="changeManagerId"></param>
		/// <returns></returns>
		[HttpGet("{changeManagerId}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<ChangeManager> GetChangeManagerById(int changeManagerId) {
			ChangeManager cm = _changeManagerRepo.GetBy(changeManagerId);

			if (cm == null) {
				return NotFound();
			}

			return cm;
		}

		/// <summary>
		/// Upgrade an employee to a changeManager
		/// </summary>
		/// <param name="employeeId"></param>
		/// <returns></returns>
		[HttpPost("{employeeId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpgradeEmployeeToChangeManager(int employeeId) {
			try {

				Employee empl = _employeeRepo.GetBy(employeeId);

				Organization o = _organizationRepo.GetAll().Where(o => o.Employees.Any(e => e.Id == empl.Id)).FirstOrDefault();

				ChangeManager newCm = new ChangeManager(empl);

				o.Employees.Remove(empl);
				_changeManagerRepo.SaveChanges();

				o.ChangeManagers.Add(newCm);
				_changeManagerRepo.SaveChanges();



				return NoContent();
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}


		}

	}
}
