using P3Backend.Model.TussenTabellen;
using System.Collections;
using System.Collections.Generic;

namespace P3Backend.Model.OrganizationParts {
	public class OrganizationPart {
		public int Id { get; set; }

		public string Name { get; set; }

		public IList<UserOrganizationPart> UserOrganizationParts { get; set; }

		public OrganizationPartType Type { get; set; }

		public OrganizationPart(string name, OrganizationPartType type) {
			Name = name;
			Type = type;

			UserOrganizationParts = new List<UserOrganizationPart>();
		}
		protected OrganizationPart() {
			// EF
		}
	}
}