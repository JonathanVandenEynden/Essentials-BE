namespace P3Backend.Model {
	public class RoadMapItem {
		public int Id { get; set; }
		public string Title { get; set; }
		public IAssesment? Assesment { get; set; }
		public bool Done { get; set; }

		public RoadMapItem(string title) {
			Title = title;

			Done = false;

		}

		protected RoadMapItem() {
			// EF
		}
	}
}