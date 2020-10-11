using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Project {
		public int Id { get; set; }
		public string Name { get; set; }
		public List<ChangeInitiative> ChangeInitiatives { get; set; }

		public Project() {
			ChangeInitiatives = new List<ChangeInitiative>();
		}

		protected Project() {
			// EF
		}
	}
}
