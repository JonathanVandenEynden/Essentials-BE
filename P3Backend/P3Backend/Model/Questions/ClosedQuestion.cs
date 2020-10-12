using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
	public class ClosedQuestion : IQuestion {

		public IList<Answer> PossibleAnswers { get; set; }
		public int MaxAmount { get; set; }

		public ClosedQuestion(string questionString, int maxAmount) : base(questionString) {
			PossibleAnswers = new List<Answer>();
			MaxAmount = maxAmount;

		}

		protected ClosedQuestion() : base() {
			//EF
		}
	}
}
