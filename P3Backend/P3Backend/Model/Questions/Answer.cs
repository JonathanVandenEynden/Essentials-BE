namespace P3Backend.Model.Questions {
	public class Answer {
		public int Id { get; set; }
		public string AnswerString { get; set; }
		public int AmountChosen { get; set; }

		public Answer(string answerString) {
			AnswerString = answerString;

			AmountChosen = 0;
		}

		public void ChooseAnswer() => AmountChosen++;
	}
}