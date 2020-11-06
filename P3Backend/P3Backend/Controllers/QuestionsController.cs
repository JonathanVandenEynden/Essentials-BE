using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Data.Repositories;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.Questions;
using P3Backend.Model.RepoInterfaces;

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class QuestionsController : ControllerBase {
		private readonly ISurveyRepository _surveyRepository;

		public QuestionsController(ISurveyRepository surveyRepository) {
			_surveyRepository = surveyRepository;
		}

		/// <summary>
		/// Gives back all the questions and possible answers for a specific survey, given its Id
		/// </summary>
		/// <param name="surveyId">The Id of the survey</param>
		/// <returns>A survey with questions and possible answers</returns>
		[HttpGet("{surveyId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetQuestionsFromSurvey(int surveyId) {
			Survey survey = _surveyRepository.GetBy(surveyId);
			if (survey == null) {
				return NotFound();
			}
			return Ok(survey);
		}

		/// <summary>
		/// Make a question with a given surveyId
		/// </summary>
		/// <param name="surveyId">The Id of the survey</param>
		/// <param name="questionDTO">The questionString and type of the question</param>
		/// <returns>The question that was created</returns>        
		[HttpPost("{surveyId}")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<Question> PostQuestionToSurvey(int surveyId, QuestionDTO questionDTO) {

			Survey survey = _surveyRepository.GetBy(surveyId);
			if (survey == null)
				return NotFound("There was no survey for the given surveyId");
			Question question = null;


			try {
				switch (questionDTO.Type) {
					case QuestionType.MULTIPLECHOICE:
					question = new MultipleChoiceQuestion(questionDTO.QuestionString);
					break;
					case QuestionType.RANGED:
					question = new RangedQuestion(questionDTO.QuestionString);
					break;
					case QuestionType.YESNO:
					question = new YesNoQuestion(questionDTO.QuestionString);
					break;
					case QuestionType.OPEN:
					question = new OpenQuestion(questionDTO.QuestionString);
					break;
				}

				if (question == null) {
					throw new Exception();
				}


				survey.Questions.Add(question);
				_surveyRepository.SaveChanges();
				return CreatedAtAction(nameof(GetQuestionsFromSurvey), new { surveyId }, question);
			}
			catch {
				return BadRequest("The Type of the question does not exist or is not yet implemented");
			}
		}


		/// <summary>
		/// Make answer(s) for a specific survey
		/// </summary>
		/// <param name="questionId">The Id of the question</param>
		/// <param name="possibleAnswers">List of strings for chosen answers (new or filled in)</param>
		/// <param name="initialize">Wether the question is being created or not</param>
		/// <returns>NoContent</returns>
		[Route("[action]/{questionId}")]
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult PostAnswerToQuestion(int questionId, List<string> possibleAnswers, bool initialize = false) {
			Question question = _surveyRepository.GetQuestion(questionId);
			if (question == null) {
				return NotFound("There was no question for the given questionId");
			}

			// TODO Userid veranderen naar User.identity.Name/_usermanager
			int userid = 1;
			if (!initialize) {
				question.CompleteQuestion(userid);
			}

			try {
				switch (question.Type) {
					case QuestionType.MULTIPLECHOICE:
					((MultipleChoiceQuestion)question).AddPossibleAnswers(possibleAnswers, initialize);
					break;
					case QuestionType.RANGED:
					((RangedQuestion)question).AddAnswer(possibleAnswers);
					break;
					case QuestionType.YESNO:
					((YesNoQuestion)question).AddAnswer(possibleAnswers);
					break;
					default:
					((OpenQuestion)question).AddAnswer(possibleAnswers);
					break;
				}
				_surveyRepository.UpdateQuestions(question);
				_surveyRepository.SaveChanges();
				return NoContent();
			}
			catch {
				return BadRequest("The Type of the question does not exist or is not yet implemented");
			}
		}


		/// <summary>
		/// Delete all the questions and possible answers from the survey (This won't delete the survey, only its questions. If you Get this survey after using this API-call, you will get back a survey with an emtpy list of Questions)
		/// </summary>
		/// <param name="surveyId">The Id of the survey</param>
		/// <returns>NoContent</returns>
		[HttpDelete("{surveyId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult DeleteQuestions(int surveyId) {
			IAssessment survey = _surveyRepository.GetBy(surveyId);
			if (survey == null) {
				return NotFound("The survey with given Id does not exist or was allready deleted");
			}
			survey.Questions.Clear();
			_surveyRepository.SaveChanges();
			return NoContent();
		}
	}
}
