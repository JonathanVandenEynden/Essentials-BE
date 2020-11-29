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

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class DashboardController : ControllerBase {
		private readonly IRoadmapItemRepository _roadmapItemRepository;
		private readonly IChangeInitiativeRepository _changeInitiativeRepository;
		private readonly ISurveyRepository _surveyRepository;
		private readonly IChangeManagerRepository _changeManagerRepository;

		public DashboardController(IRoadmapItemRepository roadMapItemsRepository, IChangeInitiativeRepository changeInitiativeRepository, ISurveyRepository surveyRepository, IChangeManagerRepository changeManagerRepository) {
			_roadmapItemRepository = roadMapItemsRepository;
			_changeInitiativeRepository = changeInitiativeRepository;
			_surveyRepository = surveyRepository;
			_changeManagerRepository = changeManagerRepository;
		}


		/// <summary>
		/// Get amount of Survey's filled in from ChangeInitiative by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("[action]/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<double> GetFilledInSurveysOfChangeInitiative(int id) {
			ChangeInitiative ci = _changeInitiativeRepository.GetBy(id);

			if (ci == null) {
				return NotFound("Item not found");
			}

			List<IAssessment> surveys = new List<IAssessment>();
			foreach (var r in ci.RoadMap) {
				surveys.Add(r.Assessment);
			}

			var total = surveys.Count();
			double filledIn = 0;

			List<Question> questions = new List<Question>();

			foreach (var s in surveys) {
				if (s != null) {
					questions.AddRange(s.Questions);
				}
			}

			foreach (var q in questions) {
				if (q.QuestionRegistered.Count() != 0) {
					filledIn++;
				}
			}

			return ((filledIn / total) * 100);
		}



		/// <summary>
		/// Get mood from ChangeInitiative by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("[action]/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<Dictionary<int, int>> GetMoodFromChangeInitiative(int id) {
			ChangeInitiative ci = _changeInitiativeRepository.GetBy(id);

			Dictionary<int, int> moods = new Dictionary<int, int>();
			moods.Add(1, 0);
			moods.Add(2, 0);
			moods.Add(3, 0);
			moods.Add(4, 0);
			moods.Add(5, 0);

			if (ci == null) {
				return NotFound("Item not found");
			}

			List<IAssessment> surveys = new List<IAssessment>();
			foreach (var r in ci.RoadMap) {
				surveys.Add(r.Assessment);
			}

			List<RangedQuestion> feedback = new List<RangedQuestion>();

			foreach (var s in surveys) {
				if (s != null) {
					feedback.Add(s.Feedback);
				}

			}

			foreach (var f in feedback) {
				if (f != null) {
					foreach (var k in f.PossibleAnswers.Keys) {

						if (k % 1 == 0 && k != 0) {
							moods[Convert.ToInt32(k)] += f.PossibleAnswers[k];
						}
					}
				}
			}

			return moods;
		}
	}
}
