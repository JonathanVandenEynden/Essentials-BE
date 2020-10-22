using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class ChangeGroup {
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		public List<IUser> Users { get; set; }


		public ChangeGroup(string name) {
			Name = name;

			Users = new List<IUser>();
		}

		protected ChangeGroup() {
			// EF
		}
	}
}
