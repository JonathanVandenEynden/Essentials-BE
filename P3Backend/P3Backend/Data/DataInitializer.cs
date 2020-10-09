using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace P3Backend.Data {
	public class DataInitializer {

		private readonly ApplicationDbContext _dbContext;
		private readonly UserManager<IdentityUser> _usermanager;

		public DataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager) {
			_dbContext = dbContext;
			_usermanager = userManager;
		}

		public async Task InitializeData() {
			//_dbContext.Database.EnsureDeleted();
			if (_dbContext.Database.EnsureCreated()) {
				_dbContext.SaveChanges();
				Console.WriteLine("Database created");
			}
		}
	}
}