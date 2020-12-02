using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Data;
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
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class OrganizationsController : ControllerBase {

		private readonly IOrganizationRepository _organizationRepository;
		private readonly IAdminRepository _adminRepository;
		private readonly IChangeInitiativeRepository _changeInitiativeRepo;
		private readonly UserManager<IdentityUser> _usermanager;
		private readonly ApplicationDbContext _dbContext;

		public OrganizationsController(IOrganizationRepository organizationRepo,
			IAdminRepository adminRepository,
			IChangeInitiativeRepository changeInitiativeRepo,
			UserManager<IdentityUser> userManager,
			ApplicationDbContext dbContext) {
			_organizationRepository = organizationRepo;
			_adminRepository = adminRepository;
			_changeInitiativeRepo = changeInitiativeRepo;
			_usermanager = userManager;
			_dbContext = dbContext;
		}

		[HttpGet("{organizationId}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Policy = "ChangeManagerAccess")]
		public ActionResult<Organization> GetOrganizationById(int organizationId) {
			Organization o = _organizationRepository.GetBy(organizationId);

			if (o == null) {
				return NotFound("Organization not found");
			}

			return o;
		}

		/// <summary>
		/// Get all organizations for an admin
		/// </summary>
		/// <returns></returns>
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<List<Organization>> GetOrganizationsForAdmin() {
			Admin loggedInAdmin = _adminRepository.GetByEmail(User.Identity.Name);

			if (loggedInAdmin == null) {
				return NotFound("admin not found");
			}

			var orglist = new List<Organization>();

			loggedInAdmin.Organizations.ForEach(o => orglist.Add(_organizationRepository.GetBy(o.Id)));

			return orglist;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize(Policy = "AdminAccess")]
		public async Task<IActionResult> PostOrganization(OrganizationDTO dto) {
			try {

				Admin a = _adminRepository.GetByEmail(User.Identity.Name);

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

				// Make a list of the tasks to create the users
				List<Task> listOfTasks = new List<Task>();

				dto.EmployeeRecordDTOs.ForEach(async e => {
					List<string> nameParts = e.Name.Split(" ").ToList();

					string firstname = nameParts.ElementAt(0).Trim();
					nameParts.RemoveAt(0);
					string lastname = "";
					nameParts.ForEach(part => {
						lastname += " " + part.Trim();
					});
					string email = firstname + "." + lastname.Replace(" ", "") + "@" + dto.Name + ".com";

					Employee newEmpl = new Employee(firstname.Trim(), lastname.Trim(), email.ToLower().Trim());

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

				});

				// remove employee that became cm
				ChangeManager changeManager = new ChangeManager(employees.First());
				employees.RemoveAt(0);

				Organization newO = new Organization(dto.Name, employees, changeManager);
				newO.OrganizationParts.AddRange(organizationParts);

				a.Organizations.Add(newO);

				_organizationRepository.Add(newO);

				_organizationRepository.SaveChanges();

				// create cm-user
				await CreateUsersWithClaim(new List<Employee>() { changeManager }, "changeManager");

				// create employee-users
				await CreateUsersWithClaim(employees, "employee");

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
		[Authorize(Policy = "AdminAccess")]
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

		private async Task CreateUsersWithClaim(List<Employee> employees, string claim) {

			foreach (Employee e in employees) {
				var user = new IdentityUser { UserName = e.Email, Email = e.Email };
				string password = "P@ssword1" + e.GetInitials();
				await _usermanager.CreateAsync(user, password);
				await _usermanager.AddClaimAsync(user, new Claim(ClaimTypes.Role, claim));
			}
		}
	}
}
