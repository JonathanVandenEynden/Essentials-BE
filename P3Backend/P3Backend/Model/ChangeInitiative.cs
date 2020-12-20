using P3Backend.Model.ChangeTypes;
using P3Backend.Model.DTO_s;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace P3Backend.Model {
    public class ChangeInitiative {

        private string _name;
        private string _desc;
        private DateTime _endDate;
        private DateTime _startDate;
        private Employee _changeSponsor;
        private IChangeType _changeType;

        public int Id {
            get; set;
        }
        [Required]
        public string Name {
            get {
                return _name;
            }
            set {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Name cannot be null or empty");
                } else {
                    _name = value;
                }
            }
        }
        [Required]
        public String Description {
            get {
                return _desc;
            }
            set {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value) || value.Length < 5) {
                    throw new ArgumentException(
                        "Description cannot be null or empty and must be at least 5 characters long");
                } else {
                    _desc = value;
                }
            }
        }
        [Required]
        public DateTime StartDate {
            get {
                return _startDate;
            }
            set {
                if (value <= DateTime.Now)
                    throw new ArgumentException("Start must be in the future");
                else
                    _startDate = value;
            }
        }
        [Required]
        public DateTime EndDate {
            get {
                return _endDate;
            }
            set {
                if (value <= StartDate)
                    throw new ArgumentException("End must be after start");
                else
                    _endDate = value;
            }
        }

        public ChangeGroup ChangeGroup {
            get; set;
        }

        public Employee ChangeSponsor {
            get => _changeSponsor;
            set {
                if (value.Equals(null)) {
                    throw new ArgumentException("Change Sponsor is required and cannot be null");
                }
                _changeSponsor = value;
            }
        } // could also be another CM

        public IChangeType ChangeType {
            get => _changeType;
            set {
                if (value.Equals(null))
                    throw new ArgumentException("ChangeType is required and cannot be null");
                _changeType = value;
            }
        }

        public IList<RoadMapItem> RoadMap {
            get; set;
        }

        public double Progress => (Convert.ToDouble(RoadMap.Count(e => e.Done)) / RoadMap.Count()) * 100;

        public ChangeInitiative(string name, string desc, DateTime start, DateTime end, Employee sponsor, IChangeType changeType, ChangeGroup changeGroup) {
            Name = name;
            Description = desc;
            StartDate = start;
            EndDate = end;
            ChangeSponsor = sponsor;
            ChangeType = changeType;
            ChangeGroup = new ChangeGroup("All Employees");
            RoadMap = new List<RoadMapItem>();
            ChangeGroup = changeGroup;

        }

        protected ChangeInitiative() {
            // EF
        }

        internal void Update(ChangeInitiativeDTO dto) {
            Name = dto.Name;
            Description = dto.Description;
            _startDate = dto.StartDate;
            EndDate = dto.EndDate;
        }


    }
}
