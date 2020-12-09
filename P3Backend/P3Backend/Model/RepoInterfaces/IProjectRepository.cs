using P3Backend.Model.Users;
using System.Collections.Generic;

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
