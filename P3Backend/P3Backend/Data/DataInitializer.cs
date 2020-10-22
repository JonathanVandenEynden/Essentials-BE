using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using P3Backend.Model;
using P3Backend.Model.ChangeTypes;
using P3Backend.Model.OrganizationParts;
using P3Backend.Model.Users;

namespace P3Backend.Data {
	public class DataInitializer {

		private readonly ApplicationDbContext _dbContext;
		private readonly UserManager<IdentityUser> _usermanager;

		public DataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager) {
			_dbContext = dbContext;
			_usermanager = userManager;
		}

		public async Task InitializeData() {
			_dbContext.Database.EnsureDeleted();
			if (_dbContext.Database.EnsureCreated()) {

				#region Surveys
				Survey survey1 = new Survey();
				Survey survey2 = new Survey();
				Survey survey3 = new Survey();
				Survey survey4 = new Survey();
				#endregion

				#region RoadmapItems
				RoadMapItem roadMapItem1 = new RoadMapItem("Step 1", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1));
				RoadMapItem roadMapItem2 = new RoadMapItem("Step 2", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1));
				RoadMapItem roadMapItem3 = new RoadMapItem("Step 3", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1));
				RoadMapItem roadMapItem4 = new RoadMapItem("Step 4", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(1));
				roadMapItem1.Assesment = survey1;
				roadMapItem2.Assesment = survey2;
				roadMapItem3.Assesment = survey3;
				roadMapItem4.Assesment = survey4;
				#endregion

				#region OrganizationalChangeTypes
				OrganizationalChangeType organizationalChange = new OrganizationalChangeType();
				EconomicalChangeType economicalChange = new EconomicalChangeType();
				PersonalChangeType personalChange = new PersonalChangeType();
				TechnologicalChangeType technologicalChange = new TechnologicalChangeType();
                #endregion

                #region ChangeInitiatives
                Employee sponsor = new Employee("Sponser", "Sponser", "sponser@essentials.com");
				ChangeInitiative changeInitiative1 = new ChangeInitiative("New Catering", "A new catering will be added to the cafetari on the ground floor", DateTime.Now.AddHours(1), DateTime.Now.AddDays(31), sponsor, organizationalChange);
				ChangeInitiative changeInitiative2 = new ChangeInitiative("Expansion German Market", "We will try to expand more on the German Market", DateTime.Now.AddHours(1), DateTime.Now.AddDays(31), sponsor, economicalChange);
				changeInitiative1.RoadMap.Add(roadMapItem1);
				changeInitiative1.RoadMap.Add(roadMapItem2);
				changeInitiative2.RoadMap.Add(roadMapItem3);
				changeInitiative2.RoadMap.Add(roadMapItem4);
				#endregion

				#region Projects
				Project project = new Project("Project1");
				project.ChangeInitiatives.Add(changeInitiative1);
				project.ChangeInitiatives.Add(changeInitiative2);
				var projects = new List<Project> {project};
				_dbContext.Projects.AddRange(projects);
				#endregion

				#region OrganizationParts
				OrganizationPart organizationPart1 = new OrganizationPart("Giga Berlin", OrganizationPartType.FACTORY);
				#endregion

				#region ChangeGroups
				ChangeGroup management = new ChangeGroup("Management");
                #endregion

                #region ChangeManagers
                ChangeManager changeManagerSuktrit = new ChangeManager("Sukrit", "Bhattacharya", "Sukrit.bhattacharya@essentials.com");
				changeManagerSuktrit.CreatedChangeInitiatives.Add(changeInitiative1);
				changeManagerSuktrit.CreatedChangeInitiatives.Add(changeInitiative2);
				changeManagerSuktrit.OrganizationParts.Add(organizationPart1);
				management.Users.Add(changeManagerSuktrit);				
				_dbContext.ChangeGroups.Add(management);
                #endregion


                _dbContext.SaveChanges();
				Console.WriteLine("Database created");
			}
		}
	}
}