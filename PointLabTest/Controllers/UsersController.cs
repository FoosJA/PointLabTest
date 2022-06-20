using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointLabTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointLabTest.Controllers
{
	[Route("api/user")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private AppDataBaseContext _ctx;
		public void ValuesController(AppDataBaseContext ctx) => _ctx = ctx;

		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "val1", "val2" };
		}

		[HttpPost]
		public string Post([FromBody] string value)
		{
			return value + "test";
			/*User user = new User();
			user.UserName = value;
			user.Email = 20;


			_ctx.Users.Add(user); //Insert
			_ctx.SaveChanges();   //commint*/
		}
	}
}
