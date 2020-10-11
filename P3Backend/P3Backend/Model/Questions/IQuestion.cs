namespace P3Backend.Model.Questions {
	public abstract class IQuestion {

		public int Id { get; set; }
		public string QuestionString { get; set; }

		protected IQuestion(string questionString) {
			QuestionString = questionString;
		}
	}
}