using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.QuestionsConfiguration {
	public class IQuestionConfiguration : IEntityTypeConfiguration<IQuestion> {
		public void Configure(EntityTypeBuilder<IQuestion> builder) {

			builder.HasKey(q => q.Id);

			builder.Property(q => q.QuestionString).IsRequired();
		}
	}
}
