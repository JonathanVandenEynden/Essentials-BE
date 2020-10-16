using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.DTO_s {
	public class ChangeInitiativeDTO {
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
		public int SponsorId {
			get; set;
		}
		public string ChangeType {
			get; set;
		}
	}
}
