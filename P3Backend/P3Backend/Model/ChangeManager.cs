using System.Collections.Generic;

namespace P3Backend.Model {
	public class ChangeManager : IUser {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<ChangeInitiative> changeInitiatives { get; set; }
	}

}