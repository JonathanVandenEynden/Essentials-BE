using P3Backend.Model.OrganizationParts;
using System.Collections.Generic;
using System.ComponentModel;

namespace P3Backend.Model.Users {
	public class Employee : IUser {
		public List<OrganizationPart> OrganizationParts {
			get; set;
		}


		public Employee(string firstName, string lastName, string email) : base(firstName, lastName, email) {
			this.OrganizationParts = new List<OrganizationPart>();
		}

		protected Employee() {
			// EF
		}
	}
}
