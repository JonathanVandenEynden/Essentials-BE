using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using P3Backend.Model.Questions;
using System.Collections.Generic;

namespace P3Backend.Data.Mapping.QuestionsConfiguration {
	public class YesNoQuestionConfiguration : IEntityTypeConfiguration<YesNoQuestion> {
		public void Configure(EntityTypeBuilder<YesNoQuestion> builder) {

			builder.Property(mc => mc.PossibleAnswers).HasConversion(
			   d => JsonConvert.SerializeObject(d, Formatting.None),
			   s => JsonConvert.DeserializeObject<Dictionary<bool, int>>(s)).HasMaxLength(4000);
		}
	}
}
