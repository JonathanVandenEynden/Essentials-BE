using Microsoft.EntityFrameworkCore;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Repositories {
	public class AdminRepository : IAdminRepository {

		private readonly ApplicationDbContext _context;
		private readonly DbSet<Admin> _admins;

		public AdminRepository(ApplicationDbContext context) {
			_context = context;
			_admins = _context.Admins;
		}

		public void Add(Admin a) {
			_admins.Add(a);
		}

		public void Delete(Admin a) {
			_admins.Remove(a);
		}

		public IEnumerable<Admin> GetAll() {
			return _admins
				.Include(e => e.Organizations);
		}

		public Admin GetBy(int id) {
			return _admins
				.Include(e => e.Organizations)
				.FirstOrDefault(a => a.Id == id);
		}

		public Admin GetByEmail(string email) {
			return _admins
				.Include(e => e.Organizations)
				.FirstOrDefault(a => a.Email == email);
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

		public void Update(Admin a) {
			_admins.Update(a);
		}
	}
}
