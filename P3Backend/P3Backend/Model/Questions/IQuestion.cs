using System.ComponentModel.DataAnnotations;

namespace P3Backend.Model.Questions {
	public abstract class IQuestion {

		public int Id { get; set; }
		[Required]
		public string QuestionString { get; set; }

		protected IQuestion(string questionString) {
			QuestionString = questionString;
		}

		protected IQuestion() {
			// EF
		}
	}
}