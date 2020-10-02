using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class ChangeInitiative {
		public int Id { get; set; }
		public String Description { get; set; }
		public DateTime startDate { get; set; }
		public DateTime EndDate { get; set; }
		public ChangeManager changeManager { get; set; }
		public Employee changeSponsor { get; set; }
		public List<IAssesment> Assesments { get; set; }
		public IChangeType changeType { get; set; }
	}
}
