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
        /// Make a question with a given surveyId (if you want to add possibleAnswers to multiplechoicequestions, see other api-call)
        /// </summary>
        /// <param name="surveyId">The Id of the survey</param>
        /// <param name="questionDTO">The questionString and type of the question</param>
        /// <returns></returns>
        [HttpPost("{surveyId}")]
        public ActionResult<Question> PostClosedQuestionToSurvey(int surveyId, QuestionDTO questionDTO) {         
            
            Survey survey = _surveyRepository.GetBy(surveyId);
            Question question;

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
                default:
                    question = new OpenQuestion(questionDTO.QuestionString);                    
                    break;
            }

            survey.Questions.Add(question);
            _surveyRepository.SaveChanges();
            return CreatedAtAction(nameof(GetQuestionsFromSurvey), new { surveyId }, question);
        }

        [HttpPost]
        public ActionResult AddQuestionsToMultipleChoice(int questionId, List<string> possibleAnswers, bool initialize = false) {
            Question question = _surveyRepository.GetQuestion(questionId);
            // TODO Userid veranderen naar User.identity.Name/_usermanager
            int userid = 1;
            if (!initialize) {
                question.CompleteQuestion(userid);
            }

            switch (question.Type) {
                case QuestionType.MULTIPLECHOICE:
                    ((MultipleChoiceQuestion)question).AddPossibleAnswers(possibleAnswers);
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
