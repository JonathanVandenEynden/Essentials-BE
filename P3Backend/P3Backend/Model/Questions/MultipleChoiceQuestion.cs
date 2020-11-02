using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
    public class MultipleChoiceQuestion : ClosedQuestion{
        public IDictionary<string, int> PossibleAnswers { get; set; }

        public MultipleChoiceQuestion(string questionString) : base(questionString) {
            PossibleAnswers = new Dictionary<string, int>();

        }

        public MultipleChoiceQuestion() {
            //EF
        }
    }
}
