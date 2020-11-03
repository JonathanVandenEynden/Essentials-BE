using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
    public class OpenQuestion : Question<string> {

        public OpenQuestion(string questionString) : base(questionString) {
            PossibleAnswers = new Dictionary<string, int>();
            Type = QuestionType.OPEN;
        }

        public OpenQuestion() {
            //Ef
        }
    }
}
