using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P3Backend.Model.Users;

namespace P3Backend.Model.RepoInterfaces {
	public interface IProjectRepository {
		IEnumerable<Project> GetAll();

		Project GetBy(int id);
		
		IEnumerable<Project> GetByChangeManager(ChangeManager cm);

		void Add(Project p);

		void Update(Project p);

		void Delete(Project p);

		void SaveChanges();

		Project GetByName(string name);
	}
}
