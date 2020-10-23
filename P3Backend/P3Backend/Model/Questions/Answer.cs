using System.ComponentModel.DataAnnotations;

namespace P3Backend.Model.Questions {
	public class Answer {
		public int Id { get; set; }
		[Required]
		public string AnswerString { get; set; }
		public int AmountChosen { get; set; }

		public Answer(string answerString) {
			AnswerString = answerString;

			AmountChosen = 0;
		}

		protected Answer() {
			// EF
		}

		public void ChooseAnswer() => AmountChosen++;
	}
}