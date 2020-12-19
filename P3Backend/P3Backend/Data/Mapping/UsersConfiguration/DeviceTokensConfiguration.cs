using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using P3Backend.Model.Users;
using System.Collections.Generic;

namespace P3Backend.Data.Mapping.UsersConfiguration {
    public class DeviceTokensConfiguration : IEntityTypeConfiguration<DeviceTokens> {
        public void Configure(EntityTypeBuilder<DeviceTokens> builder) {
            builder.ToTable("DeviceTokens");
            builder.Property(a => a.Tokens).HasConversion(
                d => JsonConvert.SerializeObject(d, Formatting.None),
                s => JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(s));
        }
    }
}