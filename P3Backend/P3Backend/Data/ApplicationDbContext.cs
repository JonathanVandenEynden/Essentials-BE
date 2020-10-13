using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P3Backend.Data.Mapping;
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
			modelBuilder.Entity<OpenQuestion>();
			modelBuilder.Entity<ClosedQuestion>();

			// ChangeTypes
			modelBuilder.Entity<TechnologicalChangeType>();
			modelBuilder.Entity<EconomicalChangeType>();
			modelBuilder.Entity<OrganizationalChangeType>();
			modelBuilder.Entity<PersonalChangeType>();
			modelBuilder.Entity<ChangeInitiative>();


			// configurations
			modelBuilder.ApplyConfiguration(new SurveyConfiguration());
			modelBuilder.ApplyConfiguration(new ChangeInitiativeConfiguration());



		}
	}
}