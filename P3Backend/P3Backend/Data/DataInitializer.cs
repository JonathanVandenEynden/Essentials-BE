using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using P3Backend.Model;

namespace P3Backend.Data {
	public class DataInitializer {

		private readonly ApplicationDbContext _dbContext;
		private readonly UserManager<IdentityUser> _usermanager;

		public DataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager) {
			_dbContext = dbContext;
			_usermanager = userManager;
		}

		public async Task InitializeData() {
			/*_dbContext.Database.EnsureDeleted();
			if (_dbContext.Database.EnsureCreated()) {

                #region Surveys
                Survey survey1 = new Survey();
				Survey survey2 = new Survey();				
				#endregion

				#region RoadmapItems
				RoadMapItem roadMapItem1 = new RoadMapItem("RoadmapItem number 1", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1));
				RoadMapItem roadMapItem2 = new RoadMapItem("RoadmapItem number 2", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1));
				roadMapItem1.Assesment = survey1;
				roadMapItem2.Assesment = survey2;
				var roadmapItems = new List<RoadMapItem> { roadMapItem1, roadMapItem2 };
				_dbContext.AddRange(roadmapItems);				
                #endregion

                _dbContext.SaveChanges();
				Console.WriteLine("Database created");
			}*/
		}
	}
}