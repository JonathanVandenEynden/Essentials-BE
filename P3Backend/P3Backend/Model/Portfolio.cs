using System.Collections.Generic;

namespace P3Backend.Model {
    public class Portfolio {
        public int Id { get; set; }

        public List<Project> Projects { get; set; }

        public Portfolio() {

            Projects = new List<Project>();
        }

    }
}
