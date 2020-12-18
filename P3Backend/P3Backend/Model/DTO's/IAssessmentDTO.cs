using P3Backend.Model.Questions;
using System.Collections.Generic;

namespace P3Backend.Model.DTO_s {
	public class IAssessmentDTO {
		public List<Question> Questions { get; set; }
		public MultipleChoiceQuestion Feedback { get; set; }
	}
}
