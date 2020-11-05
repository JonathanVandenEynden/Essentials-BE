using Microsoft.EntityFrameworkCore;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Repositories {
	public class ChangeManagerRepository : IChangeManagerRepository {
		private readonly ApplicationDbContext _context;
		private readonly DbSet<ChangeManager> _changeManagers;

		public ChangeManagerRepository(ApplicationDbContext context) {
			_context = context;
			_changeManagers = _context.ChangeManagers;
		}

		public void Add(ChangeManager cm) {
			_changeManagers.Add(cm);
		}

		public void Delete(ChangeManager cm) {
			_changeManagers.Remove(cm);
		}

		public IEnumerable<ChangeManager> GetAll() {
			return _changeManagers
				.Include(e => e.EmployeeOrganizationParts).ThenInclude(eo => eo.OrganizationPart)
				.Include(e => e.CreatedChangeInitiatives).ThenInclude(ci => ci.RoadMap).ThenInclude(rmi => rmi.Assessment).ThenInclude(a => a.Questions);
		}

		public ChangeManager GetBy(int id) {
			return _changeManagers
				.Include(e => e.EmployeeOrganizationParts).ThenInclude(eo => eo.OrganizationPart)
				.Include(e => e.CreatedChangeInitiatives).ThenInclude(ci => ci.ChangeSponsor)
				.Include(e => e.CreatedChangeInitiatives).ThenInclude(ci => ci.ChangeGroup)
				.Include(e => e.CreatedChangeInitiatives).ThenInclude(ci => ci.RoadMap).ThenInclude(rmi => rmi.Assessment).ThenInclude(a => a.Questions)
				.FirstOrDefault(cm => cm.Id == id);
		}

		public ChangeManager GetByEmail(string email) {
			return _changeManagers
				.Include(e => e.EmployeeOrganizationParts).ThenInclude(eo => eo.OrganizationPart)
				.Include(e => e.CreatedChangeInitiatives).ThenInclude(ci => ci.ChangeSponsor)
				.Include(e => e.CreatedChangeInitiatives).ThenInclude(ci => ci.ChangeGroup)
				.Include(e => e.CreatedChangeInitiatives).ThenInclude(ci => ci.RoadMap).ThenInclude(rmi => rmi.Assessment).ThenInclude(a => a.Questions)
				.FirstOrDefault(cm => cm.Email == email);
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

		public void Update(ChangeManager cm) {
			_changeManagers.Update(cm);
		}
	}
}
