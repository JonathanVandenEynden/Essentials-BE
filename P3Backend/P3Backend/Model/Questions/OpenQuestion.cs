using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
    public class OpenQuestion : Question {
        public List<string> Answers { get; set; }

        public OpenQuestion(string questionString) : base(questionString) {
            Answers = new List<string>();
            Type = QuestionType.OPEN;
        }

        public OpenQuestion() {
            //Ef
        }
    }
}
