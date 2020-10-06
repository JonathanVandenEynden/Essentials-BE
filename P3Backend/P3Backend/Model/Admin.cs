using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Admin : IUser {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Country Country { get; set; }
		public Office Office { get; set; }
		public Factory Factory { get; set; }
		public Department Department { get; set; }
		public Team Team { get; set; }
		public List<ChangeInitiative> changeInitiatives { get; set; }
		public List<OrganizationPart> OrganizationPart { get; set; }
	}
}
