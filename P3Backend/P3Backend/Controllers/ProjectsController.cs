using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;
using System;
using System.Collections.Generic;

namespace P3Backend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(Policy = "ChangeManagerAccess")]
        public ActionResult<IEnumerable<Project>> GetProjectsForOrganization(int organizationId) {
            Organization o = _organizationRepo.GetBy(organizationId);

            if (o == null) {
                return NotFound("Organization not found");
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
        [Authorize(Policy = "ChangeManagerAccess")]
        public ActionResult<Project> GetProjectById(int projectId) {
            Project p = _projectRepo.GetBy(projectId);

            if (p == null) {
                return NotFound("Project not found");
            }
            return p;
        }

        /// <summary>
        /// create a new project for an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("{organizationId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Policy = "ChangeManagerAccess")]
        public IActionResult PostProjectToOrganization(int organizationId, ProjectDTO dto) {

            try {
                Organization o = _organizationRepo.GetBy(organizationId);

                if (o == null) {
                    return NotFound("Organization not found");
                }

                Project newP = new Project(dto.Name);

                o.Portfolio.Projects.Add(newP);

                _projectRepo.SaveChanges();

                return CreatedAtAction(nameof(GetProjectById), new { projectId = newP.Id }, newP);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

        }


    }
}
