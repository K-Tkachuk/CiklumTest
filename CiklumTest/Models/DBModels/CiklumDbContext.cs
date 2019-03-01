using System;
using Microsoft.EntityFrameworkCore;

namespace CiklumTest.Models.DBModels
{

	public class CiklumDbContext : DbContext
	{
		public CiklumDbContext(DbContextOptions<CiklumDbContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<ToDo> ToDos { get; set; }
	}
}
