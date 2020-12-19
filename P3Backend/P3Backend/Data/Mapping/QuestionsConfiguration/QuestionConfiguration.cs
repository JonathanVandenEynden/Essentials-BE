using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using P3Backend.Model.Questions;
using System;
using System.Collections.Generic;

namespace P3Backend.Data.Mapping.QuestionsConfiguration {
    public class QuestionConfiguration : IEntityTypeConfiguration<Question> {
        public void Configure(EntityTypeBuilder<Question> builder) {
            builder.Property(a => a.QuestionRegistered).HasConversion(
               d => JsonConvert.SerializeObject(d, Formatting.None),
               s => JsonConvert.DeserializeObject<Dictionary<int, DateTime>>(s));
        }
    }
}
