using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Users {
	public class Admin : IUser {


		public Admin(string firstName, string lastName, string email) {
			FirstName = firstName;
			LastName = lastName;
			Email = email;
		}
	}
}
