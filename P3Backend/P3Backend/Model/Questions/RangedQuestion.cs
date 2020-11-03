using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
    public class RangedQuestion : Question<double> {
        private const double RANGE_AMOUNT = 5;
        private const double RANGE_STEP = 0.5;

        public RangedQuestion(string questionString) : base(questionString) {
            PossibleAnswers = new Dictionary<double, int>();
            for (double i = 0.0; i <= RANGE_AMOUNT; i += RANGE_STEP) {
                PossibleAnswers.Add(i, 0);
            }
            Type = QuestionType.RANGED;
        }

        public RangedQuestion() {
            //Ef
        }
    }
}
