using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public abstract class ClosedQuestion : Question {
		public abstract int Id { get; set; }
		public abstract string Question { get; set; }
	}
}
