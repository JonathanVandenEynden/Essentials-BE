using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.Questions;
using P3Backend.Model.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Backend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SurveyController : ControllerBase {
        public readonly ISurveyRepository _surveyRepository;
        public readonly IPresetRepository _presetRepository;
        public readonly IRoadmapItemRepository _roadmapItemRepository;

        public SurveyController(ISurveyRepository surveyRepository, IRoadmapItemRepository roadMapItemRepository, IPresetRepository presetRepository) {
            _surveyRepository = surveyRepository;
            _presetRepository = presetRepository;
            _roadmapItemRepository = roadMapItemRepository;
        }


        /// <summary>
        /// Get all surveys
        /// </summary>
        /// <returns>All surveys</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = "EmployeeAccess")]
        public IEnumerable<Survey> GetSurveys() {
            IEnumerable<Survey> surveys = _surveyRepository.GetAll();
            return surveys;
        }

        /// <summary>
        /// Get survey with a given surveyId
        /// </summary>
        /// <param name="id">The id of the survey</param>
        /// <returns>One survey by id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "EmployeeAccess")]
        public ActionResult<Survey> GetSurvey(int id) {
            try {
                Survey survey = _surveyRepository.GetBy(id);
                if (survey == null)
                    return NotFound("There was no survey with this id");
                return survey;
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

        }


        /// <summary>
        /// Make an empty survey for a given roadmapItem or convert a presetsurvey to a normal survey
        /// </summary>
        /// <param name="roadmapItemId">id of the roadmapItem</param>
        /// <param name="thema">Optional parameter, if you need a presetsurvey</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "ChangeManagerAccess")]
        public ActionResult<Survey> PostSurvey(int roadmapItemId, string thema = null) {
            try {
                RoadMapItem rmi = _roadmapItemRepository.GetBy(roadmapItemId);                

                if (rmi == null) {
                    return NotFound("Roadmap item not found");
                }

                // Delete old Survey from db
                if (rmi.Assessment != null) {
                    _surveyRepository.Delete(rmi.Assessment as Survey);
                }
                Survey survey = new Survey(rmi);

                if (thema != null) {
                    PresetSurvey ps = _presetRepository.GetBy(thema);
                    foreach(Question q in ps.PresetQuestions) {
                        Question question = null;
                        switch (q.Type) {
                            case QuestionType.MULTIPLECHOICE:
                                question = new MultipleChoiceQuestion(q.QuestionString);
                                break;
                            case QuestionType.RANGED:
                                question = new RangedQuestion(q.QuestionString);
                                break;
                            case QuestionType.YESNO:
                                question = new YesNoQuestion(q.QuestionString);
                                break;
                            case QuestionType.OPEN:
                                question = new OpenQuestion(q.QuestionString);
                                break;
                        }
                        if (question == null) {
                            throw new Exception("Question was not defined");
                        } else {
                            survey.Questions.Add(question);
                        }
                    }                    
                    
                }

                rmi.Assessment = survey;
                _roadmapItemRepository.SaveChanges();
                return CreatedAtAction(nameof(GetSurvey), new { id = survey.Id }, survey);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get survey with a given RoadmapItemId
        /// </summary>
        /// <param name="roadmapItemId">id of the roadmapItem</param>
        /// <returns>One survey by roadmapItemId</returns>
        [Route("[action]/{roadmapItemId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "EmployeeAccess")]
        public ActionResult<Survey> GetSurveyByRoadmapItemId(int roadmapItemId) {
            try {
                IAssessment survey = _roadmapItemRepository.GetBy(roadmapItemId).Assessment;
                if (survey == null)
                    return NotFound("The Assessment with given Id does not exist");
                if (survey is Survey) {
                    return (Survey)_roadmapItemRepository.GetBy(roadmapItemId).Assessment;
                } else {
                    return BadRequest("The assesment was not a survey");
                }
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        


        /// <summary>
        /// Delete a survey with a given roadmapItemId
        /// </summary>
        /// <param name="roadmapItemId">id of the roadmapItem</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{roadmapItemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "ChangeManagerAccess")]
        public IActionResult DeleteSurveyByRoadmapItemId(int roadmapItemId) {
            try {
                RoadMapItem roadmapItem = _roadmapItemRepository.GetBy(roadmapItemId);
                if (roadmapItem == null)
                    return NotFound("Roadmap with given id does not exist");
                IAssessment assesment = roadmapItem.Assessment;
                if (assesment == null)
                    return NotFound("Roadmap doesn't have a survey or the survey is allready deleted");
                roadmapItem.Assessment = null;
                _surveyRepository.Delete((Survey)assesment);
                _surveyRepository.SaveChanges();
                return NoContent();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        

    }
}
