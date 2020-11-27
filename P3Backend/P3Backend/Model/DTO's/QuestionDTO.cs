using P3Backend.Model.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.DTO_s {
	public class QuestionDTO {
		[Required]
		public string QuestionString { get; set; }
		[Required]
		public QuestionType Type { get; set; }
	}
}
