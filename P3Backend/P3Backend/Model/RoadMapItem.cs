using System;

namespace P3Backend.Model {
	public class RoadMapItem {
		public string Titel { get; set; }
		public IAssesment? Assesment { get; set; }
		public bool Done { get; set; }
		public DateTime Start { get; set; }
		public DateTime End {
			get {
				return End;
			}
			set {
				if (value < Start)
					throw new ArgumentException();
				else
					_ = value;
			}
		}

	}
}