using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace P3Backend.Data {
	public class DataInitializer {
		private readonly ApplicationDbContext _dbContext;

		public DataInitializer(ApplicationDbContext dbContext) {
			_dbContext = dbContext;
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