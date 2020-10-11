using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Portfolio {
		public int Id { get; set; }

		public List<Project> Projects { get; set; }

		public Portfolio() {

			Projects = new List<Project>();
		}

	}
}
