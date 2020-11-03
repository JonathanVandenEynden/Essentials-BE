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
    public class QuestionsController<X> : ControllerBase
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
        /// <param name=""></param>
        /// <param name="surveyId">The Id of the survey</param>
        /// <param name="dto">Questions and possible answers</param>
        /// <returns></returns>
        [HttpPost("{surveyId}")]
        public ActionResult<Question<X>> PostClosedQuestionToSurvey(int surveyId, QuestionDTO questionDTO, List<string>? possibleAnwsers) {

            Question<X> question;
            if(questionDTO.Type == QuestionType.MULTIPLECHOICE) {

                question = new MultipleChoiceQuestion<X>(questionDTO.QuestionString);
                
            } else if (questionDTO.Type == QuestionType.YESNO) {
                question = new YesNoQuestion(questionDTO.QuestionString);
            } else {
                OpenQuestion question = new OpenQuestion(questionDTO.QuestionString);
            }
            return null;

            

            /*switch (questionDTO.Type) {                

                case QuestionType.MULTIPLECHOICE:
                    MultipleChoiceQuestion question = new MultipleChoiceQuestion(questionDTO.QuestionString);
                    
                    break;
                case QuestionType.RANGED:
                    RangedQuestion question = new RangedQuestion(questionDTO.QuestionString);
                    break;
                case QuestionType.YESNO:
                    question = new YesNoQuestion(questionDTO.QuestionString);
                    break;
                case QuestionType.OPEN:
                    question = new OpenQuestion(questionDTO.QuestionString);
                    break;
            }*/

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
