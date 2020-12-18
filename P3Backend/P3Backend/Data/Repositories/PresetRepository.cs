using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P3Backend.Model;
using P3Backend.Model.Questions;
using P3Backend.Model.RepoInterfaces;

namespace P3Backend.Data.Repositories {
	public class PresetRepository : IPresetRepository {
		private readonly ApplicationDbContext _context;
		private readonly DbSet<PresetSurvey> _presetSurveys;
		private readonly DbSet<Question> _questions;

		public PresetRepository(ApplicationDbContext context) {
			_context = context;
			_presetSurveys = _context.PresetSurveys;
			_questions = _context.Questions;
		}

		public IEnumerable<PresetSurvey> GetAll() {
			return _presetSurveys.Include(ps => ps.PresetQuestions);
		}

		public PresetSurvey GetBy(int id) {
			return _presetSurveys.Include(ps => ps.PresetQuestions).SingleOrDefault(ps => id == ps.Id);
		}

		public Question GetQuestion(int id) {
			return _questions.FirstOrDefault(q => q.Id == id);
		}

		public PresetSurvey GetBy(string theme) {
			return _presetSurveys.Include(ps => ps.PresetQuestions).SingleOrDefault(ps => String.Equals(ps.Theme, theme));			
		}

		public void Add(PresetSurvey ps) {
			_presetSurveys.Add(ps);
		}

		public void Update(PresetSurvey ps) {
			_presetSurveys.Update(ps);
		}

		public void Delete(PresetSurvey ps) {
			_presetSurveys.Remove(ps);
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

		public void UpdateQuestions(Question q) {
			_questions.Update(q);
			_context.SaveChanges();
		}
	}
}