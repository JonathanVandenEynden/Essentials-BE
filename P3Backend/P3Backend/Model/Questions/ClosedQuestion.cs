using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
	public abstract class ClosedQuestion {
		public int Id { get; set; }
		[Required]
		public string QuestionString { get; set; }

		public ClosedQuestion(string questionString) {
			QuestionString = questionString;
        }


		protected ClosedQuestion() {
			//EF
        }


		/*public IList<Answer> PossibleAnswers { get; set; }
		public int MaxAmount { get; set; }

		public ClosedQuestion(string questionString, int maxAmount)  {
			PossibleAnswers = new List<Answer>();
			MaxAmount = maxAmount;

		}

		protected ClosedQuestion() : base() {
			//EF
		}*/
	}
}
