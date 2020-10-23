using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]

	public class ProjectsController : ControllerBase {

		private readonly IProjectRepository _projectRepo;
		private readonly IOrganizationRepository _organizationRepo;

		public ProjectsController(IProjectRepository projectRepo,
			IOrganizationRepository organizationRepo) {
			_projectRepo = projectRepo;
			_organizationRepo = organizationRepo;
		}

		/// <summary>
		///	get the projects of an organization
		/// </summary>
		/// <param name="organizationId"></param>
		/// <returns></returns>
		[Route("[action]/{organizationId}")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<Project>> GetProjectsForOrganization(int organizationId) {
			Organization o = _organizationRepo.GetBy(organizationId);

			if (o == null) {
				return NotFound();
			}
			return o.Portfolio.Projects;
		}

		/// <summary>
		/// Get a project with a specific id
		/// </summary>
		/// <param name="projectId"></param>
		/// <returns></returns>
		[HttpGet("{projectId}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<Project> GetProjectById(int projectId) {
			Project p = _projectRepo.GetBy(projectId);

			if (p == null) {
				return NotFound();
			}
			return p;
		}

		[HttpPost("{organizationId}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public IActionResult PostProjectToOrganization(int organizationId, ProjectDTO dto) {

			try {
				Organization o = _organizationRepo.GetBy(organizationId);

				Project newP = new Project(dto.Name);

				o.Portfolio.Projects.Add(newP);

				_projectRepo.SaveChanges();

				return CreatedAtAction(nameof(GetProjectById), new { projectId = newP.Id }, newP);
			}
			catch {
				return BadRequest();
			}

		}


	}
}
