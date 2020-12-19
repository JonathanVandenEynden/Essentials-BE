using System.Collections.Generic;

namespace P3Backend.Model.DTO_s {
    public class OrganizationDTO {
        public string Name { get; set; }
        public List<EmployeeRecordDTO> EmployeeRecordDTOs { get; set; }
        // CM-role is assigned to the first employee;
        //public EmployeeDTO ChangeManager { get; set; }

    }
}
