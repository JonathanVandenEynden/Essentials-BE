using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class ChangeGroup {
		public int Id { get; set; }

		public List<IUser> Users { get; set; }

		public ChangeInitiative changeInitiative { get; set; }


	}
}
