using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace P3Backend.Model.DTO_s {
	public class RoadMapItemDTO {
		public string Title {
			get; set;
		}

		public DateTime StartDate {
			get; set;
		}

		public DateTime EndDate {
			get; set;
		}
	}
}
