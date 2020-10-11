using P3Backend.Model.OrganizationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Organization {
		public int Id { get; set; }
		public List<IOrganizationPart> OrganizationParts { get; set; }
	}
}
