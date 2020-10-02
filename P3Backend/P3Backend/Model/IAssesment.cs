using System.Collections.Generic;

namespace P3Backend.Model {
	public interface IAssesment {
		public List<Question> Questions { get; set; }
	}
}