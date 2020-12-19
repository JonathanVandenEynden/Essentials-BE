using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P3Backend.Model {
    public class Project {
        public string _name;

        public int Id { get; set; }
        [Required]
        public string Name {
            get { return _name; }
            set {
                if (value == null)
                    throw new ArgumentException("Name should not be empty");
                else
                    _name = value;
            }
        }
        public List<ChangeInitiative> ChangeInitiatives { get; set; }

        public Project(string name) {
            Name = name;
            ChangeInitiatives = new List<ChangeInitiative>();
        }

        protected Project() {
            // EF
        }

    }
}
