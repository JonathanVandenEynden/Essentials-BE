using System.Collections.Generic;

namespace P3Backend.Model.Users {
    public class Admin : IUser {

        public List<Organization> Organizations { get; set; }

        public Admin(string firstName, string lastName, string email) : base(firstName, lastName, email) {

            Organizations = new List<Organization>();

        }

        protected Admin() {
            // EF
        }
    }
}
