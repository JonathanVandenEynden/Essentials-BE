using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class OrganizationsController : ControllerBase {

		private readonly IOrganizationRepository _organizationRepository;
		private readonly IAdminRepository _adminRepository;
		private readonly IChangeInitiativeRepository _changeInitiativeRepo;

		public OrganizationsController(IOrganizationRepository organizationRepo,
			IAdminRepository adminRepository,
			IChangeInitiativeRepository changeInitiativeRepo) {
			_organizationRepository = organizationRepo;
			_adminRepository = adminRepository;
			_changeInitiativeRepo = changeInitiativeRepo;
		}

		[HttpGet("{organizationId}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<Organization> GetOrganizationById(int organizationId) {
			Organization o = _organizationRepository.GetBy(organizationId);

			if (o == null) {
				return NotFound("Organization not found");
			}

			return o;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult PostOrganization(int AdminId, OrganizationDTO dto) {
			try {

				Admin a = _adminRepository.GetBy(AdminId);

				if (a == null) {
					return NotFound("Admin not found");
				}

				List<Employee> employees = new List<Employee>();

				dto.EmployeeDTOs.ForEach(dto => {
					Employee newEmpl = new Employee(dto.FirstName, dto.LastName, dto.Email);
					employees.Add(newEmpl);
				});

				ChangeManager changeManager = new ChangeManager(dto.ChangeManager.FirstName, dto.ChangeManager.LastName, dto.ChangeManager.Email);

				Organization newO = new Organization(dto.Name, employees, changeManager);

				a.Organizations.Add(newO);

				_organizationRepository.Add(newO);

				_organizationRepository.SaveChanges();

				return CreatedAtAction(nameof(GetOrganizationById), new { organizationId = newO.Id }, newO);
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}


		[HttpDelete("{organizationId}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Delete(int organizationId) {
			try {
				Organization oldO = _organizationRepository.GetBy(organizationId);

				if (oldO == null) {
					return NotFound("Organization not found");
				}

				_organizationRepository.Delete(oldO);
				_organizationRepository.SaveChanges();

				return NoContent();
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}

		}
	}
}
