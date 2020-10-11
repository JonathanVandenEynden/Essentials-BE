using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.OrganizationParts {
	public class Country : IOrganizationPart {
		// enum van maken?

		public Country(string name) : base(name) {
		}

		protected Country() {
			// EF
		}
	}

}
