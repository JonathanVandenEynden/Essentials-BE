using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.DTO_s {
	public class ChangeGroupDTO {
		public string Name { get; set; }
		public List<int> UserIds { get; set; }
	}
}
