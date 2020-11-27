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

		/// <summary>
		/// Add a roadmap item to a change initiative
		/// </summary>
		/// <param name="changeInitiativeId"></param>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPost("{changeInitiativeId}")]
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

		[HttpPut("{roadmapItemId}")]
		public IActionResult PutRoadMapItem(int roadmapItemId, RoadMapItemDTO dto) {
			try {
				RoadMapItem rmi = _roadmapItemRepository.GetBy(roadmapItemId);

				if (rmi == null) {
					return NotFound("Roadmap item with this id not found");
				}

				rmi.Update(dto);

				_roadmapItemRepository.SaveChanges();

				return NoContent();

			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{roadmapItemId}")]
		public IActionResult DeleteRoadMapItem(int roadmapItemId) {
			try {
				RoadMapItem rmi = _roadmapItemRepository.GetBy(roadmapItemId);

				if (rmi == null) {
					return NotFound("Roadmap item with this id not found");
				}

				//rmi.Assessment = null;
				//_roadmapItemRepository.SaveChanges();

				_roadmapItemRepository.Delete(rmi);
				_roadmapItemRepository.SaveChanges();

				return NoContent();
			}
			catch (Exception e) {
				return BadRequest(e.Message);
			}
		}
	}
}
