using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.DTO_s
{
    public class AnswerDTO
    {
        [Required]
        public string AnswerString { get; set; }
        public int AmountChosen { get; set; }
    }
}
