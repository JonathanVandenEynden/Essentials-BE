using P3Backend.Model.TussenTabellen;
using System.Collections.Generic;

namespace P3Backend.Model.Users {
    public class Employee : IUser {

        public List<EmployeeOrganizationPart> EmployeeOrganizationParts { get; set; }


        public Employee(string firstName, string lastName, string email) : base(firstName, lastName, email) {
            EmployeeOrganizationParts = new List<EmployeeOrganizationPart>();
        }

        protected Employee() {
            // EF
        }
    }
}
