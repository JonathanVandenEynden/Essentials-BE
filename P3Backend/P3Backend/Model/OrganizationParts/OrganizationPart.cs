using System;
using P3Backend.Model.TussenTabellen;
using System.Collections;
using System.Collections.Generic;
using Namotion.Reflection;

namespace P3Backend.Model.OrganizationParts {
	public class OrganizationPart {
		private OrganizationPartType _type;
		private string _name;
		public int Id { get; set; }

		public string Name {
			get => _name;
			private set {
				if (String.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Name cannot be null or empty");
				_name = value;
			}
		}

		public IList<EmployeeOrganizationPart> EmployeeOrganizationParts { get; set; }

		public OrganizationPartType Type {
			get => _type;
			set {
				_type = value;
				//! Check redundant because the value must be an OrganizationPartType eitherway
				//if (Enum.GetValues(typeof(OrganizationPartType)).HasProperty(value.ToString())) {
				//	_type = value;
				//}
				//else {
				//	throw new InvalidOperationException("Value is not part of the enum");
				//}
			}
		}

		public OrganizationPart(string name, OrganizationPartType type) {
			Name = name;
			Type = type;

			EmployeeOrganizationParts = new List<EmployeeOrganizationPart>();
		}
		protected OrganizationPart() {
			// EF
		}
	}
}