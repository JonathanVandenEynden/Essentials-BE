using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace P3Backend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChangeManagersController : ControllerBase {

        private readonly IChangeManagerRepository _changeManagerRepo;
        private readonly IOrganizationRepository _organizationRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public ChangeManagersController(IChangeManagerRepository changeManagerRepo,
            IOrganizationRepository organizationRepo,
            IEmployeeRepository employeeRepo,
            UserManager<IdentityUser> userManager) {
            _changeManagerRepo = changeManagerRepo;
            _organizationRepo = organizationRepo;
            _employeeRepo = employeeRepo;
            _userManager = userManager;
        }

        /// <summary>
        /// Get the changeManagers of an organization
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "ChangeManagerAccess")]
        public ActionResult<IEnumerable<ChangeManager>> GetChangeManagersFromOrganization() {
            try {
                Employee loggedInEmployee = _employeeRepo.GetByEmail(User.Identity.Name);

                Organization o = _organizationRepo.GetAll().FirstOrDefault(o => o.ChangeManagers.Any(cm => cm.Id == loggedInEmployee.Id) || o.Employees.Any(e => e.Id == loggedInEmployee.Id));

                return o.ChangeManagers;
            } catch {
                return NotFound("Organization not found");
            }
        }

        /// <summary>
        /// Get a changeManager with a given Id
        /// </summary>
        /// <param name="changeManagerId"></param>
        /// <returns></returns>
        [HttpGet("{changeManagerId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "AdminAccess")]
        public ActionResult<ChangeManager> GetChangeManagerById(int changeManagerId) {
            ChangeManager cm = _changeManagerRepo.GetBy(changeManagerId);

            if (cm == null) {
                return NotFound("Change manager not found");
            }

            return cm;
        }

        /// <summary>
        /// Get changemanager by a given email
        /// </summary>
        /// <param name="email">the email of the changemanager</param>
        /// <returns>changemanager obj</returns>
        [HttpGet("[action]/{email}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "AdminAccess")]
        public ActionResult<ChangeManager> GetChangeManagerByEmail(string email) {
            ChangeManager e = _changeManagerRepo.GetByEmail(email);

            if (e == null) {
                return NotFound("Employee not found");
            }

            return e;
        }

        /// <summary>
        /// Upgrade an employee to a changeManager
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPost("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "ChangeManagerAccess")]
        public async Task<ActionResult> UpgradeEmployeeToChangeManager(int employeeId) {
            try {

                Employee empl = _employeeRepo.GetBy(employeeId);

                if (empl == null) {
                    return NotFound("Employee not found");
                }

                Organization o = _organizationRepo.GetAll().Where(o => o.Employees.Any(e => e.Id == empl.Id)).FirstOrDefault();

                if (o == null) {
                    return NotFound("There is no organization with this employee");
                }

                ChangeManager newCm = new ChangeManager(empl);

                o.Employees.Remove(empl);
                _organizationRepo.SaveChanges();
                _employeeRepo.Delete(empl);
                _changeManagerRepo.Update(newCm);
                o.ChangeManagers.Add(newCm);
                _organizationRepo.SaveChanges();

                // update claims
                var user = await _userManager.FindByNameAsync(newCm.Email);
                await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, "employee"));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "changeManager"));

                return NoContent();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }


        }

    }
}
