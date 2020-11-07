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
using System.Linq;
using Xunit;

namespace P3Backend.Test.Controllers {
	public class ChangeInitiativesControllerTest {

		private DummyData _dummyData;

		private readonly ChangeInitiativesController _controller;
		private readonly Mock<IChangeInitiativeRepository> _changeInitiativeRepo;
		private readonly Mock<IUserRepository> _userRepo;
		private readonly Mock<IProjectRepository> _projectRepo;
		private readonly Mock<IChangeManagerRepository> _changeManagerRepo;
		private readonly Mock<IEmployeeRepository> _employeeRepo;


		public ChangeInitiativesControllerTest() {
			_changeInitiativeRepo = new Mock<IChangeInitiativeRepository>();
			_userRepo = new Mock<IUserRepository>();
			_projectRepo = new Mock<IProjectRepository>();
			_changeManagerRepo = new Mock<IChangeManagerRepository>();
			_employeeRepo = new Mock<IEmployeeRepository>();

			_controller = new ChangeInitiativesController(
				_changeInitiativeRepo.Object,
				_userRepo.Object,
				_projectRepo.Object,
				_changeManagerRepo.Object,
				_employeeRepo.Object
				);

			_dummyData = new DummyData();
		}

		[Fact]
		public void GetChangeInitiativesForEmployee_returnsCorrectChangeInitiatives() {
			_changeInitiativeRepo.Setup(m => m.GetForUserId(1)).Returns(new List<ChangeInitiative>() { _dummyData.ciNewCatering });

			ActionResult<IEnumerable<ChangeInitiative>> result = _controller.GetChangeInitiativesForEmployee(1);

			Assert.IsType<ActionResult<IEnumerable<ChangeInitiative>>>(result);
			IEnumerable<ChangeInitiative> list = result.Value;
			Assert.True(list.Any(ci => ci.Id == _dummyData.ciNewCatering.Id));
		}

		[Fact]
		public void GetChangeInitiativesForEmployee_returnsNotFound() {
			_changeInitiativeRepo.Setup(m => m.GetForUserId(1)).Returns(null as List<ChangeInitiative>);

			ActionResult<IEnumerable<ChangeInitiative>> result = _controller.GetChangeInitiativesForEmployee(1);

			ActionResult response = result.Result;
			Assert.IsType<NotFoundObjectResult>(response);
		}

		[Fact]
		public void GetChangeInitiativesForChangeManager_returnsCorrectChangeInitiatives() {
			_changeManagerRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.changeManagerSuktrit);

			ActionResult<IEnumerable<ChangeInitiative>> result = _controller.GetChangeInitiativesForEmployee(1);

			Assert.IsType<ActionResult<IEnumerable<ChangeInitiative>>>(result);

			IEnumerable<ChangeInitiative> response = result.Value;

			foreach (ChangeInitiative responseCi in response) {
				Assert.Contains(_dummyData.changeManagerSuktrit.CreatedChangeInitiatives, ci => ci.Id == responseCi.Id);
			}
		}

		[Fact]
		public void GetChangeInitiativesForChangeManager_returnsNotFound() {
			_changeManagerRepo.Setup(m => m.GetBy(1)).Returns(null as ChangeManager);

			ActionResult<IEnumerable<ChangeInitiative>> result = _controller.GetChangeInitiativesForChangeManager(1);

			ActionResult response = result.Result;
			Assert.IsType<NotFoundObjectResult>(response);
		}

		[Fact]
		public void GetChangeInitiative_returnsCorrectChangeInitiative() {
			_changeInitiativeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ciExpansion);

			ActionResult<ChangeInitiative> result = _controller.GetChangeInitiative(1);

			Assert.IsType<ActionResult<ChangeInitiative>>(result);

			ChangeInitiative response = result.Value;

			Assert.True(response.Id == _dummyData.ciExpansion.Id);


		}

		[Fact]
		public void GetChangeInitiative_returnsNotfound() {
			_changeInitiativeRepo.Setup(m => m.GetBy(1)).Returns(null as ChangeInitiative);

			ActionResult<ChangeInitiative> result = _controller.GetChangeInitiative(1);

			Assert.IsType<ActionResult<ChangeInitiative>>(result);

			ActionResult response = result.Result;
			Assert.IsType<NotFoundObjectResult>(response);

		}

		[Fact]
		public void PostChangeInitiative_successfullPost_returnsCreated() {
			_employeeRepo.Setup(m => m.GetByEmail("email")).Returns(_dummyData.sponsor);
			_projectRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.project);
			_changeManagerRepo.Setup(m => m.GetBy(2)).Returns(_dummyData.changeManagerSuktrit);

			var newDTO = new ChangeInitiativeDTO() {
				Name = "ciName",
				Description = "DescriptionText",
				Sponsor = new EmployeeDTO() { Email = "email" },
				ChangeType = "personal",
				StartDate = DateTime.Now.AddDays(2),
				EndDate = DateTime.Now.AddDays(3)
			};

			var result = _controller.PostChangeInitiative(1, 2, newDTO);

			Assert.IsType<CreatedAtActionResult>(result);


		}

		[Fact]
		public void PostChangeInitiative_sponsorNotExistent_returnsNotfound() {
			_employeeRepo.Setup(m => m.GetByEmail("email")).Returns(null as Employee);
			_projectRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.project);
			_changeManagerRepo.Setup(m => m.GetBy(2)).Returns(_dummyData.changeManagerSuktrit);

			var newDTO = new ChangeInitiativeDTO() {
				Name = "ciName",
				Description = "DescriptionText",
				Sponsor = new EmployeeDTO() { Email = "email" },
				ChangeType = "personal",
				StartDate = DateTime.Now.AddDays(2),
				EndDate = DateTime.Now.AddDays(3)
			};

			var result = _controller.PostChangeInitiative(1, 2, newDTO);

			Assert.IsType<NotFoundObjectResult>(result);
		}

		[Fact]
		public void PostChangeInitiative_projectNotExistent_returnsBadRequest() {
			_employeeRepo.Setup(m => m.GetByEmail("email")).Returns(_dummyData.sponsor);
			_projectRepo.Setup(m => m.GetBy(1)).Returns(null as Project);
			_changeManagerRepo.Setup(m => m.GetBy(2)).Returns(_dummyData.changeManagerSuktrit);

			var newDTO = new ChangeInitiativeDTO() {
				Name = "ciName",
				Description = "DescriptionText",
				Sponsor = new EmployeeDTO() { Email = "email" },
				ChangeType = "personal",
				StartDate = DateTime.Now.AddDays(2),
				EndDate = DateTime.Now.AddDays(3)
			};

			var result = _controller.PostChangeInitiative(1, 2, newDTO);

			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public void PostChangeInitiative_ChangeManagerNotExistent_returnsBadRequest() {
			_employeeRepo.Setup(m => m.GetByEmail("email")).Returns(_dummyData.sponsor);
			_projectRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.project);
			_changeManagerRepo.Setup(m => m.GetBy(2)).Returns(null as ChangeManager);

			var newDTO = new ChangeInitiativeDTO() {
				Name = "ciName",
				Description = "DescriptionText",
				Sponsor = new EmployeeDTO() { Email = "email" },
				ChangeType = "personal",
				StartDate = DateTime.Now.AddDays(2),
				EndDate = DateTime.Now.AddDays(3)
			};

			var result = _controller.PostChangeInitiative(1, 2, newDTO);

			Assert.IsType<BadRequestObjectResult>(result);
		}


		[Fact]
		public void UpdateChangeInitiative_successfullUpdate_returnsCreatedAtAction() {
			_changeInitiativeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ciNewCatering);

			var updateDTO = new ChangeInitiativeDTO() {
				Name = "ciName",
				Description = "DescriptionText",
				Sponsor = new EmployeeDTO() { Email = "email" },
				ChangeType = "personal",
				StartDate = DateTime.Now.AddDays(2),
				EndDate = DateTime.Now.AddDays(3)
			};

			var result = _controller.UpdateChangeInitiative(1, updateDTO);

			Assert.IsType<CreatedAtActionResult>(result);

			Assert.Equal(_dummyData.ciNewCatering.Name, updateDTO.Name);
			Assert.Equal(_dummyData.ciNewCatering.Description, updateDTO.Description);
			Assert.Equal(_dummyData.ciNewCatering.StartDate, updateDTO.StartDate);
			Assert.Equal(_dummyData.ciNewCatering.EndDate, updateDTO.EndDate);

		}

		[Fact]
		public void UpdateChangeInitiative_ChangeInitiativeNotExistent_returnsBadRequest() {
			_changeInitiativeRepo.Setup(m => m.GetBy(1)).Returns(null as ChangeInitiative);

			var updateDTO = new ChangeInitiativeDTO() {
				Name = "ciName",
				Description = "DescriptionText",
				Sponsor = new EmployeeDTO() { Email = "email" },
				ChangeType = "personal",
				StartDate = DateTime.Now.AddDays(2),
				EndDate = DateTime.Now.AddDays(3)
			};

			var result = _controller.UpdateChangeInitiative(1, updateDTO);

			Assert.IsType<BadRequestObjectResult>(result);



		}

		[Fact]
		public void UpdateChangeInitiative_EndDateNotCorrect_returnsBadRequest() {
			_changeInitiativeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ciNewCatering);

			var updateDTO = new ChangeInitiativeDTO() {
				Name = "ciName",
				Description = "DescriptionText",
				Sponsor = new EmployeeDTO() { Email = "email" },
				ChangeType = "personal",
				StartDate = DateTime.Now.AddDays(2),
				EndDate = DateTime.Now.AddDays(1)
			};

			var result = _controller.UpdateChangeInitiative(1, updateDTO);

			Assert.IsType<BadRequestObjectResult>(result);



		}

		[Fact]
		public void DeleteChangeInitiative_SuccessfullDelete_returnNoContent() {
			_changeInitiativeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ciNewCatering);

			var result = _controller.DeleteChangeInitiative(1);

			Assert.IsType<NoContentResult>(result);

		}

		[Fact]
		public void DeleteChangeInitiative_ChangeInitiativeNotExistent_returnNotFound() {
			_changeInitiativeRepo.Setup(m => m.GetBy(1)).Returns(null as ChangeInitiative);

			var result = _controller.DeleteChangeInitiative(1);

			Assert.IsType<NotFoundObjectResult>(result);

		}



	}
}
