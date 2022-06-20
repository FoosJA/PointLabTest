using Microsoft.EntityFrameworkCore;
using PointLabTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointLabTest
{
	public class AppDataBaseContext: DbContext
	{
		public AppDataBaseContext(DbContextOptions<AppDataBaseContext> options)
		  : base(options)
		{

		}
		public DbSet<User> Users { get; set; }
	}
}
