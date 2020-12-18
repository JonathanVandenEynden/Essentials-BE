using System.Collections.Generic;
using P3Backend.Model.Questions;

namespace P3Backend.Model {
	public class PresetSurvey {
		public int Id { get; set; }
		public string Theme { get; set; }
		public List<Question> PresetQuestions { get; set; }

		public PresetSurvey() {
			//EF
		}

		public PresetSurvey(string theme) {
			Theme = theme;
			PresetQuestions = new List<Question>();
		}

	}
}