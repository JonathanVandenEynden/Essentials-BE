using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualBasic;
using P3Backend.Model;
using P3Backend.Model.ChangeTypes;
using P3Backend.Model.OrganizationParts;
using P3Backend.Model.Questions;
using P3Backend.Model.TussenTabellen;
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
				//if (!_dbContext.Admins.Any()) { // DEZE LIJN UIT COMMENTAAR EN 2 ERBOVEN IN COMMENTAAR VOOR DEPLOYEN

				#region Admin
				Admin admin = new Admin("Simon", "De Wilde", "simon.dewilde@student.hogent.be");
				_dbContext.Admins.Add(admin);
				#endregion

				#region Employees
				Employee sponsor = new Employee("Sponser", "Sponser", "sponser@essentials.com");
				Employee ziggy = new Employee("Ziggy", "Moens", "ziggy@essentials.com");
				Employee marbod = new Employee("Marbod", "Naassens", "marbod@essentials.com");
				_dbContext.Employees.AddRange(new List<Employee>() { sponsor, ziggy, marbod });
				#endregion

				#region Changemananger
				ChangeManager changeManagerSuktrit = new ChangeManager("Sukrit", "Bhattacharya", "Sukrit.bhattacharya@essentials.com");
				_dbContext.ChangeManagers.Add(changeManagerSuktrit);
				#endregion

				#region Organization
				Organization hogent = new Organization("Hogent", new List<Employee>() { sponsor, ziggy, marbod }, changeManagerSuktrit);
				admin.Organizations.Add(hogent);

				_dbContext.Organizations.Add(hogent);
				#endregion

				#region OrganizationalParts
				OrganizationPart jellyTeam = new OrganizationPart("Jelly Team", OrganizationPartType.TEAM);
				OrganizationPart belgium = new OrganizationPart("belgium", OrganizationPartType.COUNTRY);
				OrganizationPart netherlands = new OrganizationPart("The Netherlands", OrganizationPartType.COUNTRY);
				OrganizationPart officeBE = new OrganizationPart("Belgian Office", OrganizationPartType.OFFICE);
				OrganizationPart officeNL = new OrganizationPart("Dutch Office", OrganizationPartType.OFFICE);
				OrganizationPart departmentHR = new OrganizationPart("HR", OrganizationPartType.DEPARTMENT);

				changeManagerSuktrit.EmployeeOrganizationParts.Add(new EmployeeOrganizationPart(changeManagerSuktrit, departmentHR));
				changeManagerSuktrit.EmployeeOrganizationParts.Add(new EmployeeOrganizationPart(changeManagerSuktrit, belgium));
				changeManagerSuktrit.EmployeeOrganizationParts.Add(new EmployeeOrganizationPart(changeManagerSuktrit, officeBE));

				ziggy.EmployeeOrganizationParts.Add(new EmployeeOrganizationPart(ziggy, belgium));
				ziggy.EmployeeOrganizationParts.Add(new EmployeeOrganizationPart(ziggy, jellyTeam));
				ziggy.EmployeeOrganizationParts.Add(new EmployeeOrganizationPart(ziggy, officeBE));

				marbod.EmployeeOrganizationParts.Add(new EmployeeOrganizationPart(marbod, netherlands));
				marbod.EmployeeOrganizationParts.Add(new EmployeeOrganizationPart(marbod, jellyTeam));
				marbod.EmployeeOrganizationParts.Add(new EmployeeOrganizationPart(marbod, officeNL));

				IList<OrganizationPart> ops = new List<OrganizationPart>() {
					jellyTeam,
					belgium,
					netherlands,
					officeBE,
					officeNL,
					departmentHR
				};

				hogent.OrganizationParts.AddRange(ops);

				#endregion


				#region Projects
				Project project = new Project("Our big project");
				//var projects = new List<Project> { project };
				//_dbContext.Projects.AddRange(projects);
				hogent.Portfolio.Projects.Add(project);

				_dbContext.Projects.Add(project);
				#endregion

				#region OrganizationalChangeTypes
				OrganizationalChangeType organizationalChange = new OrganizationalChangeType();
				EconomicalChangeType economicalChange = new EconomicalChangeType();
				//PersonalChangeType personalChange = new PersonalChangeType();
				//TechnologicalChangeType technologicalChange = new TechnologicalChangeType();
				#endregion

				#region ChangeInitiatives
				ChangeInitiative ciNewCatering = new ChangeInitiative("New Catering", "A new catering will be added to the cafeteria on the ground floor", DateTime.Now.AddHours(1), DateTime.Now.AddDays(31), sponsor, organizationalChange);
				ChangeInitiative ciExpansion = new ChangeInitiative("Expansion German Market", "We will try to expand more on the German Market", DateTime.Now.AddHours(1), DateTime.Now.AddDays(31), sponsor, economicalChange);
				changeManagerSuktrit.CreatedChangeInitiatives.Add(ciNewCatering);
				changeManagerSuktrit.CreatedChangeInitiatives.Add(ciExpansion);

				project.ChangeInitiatives.Add(ciNewCatering);
				project.ChangeInitiatives.Add(ciExpansion);

				_dbContext.ChangeInitiatives.Add(ciNewCatering);
				_dbContext.ChangeInitiatives.Add(ciExpansion);
				#endregion

				#region ChangeGroups
				ChangeGroup allEmployees = new ChangeGroup("All mployees");
				allEmployees.Users.AddRange(new List<IUser>() { ziggy, marbod });

				ChangeGroup justBelgium = new ChangeGroup("Just Belgium");
				justBelgium.Users.Add(ziggy);

				ciNewCatering.ChangeGroup = allEmployees;
				ciExpansion.ChangeGroup = justBelgium;

				_dbContext.ChangeGroups.Add(allEmployees);
				_dbContext.ChangeGroups.Add(justBelgium);
				#endregion

				#region RoadmapItems
				RoadMapItem roadMapItemResto1 = new RoadMapItem("Stop contract with old catering service", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemResto2 = new RoadMapItem("Make contract with new catering service", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemResto3 = new RoadMapItem("Rennovate Restaurants", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1));
				RoadMapItem roadMapItemResto4 = new RoadMapItem("Open new restaurant", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(1));
				RoadMapItem roadMapItemResto5 = new RoadMapItem("Stop contract with old catering service", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemResto6 = new RoadMapItem("Make contract with new catering service", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1));
				RoadMapItem roadMapItemResto7 = new RoadMapItem("Rennovate Restaurants", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1));
				RoadMapItem roadMapItemResto8 = new RoadMapItem("Open new restaurant", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(1));
				ciNewCatering.RoadMap.Add(roadMapItemResto1);
				ciNewCatering.RoadMap.Add(roadMapItemResto2);
				ciNewCatering.RoadMap.Add(roadMapItemResto3);
				ciNewCatering.RoadMap.Add(roadMapItemResto4);
				ciNewCatering.RoadMap.Add(roadMapItemResto5);
				ciNewCatering.RoadMap.Add(roadMapItemResto6);
				ciNewCatering.RoadMap.Add(roadMapItemResto7);
				ciNewCatering.RoadMap.Add(roadMapItemResto8);

				RoadMapItem roadMapItemExpansion1 = new RoadMapItem("Prepare Expansion", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemExpansion2 = new RoadMapItem("Risk Managemant", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemExpansion3 = new RoadMapItem("Excecute Expansion", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemExpansion4 = new RoadMapItem("Review Expansion", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemExpansion5 = new RoadMapItem("Prepare Expansion", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemExpansion6 = new RoadMapItem("Risk Managemant", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1));
				RoadMapItem roadMapItemExpansion7 = new RoadMapItem("Excecute Expansion", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1));
				RoadMapItem roadMapItemExpansion8 = new RoadMapItem("Review Expansion", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(1));
				ciExpansion.RoadMap.Add(roadMapItemExpansion1);
				ciExpansion.RoadMap.Add(roadMapItemExpansion2);
				ciExpansion.RoadMap.Add(roadMapItemExpansion3);
				ciExpansion.RoadMap.Add(roadMapItemExpansion4);
				ciExpansion.RoadMap.Add(roadMapItemExpansion5);
				ciExpansion.RoadMap.Add(roadMapItemExpansion6);
				ciExpansion.RoadMap.Add(roadMapItemExpansion7);
				ciExpansion.RoadMap.Add(roadMapItemExpansion8);


				IList<RoadMapItem> rmis = new List<RoadMapItem>() {
					roadMapItemExpansion1,
					roadMapItemExpansion2,
					roadMapItemExpansion3,
					roadMapItemExpansion4,
					roadMapItemExpansion5,
					roadMapItemExpansion6,
					roadMapItemExpansion7,
					roadMapItemExpansion8,
					roadMapItemResto1,
					roadMapItemResto2,
					roadMapItemResto3,
					roadMapItemResto4,
					roadMapItemResto5,
					roadMapItemResto6,
					roadMapItemResto7,
					roadMapItemResto8
				};
				_dbContext.RoadMapItems.AddRange(rmis);

				#endregion

				#region Surveys
				Survey surveyResto1 = new Survey();
				Survey surveyResto2 = new Survey();
				Survey surveyResto3 = new Survey();
				Survey surveyResto4 = new Survey();

				ClosedQuestion questionResto1 = new ClosedQuestion("What was you opinion about the old Catering?", 1);
				questionResto1.PossibleAnswers = new List<Answer>() { new Answer("good"), new Answer("okay"), new Answer("bad") };
				surveyResto1.Questions.Add(questionResto1);
				ClosedQuestion questionResto2 = new ClosedQuestion("What was you opinion about the new Catering?", 1);
				questionResto2.PossibleAnswers = new List<Answer>() { new Answer("good"), new Answer("okay"), new Answer("bad") };
				surveyResto2.Questions.Add(questionResto2);
				ClosedQuestion questionResto3 = new ClosedQuestion("What was you opinion about the rennovation?", 1);
				questionResto3.PossibleAnswers = new List<Answer>() { new Answer("good"), new Answer("okay"), new Answer("bad") };
				surveyResto3.Questions.Add(questionResto3);
				ClosedQuestion questionResto4 = new ClosedQuestion("What is you opinion about the food?", 1);
				questionResto4.PossibleAnswers = new List<Answer>() { new Answer("good"), new Answer("okay"), new Answer("bad") };
				surveyResto4.Questions.Add(questionResto4);

				roadMapItemResto1.Assesment = surveyResto1;
				roadMapItemResto2.Assesment = surveyResto2;
				roadMapItemResto3.Assesment = surveyResto3;
				roadMapItemResto4.Assesment = surveyResto4;

				/////////////
				Survey surveyExpansion1 = new Survey();
				Survey surveyExpansion2 = new Survey();
				Survey surveyExpansion3 = new Survey();
				Survey surveyExpansion4 = new Survey();

				ClosedQuestion questionExpansion1 = new ClosedQuestion("What was you opinion about the old size of the company?", 1);
				questionExpansion1.PossibleAnswers = new List<Answer>() { new Answer("good"), new Answer("okay"), new Answer("bad") };
				surveyExpansion1.Questions.Add(questionExpansion1);
				ClosedQuestion questionExpansion2 = new ClosedQuestion("What was you opinion about the new size of the company?", 1);
				questionExpansion2.PossibleAnswers = new List<Answer>() { new Answer("good"), new Answer("okay"), new Answer("bad") };
				surveyExpansion2.Questions.Add(questionExpansion2);
				ClosedQuestion questionExpansion3 = new ClosedQuestion("What is your oppinion about the risks?", 1);
				questionExpansion3.PossibleAnswers = new List<Answer>() { new Answer("good"), new Answer("okay"), new Answer("bad") };
				surveyExpansion3.Questions.Add(questionExpansion3);
				ClosedQuestion questionExpansion4 = new ClosedQuestion("What is you opinion after the expansion?", 1);
				questionExpansion4.PossibleAnswers = new List<Answer>() { new Answer("good"), new Answer("okay"), new Answer("bad") };
				surveyExpansion4.Questions.Add(questionExpansion4);

				roadMapItemExpansion1.Assesment = surveyExpansion1;
				roadMapItemExpansion2.Assesment = surveyExpansion2;
				roadMapItemExpansion3.Assesment = surveyExpansion3;
				roadMapItemExpansion4.Assesment = surveyExpansion4;

				IList<Survey> s = new List<Survey>() {
					surveyExpansion1,
					surveyExpansion2,
					surveyExpansion3,
					surveyExpansion4,
					surveyResto1,
					surveyResto2,
					surveyResto3,
					surveyResto4
				};
				_dbContext.Surveys.AddRange(s);
				#endregion






				//#region OrganizationParts
				//OrganizationPart organizationPart1 = new OrganizationPart("Giga Berlin", OrganizationPartType.FACTORY);
				//#endregion

				//#region ChangeManagers
				//changeManagerSuktrit.CreatedChangeInitiatives.Add(ciNewCatering);
				//changeManagerSuktrit.CreatedChangeInitiatives.Add(ciExpansion);
				//changeManagerSuktrit.OrganizationParts.Add(organizationPart1);
				//management.Users.Add(changeManagerSuktrit);
				//_dbContext.ChangeGroups.Add(management);
				//#endregion


				_dbContext.SaveChanges();
				Console.WriteLine("Database created");
			}
		}
	}
}