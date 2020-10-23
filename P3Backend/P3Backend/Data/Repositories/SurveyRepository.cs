using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using P3Backend.Model;
using P3Backend.Model.Questions;
using P3Backend.Model.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Repositories {
	public class SurveyRepository : ISurveyRepository {

		private readonly ApplicationDbContext _context;
		private readonly DbSet<Survey> _surveys;

		public SurveyRepository(ApplicationDbContext context) {
			_context = context;
			_surveys = _context.Surveys;
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

		public void Update(Survey s) {
			_surveys.Update(s);
		}
	}
}
