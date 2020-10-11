using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
	public abstract class ClosedQuestion : IQuestion {
		public IList<string> Awnsers { get; set; }

		public ClosedQuestion(string questionString) : base(questionString) {
			Awnsers = new List<string>();
		}
	}
}
