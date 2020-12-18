using P3Backend.Model.Users;
using System.Collections.Generic;

namespace P3Backend.Model.RepoInterfaces {
	public interface IEmployeeRepository {
		IEnumerable<Employee> GetAll();

		Employee GetBy(int id);

		void Add(Employee e);

		void Update(Employee e);

		void Delete(Employee e);

		void SaveChanges();

		Employee GetByEmail(string email);
	}
}
