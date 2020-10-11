namespace P3Backend.Model.OrganizationParts {
	public abstract class IOrganizationPart {
		public int Id { get; set; }
		public Organization Organization { get; set; }
	}
}