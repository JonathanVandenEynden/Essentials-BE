using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.DTO_s
{
    public class IQuestionDTO
    {
        [Required]
        public string QuestionString { get; set; }
    }
}
