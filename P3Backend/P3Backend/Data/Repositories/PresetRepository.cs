using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P3Backend.Model;
using P3Backend.Model.Questions;
using P3Backend.Model.RepoInterfaces;

namespace P3Backend.Data.Repositories
{
    public class PresetRepository : IPresetRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<PresetSurvey> _presetSurveys;

        public PresetRepository(ApplicationDbContext context)
        {
            _context = context;
            _presetSurveys = _context.PresetSurveys;
        }

        public IEnumerable<PresetSurvey> GetAll()
        {
            return _presetSurveys.Include(ps => ps.PresetQuestion);
        }

        public PresetSurvey GetBy(int id)
        {
            return _presetSurveys.Include(ps => ps.PresetQuestion).SingleOrDefault(ps => id == ps.Id);
        }

        public IEnumerable<PresetSurvey> GetBy(string theme)
        {
            return _presetSurveys.Where(ps => String.Equals(theme, ps.Theme)).Include(ps => ps.PresetQuestion);
        }

        public void Add(PresetSurvey ps)
        {
            _presetSurveys.Add(ps);
        }

        public void Update(PresetSurvey ps)
        {
            _presetSurveys.Update(ps);
        }

        public void Delete(PresetSurvey ps)
        {
            _presetSurveys.Remove(ps);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}