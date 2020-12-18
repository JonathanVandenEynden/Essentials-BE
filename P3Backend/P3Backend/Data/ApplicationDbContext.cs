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
using P3Backend.Model.TussenTabellen;
using P3Backend.Model.Users;

namespace P3Backend.Data {
	public class ApplicationDbContext : IdentityDbContext {

		public DbSet<IUser> Users {
			get; set;
		}
		public DbSet<ChangeManager> ChangeManagers {
			get; set;
		}
		public DbSet<Employee> Employees {
			get; set;
		}
		public DbSet<ChangeInitiative> ChangeInitiatives {
			get; set;
		}
		public DbSet<Survey> Surveys {
			get; set;
		}
		public DbSet<ChangeGroup> ChangeGroups {
			get; set;
		}
		public DbSet<Organization> Organizations {
			get; set;
		}
		public DbSet<Project> Projects {
			get; set;
		}
		public DbSet<RoadMapItem> RoadMapItems {
			get; set;
		}
		public DbSet<Admin> Admins {
			get; set;
		}
		public DbSet<OrganizationPart> OrganizationParts {
			get; set;
		}
		public DbSet<Question> Questions {
			get; set;
		}

		public DbSet<PresetSurvey> PresetSurveys {
			get; set;
		}

		public DbSet<DeviceTokens> DeviceTokens
		{
			get;
			set;
		}


		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			// Users
			modelBuilder.Entity<Admin>();
			modelBuilder.Entity<ChangeManager>();
			modelBuilder.Entity<Employee>();
			modelBuilder.Entity<DeviceTokens>();


			// Assesments
			modelBuilder.Entity<Survey>();
			modelBuilder.Entity<PresetSurvey>();

			// Questions
			modelBuilder.Entity<Question>();
			modelBuilder.Entity<MultipleChoiceQuestion>();
			modelBuilder.Entity<YesNoQuestion>();
			modelBuilder.Entity<RangedQuestion>();
			modelBuilder.Entity<OpenQuestion>();

			// OrganizationalTypes
			modelBuilder.Entity<OrganizationPart>();


			// ChangeTypes
			modelBuilder.Entity<TechnologicalChangeType>();
			modelBuilder.Entity<EconomicalChangeType>();
			modelBuilder.Entity<OrganizationalChangeType>();
			modelBuilder.Entity<PersonalChangeType>();

			// Other
			modelBuilder.Entity<ChangeInitiative>();
			modelBuilder.Entity<ChangeGroup>();
			modelBuilder.Entity<Organization>();
			modelBuilder.Entity<Portfolio>();
			modelBuilder.Entity<Project>();
			modelBuilder.Entity<RoadMapItem>();

			//Tussentabellen
			modelBuilder.Entity<EmployeeOrganizationPart>()
				.HasKey(eo => new { eo.EmployeeId, eo.OrganizationPartId });
			modelBuilder.Entity<EmployeeOrganizationPart>()
				.HasOne(eo => eo.Employee)
				.WithMany(e => e.EmployeeOrganizationParts)
				.HasForeignKey(eo => eo.EmployeeId);
			modelBuilder.Entity<EmployeeOrganizationPart>()
				.HasOne(eo => eo.OrganizationPart)
				.WithMany(o => o.EmployeeOrganizationParts)
				.HasForeignKey(eo => eo.OrganizationPartId);
			//
			modelBuilder.Entity<EmployeeChangeGroup>()
				.HasKey(ecg => new { ecg.EmployeeId, ecg.ChangeGroupId });
			modelBuilder.Entity<EmployeeChangeGroup>()
				.HasOne(ecg => ecg.Employee)
				.WithMany(e => e.EmployeeChangeGroups)
				.HasForeignKey(ecg => ecg.EmployeeId);
			modelBuilder.Entity<EmployeeChangeGroup>()
				.HasOne(ecg => ecg.ChangeGroup)
				.WithMany(cg => cg.EmployeeChangeGroups)
				.HasForeignKey(ecg => ecg.ChangeGroupId);

			#region configurations

			// Users
			modelBuilder.ApplyConfiguration(new IUserConfiguration());
			modelBuilder.ApplyConfiguration(new AdminConfiguration());
			modelBuilder.ApplyConfiguration(new ChangeManagerConfiguration());
			modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
			modelBuilder.ApplyConfiguration(new DeviceTokensConfiguration());


			// Assesments
			modelBuilder.ApplyConfiguration(new IAssessmentConfiguration());
			modelBuilder.ApplyConfiguration(new SurveyConfiguration());
			modelBuilder.ApplyConfiguration(new PresetSurveyConfiguration());


			// OrganizationalParts
			modelBuilder.ApplyConfiguration(new OrganizationalPartConfiguration());


			// ChangeTypes
			modelBuilder.ApplyConfiguration(new IChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new EconomicalChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new OrganizationalChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new PersonalChangeTypeConfiguration());
			modelBuilder.ApplyConfiguration(new TechnologicalChangeTypeConfiguration());

			// Questions
			modelBuilder.ApplyConfiguration(new QuestionConfiguration());
			modelBuilder.ApplyConfiguration(new MultipleChoiceQuestionConfiguration());
			modelBuilder.ApplyConfiguration(new YesNoQuestionConfiguration());
			modelBuilder.ApplyConfiguration(new RangedQuestionConfiguration());
			modelBuilder.ApplyConfiguration(new OpenQuestionConfiguration());


			// Other
			modelBuilder.ApplyConfiguration(new ChangeInitiativeConfiguration());
			modelBuilder.ApplyConfiguration(new ChangeGroupConfiguration());
			modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
			modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
			modelBuilder.ApplyConfiguration(new ProjectConfiguration());
			modelBuilder.ApplyConfiguration(new RoadMapItemConfiguration());

			#endregion



		}
	}
}