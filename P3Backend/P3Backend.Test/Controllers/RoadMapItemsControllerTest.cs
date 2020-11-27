using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using NUnit.Framework.Constraints;
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
	public class RoadMapItemsControllerTest {

		private readonly DummyData _dummyData;

		private readonly RoadMapItemsController _controller;

		private readonly Mock<IRoadmapItemRepository> _rmiRepo;
		private readonly Mock<IChangeInitiativeRepository> _changeRepo;

		public RoadMapItemsControllerTest() {
			_dummyData = new DummyData();

			_rmiRepo = new Mock<IRoadmapItemRepository>();
			_changeRepo = new Mock<IChangeInitiativeRepository>();

			_controller = new RoadMapItemsController(_rmiRepo.Object, _changeRepo.Object);
		}


		[Fact]
		public void GetRoadMapItem_ReturnsCorrectRmi() {
			_rmiRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.roadMapItemExpansion1);

			var result = _controller.GetRoadMapItem(1);

			Assert.IsType<ActionResult<RoadMapItem>>(result);

			var rmi = result.Value;

			Assert.Equal(_dummyData.roadMapItemExpansion1.Title, rmi.Title);
		}

		[Fact]
		public void GetRoadMapItem_RmiNotExistent_ReturnsNotFound() {
			_rmiRepo.Setup(m => m.GetBy(1)).Returns(null as RoadMapItem);

			var result = _controller.GetRoadMapItem(1);

			Assert.IsType<NotFoundObjectResult>(result.Result);

		}

		[Fact]
		public void GetRoadMapItemsForChangeInitiative_ReturnsCorrectRmis() {
			_changeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ciExpansion);

			var result = _controller.GetRoadMapItemsForChangeInitiative(1);

			Assert.IsType<ActionResult<IEnumerable<RoadMapItem>>>(result);

			var response = result.Value;

			foreach (RoadMapItem rmi in _dummyData.ciExpansion.RoadMap) {
				Assert.Contains(response, responseRmi => responseRmi.Title.Equals(rmi.Title));
			}
		}

		[Fact]
		public void GetRoadMapItemsForChangeInitiative_CiNonExistent_ReturnsNotFound() {
			_changeRepo.Setup(m => m.GetBy(1)).Returns(null as ChangeInitiative);

			var result = _controller.GetRoadMapItemsForChangeInitiative(1);

			Assert.IsType<NotFoundObjectResult>(result.Result);

		}

		[Fact]
		public void PostRoadMapItem_SuccessfullPost_returnsCreated() {
			_changeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ciExpansion);

			RoadMapItemDTO newDTO = new RoadMapItemDTO() {
				Title = "nieuw item",
				StartDate = DateTime.Now.AddDays(1),
				EndDate = DateTime.Now.AddDays(2)
			};

			var result = _controller.PostRoadMapItem(1, newDTO);

			Assert.IsType<CreatedAtActionResult>(result);

			// unable to test further  --> no value

		}

		[Fact]
		public void PostRoadMapItem_CiNonExistent_returnsCreated() {
			_changeRepo.Setup(m => m.GetBy(1)).Returns(null as ChangeInitiative);

			RoadMapItemDTO newDTO = new RoadMapItemDTO() {
				Title = "nieuw item",
				StartDate = DateTime.Now.AddDays(1),
				EndDate = DateTime.Now.AddDays(2)
			};

			var result = _controller.PostRoadMapItem(1, newDTO);

			Assert.IsType<NotFoundObjectResult>(result);


		}

		[Fact]
		public void PostRoadMapItem_NoTitle_returnsBadRequest() {
			_changeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ciExpansion);

			RoadMapItemDTO newDTO = new RoadMapItemDTO() {
				//Title = "nieuw item",
				StartDate = DateTime.Now.AddDays(1),
				EndDate = DateTime.Now.AddDays(2)
			};

			var result = _controller.PostRoadMapItem(1, newDTO);

			Assert.IsType<BadRequestObjectResult>(result);

			// unable to test further  --> no value

		}

		[Fact]
		public void PostRoadMapItem_NoStart_returnsBadRequest() {
			_changeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ciExpansion);

			RoadMapItemDTO newDTO = new RoadMapItemDTO() {
				Title = "nieuw item",
				//StartDate = DateTime.Now.AddDays(1),
				EndDate = DateTime.Now.AddDays(2)
			};

			var result = _controller.PostRoadMapItem(1, newDTO);

			Assert.IsType<BadRequestObjectResult>(result);

			// unable to test further  --> no value

		}

		[Fact]
		public void PostRoadMapItem_NoEnd_returnsBadRequest() {
			_changeRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.ciExpansion);

			RoadMapItemDTO newDTO = new RoadMapItemDTO() {
				Title = "nieuw item",
				StartDate = DateTime.Now.AddDays(1),
				//EndDate = DateTime.Now.AddDays(2)
			};

			var result = _controller.PostRoadMapItem(1, newDTO);

			Assert.IsType<BadRequestObjectResult>(result);

			// unable to test further  --> no value

		}

		/*
		 * 
		 * 
		 * Tests for wrong start end end are in the model class testing
		 * 
		 * 
		 */
	}
}
