using System.Collections.Generic;

namespace P3Backend.Model.Questions {
	public class OpenQuestion : Question {

		public Dictionary<string, int> PossibleAnswers { get; set; }

		public OpenQuestion(string questionString) : base(questionString) {
			PossibleAnswers = new Dictionary<string, int>();
			Type = QuestionType.OPEN;
		}

		public OpenQuestion() {
			//Ef
		}

		public void AddAnswer(List<string> answers) {
			foreach (string a in answers) {
				if (PossibleAnswers.ContainsKey(a)) {
					PossibleAnswers[a]++;
				}
				else {
					PossibleAnswers.Add(a, 1);
				}
			}
		}
	}
}
