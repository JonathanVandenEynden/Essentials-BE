using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model;


namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class SurveyController : ControllerBase {
		public readonly ISurveyRepository _surveyRepository;
		public readonly IRoadmapItemRepository _roadmapItemRepository;

		public SurveyController(ISurveyRepository surveyRepository, IRoadmapItemRepository roadMapItemRepository) {
			_surveyRepository = surveyRepository;
			_roadmapItemRepository = roadMapItemRepository;
		}

		/// <summary>
		/// Get all surveys
		/// </summary>
		/// <returns>All surveys</returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
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
		public ActionResult<Survey> GetSurvey(int id) {
			try {
				Survey survey = _surveyRepository.GetBy(id);
				if (survey == null)
					return NotFound("There was no survey with this id");
				return survey;
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}

		}

		/// <summary>
		/// Make an empty survey for a given roadmapItem
		/// </summary>
		/// <param name="roadmapItemId">id of the roadmapItem</param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Survey> PostSurvey(int roadmapItemId) {
			try {
				Survey survey = new Survey();
				_roadmapItemRepository.GetBy(roadmapItemId).Assesment = survey;
				_roadmapItemRepository.SaveChanges();
				return CreatedAtAction(nameof(GetSurvey), new { id = survey.Id }, survey);
			}
			catch (Exception e) {
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
		public ActionResult<Survey> GetSurveyByRoadmapItemId(int roadmapItemId) {
			try {
				IAssesment survey = _roadmapItemRepository.GetBy(roadmapItemId).Assesment;
				if (survey is Survey) {
					return (Survey)_roadmapItemRepository.GetBy(roadmapItemId).Assesment;
				}
				else {
					return BadRequest("The assesment was not a survey");
				}
			}
			catch (Exception e) {
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
		public IActionResult DeleteSurveyByRoadmapItemId(int roadmapItemId) {
			try {
				RoadMapItem roadmapItem = _roadmapItemRepository.GetBy(roadmapItemId);
				if (roadmapItem == null)
					return NotFound("Roadmap with given id does not exist");
				IAssesment assesment = roadmapItem.Assesment;
				if (assesment == null)
					return NotFound("Roadmap doesn't have a survey or the survey is allready deleted");
				roadmapItem.Assesment = null;
				_surveyRepository.Delete((Survey)assesment);
				_surveyRepository.SaveChanges();
				return NoContent();
			}
			catch {
				return BadRequest();
			}
		}

	}
}
