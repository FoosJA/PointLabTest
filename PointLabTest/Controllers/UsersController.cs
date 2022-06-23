using Microsoft.AspNetCore.Mvc;
using PointLabTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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


		

		[HttpPost("register")]
		public IActionResult Post(string username, string password, string email)
		{
			UserData user = new UserData() { UserName = username, Email = email, Password = GetHash(password) };
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
				return new JsonResult(new { Success = false, Message = info });
			}

			if (_ctx.Users.Any(x => x.Email == user.Email))
			{
				Response.StatusCode = 400;
				return new JsonResult(new { Success = false, Message = "Пользователь с таким email уже существует!" });
			}

			_ctx.Users.Add(user);
			_ctx.SaveChanges();
			return new JsonResult(new { Success = true, Message = "Пользователь зарегистрирован." });
		}


		[HttpPost("login")]
		public IActionResult Post(string username, string password)
		{
			if (username is null || password is null)
			{
				Response.StatusCode = 400;
				return new JsonResult(new { userdata = (string)null, Success = false, Message = "Некорректный запрос!" });
			}
			try
			{
				var hashPassword = GetHash(password);
				UserData user = _ctx.Users.Single(x => x.UserName == username && x.Password == hashPassword);
				return new JsonResult(new { userdata = user, Success = true, Message = "Вход выолнен успешно." });
			}
			catch
			{
				Response.StatusCode = 401;
				return new JsonResult(new { userdata = (string)null, Success = false, Message = "Пользователь не найден." });
			}
		}
		/// <summary>
		/// Метод хеширования
		/// </summary>
		/// <param name="password">пароль</param>
		/// <returns></returns>
		public string GetHash(string password)
		{
			var md5 = MD5.Create();
			var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

			return Convert.ToBase64String(hash);
		}

	}
}
