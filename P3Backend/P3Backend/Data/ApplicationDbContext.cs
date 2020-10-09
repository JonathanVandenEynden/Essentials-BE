using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P3Backend.Model;

namespace P3Backend.Data {
	public class ApplicationDbContext : IdentityDbContext {

		public DbSet<IUser> Users { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Admin>();
			modelBuilder.Entity<ChangeManager>();
			modelBuilder.Entity<Employee>();
			modelBuilder.Entity<Survey>();
			modelBuilder.Entity<TechnologicalChangeType>();
			modelBuilder.Entity<EconomicalChangeType>();
			modelBuilder.Entity<OrganizationalChangeType>();
			modelBuilder.Entity<PersonalChangeType>();
			modelBuilder.Entity<OpenQuestion>();
			modelBuilder.Entity<ClosedQuestion>();
			modelBuilder.Entity<YesNoQuestion>();
			modelBuilder.Entity<ChoiceQuestion>();
			modelBuilder.Entity<MultipleChoice>();

		}
	}
}