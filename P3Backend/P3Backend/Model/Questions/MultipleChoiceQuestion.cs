using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
    public class MultipleChoiceQuestion: Question {
        public Dictionary<string, int> PossibleAnswers { get; set; }

        public MultipleChoiceQuestion(string questionString) : base(questionString) {
            PossibleAnswers = new Dictionary<string, int>();
            Type = QuestionType.MULTIPLECHOICE;
        }

        public MultipleChoiceQuestion() {
            //EF
        }

        public void AddPossibleAnswers(List<string> answers, bool initialize) {
            if (initialize) {
                answers.ForEach(s => PossibleAnswers.Add(s, 0));
            } else {
                foreach (string a in answers) {
                    if (PossibleAnswers.ContainsKey(a)) {
                        PossibleAnswers[a]++;
                    } else {
                        PossibleAnswers.Add(a, 1);
                    }
                }
            }
            
        }
    }
}
