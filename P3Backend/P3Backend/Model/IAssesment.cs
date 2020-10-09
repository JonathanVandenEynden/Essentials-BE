using System.Collections.Generic;

namespace P3Backend.Model {
	public abstract class IAssesment {
		public int Id { get; set; }
		public List<Question> Questions { get; set; }

		public IAssesment() {

		}
	}
}