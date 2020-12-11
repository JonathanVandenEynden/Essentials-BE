using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using P3Backend.Model.Questions;
using System.Collections.Generic;

namespace P3Backend.Data.Mapping.QuestionsConfiguration {
    public class RangedQuestionConfiguration : IEntityTypeConfiguration<RangedQuestion> {
        public void Configure(EntityTypeBuilder<RangedQuestion> builder) {


            builder.Property(mc => mc.PossibleAnswers).HasConversion(
               d => JsonConvert.SerializeObject(d, Formatting.None),
               s => JsonConvert.DeserializeObject<Dictionary<double, int>>(s)).HasMaxLength(4000);
        }
    }
}
