using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.RepoInterfaces {
	public interface IChangeGroupRepository {

		IEnumerable<ChangeGroup> GetAll();

		ChangeGroup GetBy(int id);

		void Add(ChangeGroup cg);

		void Update(ChangeGroup cg);

		void Delete(ChangeGroup cg);

		void SaveChanges();

		ChangeGroup GetByName(string name);

		List<ChangeGroup> GetForUserId(int userId);
	}
}
