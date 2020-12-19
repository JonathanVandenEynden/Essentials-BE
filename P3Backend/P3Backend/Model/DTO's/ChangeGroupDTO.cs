using System.Collections.Generic;

namespace P3Backend.Model.DTO_s {
    public class ChangeGroupDTO {
        public string Name { get; set; }
        public List<int> UserIds { get; set; }
    }
}
