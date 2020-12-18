using System;
using System.Collections.Generic;

namespace P3Backend.Model.Questions {
	public class RangedQuestion : Question {
		private const double RANGE_AMOUNT = 5;
		private const double RANGE_STEP = 0.5;
		public Dictionary<double, int> PossibleAnswers { get; set; }

		public RangedQuestion(string questionString) : base(questionString) {
			PossibleAnswers = new Dictionary<double, int>();
			for (double i = 0.0; i <= RANGE_AMOUNT; i += RANGE_STEP) {
				PossibleAnswers.Add(i, 0);
			}
			Type = QuestionType.RANGED;
		}

		public RangedQuestion() {
			//Ef
		}

		public void AddAnswer(List<string> answers) {
			foreach (string a in answers) {
				if (PossibleAnswers.ContainsKey(Convert.ToDouble(a))) {
					PossibleAnswers[Convert.ToDouble(a)]++;
				}
				else {
					PossibleAnswers.Add(Convert.ToDouble(a), 1);
				}
			}
		}
	}
}
