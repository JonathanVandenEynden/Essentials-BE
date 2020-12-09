using P3Backend.Model.Questions;
using System.ComponentModel.DataAnnotations;

namespace P3Backend.Model.DTO_s {
    public class QuestionDTO {
        [Required]
        public string QuestionString { get; set; }
        [Required]
        public QuestionType Type { get; set; }
    }
}
