using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.DTO_s {
	public class EmployeeRecordDTO {
		public string Name { get; set; }
		public string Country { get; set; }
		public string Office { get; set; }
		public string Factory { get; set; }
		public string Department { get; set; }
		public string Team { get; set; }
	}
}