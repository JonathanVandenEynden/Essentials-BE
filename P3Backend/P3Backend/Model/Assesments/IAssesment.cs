using P3Backend.Model.Questions;
using System.Collections.Generic;

namespace P3Backend.Model {
	public abstract class IAssesment {
		public int Id { get; set; }
		public List<IQuestion> Questions { get; set; }
		public ClosedQuestion Feedback { get; set; }

		protected IAssesment() {
			Questions = new List<IQuestion>();

			//TODO misschien nog aan te passen naar wens
			Feedback = new ClosedQuestion("How is your mood about this change initiative?", 1);

			Feedback.PossibleAnswers.Add(new Answer("Bad"));
			Feedback.PossibleAnswers.Add(new Answer("OK"));
			Feedback.PossibleAnswers.Add(new Answer("Good"));
		}

	}
}