namespace P3Backend.Model.OrganizationParts {
	public abstract class IOrganizationPart {
		public int Id { get; set; }

		public string Name { get; set; }

		protected IOrganizationPart(string name) {
			Name = name;
		}
	}
}