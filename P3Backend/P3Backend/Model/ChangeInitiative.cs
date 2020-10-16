using P3Backend.Model.ChangeTypes;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class ChangeInitiative {

		private DateTime _endDate;
		private DateTime _startDate;

		public int Id {
			get; set;
		}
		[Required]
		public string Name {
			get; set;
		}
		[Required]
		public String Description {
			get; set;
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
		public IUser ChangeSponsor {
			get; set;
		} // could also be another CM
		public IChangeType ChangeType {
			get; set;
		}
		public IList<RoadMapItem> RoadMap {
			get; set;
		}

		public ChangeInitiative(string name, string desc, DateTime start, DateTime end, IUser sponsor, IChangeType changeType) {
			Name = name;
			Description = desc;
			StartDate = start;
			EndDate = end;
			ChangeSponsor = sponsor;
			ChangeType = changeType;

			RoadMap = new List<RoadMapItem>();
			// TODO standaard voorbereiding item toevoegen aan roadmap
			// TODO Changegroup
		}

		protected ChangeInitiative() {
			// EF
		}
	}
}
