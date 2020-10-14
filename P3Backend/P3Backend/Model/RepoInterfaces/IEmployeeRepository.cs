using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.RepoInterfaces {
	interface IEmployeeRepository {
		IEnumerable<Employee> GetAll();

		Employee GetBy(int id);

		void Add(Employee e);

		void Update(Employee e);

		void Delete(Employee e);

		void SaveChanges();

		Employee GetByEmail(string email);
	}
}
