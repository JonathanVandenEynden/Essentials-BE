using Microsoft.EntityFrameworkCore;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Repositories {
	public class ProjectRepository : IProjectRepository {

		private readonly ApplicationDbContext _context;
		private readonly DbSet<Project> _projects;

		public ProjectRepository(ApplicationDbContext context) {
			_context = context;
			_projects = _context.Projects;
		}
		public void Add(Project p) {
			_projects.Add(p);
		}

		public void Delete(Project p) {
			_projects.Remove(p);
		}

		public IEnumerable<Project> GetAll() {
			return _projects
				.Include(p => p.ChangeInitiatives);
		}

		public Project GetBy(int id) {
			return _projects
				.Include(p => p.ChangeInitiatives)
				.FirstOrDefault(p => p.Id == id);
		}

		public Project GetByName(string name) {
			return _projects
				.Include(p => p.ChangeInitiatives)
				.FirstOrDefault(p => p.Name == name);
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

		public void Update(Project p) {
			_projects.Update(p);
		}
	}
}
