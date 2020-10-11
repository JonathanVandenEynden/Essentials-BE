using Microsoft.AspNetCore.Identity;
using P3Backend.Model.OrganizationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public abstract class IUser {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public Country Country { get; set; }
		public Office Office { get; set; }
		public Factory Factory { get; set; }
		public Department Department { get; set; }
		public Team Team { get; set; }


		public List<ChangeInitiative> ChangeInitiatives { get; set; }

		public List<IOrganizationPart> OrganizationPart { get; set; }
		public IUser() {

		}

	}
}
