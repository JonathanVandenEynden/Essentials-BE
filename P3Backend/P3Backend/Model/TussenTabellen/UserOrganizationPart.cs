using P3Backend.Model.OrganizationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.TussenTabellen {
	public class UserOrganizationPart {

		public int UserId { get; set; }
		public IUser User { get; set; }
		public int OrgaizationPartId { get; set; }
		public OrganizationPart OrganizationPart { get; set; }
	}
}
