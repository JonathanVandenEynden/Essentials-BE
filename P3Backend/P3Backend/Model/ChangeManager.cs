using System.Collections.Generic;

namespace P3Backend.Model {
	public class ChangeManager : IUser {
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