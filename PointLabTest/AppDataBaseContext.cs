using Microsoft.EntityFrameworkCore;
using PointLabTest.Models;

namespace PointLabTest
{
	public class AppDataBaseContext : DbContext
	{
		public AppDataBaseContext(DbContextOptions<AppDataBaseContext> options)
		  : base(options)
		{

		}
		public DbSet<UserData> Users { get; set; }
	}
}
