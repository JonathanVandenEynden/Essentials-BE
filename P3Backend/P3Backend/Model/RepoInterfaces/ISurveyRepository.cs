using P3Backend.Model.Questions;
using System.Collections.Generic;

namespace P3Backend.Model.RepoInterfaces {
    public interface ISurveyRepository {
        public void Add(Survey s);

        public void Delete(Survey s);

        public IEnumerable<Survey> GetAll();

        public Survey GetBy(int id);

        public void SaveChanges();

        public void Update(Survey s);

        public void UpdateQuestions(Question q);

        public Question GetQuestion(int id);
    }
}
