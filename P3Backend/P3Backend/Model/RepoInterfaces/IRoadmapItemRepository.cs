using System.Collections.Generic;

namespace P3Backend.Model.RepoInterfaces {
	public interface IRoadmapItemRepository {
		IEnumerable<RoadMapItem> GetAll();

		RoadMapItem GetBy(int id);

		void Add(RoadMapItem rmi);

		void Update(RoadMapItem rmi);

		void Delete(RoadMapItem rmi);

		void SaveChanges();

		RoadMapItem GetByTitle(string title);
	}
}
