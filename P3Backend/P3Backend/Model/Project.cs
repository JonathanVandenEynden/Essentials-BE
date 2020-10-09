using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Project {
		public int Id { get; set; }
		public List<ChangeInitiative> ChangeInitiatives { get; set; }
	}
}
