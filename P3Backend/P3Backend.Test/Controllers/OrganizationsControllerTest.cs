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
	public class OrganizationsControllerTest {

		private readonly DummyData _dummyData;

		private readonly OrganizationsController _controller;

		private readonly Mock<IOrganizationRepository> _organizationRepo;
		private readonly Mock<IAdminRepository> _adminRepo;
		private readonly Mock<IChangeInitiativeRepository> _changeRepo;

		public OrganizationsControllerTest() {
			_dummyData = new DummyData();

			_organizationRepo = new Mock<IOrganizationRepository>();
			_adminRepo = new Mock<IAdminRepository>();
			_changeRepo = new Mock<IChangeInitiativeRepository>();

			_controller = new OrganizationsController(_organizationRepo.Object, _adminRepo.Object, _changeRepo.Object);
		}

		[Fact]
		public void GetOrganizationById_ReturnsCorrectOrganization() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.hogent);

			var result = _controller.GetOrganizationById(1);

			Assert.IsType<ActionResult<Organization>>(result);

			Assert.Equal(_dummyData.hogent.Name, result.Value.Name);
		}

		[Fact]
		public void GetOrganizationById_OrganizationNonExistent_ReturnsNotFound() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(null as Organization);

			var result = _controller.GetOrganizationById(1);

			Assert.IsType<NotFoundObjectResult>(result.Result);
		}

		[Fact]
		public void PostOrganization_SuccessfullPost_returnsCreated() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.admin);

			OrganizationDTO newDTO = new OrganizationDTO() {
				ChangeManager = new EmployeeDTO() { FirstName = "nieuwe", LastName = "cm", Email = "nieuwecm@email.com" },
				Name = "Nieuwe organisatie",
				EmployeeDTOs = new List<EmployeeDTO>() {
					new EmployeeDTO(){ FirstName="employee1", LastName="nieuwe", Email="niewe@employee1.com" },
					new EmployeeDTO(){ FirstName="employee2", LastName="nieuwe", Email="niewe@employee2.com" },
					new EmployeeDTO(){ FirstName="employee3", LastName="nieuwe", Email="niewe@employee3.com" }
				}
			};

			// TODO user definieren
			var result = _controller.PostOrganization(newDTO);

			Assert.IsType<CreatedAtActionResult>(result);

			// Not able to check further, result has no value to test

		}

		[Fact]
		public void PostOrganization_AdminNonExistent_returnsNotFound() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(null as Admin);

			OrganizationDTO newDTO = new OrganizationDTO() {
				ChangeManager = new EmployeeDTO() { FirstName = "nieuwe", LastName = "cm", Email = "nieuwecm@email.com" },
				Name = "Nieuwe organisatie",
				EmployeeDTOs = new List<EmployeeDTO>() {
					new EmployeeDTO(){ FirstName="employee1", LastName="nieuwe", Email="niewe@employee1.com" },
					new EmployeeDTO(){ FirstName="employee2", LastName="nieuwe", Email="niewe@employee2.com" },
					new EmployeeDTO(){ FirstName="employee3", LastName="nieuwe", Email="niewe@employee3.com" }
				}
			};

			// TODO user definieren
			var result = _controller.PostOrganization(newDTO);

			Assert.IsType<NotFoundObjectResult>(result);

			// Not able to check further, result has no value to test

		}

		[Fact]
		public void PostOrganization_NoChangeManager_returnsBadRequest() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.admin);

			OrganizationDTO newDTO = new OrganizationDTO() {
				//ChangeManager = new EmployeeDTO() { FirstName = "nieuwe", LastName = "cm", Email = "nieuwecm@email.com" },
				Name = "Nieuwe organisatie",
				EmployeeDTOs = new List<EmployeeDTO>() {
					new EmployeeDTO(){ FirstName="employee1", LastName="nieuwe", Email="niewe@employee1.com" },
					new EmployeeDTO(){ FirstName="employee2", LastName="nieuwe", Email="niewe@employee2.com" },
					new EmployeeDTO(){ FirstName="employee3", LastName="nieuwe", Email="niewe@employee3.com" }
				}
			};

			// TODO user definieren
			var result = _controller.PostOrganization(newDTO);

			Assert.IsType<BadRequestObjectResult>(result);

			// Not able to check further, result has no value to test

		}

		[Fact]
		public void PostOrganization_NoEmployees_returnsBadRequest() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.admin);

			OrganizationDTO newDTO = new OrganizationDTO() {
				ChangeManager = new EmployeeDTO() { FirstName = "nieuwe", LastName = "cm", Email = "nieuwecm@email.com" },
				Name = "Nieuwe organisatie",
				//EmployeeDTOs = new List<EmployeeDTO>() {
				//	new EmployeeDTO(){ FirstName="employee1", LastName="nieuwe", Email="niewe@employee1.com" },
				//	new EmployeeDTO(){ FirstName="employee2", LastName="nieuwe", Email="niewe@employee2.com" },
				//	new EmployeeDTO(){ FirstName="employee3", LastName="nieuwe", Email="niewe@employee3.com" }
				//}
			};

			// TODO user definieren
			var result = _controller.PostOrganization(newDTO);

			Assert.IsType<BadRequestObjectResult>(result);

			// Not able to check further, result has no value to test

		}

		[Fact]
		public void PostOrganization_NoName_returnsBadRequest() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.admin);

			OrganizationDTO newDTO = new OrganizationDTO() {
				ChangeManager = new EmployeeDTO() { FirstName = "nieuwe", LastName = "cm", Email = "nieuwecm@email.com" },
				//Name = "Nieuwe organisatie",
				EmployeeDTOs = new List<EmployeeDTO>() {
					new EmployeeDTO(){ FirstName="employee1", LastName="nieuwe", Email="niewe@employee1.com" },
					new EmployeeDTO(){ FirstName="employee2", LastName="nieuwe", Email="niewe@employee2.com" },
					new EmployeeDTO(){ FirstName="employee3", LastName="nieuwe", Email="niewe@employee3.com" }
				}
			};

			// TODO user definieren
			var result = _controller.PostOrganization(newDTO);

			Assert.IsType<BadRequestObjectResult>(result);

			// Not able to check further, result has no value to test

		}

		[Fact]
		public void Delete_SuccessfullDelete_ReturnsNoContent() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.hogent);

			var result = _controller.Delete(1);

			Assert.IsType<NoContentResult>(result);

		}

		[Fact]
		public void Delete_OrganizationNonExistent_ReturnsNotFound() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(null as Organization);

			var result = _controller.Delete(1);

			Assert.IsType<NotFoundObjectResult>(result);

		}

	}
}
