using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.OrganizationParts;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.TussenTabellen;
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

		[HttpPost("{adminId}")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult PostOrganization(int adminId, OrganizationDTO dto) {
			try {
				// search admin
				Admin a = _adminRepository.GetBy(adminId);

				if (a == null) {
					return NotFound("Admin not found");
				}

				// create organizational parts
				List<OrganizationPart> organizationParts = new List<OrganizationPart>();

				dto.EmployeeRecordDTOs.ForEach(record => {
					// country
					if (!string.IsNullOrEmpty(record.Country.Trim()) && !organizationParts.Any(p => p.Type == OrganizationPartType.COUNTRY && p.Name.Equals(record.Country))) {
						organizationParts.Add(new OrganizationPart(record.Country, OrganizationPartType.COUNTRY));
					}
					// office
					if (!string.IsNullOrEmpty(record.Office.Trim()) && !organizationParts.Any(p => p.Type == OrganizationPartType.OFFICE && p.Name.Equals(record.Office))) {
						organizationParts.Add(new OrganizationPart(record.Office, OrganizationPartType.OFFICE));
					}
					// factory
					if (!string.IsNullOrEmpty(record.Factory.Trim()) && !organizationParts.Any(p => p.Type == OrganizationPartType.FACTORY && p.Name.Equals(record.Factory))) {
						organizationParts.Add(new OrganizationPart(record.Factory, OrganizationPartType.FACTORY));
					}
					// department
					if (!string.IsNullOrEmpty(record.Department.Trim()) && !organizationParts.Any(p => p.Type == OrganizationPartType.DEPARTMENT && p.Name.Equals(record.Department))) {
						organizationParts.Add(new OrganizationPart(record.Department, OrganizationPartType.DEPARTMENT));
					}
					// team
					if (!string.IsNullOrEmpty(record.Team.Trim()) && !organizationParts.Any(p => p.Type == OrganizationPartType.TEAM && p.Name.Equals(record.Team))) {
						organizationParts.Add(new OrganizationPart(record.Team, OrganizationPartType.TEAM));
					}

				});

				// create employees
				List<Employee> employees = new List<Employee>();

				dto.EmployeeRecordDTOs.ForEach(e => {
					List<string> nameParts = e.Name.Split(" ").ToList();

					string firstname = nameParts.ElementAt(0).Trim();
					nameParts.RemoveAt(0);
					string lastname = "";
					nameParts.ForEach(part => {
						lastname += " " + part.Trim();
					});
					string email = firstname + "." + lastname.Replace(" ", "") + "@" + dto.Name + ".com";

					Employee newEmpl = new Employee(firstname, lastname, email.ToLower());

					// assign country
					OrganizationPart country = organizationParts.Find(p => p.Name.Equals(e.Country) && p.Type == OrganizationPartType.COUNTRY);
					if (country != null) {
						EmployeeOrganizationPart emplCountry = new EmployeeOrganizationPart(newEmpl, country);
						country.EmployeeOrganizationParts.Add(emplCountry);
						newEmpl.EmployeeOrganizationParts.Add(emplCountry);
					}
					// assign office
					OrganizationPart office = organizationParts.Find(p => p.Name.Equals(e.Office) && p.Type == OrganizationPartType.OFFICE);
					if (office != null) {
						EmployeeOrganizationPart emplOffice = new EmployeeOrganizationPart(newEmpl, office);
						office.EmployeeOrganizationParts.Add(emplOffice);
						newEmpl.EmployeeOrganizationParts.Add(emplOffice);
					}
					// assign factory
					OrganizationPart factory = organizationParts.Find(p => p.Name.Equals(e.Factory) && p.Type == OrganizationPartType.FACTORY);
					if (factory != null) {
						EmployeeOrganizationPart emplFactory = new EmployeeOrganizationPart(newEmpl, factory);
						factory.EmployeeOrganizationParts.Add(emplFactory);
						newEmpl.EmployeeOrganizationParts.Add(emplFactory);
					}
					// assign department
					OrganizationPart department = organizationParts.Find(p => p.Name.Equals(e.Department) && p.Type == OrganizationPartType.DEPARTMENT);
					if (department != null) {
						EmployeeOrganizationPart emplDepartment = new EmployeeOrganizationPart(newEmpl, department);
						department.EmployeeOrganizationParts.Add(emplDepartment);
						newEmpl.EmployeeOrganizationParts.Add(emplDepartment);
					}
					// assign team
					OrganizationPart team = organizationParts.Find(p => p.Name.Equals(e.Team) && p.Type == OrganizationPartType.TEAM);
					if (team != null) {
						EmployeeOrganizationPart emplTeam = new EmployeeOrganizationPart(newEmpl, team);
						team.EmployeeOrganizationParts.Add(emplTeam);
						newEmpl.EmployeeOrganizationParts.Add(emplTeam);
					}
					employees.Add(newEmpl);

					// TODO accounts aanmaken
				});

				ChangeManager changeManager = new ChangeManager(employees.First());

				// remove employee that became cm
				employees.RemoveAt(0);

				Organization newO = new Organization(dto.Name, employees, changeManager);
				newO.OrganizationParts.AddRange(organizationParts);

				a.Organizations.Add(newO);

				_organizationRepository.Add(newO);

				_organizationRepository.SaveChanges();

				//return CreatedAtAction(nameof(GetOrganizationById), new { organizationId = newO.Id }, newO);
				return NoContent();
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
