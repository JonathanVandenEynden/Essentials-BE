using P3Backend.Model.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.DTO_s
{
    public class IAssessmentDTO
    {
        public List<Question> Questions { get; set; }
        public ClosedQuestionDTO Feedback { get; set; }
        public int AmountSubmitted { get; set; }
    }
}
