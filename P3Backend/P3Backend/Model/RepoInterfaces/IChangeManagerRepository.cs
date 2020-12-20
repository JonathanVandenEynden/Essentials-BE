using P3Backend.Model.Users;
using System.Collections.Generic;

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
