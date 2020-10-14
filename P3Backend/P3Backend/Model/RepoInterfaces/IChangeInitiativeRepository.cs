using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.RepoInterfaces {
	public interface IChangeInitiativeRepository {
		IEnumerable<ChangeInitiative> GetAll();

		ChangeInitiative GetBy(int id);

		void Add(ChangeInitiative u);

		void Update(ChangeInitiative u);

		void Delete(ChangeInitiative u);

		void SaveChanges();

		ChangeInitiative GetByName(string name);
	}
}
