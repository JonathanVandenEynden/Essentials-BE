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


        [HttpGet("{surveyId}")]
        public Survey GetQuestionsFromSurvey(int surveyId) {

            Survey survey = _surveyRepository.GetBy(surveyId);
            //survey.Questions.Add(survey.Feedback);
            return survey;
        }

        /*[HttpGet]
        [Route("Feedback/{surveyId}")]
        public ActionResult<IQuestion> GetFeedbackFromSurvey(int surveyId) {
            try {
                ClosedQuestion feedback = _surveyRepository.GetBy(surveyId).Feedback;
                if (feedback == null)
                    return NotFound("There was not yet a feedback question implemented");
                return feedback;
            }catch(Exception e) {
                return BadRequest(e.Message);
            }            
        }*/

        [HttpPost]
        [Route("ClosedQuestion/{surveyId}")]
        public ActionResult<ClosedQuestion> PostClosedQuestionToSurvey(int surveyId, ClosedQuestionDTO dto) {
            ClosedQuestion closedQuestion = new ClosedQuestion(dto.QuestionString, dto.MaxAmount);
            List<Answer> possibleAnswers = new List<Answer>();
            dto.PossibleAnswers.ForEach(e => possibleAnswers.Add(new Answer(e.AnswerString)));

            closedQuestion.PossibleAnswers = possibleAnswers;
            _surveyRepository.GetBy(surveyId).Questions.Add(closedQuestion);
            _surveyRepository.SaveChanges();
            return CreatedAtAction(nameof(GetQuestionsFromSurvey), new { surveyId = closedQuestion.Id }, closedQuestion);
        }

        /*[HttpPost]
        [Route("OpenQuestion/{surveyId}")]
        public ActionResult<OpenQuestion> PostOpenQuestionToSurvey(int surveyId, OpenQuestionDTO dto) {
            OpenQuestion openQuestion = new OpenQuestion(dto.Answer);
            _surveyRepository.GetBy(surveyId).Questions.Add(openQuestion);
            _surveyRepository.SaveChanges();
            return CreatedAtAction(nameof(GetQuestionsFromSurvey), new { surveyId = openQuestion.Id }, openQuestion);
        }*/

        [HttpDelete("{surveyId}")]
        public IActionResult DeleteQuestions(int surveyId) {
            IAssesment survey = _surveyRepository.GetBy(surveyId);
            survey.Questions.Clear();
            _surveyRepository.SaveChanges();
            return NoContent();
        }
    }
}
