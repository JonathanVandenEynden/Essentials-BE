using Moq;
using P3Backend.Controllers;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Test.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace P3Backend.Test.Controllers {
	class ChangeInitiativesControllerTest {

		private DummyData _dummyData;

		private readonly ChangeInitiativesController _controller;
		private readonly Mock<IChangeInitiativeRepository> _changeInitiativeRepo;
		private readonly Mock<IUserRepository> _userRepo;
		private readonly Mock<IProjectRepository> _projectRepo;
		private readonly Mock<IChangeManagerRepository> _changeManagerRepo;
		private readonly Mock<IEmployeeRepository> _employeeRepo;


		public ChangeInitiativesControllerTest() {
			_changeInitiativeRepo = new Mock<IChangeInitiativeRepository>();
			_userRepo = new Mock<IUserRepository>();
			_projectRepo = new Mock<IProjectRepository>();
			_changeManagerRepo = new Mock<IChangeManagerRepository>();
			_employeeRepo = new Mock<IEmployeeRepository>();

			_controller = new ChangeInitiativesController(
				_changeInitiativeRepo.Object,
				_userRepo.Object,
				_projectRepo.Object,
				_changeManagerRepo.Object,
				_employeeRepo.Object
				);

			_dummyData = new DummyData();
		}

	}
}
