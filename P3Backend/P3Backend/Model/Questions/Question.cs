using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Model.Questions {
	public abstract class Question {
		public int Id { get; set; }
		[Required]
		public string QuestionString { get; set; }
		public QuestionType Type { get; set; }

		public Question(string questionString) {
			QuestionString = questionString;
        }

		protected Question() {
			//EF
        }		
	}
}
