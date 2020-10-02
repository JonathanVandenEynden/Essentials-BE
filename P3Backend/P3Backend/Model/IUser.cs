using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public interface IUser {
		public int Id { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }

		public List<ChangeInitiative> changeInitiatives { get; set; }

		public List<OrganizationPart> OrganizationPart { get; set; }
	}
}
