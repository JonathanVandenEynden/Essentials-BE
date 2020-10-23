using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.QuestionsConfiguration {
	public class ClosedQuestionConfiguration : IEntityTypeConfiguration<ClosedQuestion> {
		public void Configure(EntityTypeBuilder<ClosedQuestion> builder) {

			builder.Property(cq => cq.MaxAmount);

			builder.HasMany(cq => cq.PossibleAnswers).WithOne();

		}
	}
}
