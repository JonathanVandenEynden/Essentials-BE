using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Repositories {
	public class UserRepository : IUserRepository {
		public void Add(IUser u) {
			throw new NotImplementedException();
		}

		public void Delete(IUser u) {
			throw new NotImplementedException();
		}

		public IEnumerable<IUser> GetAll() {
			throw new NotImplementedException();
		}

		public IUser GetBy(int id) {
			throw new NotImplementedException();
		}

		public IUser GetByEmail(string email) {
			throw new NotImplementedException();
		}

		public void SaveChanges() {
			throw new NotImplementedException();
		}

		public void Update(IUser u) {
			throw new NotImplementedException();
		}
	}
}
