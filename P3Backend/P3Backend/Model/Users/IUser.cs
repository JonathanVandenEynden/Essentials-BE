using Microsoft.AspNetCore.Identity;
using P3Backend.Model.OrganizationParts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public abstract class IUser {
		public int Id { get; set; }

		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[EmailAddress]
		public string Email { get; set; }

		public IUser(string firstName, string lastName, string email) {
			FirstName = firstName;
			LastName = lastName;
			Email = email;
		}

		protected IUser() {
			// EF
		}

	}
}
