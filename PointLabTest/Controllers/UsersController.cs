using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointLabTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace PointLabTest.Controllers
{
	[Route("api/user")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private AppDataBaseContext _ctx;
		public UsersController(AppDataBaseContext ctx)
		{ _ctx = ctx; }


		[HttpGet("test")]
		public IEnumerable<string> Get()
		{
			return new string[] { "val1", "val2" };
		}

		[HttpPost("register")]
		public IActionResult Post(string username, string password, string email)
		{
			User user = new User() { UserName = username, Email = email, Password = GetHash(password) };
			var validCtx = new ValidationContext(user);
			var validRst = new List<ValidationResult>();
			if (!Validator.TryValidateObject(user, validCtx, validRst, true))
			{
				string info = "";
				foreach (var error in validRst)
				{
					info += error.ErrorMessage;
				}
				Response.StatusCode = 400;
				return new JsonResult(new MyResponse() { Success = false, Message = info });
			}

			if (_ctx.Users.Any(x => x.Email == user.Email))
			{
				Response.StatusCode = 400;
				return new JsonResult(new MyResponse() { Success = false, Message = "Пользователь с таким email уже существует" });
			}

			_ctx.Users.Add(user);
			_ctx.SaveChanges();
			return new JsonResult(new MyResponse() { Success = true, Message = "Успешный успех" });
		}
		[HttpPost("login")]
		public IActionResult Post(string username, string password)
		{
			if (username is null || password is null)
			{
				Response.StatusCode = 400;
				return new JsonResult(new MyResponse() { Success = false, Message = "Некорректный запрос" });
			}
			try//TODO: Ответ должен содержать класс userdata с полями пользователя
			{
				var hashPassword = GetHash(password);
				User user = _ctx.Users.Single(x => x.UserName == username && x.Password == hashPassword);
				return new JsonResult(new MyResponse() { Success = true, Message = "Успешный успех" });
			}
			catch
			{
				Response.StatusCode = 401;
				return new JsonResult(new MyResponse() { Success = false, Message = "Пользователь не найден" });
			}
		}

		public string GetHash(string input)
		{
			var md5 = MD5.Create();
			var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

			return Convert.ToBase64String(hash);
		}

	}
}
