using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Admin : IUser {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<ChangeInitiative> changeInitiatives { get; set; }
	}
}
