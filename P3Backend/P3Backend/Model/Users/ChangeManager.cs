using P3Backend.Model.OrganizationParts;
using System.Collections.Generic;

namespace P3Backend.Model.Users {
	public class ChangeManager : IUser {
		public Country Country { get; set; }
		public Office Office { get; set; }
		public Factory Factory { get; set; }
		public Department Department { get; set; }
		public Team Team { get; set; }

		public List<ChangeGroup> ChangeGroups { get; set; }
		public IList<ChangeInitiative> CreatedChangeInitiatives { get; set; }

		//public IList<ChangeInitiative> CreatedChangeInitiatives { get; set; }

		public ChangeManager(string firstName, string lastName, string email) {
			FirstName = firstName;
			LastName = lastName;
			Email = email;

			ChangeGroups = new List<ChangeGroup>();
			CreatedChangeInitiatives = new List<ChangeInitiative>();

		}

		protected ChangeManager() {
			// EF
		}
	}

}