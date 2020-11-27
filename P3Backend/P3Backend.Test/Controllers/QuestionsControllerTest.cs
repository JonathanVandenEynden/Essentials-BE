using Microsoft.AspNetCore.Mvc;
using Moq;
using P3Backend.Controllers;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.Questions;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Test.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P3Backend.Test.Controllers {
	public class QuestionsControllerTest {

		private readonly DummyData _dummyData;

		private readonly QuestionsController _controller;

		private readonly Mock<ISurveyRepository> _surveyRepo;

		public QuestionsControllerTest() {
			_dummyData = new DummyData();

			_surveyRepo = new Mock<ISurveyRepository>();

			_controller = new QuestionsController(_surveyRepo.Object);
		}

		[Fact]
		public void GetQuestionsFromSurvey_ReturnsCorrectSurveyWithQuestions() {
			_surveyRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.surveyExpansion1);

			var result = _controller.GetQuestionsFromSurvey(1);

			Assert.IsType<ActionResult<IEnumerable<Question>>>(result);

			var questions = result.Value;

			_dummyData.surveyExpansion1.Questions.ForEach(q => {
				Assert.Contains(questions, responseQ => responseQ.QuestionString.Equals(q.QuestionString));
			});

		}

		[Fact]
		public void GetQuestionsFromSurvey_SurveyNonExistent_ReturnsNotFound() {
			_surveyRepo.Setup(m => m.GetBy(1)).Returns(null as Survey);

			var result = _controller.GetQuestionsFromSurvey(1);

			Assert.IsType<NotFoundResult>(result.Result);

		}

		[Fact]
		public void PostQuestionToSurvey_SuccessfullPost_ReturnsCreated() {
			_surveyRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.surveyExpansion1);

			QuestionDTO newDTO = new QuestionDTO() {
				QuestionString = "Vraagje voor de sfeer",
				Type = QuestionType.OPEN
			};

			var result = _controller.PostQuestionToSurvey(1, newDTO);

			Assert.IsType<CreatedAtActionResult>(result.Result);

			// unable to test further -> return value is null
		}

		[Fact]
		public void PostQuestionToSurvey_SurveyNonExistent_ReturnsNotFound() {
			_surveyRepo.Setup(m => m.GetBy(1)).Returns(null as Survey);

			QuestionDTO newDTO = new QuestionDTO() {
				QuestionString = "Vraagje voor de sfeer",
				Type = QuestionType.OPEN
			};

			var result = _controller.PostQuestionToSurvey(1, newDTO);

			Assert.IsType<NotFoundObjectResult>(result.Result);

		}

		[Fact]
		public void PostQuestionToSurvey_NoQuestionString_ReturnsBadRequest() {
			_surveyRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.surveyExpansion1);

			QuestionDTO newDTO = new QuestionDTO() {
				//QuestionString = "Vraagje voor de sfeer",
				Type = QuestionType.OPEN
			};

			var result = _controller.PostQuestionToSurvey(1, newDTO);

			Assert.IsType<BadRequestObjectResult>(result.Result);


		}

		[Fact]
		public void PostQuestionToSurvey_NoCorrectType_ReturnsBadRequest() {
			_surveyRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.surveyExpansion1);

			QuestionDTO newDTO = new QuestionDTO() {
				QuestionString = "Vraagje voor de sfeer",
				Type = (QuestionType)100
			};

			var result = _controller.PostQuestionToSurvey(1, newDTO);

			Assert.IsType<BadRequestObjectResult>(result.Result);

		}

		[Fact]
		public void PostAnswerToQuestion_INITIAL_SuccessfullPost_resturnsNoContent() {
			_surveyRepo.Setup(m => m.GetQuestion(1)).Returns(_dummyData.questionExpansion1);

			List<String> answers = new List<String>() { "azer", "qsdf", "wxcv" };

			var result = _controller.PostAnswerToQuestion(1, answers, true);

			Assert.IsType<NoContentResult>(result);

			// unable to test further

		}

		[Fact]
		public void PostAnswerToQuestion_SuccessfullPost_resturnsNoContent() {
			_surveyRepo.Setup(m => m.GetQuestion(1)).Returns(_dummyData.questionExpansion1);

			List<String> answers = new List<String>() { "azer", "qsdf", "wxcv" };

			var result = _controller.PostAnswerToQuestion(1, answers, false);

			Assert.IsType<NoContentResult>(result);

			// unable to test further

			//foreach (string answer in _dummyData.questionExpansion1.PossibleAnswers.Keys) {
			//	Assert.Contains()
			//}

		}

		[Fact]
		public void PostAnswerToQuestion_QuestionNonExistent_resturnsNotFound() {
			_surveyRepo.Setup(m => m.GetQuestion(1)).Returns(null as Question);

			List<String> answers = new List<String>() { "azer", "qsdf", "wxcv" };

			var result = _controller.PostAnswerToQuestion(1, answers, false);

			Assert.IsType<NotFoundObjectResult>(result);

		}

		[Fact]
		public void DeleteQuestions_SuccessfullDelete_ReturnsNoContent() {
			_surveyRepo.Setup(m => m.GetBy(1)).Returns(_dummyData.surveyExpansion1);

			var result = _controller.DeleteQuestions(1);

			Assert.IsType<NoContentResult>(result);

			Assert.Empty(_dummyData.surveyExpansion1.Questions);
		}

		[Fact]
		public void DeleteQuestions_QuestionNonExistent_ReturnsNotFound() {
			_surveyRepo.Setup(m => m.GetBy(1)).Returns(null as Survey);

			var result = _controller.DeleteQuestions(1);

			Assert.IsType<NotFoundObjectResult>(result);

		}

	}
}
