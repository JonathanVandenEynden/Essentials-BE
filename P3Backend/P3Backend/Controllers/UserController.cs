using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P3Backend.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class UserController : ControllerBase {
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		// GET: api/<UserController>
		[HttpGet]
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public string Get(int id) {
			return "value";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		// POST api/<UserController>
		[HttpPost]
		public void Post([FromBody] string value) {
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value) {
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public void Delete(int id) {
		}
	}
}
