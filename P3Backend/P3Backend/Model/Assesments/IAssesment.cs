using P3Backend.Model.Questions;
using System.Collections.Generic;

namespace P3Backend.Model {
	public abstract class IAssesment {
		public int Id { get; set; }
		public List<IQuestion> Questions { get; set; }

		protected IAssesment() {
			Questions = new List<IQuestion>();
		}

	}
}