﻿using NJsonSchema.Annotations;
using P3Backend.Model.OrganizationParts;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Organization {
		private string name;

		public int Id { get; set; }

		[Required]
		public string Name {
			get { return name; }
			set {
				if (value == null)
					throw new ArgumentException("Name of organization should not be null");
				else
					name = value;
            }
        }
		[Required]
		public List<Employee> Employees { get; set; }
		[Required]
		public List<ChangeManager> ChangeManagers { get; set; }

		public List<OrganizationPart> OrganizationParts { get; set; }

		[Required]
		public Portfolio Portfolio { get; set; }

		public Organization(string name, List<Employee> employees, ChangeManager cm) {
			Name = name;
			Employees = employees;
			ChangeManagers = new List<ChangeManager>() { cm };

			OrganizationParts = new List<OrganizationPart>();
			Portfolio = new Portfolio();
		}

		protected Organization() {
			// EF
		}
	}
}
