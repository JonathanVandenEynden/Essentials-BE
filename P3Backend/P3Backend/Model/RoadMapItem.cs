using System;
using System.ComponentModel.DataAnnotations;

namespace P3Backend.Model {
	public class RoadMapItem {
		private DateTime _startDate;
		private DateTime _endDate;

		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public IAssessment Assessment { get; set; }
		[Required]
		public bool Done { get; set; }

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

		public RoadMapItem(string title, DateTime start, DateTime end) {
			Title = title;
			StartDate = start;
			EndDate = end;
			Done = false;
		}

		protected RoadMapItem() {
			// EF
		}
	}
}