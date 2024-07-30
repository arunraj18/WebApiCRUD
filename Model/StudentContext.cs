using System;
using Microsoft.EntityFrameworkCore;
namespace CRUDopr.Model
{
	public class StudentContext : DbContext
	{
		public StudentContext(DbContextOptions<StudentContext> options) : base(options)
		{

		}

		public DbSet<Student> Students { get; set; }
	}
}

