
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using P3Backend.Model;
using P3Backend.Model.Questions;

namespace P3Backend.Data.Mapping.AssesmentConfiguration {
	public class PresetSurveyConfiguration : IEntityTypeConfiguration<PresetSurvey> {
		public void Configure(EntityTypeBuilder<PresetSurvey> builder) {
			//SP00kY
		}
	}
}