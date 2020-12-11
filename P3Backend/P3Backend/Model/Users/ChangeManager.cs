using System.Collections.Generic;

namespace P3Backend.Model.Users {
    public class ChangeManager : Employee {

        public IList<ChangeInitiative> CreatedChangeInitiatives { get; set; }


        public ChangeManager(string firstName, string lastName, string email) : base(firstName, lastName, email) {

            CreatedChangeInitiatives = new List<ChangeInitiative>();

        }

        protected ChangeManager() {
            // EF
        }

        public ChangeManager(Employee e) : this(e.FirstName, e.LastName, e.Email) {
            Id = e.Id;
            EmployeeOrganizationParts = e.EmployeeOrganizationParts;
        }
    }

}