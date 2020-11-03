using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
    public class MultipleChoiceQuestion: Question<string> {
        
        public MultipleChoiceQuestion(string questionString) : base(questionString) {
            PossibleAnswers = new Dictionary<string, int>();
            Type = QuestionType.MULTIPLECHOICE;
        }

        public MultipleChoiceQuestion() {
            //EF
        }

        public void AddPossibleAnswers(List<string> answers) {
            answers.ForEach(s => PossibleAnswers.Add(s, 0));
        }
    }
}
