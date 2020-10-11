using P3Backend.Model.OrganizationParts;
using System.Collections.Generic;

namespace P3Backend.Model.Users {
	public class ChangeManager : IUser {
		public Country Country { get; set; }
		public Office Office { get; set; }
		public Factory Factory { get; set; }
		public Department Department { get; set; }
		public Team Team { get; set; }


		public IList<ChangeInitiative> MyChangeInitiatives { get; set; }

		// TODO mappping of the classes
		//public IList<ChangeInitiative> CreatedChangeInitiatives { get; set; }

		public ChangeManager(string firstName, string lastName, string email) {
			FirstName = firstName;
			LastName = lastName;
			Email = email;

			MyChangeInitiatives = new List<ChangeInitiative>();
			// TODO mappping of the classes
			//CreatedChangeInitiatives = new List<ChangeInitiative>();

		}
	}

}