using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using P3Backend.Model;
using P3Backend.Model.ChangeTypes;
using P3Backend.Model.OrganizationParts;
using P3Backend.Model.Questions;
using P3Backend.Model.TussenTabellen;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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

				#region Update Discriminator table
				// Trigger to edit the discriminator field when the employee is upgraded
				await _dbContext.Database.ExecuteSqlRawAsync("drop trigger if exists update_discriminator");
				await _dbContext.Database.ExecuteSqlRawAsync(
												"create trigger update_discriminator on dbo.Users " +
												"for update as " +
												"if UPDATE(OrganizationId) " +
												"begin " +
												"declare @id int " +
												"select top 1 @id = u.Id from deleted u " +
												"update dbo.Users set Discriminator = 'ChangeManager' where Id = @id " +
												"end"
											);
				#endregion

				#region Admin
				Admin simon = new Admin("Simon", "De Wilde", "simon.dewilde@essentials.com");
				Admin jonathan = new Admin("Jonatan", "Vanden Eynden Van Lysebeth", "jonathan.vandeneyndenvanlysebeth@essentials.com");
				_dbContext.Admins.AddRange(new List<Admin>() { simon, jonathan });
				#endregion

				#region Employees
				Employee sponsor = new Employee("Sponser", "Sponser", "sponser@hogent.com");
				Employee ziggy = new Employee("Ziggy", "Moens", "ziggy@hogent.com");
				Employee marbod = new Employee("Marbod", "Naassens", "marbod@hogent.com");
				Employee sebastien = new Employee("Sébastien", "De Pauw", "sebastien@hogent.com");
				Employee sven = new Employee("Sven", "Wyseur", "sven@hogent.com");
				Employee elias = new Employee("Elias", "Ameye", "elias@hogent.com");
				Employee lotte = new Employee("Lotte", "Van Achter", "lotte@hogent.com");
				Employee maud = new Employee("Maud", "Dijkstra", "maud@hogent.com");
				_dbContext.Employees.AddRange(new List<Employee>() { sponsor, ziggy, marbod, sebastien, sven, elias, lotte, maud });
				#endregion

				#region Changemananger
				ChangeManager changeManagerSuktrit = new ChangeManager("Sukrit", "Bhattacharya", "Sukrit.bhattacharya@hogent.com");
				_dbContext.ChangeManagers.Add(changeManagerSuktrit);
				#endregion

				#region Organization
				Organization hogent = new Organization("Hogent", new List<Employee>() { sponsor, ziggy, marbod, sebastien, sven, elias, lotte, maud }, changeManagerSuktrit);
				simon.Organizations.Add(hogent);
				_dbContext.Organizations.Add(hogent);
				#endregion

				#region OrganizationalParts
				OrganizationPart jellyTeam = new OrganizationPart("Jelly Team", OrganizationPartType.TEAM);
				OrganizationPart belgium = new OrganizationPart("Belgium", OrganizationPartType.COUNTRY);
				OrganizationPart netherlands = new OrganizationPart("The Netherlands", OrganizationPartType.COUNTRY);
				OrganizationPart officeBE = new OrganizationPart("Belgian Office", OrganizationPartType.OFFICE);
				OrganizationPart officeNL = new OrganizationPart("Dutch Office", OrganizationPartType.OFFICE);
				OrganizationPart departmentHR = new OrganizationPart("Human Resources", OrganizationPartType.DEPARTMENT);
				OrganizationPart softwareTeam = new OrganizationPart("Software Development Team", OrganizationPartType.TEAM);

				var cmshr = new EmployeeOrganizationPart(changeManagerSuktrit, departmentHR);
				changeManagerSuktrit.EmployeeOrganizationParts.Add(cmshr);
				departmentHR.EmployeeOrganizationParts.Add(cmshr);
				var cmsbel = new EmployeeOrganizationPart(changeManagerSuktrit, belgium);
				changeManagerSuktrit.EmployeeOrganizationParts.Add(cmsbel);
				belgium.EmployeeOrganizationParts.Add(cmsbel);
				var cmsoffbe = new EmployeeOrganizationPart(changeManagerSuktrit, officeBE);
				changeManagerSuktrit.EmployeeOrganizationParts.Add(cmsoffbe);
				officeBE.EmployeeOrganizationParts.Add(cmsoffbe);

				var zb = new EmployeeOrganizationPart(ziggy, belgium);
				ziggy.EmployeeOrganizationParts.Add(zb);
				belgium.EmployeeOrganizationParts.Add(zb);
				var zjt = new EmployeeOrganizationPart(ziggy, jellyTeam);
				ziggy.EmployeeOrganizationParts.Add(zjt);
				jellyTeam.EmployeeOrganizationParts.Add(zjt);
				var zoffbe = new EmployeeOrganizationPart(ziggy, officeBE);
				ziggy.EmployeeOrganizationParts.Add(zoffbe);
				officeBE.EmployeeOrganizationParts.Add(zoffbe);

				var mn = new EmployeeOrganizationPart(marbod, netherlands);
				marbod.EmployeeOrganizationParts.Add(mn);
				netherlands.EmployeeOrganizationParts.Add(mn);
				var mj = new EmployeeOrganizationPart(marbod, jellyTeam);
				marbod.EmployeeOrganizationParts.Add(mj);
				jellyTeam.EmployeeOrganizationParts.Add(mj);
				var moffnl = new EmployeeOrganizationPart(marbod, officeNL);
				marbod.EmployeeOrganizationParts.Add(moffnl);
				officeNL.EmployeeOrganizationParts.Add(moffnl);

                var sb = new EmployeeOrganizationPart(sebastien, belgium);
                sebastien.EmployeeOrganizationParts.Add(sb);
                belgium.EmployeeOrganizationParts.Add(sb);
                var sj = new EmployeeOrganizationPart(sebastien, jellyTeam);
                sebastien.EmployeeOrganizationParts.Add(sj);
                jellyTeam.EmployeeOrganizationParts.Add(sj);
                var shr = new EmployeeOrganizationPart(sebastien, departmentHR);
                sebastien.EmployeeOrganizationParts.Add(shr);
                departmentHR.EmployeeOrganizationParts.Add(shr);

                var enl = new EmployeeOrganizationPart(elias, netherlands);
                elias.EmployeeOrganizationParts.Add(enl);
                netherlands.EmployeeOrganizationParts.Add(enl);
                var eoffbe = new EmployeeOrganizationPart(elias, officeBE);
                elias.EmployeeOrganizationParts.Add(eoffbe);
                officeBE.EmployeeOrganizationParts.Add(eoffbe);
                var est = new EmployeeOrganizationPart(elias, softwareTeam);
                elias.EmployeeOrganizationParts.Add(est);
                softwareTeam.EmployeeOrganizationParts.Add(est);

                var soffbe = new EmployeeOrganizationPart(sven, officeBE);
                sven.EmployeeOrganizationParts.Add(soffbe);
                officeBE.EmployeeOrganizationParts.Add(soffbe);
                var sst = new EmployeeOrganizationPart(sven, softwareTeam);
                sven.EmployeeOrganizationParts.Add(sst);
                softwareTeam.EmployeeOrganizationParts.Add(sst);
                var sbe = new EmployeeOrganizationPart(sven, belgium);
                sven.EmployeeOrganizationParts.Add(sbe);
                belgium.EmployeeOrganizationParts.Add(sbe);

                var loffnl = new EmployeeOrganizationPart(lotte, officeNL);
                lotte.EmployeeOrganizationParts.Add(loffnl);
                officeNL.EmployeeOrganizationParts.Add(loffnl);
                var fnl = new EmployeeOrganizationPart(lotte, netherlands);
                lotte.EmployeeOrganizationParts.Add(fnl);
                netherlands.EmployeeOrganizationParts.Add(fnl);
                var lhr = new EmployeeOrganizationPart(lotte, departmentHR);
                lotte.EmployeeOrganizationParts.Add(lhr);
                departmentHR.EmployeeOrganizationParts.Add(lhr);

                var mjt = new EmployeeOrganizationPart(maud, jellyTeam);
                maud.EmployeeOrganizationParts.Add(mjt);
                jellyTeam.EmployeeOrganizationParts.Add(mjt);
                var mst = new EmployeeOrganizationPart(maud, softwareTeam);
                maud.EmployeeOrganizationParts.Add(mst);
                softwareTeam.EmployeeOrganizationParts.Add(mst);
                var moffnl1 = new EmployeeOrganizationPart(maud, officeNL);
                maud.EmployeeOrganizationParts.Add(moffnl1);
                officeNL.EmployeeOrganizationParts.Add(moffnl1);
                var mnl = new EmployeeOrganizationPart(maud, netherlands);
                maud.EmployeeOrganizationParts.Add(mnl);
                netherlands.EmployeeOrganizationParts.Add(mnl);

                IList<OrganizationPart> ops = new List<OrganizationPart>() {
					jellyTeam,
					belgium,
					netherlands,
					officeBE,
					officeNL,
					departmentHR,
					softwareTeam
				};

				hogent.OrganizationParts.AddRange(ops);
				#endregion


				#region Projects
				Project project = new Project("Our big project");
				hogent.Portfolio.Projects.Add(project);
				_dbContext.Projects.Add(project);
				#endregion

				#region OrganizationalChangeTypes
				OrganizationalChangeType organizationalChange = new OrganizationalChangeType();
				EconomicalChangeType economicalChange = new EconomicalChangeType();
				#endregion


				#region ChangeGroups
				ChangeGroup allEmployees = new ChangeGroup("All employees");
				EmployeeChangeGroup ziggyAllEmployees = new EmployeeChangeGroup(ziggy, allEmployees);
				ziggy.EmployeeChangeGroups.Add(ziggyAllEmployees);
				allEmployees.EmployeeChangeGroups.Add(ziggyAllEmployees);

				EmployeeChangeGroup marbodAllEmployees = new EmployeeChangeGroup(marbod, allEmployees);
				marbod.EmployeeChangeGroups.Add(marbodAllEmployees);
				allEmployees.EmployeeChangeGroups.Add(marbodAllEmployees);

				EmployeeChangeGroup sebastienAllEmployees = new EmployeeChangeGroup(sebastien, allEmployees);
				sebastien.EmployeeChangeGroups.Add(sebastienAllEmployees);
				allEmployees.EmployeeChangeGroups.Add(sebastienAllEmployees);

				EmployeeChangeGroup svenAllEmployees = new EmployeeChangeGroup(sven, allEmployees);
				sven.EmployeeChangeGroups.Add(svenAllEmployees);
				allEmployees.EmployeeChangeGroups.Add(svenAllEmployees);

				EmployeeChangeGroup eliasAllEmployees = new EmployeeChangeGroup(elias, allEmployees);
				elias.EmployeeChangeGroups.Add(eliasAllEmployees);
				allEmployees.EmployeeChangeGroups.Add(eliasAllEmployees);

				EmployeeChangeGroup lotteAllEmployees = new EmployeeChangeGroup(lotte, allEmployees);
				lotte.EmployeeChangeGroups.Add(lotteAllEmployees);
				allEmployees.EmployeeChangeGroups.Add(lotteAllEmployees);

				EmployeeChangeGroup maudAllEmployees = new EmployeeChangeGroup(maud, allEmployees);
				maud.EmployeeChangeGroups.Add(maudAllEmployees);
				allEmployees.EmployeeChangeGroups.Add(maudAllEmployees);

				/*allEmployees.EmployeeChangeGroups.AddRange(new List<EmployeeChangeGroup>() { 
					ziggyAllEmployees,
					marbodAllEmployees, 
					sebastienAllEmployees,
					svenAllEmployees,
					eliasAllEmployees,
					lotteAllEmployees,
					maudAllEmployees
				});*/

				ChangeGroup justBelgium = new ChangeGroup("Just Belgium");
				EmployeeChangeGroup ziggyJustBelgium = new EmployeeChangeGroup(ziggy, justBelgium);
				ziggy.EmployeeChangeGroups.Add(ziggyJustBelgium);
				justBelgium.EmployeeChangeGroups.Add(ziggyJustBelgium);

				EmployeeChangeGroup sebastienJustBelgium = new EmployeeChangeGroup(sebastien, justBelgium);
				sebastien.EmployeeChangeGroups.Add(sebastienJustBelgium);
				justBelgium.EmployeeChangeGroups.Add(sebastienJustBelgium);

				EmployeeChangeGroup svenJustBelgium = new EmployeeChangeGroup(sven, justBelgium);
				sven.EmployeeChangeGroups.Add(svenJustBelgium);
				justBelgium.EmployeeChangeGroups.Add(svenJustBelgium);

				EmployeeChangeGroup eliasJustBelgium = new EmployeeChangeGroup(elias, justBelgium);
				elias.EmployeeChangeGroups.Add(eliasJustBelgium);
				justBelgium.EmployeeChangeGroups.Add(eliasJustBelgium);

				

				_dbContext.ChangeGroups.Add(allEmployees);
				_dbContext.ChangeGroups.Add(justBelgium);
				#endregion

				#region ChangeInitiatives
				ChangeInitiative ciNewCatering = new ChangeInitiative("New Catering", "A new catering will be added to the cafeteria on the ground floor", DateTime.Now.AddHours(1), DateTime.Now.AddDays(31), sponsor, organizationalChange, allEmployees);
				ChangeInitiative ciExpansion = new ChangeInitiative("Expansion German Market", "We will try to expand more on the German Market", DateTime.Now.AddHours(1), DateTime.Now.AddDays(31), sponsor, economicalChange, justBelgium);
				changeManagerSuktrit.CreatedChangeInitiatives.Add(ciNewCatering);
				changeManagerSuktrit.CreatedChangeInitiatives.Add(ciExpansion);

				project.ChangeInitiatives.Add(ciNewCatering);
				project.ChangeInitiatives.Add(ciExpansion);

				_dbContext.ChangeInitiatives.Add(ciNewCatering);
				_dbContext.ChangeInitiatives.Add(ciExpansion);
				#endregion

				#region RoadmapItems
				RoadMapItem roadMapItemResto1 = new RoadMapItem("Stop contract with old catering service", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemResto2 = new RoadMapItem("Make contract with new catering service", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemResto3 = new RoadMapItem("Rennovate Restaurants", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1));
				RoadMapItem roadMapItemResto4 = new RoadMapItem("Open new restaurant", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(1));

				ciNewCatering.RoadMap.Add(roadMapItemResto1);
				ciNewCatering.RoadMap.Add(roadMapItemResto2);
				ciNewCatering.RoadMap.Add(roadMapItemResto3);
				ciNewCatering.RoadMap.Add(roadMapItemResto4);

				RoadMapItem roadMapItemExpansion1 = new RoadMapItem("Prepare Expansion", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemExpansion2 = new RoadMapItem("Risk Managemant", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemExpansion3 = new RoadMapItem("Excecute Expansion", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1)) { Done = true };
				RoadMapItem roadMapItemExpansion4 = new RoadMapItem("Review Expansion", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(1)) { Done = true };

				ciExpansion.RoadMap.Add(roadMapItemExpansion1);
				ciExpansion.RoadMap.Add(roadMapItemExpansion2);
				ciExpansion.RoadMap.Add(roadMapItemExpansion3);
				ciExpansion.RoadMap.Add(roadMapItemExpansion4);


				IList<RoadMapItem> rmis = new List<RoadMapItem>() {
					roadMapItemExpansion1,
					roadMapItemExpansion2,
					roadMapItemExpansion3,
					roadMapItemExpansion4,

					roadMapItemResto1,
					roadMapItemResto2,
					roadMapItemResto3,
					roadMapItemResto4,

				};
				_dbContext.RoadMapItems.AddRange(rmis);

				#endregion

				#region Surveys

				Survey surveyResto1 = new Survey(roadMapItemResto1);
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
				await CreateUser(simon.Email, "P@ssword1", "admin");
				await CreateUser(jonathan.Email, "P@ssword1", "admin");
				await CreateUser(sponsor.Email, "P@ssword1", "employee");
				await CreateUser(ziggy.Email, "P@ssword1", "employee");
				await CreateUser(marbod.Email, "P@ssword1", "employee");
				await CreateUser(sebastien.Email, "P@ssword1", "employee");
				await CreateUser(sven.Email, "P@ssword1", "employee");
				await CreateUser(elias.Email, "P@ssword1", "employee");
				await CreateUser(lotte.Email, "P@ssword1", "employee");
				await CreateUser(maud.Email, "P@ssword1", "employee");
				await CreateUser(changeManagerSuktrit.Email, "P@ssword1", "changeManager");		
				#endregion

				#region Preset Surveys

				PresetSurvey presetLeadership = new PresetSurvey("Leadership");
				PresetSurvey presetCapacityAndLeadership = new PresetSurvey("Capacity And Culture");

				#region List of questions

				List<string> leadershipQuestions = new List<string> {
					"The transformation sponsor has knowledge of change management techniques and principles",
					"The transformation sponsor actively supports and understands the change management initiatives", "The sponsor is active and visible to represent the change",
					"The sponsor was successful in this role in previous change projects",
					"The sponsor is a gifted speaker or is charismatic to motivate people",
					"The sponsor can communicate the vision and strategy, including the need for change to executives and senior management",
					"The sponsor can communicate the vision and strategy, including the need for change to employees and customers",
					"The organization will listen to and follow the messages of this sponsor",
					"The sponsor is empowered by the executives to deliver the change",
					"The sponsor is an effective influencer",
					"The sponsor is able to provide for the resources and funding for the project",
					"The sponsor has direct control over the persons and processes impacted by the change",
					"The sponsor has knowledge of the systems, tools and processes impacted by the change",
					"The change is known and supported by the organization's executives",
					"The necessary funding for the change is made available",
					"People managers are (will be) instructed to assign the needed resources for the change",
					"Organization's executives want to receive progress report on the change",
					"The change project is prioritised formally",
					"The organization has had success stories with previous change projects",
					"Employees will be rewarded for embracing the change",
					"People are (will be) appointed to lead the change management project",
					"The change lead reports to the sponsor and/or has regular direct access",
					"The sponsor has successfully created a burning platform around the change"
				};

				List<string> capacityAndCultureQuestions = new List<string> {
					"People at my company know the organization’s business objectives clearly",
					"People at my organization genuinely like one another",
					"People follow clear guidelines and instructions about work",
					"People get along very well and conflicts are rare",
					"Poor performance is dealt with quickly and firmly in my organization.",
					"People often socialize outside of work",
					"People really want to beat the competition",
					"People do favors for each other because they like one another",
					"When opportunities for competitive advantage arise people move firmly to capitalize on them",
					"People make friends for the sake of friendship – there is no other agenda",
					"The strategic goals of the organization are known and shared",
					"People often confide in one another about personal matters",
					"People build close long - term relationships – someday they may be of benefit",
					"The reasons for reward and punishment are clear at my organization",
					"People know a lot about each other’s families",
					"People in my team  are determined to beat our industry competitors",
					"When faced with problem, people first check what friends and colleagues have already done, then attempt to solve it by themselves",
					"Hitting targets is the single most important thing in my organization",
					"To get something done you are allowed to work around the system",
					"Projects that are started reach an end state within finite and defined period",
					"When people leave, co - workers stay in contact to see how they are doing",
					"Roles and responsibilities are formal : it is clear where one person’s job ends and another person’s begins",
					"People protect each other at my organization",
					"Poor performance is not tolerated at any level within this organization",
					"In case a project cannot be completed or successfully realized, it is formally closed with lessons learnt documented and intermediate results archived"
				};
				#endregion

				leadershipQuestions.ForEach(question => presetLeadership.PresetQuestions.Add(new RangedQuestion(question)));
				capacityAndCultureQuestions.ForEach(question => presetCapacityAndLeadership.PresetQuestions.Add(new RangedQuestion(question)));

				_dbContext.PresetSurveys.AddRange(presetLeadership, presetCapacityAndLeadership);
				#endregion

				#region deviceTokens
				DeviceTokens tokens = new DeviceTokens();
				await foreach (var employee in _dbContext.Users) {
					tokens.init(employee.Id.ToString());
				}
				tokens.addToken("4", "dTbUjwokSICFM9mkqygU47:APA91bHqYMirEGMe1YOBeE1oy3yEICy3G0JsDqPdAviT4IIkxca625c9SMSNRCeCRxb0KDtNDm0WTw4avrAHVGc1arkcg2VuDPi3T6fzJWdLbY0WixfCSeR3Amvdzf80HauBRoJekeD4");
				tokens.addToken("5", "ew0p3vsqSg6eEsB5OuUxTZ:APA91bFwe7nNciEbKH1wreA3Q9985SpmOEQ1pokKHVWVuX1EDG-tm4Cr4pgQM_KGEPI3BsfcrYb3BWNLpbpBHqyOLRp5eb7RhEWe_cPo0dLAr2j-TM5vurx3YEUcRwsX_yuuHTI13afn");

				_dbContext.DeviceTokens.AddRange(tokens);
				#endregion

				#region Fill in survey
				roadMapItemResto4.Assessment.Questions[0].CompleteQuestion(4);
				#endregion

				_dbContext.SaveChanges();
				Console.WriteLine("Database created");
			}
		}

		private async Task CreateUser(string email, string password, string claim) {
			var user = new IdentityUser { UserName = email, Email = email };
			await _usermanager.CreateAsync(user, password);
			await _usermanager.AddClaimAsync(user, new Claim(ClaimTypes.Role, claim));
		}
	}
}
