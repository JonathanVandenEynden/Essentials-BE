using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P3Backend.Controllers;
using P3Backend.Data;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using P3Backend.Test.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace P3Backend.Test.Controllers {
	public class OrganizationsControllerTest {

		private readonly DummyData _dummyData;

		private readonly OrganizationsController _controller;

		private readonly Mock<IOrganizationRepository> _organizationRepo;
		private readonly Mock<IAdminRepository> _adminRepo;
		private readonly Mock<IChangeInitiativeRepository> _changeRepo;
		//private readonly Mock<UserManager<IdentityUser>> _userManager;
		//private readonly Mock<ApplicationDbContext> _dbContext;

		public OrganizationsControllerTest() {
			_dummyData = new DummyData();

			_organizationRepo = new Mock<IOrganizationRepository>();
			_adminRepo = new Mock<IAdminRepository>();
			_changeRepo = new Mock<IChangeInitiativeRepository>();
			//_userManager = new Mock<UserManager<IdentityUser>>();
			//_dbContext = new Mock<ApplicationDbContext>();

			_controller = new OrganizationsController(_organizationRepo.Object, _adminRepo.Object, _changeRepo.Object, null, null);
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
				Name = "Nieuwe organisatie",
				EmployeeRecordDTOs = new List<EmployeeRecordDTO>() {
					new EmployeeRecordDTO(){Name="Employee1 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
					new EmployeeRecordDTO(){Name="Employee2 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
					new EmployeeRecordDTO(){Name="Employee3 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
				}
			};

			// TODO user definieren
			var result = _controller.PostOrganization(newDTO);

			Assert.IsType<Task<IActionResult>>(result);
			Assert.IsType<NoContentResult>(result.Result);

			// Not able to check further, result has no value to test

		}

		[Fact]
		public void PostOrganization_AdminNonExistent_returnsNotFound() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(null as Admin);

			OrganizationDTO newDTO = new OrganizationDTO() {
				Name = "Nieuwe organisatie",
				EmployeeRecordDTOs = new List<EmployeeRecordDTO>() {
					new EmployeeRecordDTO(){Name="Employee1 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
					new EmployeeRecordDTO(){Name="Employee2 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
					new EmployeeRecordDTO(){Name="Employee3 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
				}
			};

			// TODO user definieren
			var result = _controller.PostOrganization(newDTO);

			Assert.IsType<Task<IActionResult>>(result);

			Assert.IsType<NotFoundObjectResult>(result.Result);



			// Not able to check further, result has no value to test

		}

		// CM always the first employee in the list
		//[Fact]
		//public void PostOrganization_NoChangeManager_returnsBadRequest() {
		//	_adminRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.admin);

		//	OrganizationDTO newDTO = new OrganizationDTO() {
		//		Name = "Nieuwe organisatie",
		//		EmployeeRecordDTOs = new List<EmployeeRecordDTO>() {
		//			new EmployeeRecordDTO(){Name="Employee1 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
		//			new EmployeeRecordDTO(){Name="Employee2 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
		//			new EmployeeRecordDTO(){Name="Employee3 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
		//		}
		//	};

		//	var result = _controller.PostOrganization(1, newDTO);

		//	Assert.IsType<BadRequestObjectResult>(result);

		//	// Not able to check further, result has no value to test

		//}

		[Fact]
		public void PostOrganization_NoEmployees_returnsBadRequest() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.admin);

			OrganizationDTO newDTO = new OrganizationDTO() {
				Name = "Nieuwe organisatie",
				//EmployeeRecordDTOs = new List<EmployeeRecordDTO>() {
				//	new EmployeeRecordDTO(){Name="Employee1 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
				//	new EmployeeRecordDTO(){Name="Employee2 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
				//	new EmployeeRecordDTO(){Name="Employee3 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
				//}
			};

			// TODO user definieren
			var result = _controller.PostOrganization(newDTO);

			Assert.IsType<Task<IActionResult>>(result);
			Assert.IsType<BadRequestObjectResult>(result.Result);

			// Not able to check further, result has no value to test

		}

		[Fact]
		public void PostOrganization_NoName_returnsBadRequest() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.admin);

			OrganizationDTO newDTO = new OrganizationDTO() {
				//Name = "Nieuwe organisatie",
				EmployeeRecordDTOs = new List<EmployeeRecordDTO>() {
					new EmployeeRecordDTO(){Name="Employee1 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
					new EmployeeRecordDTO(){Name="Employee2 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
					new EmployeeRecordDTO(){Name="Employee3 Nieuwe", Country = "", Department = "", Factory= "", Office="", Team=""},
				}
			};

			// TODO user definieren
			var result = _controller.PostOrganization(newDTO);

			Assert.IsType<Task<IActionResult>>(result);
			Assert.IsType<BadRequestObjectResult>(result.Result);

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
