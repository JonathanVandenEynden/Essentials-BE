using Microsoft.AspNetCore.Mvc;
using Moq;
using P3Backend.Controllers;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Test.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P3Backend.Test.Controllers {
	public class ChangeGroupsControllerTest {

		private readonly DummyData _dummyData;

		private readonly ChangeGroupsController _controller;

		private readonly Mock<IOrganizationRepository> _organizationRepo;
		private readonly Mock<IChangeInitiativeRepository> _changeRepo;
		private readonly Mock<IChangeGroupRepository> _changeGroupRepo;

		public ChangeGroupsControllerTest() {
			_dummyData = new DummyData();

			_organizationRepo = new Mock<IOrganizationRepository>();
			_changeRepo = new Mock<IChangeInitiativeRepository>();
			_changeGroupRepo = new Mock<IChangeGroupRepository>();

			_controller = new ChangeGroupsController(_organizationRepo.Object, _changeRepo.Object, _changeGroupRepo.Object);

		}

		// TODO Test _controller.GetChangeGroupForUser

		[Fact]
		public void GetAllGhangeGroupsOfOrganization_ReturnsCorrectChangeGroups() {
			// voorbereiden
			_dummyData.ciNewCatering.Id = 1;
			_dummyData.changeManagerSuktrit.CreatedChangeInitiatives.Clear();
			_dummyData.changeManagerSuktrit.CreatedChangeInitiatives.Add(_dummyData.ciNewCatering);

			_organizationRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.hogent);
			_changeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ciNewCatering);

			var result = _controller.GetAllGhangeGroupsOfOrganization(1);

			Assert.IsType<ActionResult<IList<ChangeGroup>>>(result);

			Assert.Contains(result.Value, cg => cg.Name.Equals(_dummyData.allEmployees.Name));

		}

		[Fact]
		public void GetAllGhangeGroupsOfOrganization_OrganizationNonExistent_ReturnsNotFound() {
			// voorbereiden
			_dummyData.ciNewCatering.Id = 1;
			_dummyData.changeManagerSuktrit.CreatedChangeInitiatives.Clear();
			_dummyData.changeManagerSuktrit.CreatedChangeInitiatives.Add(_dummyData.ciNewCatering);

			_organizationRepo.Setup(m => m.GetBy(1)).Returns(null as Organization);
			_changeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ciNewCatering);

			var result = _controller.GetAllGhangeGroupsOfOrganization(1);

			Assert.IsType<NotFoundObjectResult>(result.Result);



		}


	}
}
