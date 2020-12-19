using System.Collections.Generic;

namespace P3Backend.Model.RepoInterfaces {
    public interface IUserRepository {

        IEnumerable<IUser> GetAll();

        IUser GetBy(int id);

        void Add(IUser u);

        void Update(IUser u);

        void Delete(IUser u);

        void SaveChanges();

        IUser GetByEmail(string email);
    }
}
