using Microsoft.EntityFrameworkCore;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Repositories {
	public class UserRepository : IUserRepository {

		private readonly ApplicationDbContext _context;
		private readonly DbSet<IUser> _users;

		public UserRepository(ApplicationDbContext context) {
			_context = context;
			_users = _context.Users;
		}

		public void Add(IUser u) {
			_users.Add(u);
		}

		public void Delete(IUser u) {
			_users.Remove(u);
		}

		public IEnumerable<IUser> GetAll() {
			return _users;
		}

		public IUser GetBy(int id) {
			return _users.FirstOrDefault(u => u.Id == id);
		}

		public IUser GetByEmail(string email) {
			return _users.FirstOrDefault(u => u.Email == email);
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

		public void Update(IUser u) {
			_users.Update(u);
		}
	}
}
