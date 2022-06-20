using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointLabTest.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "val1", "val2" };
		}
	}
}
