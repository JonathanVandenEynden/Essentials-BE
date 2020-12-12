using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P3Backend.Controllers;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using P3Backend.Test.Data;
using System.Collections.Generic;
using System.Security.Principal;
using Xunit;

namespace P3Backend.Test.Controllers {
	public class ChangeManagersControllerTest {

		private readonly DummyData _dummyData;

		private readonly ChangeManagersController _controller;

		private readonly Mock<IChangeManagerRepository> _changeManagerRepo;
		private readonly Mock<IOrganizationRepository> _organizationRepo;
		private readonly Mock<IEmployeeRepository> _employeeRepo;


		public ChangeManagersControllerTest() {
			_dummyData = new DummyData();

			_changeManagerRepo = new Mock<IChangeManagerRepository>();
			_organizationRepo = new Mock<IOrganizationRepository>();
			_employeeRepo = new Mock<IEmployeeRepository>();


			_controller = new ChangeManagersController(_changeManagerRepo.Object, _organizationRepo.Object, _employeeRepo.Object, null);

		}

		[Fact]
		public void GetChangeManagersFromOrganization_returnsCorrectChangeManagers() {
			var fakeIdentity = new GenericIdentity("email");
			var fakePrincipal = new GenericPrincipal(fakeIdentity, null);

			_controller.ControllerContext = new ControllerContext() {
				HttpContext = new DefaultHttpContext() { User = fakePrincipal }
			};

			_employeeRepo.Setup(m => m.GetByEmail("email")).Returns(_dummyData.changeManagerSuktrit);
			_organizationRepo.Setup(m => m.GetAll()).Returns(new List<Organization>() { _dummyData.hogent });

			var result = _controller.GetChangeManagersFromOrganization();

			Assert.IsType<ActionResult<IEnumerable<ChangeManager>>>(result);

			var list = result.Value;

			Assert.Contains(list, cm => cm.Id == _dummyData.changeManagerSuktrit.Id);

		}

		[Fact]
		public void GetChangeManagersFromOrganization_OrganizationNonExistent_returnsNotFound() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(null as Organization);

			var result = _controller.GetChangeManagersFromOrganization();

			Assert.IsType<NotFoundObjectResult>(result.Result);

		}

		[Fact]
		public void GetChangeManagersByID_returnsCorrectChangeManager() {
			_changeManagerRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.changeManagerSuktrit);

			var result = _controller.GetChangeManagerById(1);

			Assert.IsType<ActionResult<ChangeManager>>(result);

			var response = result.Value;

			Assert.Equal(_dummyData.changeManagerSuktrit.Id, response.Id);

		}

		[Fact]
		public void GetChangeManagersByID_ChangeManagerNonExistent_returnsNotFound() {
			_changeManagerRepo.Setup(m => m.GetBy(1)).Returns(null as ChangeManager);

			var result = _controller.GetChangeManagerById(1);

			Assert.IsType<NotFoundObjectResult>(result.Result);


		}

		[Fact]
		public void UpgradeEmployeeToChangeManager_SuccessfullUpgrade_returnsNoContent() {
			_employeeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.marbod);
			_organizationRepo.Setup(m => m.GetAll()).Returns(new List<Organization>() { _dummyData.hogent });

			var result = _controller.UpgradeEmployeeToChangeManager(1);

			Assert.IsType<NoContentResult>(result);

			Assert.Contains(_dummyData.hogent.ChangeManagers, cm => cm.FirstName.Equals("Marbod"));
			Assert.DoesNotContain(_dummyData.hogent.Employees, cm => cm.FirstName.Equals("Marbod"));

		}


		[Fact]
		public void UpgradeEmployeeToChangeManager_EmployeeNonExistent_returnsNotFound() {
			_employeeRepo.Setup(m => m.GetBy(1)).Returns(null as Employee);
			_organizationRepo.Setup(m => m.GetAll()).Returns(new List<Organization>() { _dummyData.hogent });

			var result = _controller.UpgradeEmployeeToChangeManager(1);

			Assert.IsType<NotFoundObjectResult>(result);

		}

		[Fact]
		public void UpgradeEmployeeToChangeManager_NoOrganizations_returnsBadRequest() {
			_employeeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.marbod);
			_organizationRepo.Setup(m => m.GetAll()).Returns(null as List<Organization>);

			var result = _controller.UpgradeEmployeeToChangeManager(1);

			Assert.IsType<BadRequestObjectResult>(result);

		}

		[Fact]
		public void UpgradeEmployeeToChangeManager_EmployeeNotInOrganizations_returnsNotFound() {
			_employeeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.marbod);
			_organizationRepo.Setup(m => m.GetAll()).Returns(new List<Organization>() { new Organization("niuweNaam", new List<Employee>(), _dummyData.changeManagerSuktrit) });

			var result = _controller.UpgradeEmployeeToChangeManager(1);

			Assert.IsType<NotFoundObjectResult>(result);

		}





	}
}
