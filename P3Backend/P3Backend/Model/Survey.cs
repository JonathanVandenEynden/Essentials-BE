using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Survey : IAssesment {
		public int Id { get; set; }
		public List<Question> Questions { get; set; }
	}
}
