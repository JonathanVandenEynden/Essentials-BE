using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace P3Backend.Model {
    public abstract class IUser {
        private string _firstName;
        private string _lastName;
        private string _email;
        public int Id { get; set; }

        [Required]
        public string FirstName {
            get => _firstName;
            private set {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("firstName cannot be null or empty");
                _firstName = value;
            }
        }

        [Required]
        public string LastName {
            get => _lastName;
            private set {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("lastName cannot be null or empty");
                _lastName = value;
            }
        }

        [EmailAddress]
        public string Email {
            get => _email;
            private set {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email cannot be null or empty");
                _email = value;
            }
        }

        public IUser(string firstName, string lastName, string email) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        protected IUser() {
            // EF
        }

        public string GetInitials() {
            List<string> nameParts = new List<string>();

            nameParts.Add(FirstName);
            nameParts.AddRange(LastName.Split(" ").ToList());

            string initials = "";

            nameParts.ForEach(p => initials += p.ToCharArray()[0]);

            return initials.ToUpper();

        }

    }
}
