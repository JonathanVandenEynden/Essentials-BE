using P3Backend.Model.OrganizationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Team : IOrganizationPart {
		// enum van maken?

		public Team(string name) : base(name) {
		}

		protected Team() {
			// EF
		}
	}
}
