using System.Collections;
using System.Collections.Generic;

namespace P3Backend.Model.OrganizationParts {
	public class OrganizationPart {
		public int Id { get; set; }

		public string Name { get; set; }

		public IList<IUser> Users { get; set; }

		public OrganizationPartType Type { get; set; }

		public OrganizationPart(string name, OrganizationPartType type) {
			Name = name;
			Type = type;

			Users = new List<IUser>();
		}
		protected OrganizationPart() {
			// EF
		}
	}
}