using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model.Questions;

namespace P3Backend.Model.RepoInterfaces {
	public interface IPresetRepository {
		IEnumerable<PresetSurvey> GetAll();

		PresetSurvey GetBy(int id);

		Question GetQuestion(int id);

		List<string> GetThemas();

		PresetSurvey GetBy(string theme);

		void Add(PresetSurvey ps);

		void Update(PresetSurvey ps);

		void Delete(PresetSurvey ps);

		void SaveChanges();
		void UpdateQuestions(Question q);
	}
}