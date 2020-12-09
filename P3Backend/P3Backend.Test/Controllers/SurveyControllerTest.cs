using Microsoft.AspNetCore.Mvc;
using Moq;
using P3Backend.Controllers;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Test.Data;
using System.Collections.Generic;
using Xunit;

namespace P3Backend.Test.Controllers {
    public class SurveyControllerTest {


        public readonly DummyData _dummyData;

        public readonly SurveyController _controller;

        public readonly Mock<ISurveyRepository> _surveyRepo;
        public readonly Mock<IRoadmapItemRepository> _rmiRepo;

        public SurveyControllerTest() {
            _dummyData = new DummyData();

            _surveyRepo = new Mock<ISurveyRepository>();
            _rmiRepo = new Mock<IRoadmapItemRepository>();

            _controller = new SurveyController(_surveyRepo.Object, _rmiRepo.Object);
        }

        [Fact]
        public void GetSurveys_returnsAllSurveys() {
            _surveyRepo.Setup(m => m.GetAll()).Returns(new List<Survey>() { _dummyData.surveyExpansion1, _dummyData.surveyExpansion2, _dummyData.surveyExpansion3 });

            var result = _controller.GetSurveys();

            Assert.IsType<List<Survey>>(result);

            // somehow not possibel to assert count, so it is done manually
            int counter = 0;
            foreach (Survey s in result) {
                counter++;
            }
            Assert.Equal(3, counter);

        }

        [Fact]
        public void GetSurvey_ReturnsCorrectSurvey() {
            _dummyData.surveyExpansion1.Id = 123;
            _surveyRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.surveyExpansion1);

            var result = _controller.GetSurvey(1);

            Assert.IsType<ActionResult<Survey>>(result);

            Assert.Equal(result.Value.Id, _dummyData.surveyExpansion1.Id);
        }

        [Fact]
        public void GetSurvey_SurveyNonExistent_ReturnsNotFound() {
            _surveyRepo.Setup(m => m.GetBy(1)).Returns(null as Survey);

            var result = _controller.GetSurvey(1);

            Assert.IsType<NotFoundObjectResult>(result.Result);

        }

        [Fact]
        public void PostSurvey_SuccessfullPost_ReturnsCreated() {
            _rmiRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.roadMapItemExpansion1);

            var result = _controller.PostSurvey(1);

            Assert.IsType<CreatedAtActionResult>(result.Result);

            Assert.Empty(_dummyData.roadMapItemExpansion1.Assessment.Questions);

        }

        [Fact]
        public void PostSurvey_RmiNonExistent_ReturnsNotFound() {
            _rmiRepo.Setup(m => m.GetBy(1)).Returns(null as RoadMapItem);

            var result = _controller.PostSurvey(1);

            Assert.IsType<NotFoundObjectResult>(result.Result);

        }

        [Fact]
        public void GetSurveyByRoadmapItemId_ReturnsCorrectSurvey() {
            _dummyData.roadMapItemExpansion1.Assessment.Id = 123;
            _rmiRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.roadMapItemExpansion1);

            var result = _controller.GetSurveyByRoadmapItemId(1);

            Assert.IsType<ActionResult<Survey>>(result);

            Assert.Equal(result.Value.Id, _dummyData.roadMapItemExpansion1.Assessment.Id);
        }

        [Fact]
        public void GetSurveyByRoadmapItemId_RmiNonExistent_ReturnsBadRequest() {
            _rmiRepo.Setup(m => m.GetBy(1)).Returns(null as RoadMapItem);

            var result = _controller.GetSurveyByRoadmapItemId(1);

            Assert.IsType<BadRequestObjectResult>(result.Result);

        }

        [Fact]
        public void GetSurveyByRoadmapItemId_RmiHasNoSurvey_ReturnsNotFound() {
            _dummyData.roadMapItemExpansion1.Assessment = null;
            _rmiRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.roadMapItemExpansion1);

            var result = _controller.GetSurveyByRoadmapItemId(1);

            Assert.IsType<NotFoundObjectResult>(result.Result);

        }

        [Fact]
        public void DeleteSurveyByRoadmapItemId_SuccessfullDelete_ReturnsNoContent() {
            _rmiRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.roadMapItemExpansion1);

            var result = _controller.DeleteSurveyByRoadmapItemId(1);

            Assert.IsType<NoContentResult>(result);

            Assert.Null(_dummyData.roadMapItemExpansion1.Assessment);
        }

        [Fact]
        public void DeleteSurveyByRoadmapItemId_RmiNonExistent_ReturnsNotFound() {
            _rmiRepo.Setup(m => m.GetBy(1)).Returns(null as RoadMapItem);

            var result = _controller.DeleteSurveyByRoadmapItemId(1);

            Assert.IsType<NotFoundObjectResult>(result);

        }

    }
}
