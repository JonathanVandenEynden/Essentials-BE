using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Project {
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public List<ChangeInitiative> ChangeInitiatives { get; set; }

		public Project(string name) {
			Name = name;
			ChangeInitiatives = new List<ChangeInitiative>();
		}

		protected Project() {
			// EF
		}

	}
}
