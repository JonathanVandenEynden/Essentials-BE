using System.Collections.Generic;

namespace P3Backend.Model.RepoInterfaces {
    public interface IChangeInitiativeRepository {
        IEnumerable<ChangeInitiative> GetAll();

        ChangeInitiative GetBy(int id);

        void Add(ChangeInitiative ci);

        void Update(ChangeInitiative ci);

        void Delete(ChangeInitiative ci);

        void SaveChanges();

        ChangeInitiative GetByName(string name);

        IEnumerable<ChangeInitiative> GetForUserId(int userId);


    }
}
