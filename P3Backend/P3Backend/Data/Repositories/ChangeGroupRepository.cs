using Microsoft.EntityFrameworkCore;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Repositories {
	public class ChangeGroupRepository : IChangeGroupRepository {

		private readonly ApplicationDbContext _context;
		private readonly DbSet<ChangeGroup> _changeGroups;

		public ChangeGroupRepository(ApplicationDbContext context) {
			_context = context;
			_changeGroups = _context.ChangeGroups;
		}

		public void Add(ChangeGroup cg) {
			_changeGroups.Add(cg);
		}

		public void Delete(ChangeGroup cg) {
			_changeGroups.Remove(cg);
		}

		public IEnumerable<ChangeGroup> GetAll() {
			return _changeGroups
				.Include(cg => cg.Users);
		}

		public ChangeGroup GetBy(int id) {
			return _changeGroups
				.Include(cg => cg.Users)
				.FirstOrDefault(cg => cg.Id == id);
		}

		public ChangeGroup GetByName(string name) {
			return _changeGroups
				.Include(cg => cg.Users)
				.FirstOrDefault(cg => cg.Name == name);
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

		public void Update(ChangeGroup cg) {
			_changeGroups.Update(cg);
		}
	}
}
