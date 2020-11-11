using Microsoft.AspNetCore.Mvc;
using Moq;
using P3Backend.Controllers;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Test.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P3Backend.Test.Controllers {
	public class ProjectsControllerTest {

		private readonly DummyData _dummyData;

		private readonly ProjectsController _controller;

		private readonly Mock<IProjectRepository> _projectRepo;
		private readonly Mock<IOrganizationRepository> _organizationRepo;

		public ProjectsControllerTest() {
			_dummyData = new DummyData();

			_projectRepo = new Mock<IProjectRepository>();
			_organizationRepo = new Mock<IOrganizationRepository>();

			_controller = new ProjectsController(_projectRepo.Object, _organizationRepo.Object);
		}

		[Fact]
		public void GetProjectsForOrganization_ReturnsCorrectProjects() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.hogent);

			var result = _controller.GetProjectsForOrganization(1);

			Assert.IsType<ActionResult<IEnumerable<Project>>>(result);

			Assert.Contains(result.Value, p => p.Name.Equals(_dummyData.project.Name));
		}

		[Fact]
		public void GetProjectsForOrganization_OrganizationNonExistent_ReturnsNotFound() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(null as Organization);

			var result = _controller.GetProjectsForOrganization(1);

			Assert.IsType<NotFoundObjectResult>(result.Result);

		}

		[Fact]
		public void GetProjectById_ReturnsCorrectProject() {
			_projectRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.project);

			var result = _controller.GetProjectById(1);

			Assert.IsType<ActionResult<Project>>(result);

			Assert.Equal(result.Value.Name, _dummyData.project.Name);

		}

		[Fact]
		public void GetProjectById_ProjectNonExistent_ReturnsNotFound() {
			_projectRepo.Setup(m => m.GetBy(1)).Returns(null as Project);

			var result = _controller.GetProjectById(1);

			Assert.IsType<NotFoundObjectResult>(result.Result);

		}

		[Fact]
		public void PostProjectToOrganization_SuccessfullPost_ReturnsCreatedAt() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.hogent);

			ProjectDTO newDTO = new ProjectDTO() {
				Name = "NieuwProjectse"
			};

			var result = _controller.PostProjectToOrganization(1, newDTO);

			Assert.IsType<CreatedAtActionResult>(result);

			Assert.Contains(_dummyData.hogent.Portfolio.Projects, p => p.Name.Equals(newDTO.Name));
		}

		[Fact]
		public void PostProjectToOrganization_OrganizationNonExistent_ReturnsNotFound() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(null as Organization);

			ProjectDTO newDTO = new ProjectDTO() {
				Name = "NieuwProjectse"
			};

			var result = _controller.PostProjectToOrganization(1, newDTO);

			Assert.IsType<NotFoundObjectResult>(result);
		}

		// TODO no name still possible
		[Fact]
		public void PostProjectToOrganization_NoName_ReturnsBadRequest() {
			_organizationRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.hogent);

			ProjectDTO newDTO = new ProjectDTO() {
				//Name = "NieuwProjectse"
			};

			var result = _controller.PostProjectToOrganization(1, newDTO);

			Assert.IsType<BadRequestObjectResult>(result);
		}

	}
}
