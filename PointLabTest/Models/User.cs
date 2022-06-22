using System.ComponentModel.DataAnnotations;

namespace PointLabTest.Models
{
	public class User
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 1)]
		public string UserName { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 4)]
		public string Password { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
