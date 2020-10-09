namespace P3Backend.Model {
	public class RoadMapItem {
		public int Id { get; set; }
		public string Titel { get; set; }
		public IAssesment? Assesment { get; set; }
		public bool Done { get; set; }
	}
}