using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class MultipleChoice : ClosedQuestion {
		public override int Id { get; set; }
		public override string Question { get; set; }
	}
}
