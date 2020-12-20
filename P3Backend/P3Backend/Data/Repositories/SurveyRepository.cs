using Microsoft.EntityFrameworkCore;
using P3Backend.Model;
using P3Backend.Model.Questions;
using P3Backend.Model.RepoInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace P3Backend.Data.Repositories {
    public class SurveyRepository : ISurveyRepository {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Survey> _surveys;
        private readonly DbSet<Question> _questions;

        public SurveyRepository(ApplicationDbContext context) {
            _context = context;
            _surveys = _context.Surveys;
            _questions = _context.Questions;
        }
        public void Add(Survey s) {
            _surveys.Add(s);
        }

        public void Delete(Survey s) {
            _surveys.Remove(s);
        }

        public IEnumerable<Survey> GetAll() {
            return _surveys
                .Include(s => s.Questions)
                .Include(s => s.Feedback);
        }

        public Survey GetBy(int id) {
            return _surveys
                .Include(s => s.Questions)
                .Include(s => s.Feedback)
                .FirstOrDefault(s => s.Id == id);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }

        public void UpdateQuestions(Question q) {
            _questions.Update(q);
            _context.SaveChanges();
        }



        public void Update(Survey s) {
            _surveys.Update(s);
        }

        public Question GetQuestion(int id) {
            return _questions.FirstOrDefault(q => q.Id == id);
        }
    }
}
