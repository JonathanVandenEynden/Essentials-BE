using P3Backend.Model.OrganizationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Organization {
		public int Id { get; set; }

		public string Name { get; set; }
		public IList<IOrganizationPart> OrganizationParts { get; set; }

		public Portfolio Portfolio { get; set; }

		public Organization(string name) {
			Name = name;

			OrganizationParts = new List<IOrganizationPart>();
			Portfolio = new Portfolio();
		}

		protected Organization() {
			// EF
		}
	}
}
