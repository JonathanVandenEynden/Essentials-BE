using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
	public class ClosedQuestion : IQuestion {

		public IList<Answer> PossibleAnswers { get; set; }
		public bool IsMulti { get; set; }

		public ClosedQuestion(string questionString, bool multi) : base(questionString) {
			PossibleAnswers = new List<Answer>();
			IsMulti = multi;
		}

		protected ClosedQuestion() : base() {
			//EF
		}
	}
}
