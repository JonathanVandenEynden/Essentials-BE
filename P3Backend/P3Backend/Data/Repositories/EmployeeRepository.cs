using Microsoft.EntityFrameworkCore;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using System.Collections.Generic;
using System.Linq;

namespace P3Backend.Data.Repositories {
    public class EmployeeRepository : IEmployeeRepository {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Employee> _employees;

        public EmployeeRepository(ApplicationDbContext context) {
            _context = context;
            _employees = _context.Employees;
        }

        public void Add(Employee e) {
            _employees.Add(e);
        }

        public void Delete(Employee e) {
            _employees.Remove(e);
        }

        public IEnumerable<Employee> GetAll() {
            return _employees
                .Include(e => e.EmployeeChangeGroups).ThenInclude(ecg => ecg.ChangeGroup)
                .Include(e => e.EmployeeOrganizationParts).ThenInclude(eo => eo.OrganizationPart);
        }

        public Employee GetBy(int id) {
            return _employees
                .Include(e => e.EmployeeOrganizationParts).ThenInclude(eo => eo.OrganizationPart)
                .Include(e => e.EmployeeChangeGroups).ThenInclude(ecg => ecg.ChangeGroup)
                .FirstOrDefault(e => e.Id == id);
        }

        public Employee GetByEmail(string email) {
            return _employees
                .Include(e => e.EmployeeOrganizationParts).ThenInclude(eo => eo.OrganizationPart)
                .Include(e => e.EmployeeChangeGroups).ThenInclude(ecg => ecg.ChangeGroup)
                .FirstOrDefault(e => e.Email == email);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }

        public void Update(Employee e) {
            _employees.Update(e);
        }
    }
}
