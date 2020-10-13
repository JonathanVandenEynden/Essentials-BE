using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.QuestionsConfiguration {
	public class AnswerConfiguration : IEntityTypeConfiguration<Answer> {
		public void Configure(EntityTypeBuilder<Answer> builder) {

			builder.HasKey(a => a.Id);

			builder.Property(a => a.AnswerString).IsRequired();
			builder.Property(a => a.AmountChosen);
		}
	}
}
