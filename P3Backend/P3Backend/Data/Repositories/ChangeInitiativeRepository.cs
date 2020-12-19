using Microsoft.EntityFrameworkCore;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace P3Backend.Data.Repositories {
    public class ChangeInitiativeRepository : IChangeInitiativeRepository {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<ChangeInitiative> _changeInitiatives;

        public ChangeInitiativeRepository(ApplicationDbContext context) {
            _context = context;
            _changeInitiatives = _context.ChangeInitiatives;
        }

        public void Add(ChangeInitiative ci) {
            _changeInitiatives.Add(ci);
        }

        public void Delete(ChangeInitiative ci) {
            _changeInitiatives.Remove(ci);
        }

        public IEnumerable<ChangeInitiative> GetAll() {
            return _changeInitiatives
                .Include(ci => ci.ChangeGroup)
                .ThenInclude(cg => cg.EmployeeChangeGroups)
                .ThenInclude(ec => ec.Employee)
                .Include(ci => ci.RoadMap)
                .Include(ci => ci.ChangeSponsor)
                .Include(ci => ci.ChangeType);
        }

        public ChangeInitiative GetBy(int id) {
            return _changeInitiatives
                .Include(ci => ci.ChangeGroup).ThenInclude(cg => cg.EmployeeChangeGroups).ThenInclude(ecg => ecg.Employee).ThenInclude(e => e.EmployeeChangeGroups)
                .Include(ci => ci.RoadMap).ThenInclude(rmi => rmi.Assessment).ThenInclude(a => a.Questions)
                .Include(ci => ci.RoadMap).ThenInclude(rmi => rmi.Assessment).ThenInclude(a => a.Feedback)
                .Include(ci => ci.ChangeSponsor)
                .Include(ci => ci.ChangeType)
                .FirstOrDefault(ci => ci.Id == id);
        }

        public ChangeInitiative GetByName(string name) {
            return _changeInitiatives
                .Include(ci => ci.ChangeGroup).ThenInclude(cg => cg.EmployeeChangeGroups).ThenInclude(ecg => ecg.Employee).ThenInclude(e => e.EmployeeChangeGroups)
                .Include(ci => ci.RoadMap).ThenInclude(rmi => rmi.Assessment).ThenInclude(a => a.Questions)
                .Include(ci => ci.RoadMap).ThenInclude(rmi => rmi.Assessment).ThenInclude(a => a.Feedback)
                .Include(ci => ci.ChangeSponsor)
                .Include(ci => ci.ChangeType)
                .FirstOrDefault(ci => ci.Name == name);
        }

        public IEnumerable<ChangeInitiative> GetForUserId(int userId) {
            return _changeInitiatives
                .Include(ci => ci.ChangeGroup).ThenInclude(cg => cg.EmployeeChangeGroups).ThenInclude(ecg => ecg.Employee).ThenInclude(e => e.EmployeeChangeGroups)
                .Include(ci => ci.RoadMap).ThenInclude(rmi => rmi.Assessment).ThenInclude(a => a.Questions)
                .Include(ci => ci.RoadMap).ThenInclude(rmi => rmi.Assessment).ThenInclude(a => a.Feedback)
                .Include(ci => ci.ChangeSponsor)
                .Include(ci => ci.ChangeType)
                .Where(c => c.ChangeGroup.EmployeeChangeGroups.Any(u => u.EmployeeId == userId));
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }

        public void Update(ChangeInitiative ci) {
            _changeInitiatives.Update(ci);
        }
    }
}
