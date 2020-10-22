namespace P3Backend.Model.OrganizationParts {
	public class OrganizationPart {
		public int Id {
			get; set;
		}

		public string Name {
			get; set;
		}

		public OrganizationPartType Type {
			get; set;
		}

		protected OrganizationPart(string name, OrganizationPartType type) {
			Name = name;
			Type = type;
		}
		protected OrganizationPart() {
			// EF
		}
	}
}