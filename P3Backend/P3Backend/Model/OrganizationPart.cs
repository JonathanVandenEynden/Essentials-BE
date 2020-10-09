namespace P3Backend.Model {
	public abstract class OrganizationPart {
		public int Id { get; set; }
		public Organization Organization { get; set; }
	}
}