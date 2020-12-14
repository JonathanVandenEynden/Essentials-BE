using System;
using System.Collections.Generic;

namespace P3Backend.Model.DTO_s {
	public class ChangeInitiativeDTO {
		public int Id { get; set; }
		public string Name {
			get; set;
		}
		public string Description {
			get; set;
		}
		public DateTime StartDate {
			get; set;
		}
		public DateTime EndDate {
			get; set;
		}
		public EmployeeDTO Sponsor {
			get; set;
		}
		public string ChangeType {
			get; set;
		}
		public ChangeGroupDTO ChangeGroupDto { get; set; }
	}
}
