using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.DataProtection;
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
				if (!_dbContext.Admins.Any())
				{ // DEZE LIJN UIT COMMENTAAR EN 2 ERBOVEN IN COMMENTAAR VOOR DEPLOYEN

					#region Admin
					Admin admin1 = new Admin("Simon", "De Wilde", "simon.dewilde@student.hogent.be");
					Admin admin2 = new Admin("Jonatan", "Vanden Eynden Van Lysebeth", "Jonathan.vandeneyndenvanlysebeth@student.hogent.be");
					_dbContext.Admins.AddRange(new List<Admin>() { admin1, admin2 });
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
					admin1.Organizations.Add(hogent);

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
					ChangeGroup allEmployees = new ChangeGroup("All employees");
					allEmployees.Users.AddRange(new List<Employee>() { ziggy, marbod });

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

					Survey surveyResto1 = new Survey(roadMapItemResto1);
					surveyResto1.surveyTemplates("test");
					Survey surveyResto2 = new Survey(roadMapItemResto2);
					Survey surveyResto3 = new Survey(roadMapItemResto3);
					Survey surveyResto4 = new Survey(roadMapItemResto4);

					MultipleChoiceQuestion questionResto1 = new MultipleChoiceQuestion("What was your opinion about the old Catering?");
					questionResto1.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
					surveyResto1.Questions.Add(questionResto1);
					MultipleChoiceQuestion questionResto2 = new MultipleChoiceQuestion("What was your opinion about the new Catering?");
					questionResto2.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
					surveyResto2.Questions.Add(questionResto2);
					MultipleChoiceQuestion questionResto3 = new MultipleChoiceQuestion("What was your opinion about the rennovation?");
					questionResto3.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
					surveyResto3.Questions.Add(questionResto3);
					MultipleChoiceQuestion questionResto4 = new MultipleChoiceQuestion("What is your opinion about the food?");
					questionResto4.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
					surveyResto4.Questions.Add(questionResto4);

					roadMapItemResto1.Assessment = surveyResto1;
					roadMapItemResto2.Assessment = surveyResto2;
					roadMapItemResto3.Assessment = surveyResto3;
					roadMapItemResto4.Assessment = surveyResto4;
					roadMapItemResto5.Assessment = surveyResto1;
					roadMapItemResto6.Assessment = surveyResto2;
					roadMapItemResto7.Assessment = surveyResto3;
					roadMapItemResto8.Assessment = surveyResto4;
					/////////////
					Survey surveyExpansion1 = new Survey(roadMapItemExpansion1);
					Survey surveyExpansion2 = new Survey(roadMapItemExpansion2);
					Survey surveyExpansion3 = new Survey(roadMapItemExpansion3);
					Survey surveyExpansion4 = new Survey(roadMapItemExpansion4);

					MultipleChoiceQuestion questionExpansion1 = new MultipleChoiceQuestion("What was your opinion about the old size of the company?");
					questionExpansion1.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
					YesNoQuestion yesNoQuestionExpansion1 = new YesNoQuestion("Do you think this is a good change?");
					RangedQuestion rangedQuestionExpansion1 = new RangedQuestion("How good do you think this change is?");
					OpenQuestion openQuestionExpansion1 = new OpenQuestion("How do you know about this change");
					openQuestionExpansion1.PossibleAnswers.Add("I do not know", 0);
					openQuestionExpansion1.PossibleAnswers.Add("I heard it from a friend", 0);
					surveyExpansion1.Questions.Add(questionExpansion1);
					surveyExpansion1.Questions.Add(yesNoQuestionExpansion1);
					surveyExpansion1.Questions.Add(rangedQuestionExpansion1);
					surveyExpansion1.Questions.Add(openQuestionExpansion1);

					MultipleChoiceQuestion questionExpansion2 = new MultipleChoiceQuestion("What was your opinion about the new size of the company?");
					questionExpansion2.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
					surveyExpansion2.Questions.Add(questionExpansion2);
					MultipleChoiceQuestion questionExpansion3 = new MultipleChoiceQuestion("What is yourr oppinion about the risks?");
					questionExpansion3.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
					surveyExpansion3.Questions.Add(questionExpansion3);
					MultipleChoiceQuestion questionExpansion4 = new MultipleChoiceQuestion("What is your opinion after the expansion?");
					questionExpansion4.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
					surveyExpansion4.Questions.Add(questionExpansion4);


					roadMapItemExpansion1.Assessment = surveyExpansion1;
					roadMapItemExpansion2.Assessment = surveyExpansion2;
					roadMapItemExpansion3.Assessment = surveyExpansion3;
					roadMapItemExpansion4.Assessment = surveyExpansion4;
					roadMapItemExpansion5.Assessment = surveyExpansion1;
					roadMapItemExpansion6.Assessment = surveyExpansion2;
					roadMapItemExpansion7.Assessment = surveyExpansion3;
					roadMapItemExpansion8.Assessment = surveyExpansion4;

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

					#region Create Identity users
					await CreateUser(admin1.Email, "P@ssword1", "admin");
					await CreateUser(admin2.Email, "P@ssword1", "admin");
					await CreateUser(sponsor.Email, "P@ssword1", "employee");
					await CreateUser(ziggy.Email, "P@ssword1", "employee");
					await CreateUser(marbod.Email, "P@ssword1", "employee");
					await CreateUser(changeManagerSuktrit.Email, "P@ssword1", "changeManager");
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
		private async Task CreateUser(string email, string password, string claim) {
			var user = new IdentityUser { UserName = email, Email = email };
			await _usermanager.CreateAsync(user, password);
			await _usermanager.AddClaimAsync(user, new Claim(ClaimTypes.Role, claim));
		}
	}
}