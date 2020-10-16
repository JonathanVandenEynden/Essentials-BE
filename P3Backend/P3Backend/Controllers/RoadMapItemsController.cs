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
			IRoadmapItemRepository roadMapItemsRepo) {
			_roadmapItemRepository = roadMapItemsRepo;

		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<RoadMapItem> GetRoadMapItem(int Id) {
			RoadMapItem rmi = _roadmapItemRepository.GetBy(Id);

			if (rmi == null) {

				return NotFound();
			}

			return rmi;

		}

		/// <summary>
		/// Get the RoadmapItems from a change initiative
		/// </summary>
		/// <param name="changeInitiativeId"></param>
		/// <returns></returns>
		[Route("[action]/{changeInitiativeId}")]
		[HttpGet("{changeInitiativeId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IEnumerable<RoadMapItem> GetRoadMapItemsForChangeInitiative(int changeInitiativeId) {
			ChangeInitiative ci = _changeInitiativeRepo.GetBy(changeInitiativeId);

			if (ci == null) {
				return new List<RoadMapItem>();
			}
			return ci.RoadMap;
		}

		[HttpPost]
		public IActionResult PostRoadMapItem(int changeInitiativeId, RoadMapItemDTO dto) {
			try {

				ChangeInitiative ci = _changeInitiativeRepo.GetBy(changeInitiativeId);

				RoadMapItem newRmi = new RoadMapItem(dto.Title, dto.StartDate, dto.EndDate);
				ci.RoadMap.Add(newRmi);

				_roadmapItemRepository.SaveChanges();

				return CreatedAtAction(nameof(GetRoadMapItem), new {
					id = newRmi.Id
				}, newRmi);
			}
			catch {
				return BadRequest();
			}



		}
	}
}
