using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
	public abstract class ClosedQuestion : IQuestion {
		public abstract int Id { get; set; }
		public abstract string Question { get; set; }
	}
}
