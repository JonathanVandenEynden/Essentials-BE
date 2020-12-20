using System;
using System.Collections.Generic;

namespace P3Backend.Model.Questions {
    public class YesNoQuestion : Question {
        public Dictionary<bool, int> PossibleAnswers { get; set; }
        public YesNoQuestion(string questionString) : base(questionString) {
            PossibleAnswers = new Dictionary<bool, int>() { { true, 0 }, { false, 0 } };
            Type = QuestionType.YESNO;
        }

        public YesNoQuestion() {
            //Ef
        }

        public void AddAnswer(List<string> answers) {
            foreach (string a in answers) {
                if (PossibleAnswers.ContainsKey(Convert.ToBoolean(a))) {
                    PossibleAnswers[Convert.ToBoolean(a)]++;
                } else {
                    PossibleAnswers.Add(Convert.ToBoolean(a), 1);
                }
            }
        }
    }
}
