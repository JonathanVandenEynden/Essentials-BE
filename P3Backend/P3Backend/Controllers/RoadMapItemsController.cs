using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.RepoInterfaces;

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class RoadMapItemsController : ControllerBase {

		private readonly IRoadmapItemRepository _roadmapItemRepository;
		private readonly IChangeInitiativeRepository _changeInitiativeRepo;

		public RoadMapItemsController(
			IRoadmapItemRepository roadMapItemsRepo,
			IChangeInitiativeRepository changeInitiativeRepo) {
			_roadmapItemRepository = roadMapItemsRepo;
			_changeInitiativeRepo = changeInitiativeRepo;

		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<RoadMapItem> GetRoadMapItem(int id) {
			RoadMapItem rmi = _roadmapItemRepository.GetBy(id);

			if (rmi == null) {

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
		public ActionResult<IEnumerable<RoadMapItem>> GetRoadMapItemsForChangeInitiative(int changeInitiativeId) {
			ChangeInitiative ci = _changeInitiativeRepo.GetBy(changeInitiativeId);

			if (ci == null) {
				return NotFound("Change Initiative not found");
			}
			return ci.RoadMap.ToList();
		}

		[HttpPost]
		public IActionResult PostRoadMapItem(int changeInitiativeId, RoadMapItemDTO dto) {
			try {

				ChangeInitiative ci = _changeInitiativeRepo.GetBy(changeInitiativeId);

				if (ci == null) {
					return NotFound("Change Initiative not found");
				}

				RoadMapItem newRmi = new RoadMapItem(dto.Title, dto.StartDate, dto.EndDate);
				ci.RoadMap.Add(newRmi);

				_roadmapItemRepository.SaveChanges();

				return CreatedAtAction(nameof(GetRoadMapItem), new {
					id = newRmi.Id
				}, newRmi);
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}



		}
	}
}
