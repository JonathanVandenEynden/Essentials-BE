using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.ChangeTypes;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class ChangeInitiativesController : ControllerBase {

		private readonly IChangeInitiativeRepository _changeRepo;
		private readonly IUserRepository _userRepo;
		private readonly IProjectRepository _projectRepo;
		private readonly IChangeManagerRepository _changeManagerRepo;

		public ChangeInitiativesController(
			IChangeInitiativeRepository changeRepo,
			IUserRepository userRepo,
			IProjectRepository projectRepo,
			IChangeManagerRepository changeManagerRepo) {
			_changeRepo = changeRepo;
			_userRepo = userRepo;
			_projectRepo = projectRepo;
			_changeManagerRepo = changeManagerRepo;

		}

		/// <summary>
		/// Return the change initiatives for a specific user
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>list of changes for this user</returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IEnumerable<ChangeInitiative> getChangeInitiatives(int userId) {
			IEnumerable<ChangeInitiative> changes = _changeRepo.GetForUserId(userId);

			return changes;
		}

		/// <summary>
		/// Return a change initiative by a given id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>change initiative with the given id</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<ChangeInitiative> GetChangeInitiative(int id) {
			ChangeInitiative ci = _changeRepo.GetBy(id);

			if (ci == null) {
				return NotFound();
			}

			return ci;

		}

		/// <summary>
		/// Create new ChangeInitiative
		/// </summary>
		/// <param name="dto">the type-string must be "personal", "economical", "technological" or "organizational". Default organizational</param>
		/// <returns>Created</returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult PostChangeInitiative(int projectId, int changeManagerId, ChangeInitiativeDTO dto) {
			IUser sponsor = _userRepo.GetBy(dto.SponsorId);

			if (sponsor == null) {
				return NotFound();
			}

			IChangeType type;

			switch (dto.ChangeType) {
				case "personal":
				type = new PersonalChangeType();
				break;
				case "economical":
				type = new EconomicalChangeType();
				break;
				case "technological":
				type = new TechnologicalChangeType();
				break;
				default:
				type = new OrganizationalChangeType();
				break;
			};

			try {

				Project p = _projectRepo.GetBy(projectId);
				ChangeManager cm = _changeManagerRepo.GetBy(changeManagerId);


				ChangeInitiative newCi = new ChangeInitiative(dto.Name, dto.Description, dto.StartDate, dto.EndDate, sponsor, type);

				_changeRepo.Add(newCi);
				p.ChangeInitiatives.Add(newCi);
				cm.CreatedChangeInitiatives.Add(newCi);

				_changeRepo.SaveChanges();

				return CreatedAtAction(nameof(GetChangeInitiative), new {
					id = newCi.Id
				}, newCi);
			}
			catch {
				return BadRequest();
			}


		}
	}
}
