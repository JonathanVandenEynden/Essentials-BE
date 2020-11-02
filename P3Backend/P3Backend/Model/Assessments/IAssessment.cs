using P3Backend.Model.Questions;
using System.Collections.Generic;

namespace P3Backend.Model {
	public abstract class IAssessment {
		public int Id { get; set; }
		// public List<IQuestion> Questions { get; set; }
		public List<Question> Questions { get; set; }
		public MultipleChoiceQuestion Feedback { get; set; }

		protected IAssessment() {
			Questions = new List<Question>();
			//TODO misschien nog aan te passen naar wens
			Feedback = new MultipleChoiceQuestion("How is your mood about this change initiative?");

			Feedback.PossibleAnswers.Add("Good", 0);
			Feedback.PossibleAnswers.Add("Okay", 0);
			Feedback.PossibleAnswers.Add("Bad", 0);
		}
	}
}