using P3Backend.Model.ChangeTypes;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class ChangeInitiative {
		public int Id { get; set; }
		public String Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public ChangeManager ChangeManager { get; set; }
		public Employee ChangeSponsor { get; set; }
		public IChangeType ChangeType { get; set; }
		public List<RoadMapItem> RoadMap { get; set; }
	}
}
