using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P3Backend.Model.Questions {
	public abstract class Question {
		private string _questionString;
		public int Id { get; set; }

		[Required]
		public string QuestionString {
			get => _questionString;
			set {
				if (String.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Questionstring cannot be empty or null");
				_questionString = value;
			}
		}

		public QuestionType Type { get; set; }
		public Dictionary<int, DateTime> QuestionRegistered { get; set; }

		public Question(string questionString) {
			QuestionString = questionString;
			QuestionRegistered = new Dictionary<int, DateTime>();
		}

		protected Question() {
			//EF
		}

		public void CompleteQuestion(int userid) {
			QuestionRegistered.Add(userid, DateTime.Now);
		}

	}
}
