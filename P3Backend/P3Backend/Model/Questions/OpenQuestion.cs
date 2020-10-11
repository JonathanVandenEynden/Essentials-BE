using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
	public class OpenQuestion : IQuestion {
		public OpenQuestion(string questionString) : base(questionString) {
		}

		public string Answer { get; set; }
	}
}
