using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.ChangeTypes;
using P3Backend.Model.DTO_s;
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
	public class ChangeInitiativesController : ControllerBase {

		private readonly IChangeInitiativeRepository _changeRepo;
		private readonly IUserRepository _userRepo;
		private readonly IProjectRepository _projectRepo;
		private readonly IChangeManagerRepository _changeManagerRepo;
		private readonly IEmployeeRepository _employeeRepo;

		public ChangeInitiativesController(
			IChangeInitiativeRepository changeRepo,
			IUserRepository userRepo,
			IProjectRepository projectRepo,
			IChangeManagerRepository changeManagerRepo,
			IEmployeeRepository employeeRepo) {
			_changeRepo = changeRepo;
			_userRepo = userRepo;
			_projectRepo = projectRepo;
			_changeManagerRepo = changeManagerRepo;
			_employeeRepo = employeeRepo;

		}

		///// <summary>
		///// Return the change initiatives for a specific user
		///// </summary>
		///// <param name="userId"></param>
		///// <returns>list of changes for this user</returns>
		/*[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IEnumerable<ChangeInitiative> GetChangeInitiatives(int userId) {
			IEnumerable<ChangeInitiative> changes = _changeRepo.GetForUserId(userId);

			return changes;
		}*/

		/// <summary>
		/// Get the change initiatives applicable for a user
		/// </summary>
		/// <returns></returns>
		[Route("[action]")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Authorize(Policy = "EmployeeAccess")]
		public ActionResult<IEnumerable<ChangeInitiative>> GetChangeInitiativesForEmployee() {

			Employee loggedInEmployee = _employeeRepo.GetByEmail(User.Identity.Name);

			try {
				IEnumerable<ChangeInitiative> changes = _changeRepo.GetForUserId(loggedInEmployee.Id);

				return changes.ToList();
			}
			catch (Exception e) {
				return NotFound(e.Message);
			}
		}

		/// <summary>
		/// Get the change initiatives from a change manager, filters are possible
		/// </summary>
		/// <param name="group"></param>
		/// <param name="progress"></param>
		/// <returns></returns>
		[Route("[action]")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Authorize(Policy = "ChangeManagerAccess")]
		public ActionResult<IEnumerable<ChangeInitiative>> GetChangeInitiativesForChangeManager(string group = null, string progress = null) {
			try {

				ChangeManager loggedInCm = _changeManagerRepo.GetByEmail(User.Identity.Name);

				if (string.IsNullOrEmpty(group) && string.IsNullOrEmpty(progress))
					return loggedInCm.CreatedChangeInitiatives.ToList();

				var changes = loggedInCm.CreatedChangeInitiatives.AsQueryable();

				if (!string.IsNullOrEmpty(group))
					changes = changes.Where(r => r.ChangeGroup.Name.Equals(group));
				if (!string.IsNullOrEmpty(progress)) {
					foreach (var change in changes) {
						Console.WriteLine(change.Progress);
					}
					Console.WriteLine(double.Parse(progress));
					changes = changes.Where(r => r.Progress >= double.Parse(progress));
				}
				return changes.ToList();
			}
			catch (Exception e) {
				return NotFound(e.Message);
			}
		}

		/// <summary>
		/// Return a change initiative by a given id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>change initiative with the given id</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Policy = "ChangeManagerAccess")]
		public ActionResult<ChangeInitiative> GetChangeInitiative(int id) {
			ChangeInitiative ci = _changeRepo.GetBy(id);

			if (ci == null) {
				return NotFound("Change initiative not found");
			}

			return ci;

		}

		/// <summary>
		/// Create new ChangeInitiative
		/// </summary>
		/// <param name="dto">the type-string must be "personal", "economical", "technological" or "organizational". Default organizational</param>
		/// <returns>Created</returns>
		[HttpPost("{projectId}")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Policy = "ChangeManagerAccess")]
		public IActionResult PostChangeInitiative(int projectId, ChangeInitiativeDTO dto) {

			Employee sponsor = _employeeRepo.GetByEmail(dto.Sponsor.Email);

			if (sponsor == null) {
				return NotFound("Sponsor not found");
			}

			IChangeType type = dto.ChangeType switch
			{
				"personal" => new PersonalChangeType(),
				"economical" => new EconomicalChangeType(),
				"technological" => new TechnologicalChangeType(),
				_ => new OrganizationalChangeType(),
			};
			;

			try {

				Project p = _projectRepo.GetBy(projectId);

				ChangeManager loggedInCm = _changeManagerRepo.GetByEmail(User.Identity.Name);


				ChangeInitiative newCi = new ChangeInitiative(dto.Name, dto.Description, dto.StartDate, dto.EndDate, sponsor, type);

				_changeRepo.Add(newCi);
				p.ChangeInitiatives.Add(newCi);
				loggedInCm.CreatedChangeInitiatives.Add(newCi);

				_changeRepo.SaveChanges();

				return CreatedAtAction(nameof(GetChangeInitiative), new {
					id = newCi.Id
				}, newCi);
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// update a change initiative
		/// </summary>
		/// <param name="id"></param>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "ChangeManagerAccess")]
		public IActionResult UpdateChangeInitiative(int id, ChangeInitiativeDTO dto) {

			try {
				ChangeInitiative ciToBeUpdated = _changeRepo.GetBy(id);

				ciToBeUpdated.Update(dto);

				_changeRepo.SaveChanges();

				return CreatedAtAction(nameof(GetChangeInitiative), new { id = id }, ciToBeUpdated);
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// Delete a changeInitiative with a given id
		/// </summary>
		/// <param name="id">id of changeinitiative that has to be deleted</param>
		/// <returns>NoContent</returns>
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Policy = "ChangeManagerAccess")]
		public IActionResult DeleteChangeInitiative(int id) {
			try {
				ChangeInitiative changeInitiative = _changeRepo.GetBy(id);

				if (changeInitiative == null) {
					return NotFound("ChangeInitiative does not exist or is already deleted");
				}

				_changeRepo.Delete(changeInitiative);
				_changeRepo.SaveChanges();
				return NoContent();
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}
	}
}
