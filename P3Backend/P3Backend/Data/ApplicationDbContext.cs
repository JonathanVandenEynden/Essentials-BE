using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P3Backend.Data.Mapping;
using P3Backend.Data.Mapping.AssesmentConfiguration;
using P3Backend.Data.Mapping.ChangeTypesConfiguration;
using P3Backend.Data.Mapping.OrganizationPartsConfiguration;
using P3Backend.Data.Mapping.QuestionsConfiguration;
using P3Backend.Data.Mapping.UsersConfiguration;
using P3Backend.Model;
using P3Backend.Model.ChangeTypes;
using P3Backend.Model.OrganizationParts;
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

			// OrganizationalTypes
			modelBuilder.Entity<Country>();
			modelBuilder.Entity<Department>();
			modelBuilder.Entity<Factory>();
			modelBuilder.Entity<Office>();
			modelBuilder.Entity<Team>();

			// ChangeTypes
			modelBuilder.Entity<TechnologicalChangeType>();
			modelBuilder.Entity<EconomicalChangeType>();
			modelBuilder.Entity<OrganizationalChangeType>();
			modelBuilder.Entity<PersonalChangeType>();

			// Questions
			modelBuilder.Entity<Answer>();
			modelBuilder.Entity<OpenQuestion>();
			modelBuilder.Entity<ClosedQuestion>();

			// Other
			modelBuilder.Entity<ChangeInitiative>();
			modelBuilder.Entity<ChangeGroup>();


			#region configurations

			// Users
			modelBuilder.ApplyConfiguration(new IUserConfiguration());
			modelBuilder.ApplyConfiguration(new AdminConfiguration());
			modelBuilder.ApplyConfiguration(new ChangeManagerConfiguration());
			modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

			// Assesments
			modelBuilder.ApplyConfiguration(new IAssesmentConfiguration());
			modelBuilder.ApplyConfiguration(new SurveyConfiguration());

			// OrganizationalParts
			modelBuilder.ApplyConfiguration(new IOrganizationalPartConfiguration());
			modelBuilder.ApplyConfiguration(new CountryConfiguration());
			modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
			modelBuilder.ApplyConfiguration(new FactoryConfiguration());
			modelBuilder.ApplyConfiguration(new OfficeConfiguration());
			modelBuilder.ApplyConfiguration(new TeamConfiguration());

			// ChangeTypes
			modelBuilder.ApplyConfiguration(new IChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new EconomicalChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new OrganizationalChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new PersonalChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new TechnologicalChangeTypeConfiguration());

			// Questions
			modelBuilder.ApplyConfiguration(new IQuestionConfiguration());
			modelBuilder.ApplyConfiguration(new OpenQuestionConfiguration());
			modelBuilder.ApplyConfiguration(new ClosedQuestionConfiguration());
			modelBuilder.ApplyConfiguration(new AnswerConfiguration());


			// Other
			modelBuilder.ApplyConfiguration(new ChangeInitiativeConfiguration());
			modelBuilder.ApplyConfiguration(new ChangeGroupConfiguration());

			#endregion



		}
	}
}