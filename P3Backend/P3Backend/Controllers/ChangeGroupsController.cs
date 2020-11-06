using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Data;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class ChangeGroupsController : ControllerBase {

		private readonly ApplicationDbContext _dbcontext;
		private readonly IOrganizationRepository _organizationRepository;
		private readonly IChangeInitiativeRepository _changeInitiativeRepo;


		public ChangeGroupsController(
			ApplicationDbContext dbcontext,
			IOrganizationRepository organizationRepository,
			IChangeInitiativeRepository changeInitiativeRepo) {
			_dbcontext = dbcontext;
			_organizationRepository = organizationRepository;
			_changeInitiativeRepo = changeInitiativeRepo;
		}

		/// <summary>
		/// Get all changegroups in an organization
		/// </summary>
		/// <param name="organizationId"></param>
		/// <returns></returns>
		[Route("[action]/{organizationId}")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<IList<ChangeGroup>> GetAllGhangeGroupsOfOrganization(int organizationId = 1) {

			try {
				Organization o = _organizationRepository.GetBy(organizationId);
				if (o == null) {
					return NotFound("Organization does not exist");
				}
				List<ChangeInitiative> allCI = new List<ChangeInitiative>();

				o.ChangeManagers.ForEach(cm => {
					allCI.AddRange(cm.CreatedChangeInitiatives);
				});
				List<ChangeGroup> allGroups = new List<ChangeGroup>();

				allCI.ForEach(ci => {
					ChangeInitiative currentCi = _changeInitiativeRepo.GetBy(ci.Id);
					allGroups.Add(currentCi.ChangeGroup);

				});

				return allGroups;
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}
	}
}
