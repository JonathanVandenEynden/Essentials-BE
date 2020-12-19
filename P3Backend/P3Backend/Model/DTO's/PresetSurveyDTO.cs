using P3Backend.Model.DTO_s;

namespace P3Backend.Model {
    public class PresetSurveyDTO {
        public string Theme { get; set; }
        public QuestionDTO PresetQuestion { get; set; }
    }
}