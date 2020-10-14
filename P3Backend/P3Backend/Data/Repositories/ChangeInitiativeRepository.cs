using Microsoft.EntityFrameworkCore;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Repositories {
	public class ChangeInitiativeRepository : IChangeInitiativeRepository {

		private readonly ApplicationDbContext _context;
		private readonly DbSet<ChangeInitiative> _changeInitiatives;

		public ChangeInitiativeRepository(ApplicationDbContext context) {
			_context = context;
			_changeInitiatives = _context.ChangeInitiatives;
		}

		public void Add(ChangeInitiative ci) {
			_changeInitiatives.Add(ci);
		}

		public void Delete(ChangeInitiative ci) {
			_changeInitiatives.Remove(ci);
		}

		public IEnumerable<ChangeInitiative> GetAll() {
			return _changeInitiatives;
		}

		public ChangeInitiative GetBy(int id) {
			return _changeInitiatives.FirstOrDefault(ci => ci.Id == id);
		}

		public ChangeInitiative GetByName(string name) {
			return _changeInitiatives.FirstOrDefault(ci => ci.Name == name);
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

		public void Update(ChangeInitiative ci) {
			_changeInitiatives.Update(ci);
		}
	}
}
