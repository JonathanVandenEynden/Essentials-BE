using P3Backend.Model.DTO_s;
using System;
using System.ComponentModel.DataAnnotations;

namespace P3Backend.Model {
	public class RoadMapItem {
		private string _title;
		private DateTime _startDate;
		private DateTime _endDate;
		private RoadmapItemPhase _phase;

		public int Id { get; set; }
		[Required]
		public string Title {
			get { return _title; }
			set {
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Title should not be emtpy");
				else
					_title = value;
			}
		}

		public IAssessment Assessment { get; set; }

		private bool _done;
		[Required]
		public bool Done { get => _done ? _done : DateTime.Now > EndDate; set { _done = value; } }

		[Required]
		public DateTime StartDate {
			get { return _startDate; }
			set {
				if (value < DateTime.Now)
					throw new ArgumentException("Start must be in the future");
				else
					_startDate = value;
			}
		}

		[Required]
		public DateTime EndDate {
			get { return _endDate; }
			set {
				if (value <= StartDate)
					throw new ArgumentException("End must be after start");
				else
					_endDate = value;
			}
		}

		public RoadmapItemPhase Phase {
			get {
				return _phase;
			}
			set {
				if (value != null) {
					_phase = value;
				}
				else {
					_phase = RoadmapItemPhase.Preparationphase;
				}
			}
		}

		public RoadMapItem(string title, DateTime start, DateTime end) {
			Title = title;
			StartDate = start;
			EndDate = end;
			_done = false;
			Phase = RoadmapItemPhase.Preparationphase;
		}

		protected RoadMapItem() {
			// EF
		}

		public void Update(RoadMapItemDTO dto) {
			Title = dto.Title;
			_startDate = dto.StartDate;
			EndDate = dto.EndDate;
			Phase = dto.Phase;
		}
	}
}