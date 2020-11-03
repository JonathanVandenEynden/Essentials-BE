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

namespace P3Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class QuestionsController : ControllerBase
    {
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
        public Survey GetQuestionsFromSurvey(int surveyId) {

            Survey survey = _surveyRepository.GetBy(surveyId);
            return survey;
        }
        
        /// <summary>
        /// Make questions and possible answers for a specific survey, given its Id
        /// </summary>
        /// <param name="surveyId">The Id of the survey</param>
        /// <param name="dto">Questions and possible answers</param>
        /// <returns></returns>
        [HttpPost("{surveyId}")]
        public ActionResult<ClosedQuestion> PostClosedQuestionToSurvey(int surveyId, ClosedQuestionDTO questionsDTO) {
            ClosedQuestion closedQuestion = new ClosedQuestion(questionsDTO.QuestionString, questionsDTO.MaxAmount);
            List<Answer> possibleAnswers = new List<Answer>();
            questionsDTO.PossibleAnswers.ForEach(e => possibleAnswers.Add(new Answer(e.AnswerString)));

            closedQuestion.PossibleAnswers = possibleAnswers;
            _surveyRepository.GetBy(surveyId).Questions.Add(closedQuestion);
            _surveyRepository.SaveChanges();
            return CreatedAtAction(nameof(GetQuestionsFromSurvey), new { surveyId = closedQuestion.Id }, closedQuestion);
        }

        /// <summary>
        /// Delete all the questions and possible answers from the survey (This won't delete the survey, only its questions. If you Get this survey after using this API-call, you will get back a survey with an emtpy list of Questions)
        /// </summary>
        /// <param name="surveyId">The Id of the survey</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{surveyId}")]
        public IActionResult DeleteQuestions(int surveyId) {
            IAssessment survey = _surveyRepository.GetBy(surveyId);
            survey.Questions.Clear();
            _surveyRepository.SaveChanges();
            return NoContent();
        }
    }
}
