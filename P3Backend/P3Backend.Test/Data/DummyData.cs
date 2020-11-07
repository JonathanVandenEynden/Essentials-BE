using P3Backend.Model;
using P3Backend.Model.ChangeTypes;
using P3Backend.Model.OrganizationParts;
using P3Backend.Model.Questions;
using P3Backend.Model.TussenTabellen;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace P3Backend.Test.Data {
	public class DummyData {
		#region props
		// admin
		public Admin admin;
		// employees
		public Employee sponsor;
		public Employee ziggy;
		public Employee marbod;
		// change manager
		public ChangeManager changeManagerSuktrit;
		// organization
		public Organization hogent;
		// OrganizationPart
		public OrganizationPart jellyTeam;
		public OrganizationPart belgium;
		public OrganizationPart netherlands;
		public OrganizationPart officeBE;
		public OrganizationPart officeNL;
		public OrganizationPart departmentHR;
		// project
		public Project project;
		// changetype
		public OrganizationalChangeType organizationalChange;
		public EconomicalChangeType economicalChange;
		// Change Initiative
		public ChangeInitiative ciNewCatering;
		public ChangeInitiative ciExpansion;
		// changegroup
		public ChangeGroup allEmployees;
		public ChangeGroup justBelgium;
		// roadmapitem
		public RoadMapItem roadMapItemResto1;
		public RoadMapItem roadMapItemResto2;
		public RoadMapItem roadMapItemResto3;
		public RoadMapItem roadMapItemResto4;
		public RoadMapItem roadMapItemResto5;
		public RoadMapItem roadMapItemResto6;
		public RoadMapItem roadMapItemResto7;
		public RoadMapItem roadMapItemResto8;
		public RoadMapItem roadMapItemExpansion1;
		public RoadMapItem roadMapItemExpansion2;
		public RoadMapItem roadMapItemExpansion3;
		public RoadMapItem roadMapItemExpansion4;
		public RoadMapItem roadMapItemExpansion5;
		public RoadMapItem roadMapItemExpansion6;
		public RoadMapItem roadMapItemExpansion7;
		public RoadMapItem roadMapItemExpansion8;
		// survey
		public Survey surveyResto1;
		public Survey surveyResto2;
		public Survey surveyResto3;
		public Survey surveyResto4;
		public Survey surveyExpansion1;
		public Survey surveyExpansion2;
		public Survey surveyExpansion3;
		public Survey surveyExpansion4;
		// question
		public MultipleChoiceQuestion questionResto1;
		public MultipleChoiceQuestion questionResto2;
		public MultipleChoiceQuestion questionResto3;
		public MultipleChoiceQuestion questionResto4;
		public MultipleChoiceQuestion questionExpansion1;
		public YesNoQuestion yesNoQuestionExpansion1;
		public RangedQuestion rangedQuestionExpansion1;
		public OpenQuestion openQuestionExpansion1;
		public MultipleChoiceQuestion questionExpansion2;
		public MultipleChoiceQuestion questionExpansion3;
		public MultipleChoiceQuestion questionExpansion4;
		#endregion


		public DummyData() {
			#region Admin
			admin = new Admin("Simon", "De Wilde", "simon.dewilde@student.hogent.be");
			#endregion
			#region Employees
			sponsor = new Employee("Sponser", "Sponser", "sponser@essentials.com");
			ziggy = new Employee("Ziggy", "Moens", "ziggy@essentials.com");
			marbod = new Employee("Marbod", "Naassens", "marbod@essentials.com");
			#endregion
			#region Changemananger
			changeManagerSuktrit = new ChangeManager("Sukrit", "Bhattacharya", "Sukrit.bhattacharya@essentials.com");
			#endregion
			#region Organization
			hogent = new Organization("Hogent", new List<Employee>() { sponsor, ziggy, marbod }, changeManagerSuktrit);
			admin.Organizations.Add(hogent);
			#endregion
			#region OrganizationalParts
			jellyTeam = new OrganizationPart("Jelly Team", OrganizationPartType.TEAM);
			belgium = new OrganizationPart("belgium", OrganizationPartType.COUNTRY);
			netherlands = new OrganizationPart("The Netherlands", OrganizationPartType.COUNTRY);
			officeBE = new OrganizationPart("Belgian Office", OrganizationPartType.OFFICE);
			officeNL = new OrganizationPart("Dutch Office", OrganizationPartType.OFFICE);
			departmentHR = new OrganizationPart("HR", OrganizationPartType.DEPARTMENT);

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
			project = new Project("Our big project");
			//var projects = new List<Project> { project };
			//_dbContext.Projects.AddRange(projects);
			hogent.Portfolio.Projects.Add(project);
			#endregion
			#region OrganizationalChangeTypes
			organizationalChange = new OrganizationalChangeType();
			economicalChange = new EconomicalChangeType();
			//PersonalChangeType personalChange = new PersonalChangeType();
			//TechnologicalChangeType technologicalChange = new TechnologicalChangeType();
			#endregion
			#region ChangeInitiatives
			ciNewCatering = new ChangeInitiative("New Catering", "A new catering will be added to the cafeteria on the ground floor", DateTime.Now.AddHours(1), DateTime.Now.AddDays(31), sponsor, organizationalChange);
			ciExpansion = new ChangeInitiative("Expansion German Market", "We will try to expand more on the German Market", DateTime.Now.AddHours(1), DateTime.Now.AddDays(31), sponsor, economicalChange);
			changeManagerSuktrit.CreatedChangeInitiatives.Add(ciNewCatering);
			changeManagerSuktrit.CreatedChangeInitiatives.Add(ciExpansion);

			project.ChangeInitiatives.Add(ciNewCatering);
			project.ChangeInitiatives.Add(ciExpansion);
			#endregion
			#region ChangeGroups
			allEmployees = new ChangeGroup("All employees");
			allEmployees.Users.AddRange(new List<Employee>() { ziggy, marbod });

			justBelgium = new ChangeGroup("Just Belgium");
			justBelgium.Users.Add(ziggy);

			ciNewCatering.ChangeGroup = allEmployees;
			ciExpansion.ChangeGroup = justBelgium;
			#endregion
			#region RoadmapItems
			roadMapItemResto1 = new RoadMapItem("Stop contract with old catering service", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1)) { Done = true };
			roadMapItemResto2 = new RoadMapItem("Make contract with new catering service", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1)) { Done = true };
			roadMapItemResto3 = new RoadMapItem("Rennovate Restaurants", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1));
			roadMapItemResto4 = new RoadMapItem("Open new restaurant", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(1));
			roadMapItemResto5 = new RoadMapItem("Stop contract with old catering service", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1)) { Done = true };
			roadMapItemResto6 = new RoadMapItem("Make contract with new catering service", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1));
			roadMapItemResto7 = new RoadMapItem("Rennovate Restaurants", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1));
			roadMapItemResto8 = new RoadMapItem("Open new restaurant", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(1));
			ciNewCatering.RoadMap.Add(roadMapItemResto1);
			ciNewCatering.RoadMap.Add(roadMapItemResto2);
			ciNewCatering.RoadMap.Add(roadMapItemResto3);
			ciNewCatering.RoadMap.Add(roadMapItemResto4);
			ciNewCatering.RoadMap.Add(roadMapItemResto5);
			ciNewCatering.RoadMap.Add(roadMapItemResto6);
			ciNewCatering.RoadMap.Add(roadMapItemResto7);
			ciNewCatering.RoadMap.Add(roadMapItemResto8);

			roadMapItemExpansion1 = new RoadMapItem("Prepare Expansion", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1)) { Done = true };
			roadMapItemExpansion2 = new RoadMapItem("Risk Managemant", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1)) { Done = true };
			roadMapItemExpansion3 = new RoadMapItem("Excecute Expansion", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1)) { Done = true };
			roadMapItemExpansion4 = new RoadMapItem("Review Expansion", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(1)) { Done = true };
			roadMapItemExpansion5 = new RoadMapItem("Prepare Expansion", DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1)) { Done = true };
			roadMapItemExpansion6 = new RoadMapItem("Risk Managemant", DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(1));
			roadMapItemExpansion7 = new RoadMapItem("Excecute Expansion", DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(1));
			roadMapItemExpansion8 = new RoadMapItem("Review Expansion", DateTime.Now.AddDays(4), DateTime.Now.AddDays(4).AddHours(1));
			ciExpansion.RoadMap.Add(roadMapItemExpansion1);
			ciExpansion.RoadMap.Add(roadMapItemExpansion2);
			ciExpansion.RoadMap.Add(roadMapItemExpansion3);
			ciExpansion.RoadMap.Add(roadMapItemExpansion4);
			ciExpansion.RoadMap.Add(roadMapItemExpansion5);
			ciExpansion.RoadMap.Add(roadMapItemExpansion6);
			ciExpansion.RoadMap.Add(roadMapItemExpansion7);
			ciExpansion.RoadMap.Add(roadMapItemExpansion8);
			#endregion
			#region Surveys

			surveyResto1 = new Survey(roadMapItemResto1);
			//surveyResto1.surveyTemplates("test");
			surveyResto2 = new Survey(roadMapItemResto2);
			surveyResto3 = new Survey(roadMapItemResto3);
			surveyResto4 = new Survey(roadMapItemResto4);

			questionResto1 = new MultipleChoiceQuestion("What was your opinion about the old Catering?");
			questionResto1.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
			surveyResto1.Questions.Add(questionResto1);
			questionResto2 = new MultipleChoiceQuestion("What was your opinion about the new Catering?");
			questionResto2.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
			surveyResto2.Questions.Add(questionResto2);
			questionResto3 = new MultipleChoiceQuestion("What was your opinion about the rennovation?");
			questionResto3.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
			surveyResto3.Questions.Add(questionResto3);
			questionResto4 = new MultipleChoiceQuestion("What is your opinion about the food?");
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
			surveyExpansion1 = new Survey(roadMapItemExpansion1);
			surveyExpansion2 = new Survey(roadMapItemExpansion2);
			surveyExpansion3 = new Survey(roadMapItemExpansion3);
			surveyExpansion4 = new Survey(roadMapItemExpansion4);

			questionExpansion1 = new MultipleChoiceQuestion("What was your opinion about the old size of the company?");
			questionExpansion1.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
			yesNoQuestionExpansion1 = new YesNoQuestion("Do you think this is a good change?");
			rangedQuestionExpansion1 = new RangedQuestion("How good do you think this change is?");
			openQuestionExpansion1 = new OpenQuestion("How do you know about this change");
			openQuestionExpansion1.PossibleAnswers.Add("I do not know", 0);
			openQuestionExpansion1.PossibleAnswers.Add("I heard it from a friend", 0);
			surveyExpansion1.Questions.Add(questionExpansion1);
			surveyExpansion1.Questions.Add(yesNoQuestionExpansion1);
			surveyExpansion1.Questions.Add(rangedQuestionExpansion1);
			surveyExpansion1.Questions.Add(openQuestionExpansion1);

			questionExpansion2 = new MultipleChoiceQuestion("What was your opinion about the new size of the company?");
			questionExpansion2.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
			surveyExpansion2.Questions.Add(questionExpansion2);
			questionExpansion3 = new MultipleChoiceQuestion("What is yourr oppinion about the risks?");
			questionExpansion3.AddPossibleAnswers(new List<string> { "Good", "Okay", "Bad" }, true);
			surveyExpansion3.Questions.Add(questionExpansion3);
			questionExpansion4 = new MultipleChoiceQuestion("What is your opinion after the expansion?");
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

			#endregion
		}














	}
}
