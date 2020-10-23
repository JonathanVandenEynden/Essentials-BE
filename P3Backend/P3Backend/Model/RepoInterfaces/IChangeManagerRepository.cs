using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.RepoInterfaces {
	public interface IChangeManagerRepository {

		IEnumerable<ChangeManager> GetAll();

		ChangeManager GetBy(int id);

		void Add(ChangeManager ci);

		void Update(ChangeManager ci);

		void Delete(ChangeManager ci);

		void SaveChanges();

		ChangeManager GetByEmail(string email);
	}
}
