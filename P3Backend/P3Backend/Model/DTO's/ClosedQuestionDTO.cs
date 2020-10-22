using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.DTO_s
{
    public class ClosedQuestionDTO : IQuestionDTO
    {
        public IList<AnswerDTO> PossibleAnswers { get; set; }
        public int MaxAmount { get; set; }
    }
}
