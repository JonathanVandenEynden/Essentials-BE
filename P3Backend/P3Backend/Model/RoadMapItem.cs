using System;
using System.ComponentModel.DataAnnotations;

namespace P3Backend.Model {
	public class RoadMapItem {
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public IAssesment Assesment { get; set; }
		[Required]
		public bool Done { get; set; }

		[Required]
		public DateTime StartDate {
			get { return StartDate; }
			set {
				if (value <= DateTime.Now)
					throw new ArgumentException("Start must be in the future");
				else
					_ = value;
			}
		}

		[Required]
		public DateTime EndDate {
			get { return EndDate; }
			set {
				if (value <= StartDate)
					throw new ArgumentException("End must be after start");
				else
					_ = value;
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