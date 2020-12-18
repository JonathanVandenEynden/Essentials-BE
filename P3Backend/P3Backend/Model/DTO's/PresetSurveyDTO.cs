using System.Collections.Generic;
using P3Backend.Model.DTO_s;
using P3Backend.Model.Questions;

namespace P3Backend.Model {
	public class PresetSurveyDTO {
		public string Theme { get; set; }
		public QuestionDTO PresetQuestion { get; set; }
	}
}