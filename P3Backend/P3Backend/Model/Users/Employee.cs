using P3Backend.Model.OrganizationParts;
using P3Backend.Model.TussenTabellen;
using System.Collections.Generic;
using System.ComponentModel;

namespace P3Backend.Model.Users {
	public class Employee : IUser {
		public List<UserOrganizationPart> UserOrganizationParts { get; set; }


		public Employee(string firstName, string lastName, string email) : base(firstName, lastName, email) {
			UserOrganizationParts = new List<UserOrganizationPart>();
		}

		protected Employee() {
			// EF
		}
	}
}
