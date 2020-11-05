using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.Questions;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;

namespace P3Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class DashboardController : ControllerBase
	{
		private readonly IRoadmapItemRepository _roadmapItemRepository;
		private readonly IChangeInitiativeRepository _changeInitiativeRepository;
		private readonly ISurveyRepository _surveyRepository;
		private readonly IChangeManagerRepository _changeManagerRepository;

		public DashboardController(IRoadmapItemRepository roadMapItemsRepository, IChangeInitiativeRepository changeInitiativeRepository, ISurveyRepository surveyRepository, IChangeManagerRepository changeManagerRepository)
		{
			_roadmapItemRepository = roadMapItemsRepository;
			_changeInitiativeRepository = changeInitiativeRepository;
			_surveyRepository = surveyRepository;
			_changeManagerRepository = changeManagerRepository;
		}


		/// <summary>
		/// Get a RoadmapItem by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<RoadMapItem> GetRoadMapItem(int id)
		{
			RoadMapItem rmi = _roadmapItemRepository.GetBy(id);

			if (rmi == null)
			{

				return NotFound("Item not found");
			}

			return rmi;

		}

		/// <summary>
		/// Get the RoadmapItems from a change initiative
		/// </summary>
		/// <param name="changeInitiativeId"></param>
		/// <returns></returns>
		[Route("[action]/{changeInitiativeId}")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<RoadMapItem>> GetRoadMapItemsForChangeInitiative(int changeInitiativeId)
		{
			ChangeInitiative ci = _changeInitiativeRepository.GetBy(changeInitiativeId);

			if (ci == null)
			{
				return NotFound("Change Initiative not found");
			}
			return ci.RoadMap.ToList();
		}

		/// <summary>
		/// Get the change initiatives applicable for a user
		/// </summary>
		/// <param name="employeeId"></param>
		/// <returns></returns>
		[Route("[action]/{employeeId}")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<ChangeInitiative>> GetChangeInitiativesForEmployee(int employeeId = 3)
		{
			// TODO niet meer hardcoded maken
			//IUser user = _userRepo.GetByEmail("Sukrit.bhattacharya@essentials.com");
			try
			{
				IEnumerable<ChangeInitiative> changes = _changeInitiativeRepository.GetForUserId(employeeId);

				return changes.ToList();
			}
			catch (Exception e)
			{
				return NotFound(e.Message);
			}
		}

		/// <summary>
		/// Get the change initiatives from a change manager
		/// </summary>
		/// <param name="changeManagerId"></param>
		/// <returns></returns>
		[Route("[action]/{changeManagerId}")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<ChangeInitiative>> GetChangeInitiativesForChangeManager(int changeManagerId = 2)
		{
			try
			{
				// TODO niet meer hardcoded maken
				ChangeManager cm = _changeManagerRepository.GetBy(changeManagerId);

				return cm.CreatedChangeInitiatives.ToList();
			}
			catch (Exception e)
			{
				return NotFound(e.Message);
			}
		}

		/// <summary>
		/// Return a change initiative by a given id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>change initiative with the given id</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<ChangeInitiative> GetChangeInitiative(int id)
		{
			ChangeInitiative ci = _changeInitiativeRepository.GetBy(id);

			if (ci == null)
			{
				return NotFound("Change initiative not found");
			}

			return ci;

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
		public ActionResult<Survey> GetSurveyByRoadmapItemId(int roadmapItemId)
		{
			try
			{
				IAssessment survey = _roadmapItemRepository.GetBy(roadmapItemId).Assessment;
				if (survey is Survey)
				{
					return (Survey)_roadmapItemRepository.GetBy(roadmapItemId).Assessment;
				}
				else
				{
					return BadRequest("The assesment was not a survey");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// Get all surveys
		/// </summary>
		/// <returns>All surveys</returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IEnumerable<Survey> GetSurveys()
		{
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
		public ActionResult<Survey> GetSurvey(int id)
		{
			try
			{
				Survey survey = _surveyRepository.GetBy(id);
				if (survey == null)
					return NotFound("There was no survey with this id");
				return survey;
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}


		/// <summary>
		/// Gives back all the questions and possible answers for a specific survey, given its Id
		/// </summary>
		/// <param name="surveyId">The Id of the survey</param>
		/// <returns>A survey with questions and possible answers</returns>
		[HttpGet("{surveyId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetQuestionsFromSurvey(int surveyId)
		{
			Survey survey = _surveyRepository.GetBy(surveyId);
			if (survey == null)
			{
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
		public ActionResult<Question> PostQuestionToSurvey(int surveyId, QuestionDTO questionDTO)
		{

			Survey survey = _surveyRepository.GetBy(surveyId);
			if (survey == null)
				return NotFound("There was no survey for the given surveyId");
			Question question = null;


			try
			{
				switch (questionDTO.Type)
				{
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

				if (question == null)
				{
					throw new Exception();
				}


				survey.Questions.Add(question);
				_surveyRepository.SaveChanges();
				return CreatedAtAction(nameof(GetQuestionsFromSurvey), new { surveyId }, question);
			}
			catch
			{
				return BadRequest("The Type of the question does not exist or is not yet implemented");
			}
		}
	}
}
