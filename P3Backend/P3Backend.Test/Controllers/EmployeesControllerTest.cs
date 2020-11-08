using Microsoft.AspNetCore.Mvc;
using Moq;
using P3Backend.Controllers;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using P3Backend.Test.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P3Backend.Test.Controllers {
	public class EmployeesControllerTest {

		private readonly DummyData _dummyData;

		private readonly EmployeesController _controller;

		private readonly Mock<IEmployeeRepository> _employeeRepo;
		private readonly Mock<IOrganizationRepository> _organizationRepo;

		public EmployeesControllerTest() {
			_dummyData = new DummyData();

			_employeeRepo = new Mock<IEmployeeRepository>();
			_organizationRepo = new Mock<IOrganizationRepository>();

			_controller = new EmployeesController(_employeeRepo.Object, _organizationRepo.Object);
		}

		[Fact]
		public void GetAllEmployeesFromOrganization_ReturnsCorrectEmployees() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.hogent);

			var result = _controller.GetAllEmployeesFromOrganization(1);

			Assert.IsType<ActionResult<IEnumerable<Employee>>>(result);

			foreach (Employee e in _dummyData.hogent.Employees) {
				Assert.Contains(result.Value, responseE => responseE.Id == e.Id);
			}
		}

		[Fact]
		public void GetAllEmployeesFromOrganization_OrganizationNonExtistent_ReturnsNotFound() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(null as Organization);

			var result = _controller.GetAllEmployeesFromOrganization(1);

			Assert.IsType<NotFoundObjectResult>(result.Result);


		}

		[Fact]
		public void GetEmployeeById_ReturnsCorrectEmployee() {
			_employeeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.marbod);

			var result = _controller.GetEmployeeById(1);

			Assert.IsType<ActionResult<Employee>>(result);

			var empl = result.Value;

			Assert.Equal(empl.Id, _dummyData.marbod.Id);
		}

		[Fact]
		public void GetEmployeeById_EmployeeNonExistent_ReturnsNotFound() {
			_employeeRepo.Setup(m => m.GetBy(1)).Returns(null as Employee);

			var result = _controller.GetEmployeeById(1);

			Assert.IsType<NotFoundObjectResult>(result.Result);

		}

		[Fact]
		public void PostEmployee_SuccessfullPost_returnsCreated() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.hogent);

			EmployeeDTO newDTO = new EmployeeDTO() {
				FirstName = "Testje",
				LastName = "TestMans",
				Email = "email"
			};

			var result = _controller.PostEmployee(1, newDTO);

			Assert.IsType<CreatedAtActionResult>(result);

			Assert.Contains(_dummyData.hogent.Employees, e => e.FirstName.Equals(newDTO.FirstName));
		}

		[Fact]
		public void PostEmployee_OrganizationNonExistent_returnsNotFound() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(null as Organization);

			EmployeeDTO newDTO = new EmployeeDTO() {
				FirstName = "Testje",
				LastName = "TestMans",
				Email = "email"
			};

			var result = _controller.PostEmployee(1, newDTO);

			Assert.IsType<NotFoundObjectResult>(result);

		}


		// Cannot be tested, because the employee is not removed from the dummmydata-organization
		//[Fact]
		//public void DeleteEmployee_SuccessfullDelete_ReturnsNoContent() {
		//	_employeeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ziggy);

		//	var result = _controller.DeleteEmployee(1);

		//	Assert.IsType<NoContentResult>(result);

		//	Assert.DoesNotContain(_dummyData.hogent.Employees, e => e.FirstName.Equals(_dummyData.ziggy.FirstName));
		//}

		[Fact]
		public void DeleteEmployee_EmployeeNonExistent_ReturnsNotFound() {
			_employeeRepo.Setup(m => m.GetBy(1)).Returns(null as Employee);

			var result = _controller.DeleteEmployee(1);

			Assert.IsType<NotFoundObjectResult>(result);

		}


	}
}
