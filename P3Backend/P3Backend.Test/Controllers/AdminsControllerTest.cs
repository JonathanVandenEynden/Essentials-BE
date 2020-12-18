using Microsoft.AspNetCore.Mvc;
using Moq;
using P3Backend.Controllers;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using P3Backend.Test.Data;
using System.Collections.Generic;
using Xunit;

namespace P3Backend.Test.Controllers {
	public class AdminsControllerTest {

		private readonly DummyData _dummyData;

		private readonly AdminsController _controller;

		private readonly Mock<IAdminRepository> _adminRepo;

		public AdminsControllerTest() {
			_dummyData = new DummyData();

			_adminRepo = new Mock<IAdminRepository>();

			_controller = new AdminsController(_adminRepo.Object);
		}


		[Fact]
		public void GetAllAdmins_returnsCorrectAdmins() {
			_adminRepo.Setup(m => m.GetAll()).Returns(new List<Admin>() { _dummyData.admin });

			var result = _controller.GetAllAdmins();

			Assert.IsType<List<Admin>>(result);

			Assert.Contains(result, a => a.FirstName.Equals(_dummyData.admin.FirstName));
		}

		[Fact]
		public void GetAdminById_ReturnsCorrectAdmin() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.admin);

			var result = _controller.GetAdminById(1);

			Assert.IsType<ActionResult<Admin>>(result);

			Assert.Equal(result.Value.FirstName, _dummyData.admin.FirstName);
		}


		[Fact]
		public void GetAdminById_AdminNonExistent_ReturnsNotFound() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(null as Admin);

			var result = _controller.GetAdminById(1);

			Assert.IsType<NotFoundObjectResult>(result.Result);

		}

		[Fact]
		public void PostAdmin_SuccessfullPost_ReturnsCreated() {

			AdminDTO newDTO = new AdminDTO() {
				FirstName = "Nieuwe",
				LastName = "Admin",
				Email = "emailtje"
			};

			var result = _controller.PostAdmin(newDTO);

			Assert.IsType<CreatedAtActionResult>(result);
		}


		[Fact]
		public void DeleteAdmin_SuccessfullDelete_ReturnsNoContent() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.admin);

			var result = _controller.DeleteAdmin(1);

			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public void DeleteAdmin_AdminNonExistent_ReturnsNotFound() {
			_adminRepo.Setup(m => m.GetBy(1)).Returns(null as Admin);

			var result = _controller.DeleteAdmin(1);

			Assert.IsType<NotFoundObjectResult>(result);
		}
	}
}
