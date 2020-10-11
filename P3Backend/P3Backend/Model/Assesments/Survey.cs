using P3Backend.Model.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model {
	public class Survey : IAssesment {
		public int Id { get; set; }
		public List<IQuestion> Questions { get; set; }

		public Survey() {

		}
	}
}
