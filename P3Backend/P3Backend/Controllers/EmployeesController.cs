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

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]

	public class EmployeesController : ControllerBase {
		private readonly IEmployeeRepository _employeeRepo;
		private readonly IOrganizationRepository _organizationRepo;

		public EmployeesController(IEmployeeRepository employeeRepository,
			IOrganizationRepository organizationRepo) {
			_employeeRepo = employeeRepository;
			_organizationRepo = organizationRepo;
		}

		/// <summary>
		/// Get all employees of an organization
		/// </summary>
		/// <returns></returns>
		[Route("[action]/{organizationId}")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<Employee>> GetAllEmployeesFromOrganization(int organizationId) {
			try {
				return _organizationRepo.GetBy(organizationId).Employees;
			}
			catch {
				return NotFound();
			}
		}
		/// <summary>
		/// Get employee by a given Id
		/// </summary>
		/// <param name="employeeId">the id of the employee</param>
		/// <returns>employee obj</returns>
		[HttpGet("{employeeId}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<Employee> GetEmployeeById(int employeeId) {
			Employee a = _employeeRepo.GetBy(employeeId);

			if (a == null) {
				return NotFound();
			}

			return a;
		}

		/// <summary>
		/// Create a new employee
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPost("{organizationId}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public IActionResult PostEmployee(int organizationId, EmployeeDTO dto) {
			try {
				Organization o = _organizationRepo.GetBy(organizationId);

				Employee newE = new Employee(dto.FirstName, dto.LastName, dto.Email);

				o.Employees.Add(newE);

				_employeeRepo.SaveChanges();

				return CreatedAtAction(nameof(GetEmployeeById), new { employeeId = newE.Id }, newE);
			}
			catch {
				return BadRequest();
			}
		}

		/// <summary>
		/// Delete an employee
		/// </summary>
		/// <param name="employeeId"></param>
		/// <returns></returns>
		[HttpDelete("{employeeId}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public IActionResult DeleteEmployee(int employeeId) {
			Employee e = _employeeRepo.GetBy(employeeId);

			if (e == null) {
				return NotFound();
			}

			_employeeRepo.Delete(e);
			_employeeRepo.SaveChanges();

			return NoContent();
		}
	}
}
