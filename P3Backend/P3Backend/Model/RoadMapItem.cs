using System;

namespace P3Backend.Model {
	public class RoadMapItem {
		public int Id { get; set; }
		public string Title { get; set; }
		public IAssesment Assesment { get; set; }
		public bool Done { get; set; }

		public DateTime StartDate {
			get { return StartDate; }
			set {
				if (value <= DateTime.Now)
					throw new ArgumentException("Start must be in the future");
				else
					_ = value;
			}
		}
		public DateTime EndDate {
			get { return EndDate; }
			set {
				if (value <= StartDate)
					throw new ArgumentException("End must be after start");
				else
					_ = value;
			}
		}

		// TODO start en end bijzetten
		public RoadMapItem(string title,) {
			Title = title;

			Done = false;

		}

		protected RoadMapItem() {
			// EF
		}
	}
}