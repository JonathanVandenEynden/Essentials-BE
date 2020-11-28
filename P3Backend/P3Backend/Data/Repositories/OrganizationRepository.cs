using Microsoft.EntityFrameworkCore;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Repositories {
	public class OrganizationRepository : IOrganizationRepository {

		private readonly ApplicationDbContext _context;
		private readonly DbSet<Organization> _organizations;

		public OrganizationRepository(ApplicationDbContext context) {
			_context = context;
			_organizations = _context.Organizations;
		}

		public void Add(Organization o) {
			_organizations.Add(o);
		}

		public void Delete(Organization o) {
			_organizations.Remove(o);
		}

		public IEnumerable<Organization> GetAll() {
			return _organizations
				.Include(o => o.Portfolio).ThenInclude(p => p.Projects)
				.Include(o => o.Employees).ThenInclude(e => e.EmployeeOrganizationParts)
				.Include(o => o.ChangeManagers).ThenInclude(cm => cm.CreatedChangeInitiatives).ThenInclude(ci => ci.RoadMap)
				.Include(o => o.OrganizationParts);
		}

		public Organization GetBy(int id) {
			return _organizations
				.Include(o => o.Portfolio).ThenInclude(p => p.Projects)
				.Include(o => o.Employees)//.ThenInclude(e => e.EmployeeOrganizationParts) // causes loop --> out of memory
				.Include(o => o.ChangeManagers).ThenInclude(cm => cm.CreatedChangeInitiatives).ThenInclude(ci => ci.RoadMap)
				.Include(o => o.OrganizationParts)
				.FirstOrDefault(o => o.Id == id);
		}

		public Organization GetByName(string name) {
			return _organizations
				.Include(o => o.Portfolio).ThenInclude(p => p.Projects)
				.Include(o => o.Employees).ThenInclude(e => e.EmployeeOrganizationParts)
				.Include(o => o.ChangeManagers).ThenInclude(cm => cm.CreatedChangeInitiatives).ThenInclude(ci => ci.RoadMap)
				.Include(o => o.OrganizationParts)
				.FirstOrDefault(o => o.Name == name);
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

		public void Update(Organization o) {
			_organizations.Update(o);
		}
	}
}
