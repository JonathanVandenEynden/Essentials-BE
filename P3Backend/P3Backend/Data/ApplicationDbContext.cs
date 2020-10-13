using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P3Backend.Data.Mapping;
using P3Backend.Data.Mapping.AssesmentConfiguration;
using P3Backend.Data.Mapping.ChangeTypesConfiguration;
using P3Backend.Data.Mapping.UsersConfiguration;
using P3Backend.Model;
using P3Backend.Model.ChangeTypes;
using P3Backend.Model.Questions;
using P3Backend.Model.Users;

namespace P3Backend.Data {
	public class ApplicationDbContext : IdentityDbContext {

		public DbSet<IUser> Users { get; set; }
		public DbSet<ChangeInitiative> ChangeInitiatives { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			// Users
			modelBuilder.Entity<Admin>();
			modelBuilder.Entity<ChangeManager>();
			modelBuilder.Entity<Employee>();

			// Assesments
			modelBuilder.Entity<Survey>();

			// Questions
			modelBuilder.Entity<OpenQuestion>();
			modelBuilder.Entity<ClosedQuestion>();

			// ChangeTypes
			modelBuilder.Entity<TechnologicalChangeType>();
			modelBuilder.Entity<EconomicalChangeType>();
			modelBuilder.Entity<OrganizationalChangeType>();
			modelBuilder.Entity<PersonalChangeType>();

			// Other
			modelBuilder.Entity<ChangeInitiative>();


			#region configurations

			// Users
			modelBuilder.ApplyConfiguration(new IUserConfiguration());
			modelBuilder.ApplyConfiguration(new AdminConfiguration());

			// Assesments
			modelBuilder.ApplyConfiguration(new IAssesmentConfiguration());
			modelBuilder.ApplyConfiguration(new SurveyConfiguration());

			// ChangeTypes
			modelBuilder.ApplyConfiguration(new IChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new EconomicalChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new OrganizationalChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new PersonalChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new TechnologicalChangeTypeConfiguration());

			// Other
			modelBuilder.ApplyConfiguration(new ChangeInitiativeConfiguration());

			#endregion



		}
	}
}