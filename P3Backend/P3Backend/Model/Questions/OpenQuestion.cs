using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
	public class OpenQuestion : IQuestion {
		public int Id { get; set; }
		public string Question { get; set; }


		public string Answer { get; set; }
	}
}
