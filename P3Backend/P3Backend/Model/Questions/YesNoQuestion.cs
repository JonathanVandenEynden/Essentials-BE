using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
    public class YesNoQuestion : Question {
        public IDictionary<bool, int> PossibleAnswers { get; set; }

        public YesNoQuestion(string questionString) : base(questionString) {
            PossibleAnswers = new Dictionary<bool, int>() { { true, 0 }, { false, 0 } };
            Type = QuestionType.YESNO;
        }

        public YesNoQuestion() {
            //Ef
        }
    }
}
