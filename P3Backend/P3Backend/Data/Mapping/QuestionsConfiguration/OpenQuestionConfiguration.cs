using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.QuestionsConfiguration {
	public class OpenQuestionConfiguration : IEntityTypeConfiguration<OpenQuestion> {
		public void Configure(EntityTypeBuilder<OpenQuestion> builder) {

			builder.Property(oq => oq.Answer);
		}
	}
}
