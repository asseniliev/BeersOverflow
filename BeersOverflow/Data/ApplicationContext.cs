using System.Collections.Generic;

using AspNetCoreDemo.Models;

using Microsoft.EntityFrameworkCore;

namespace AspNetCoreDemo.Data
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
		}

		public DbSet<Beer> Beers { get; set; }
		public DbSet<Style> Styles { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			List<User> users = new List<User>()
			{
				new User { Id = 1, Username = "admin", IsAdmin = true },
				new User { Id = 2, Username = "gosho", IsAdmin = false },
				new User { Id = 3, Username = "pesho", IsAdmin = false }
			};
			modelBuilder.Entity<User>().HasData(users);

			List<Style> styles = new List<Style>()
			{
				new Style { Id = 1, Name = "Special Ale" },
				new Style { Id = 2, Name = "English Porter" },
				new Style { Id = 3, Name = "Indian Pale Ale" }
			};
			modelBuilder.Entity<Style>().HasData(styles);

			List<Beer> beers = new List<Beer>()
			{
				new Beer { Id = 1, Name = "Glarus English Ale", Abv = 4.6, CreatedById = 2 /*gosho*/, StyleId = 1 /*Special Ale*/ },
				new Beer { Id = 2, Name = "Rhombus Porter", Abv = 5.0, CreatedById = 2 /*gosho*/, StyleId = 2 /*English Porter*/ },
				new Beer { Id = 3, Name = "Opasen Char", Abv = 6.6, CreatedById = 3 /*pesho*/, StyleId = 3 /*Indian Pale Ale*/ }
			};
			modelBuilder.Entity<Beer>().HasData(beers);

			// Seed the database with user-beer ratings
			List<Rating> ratings = new List<Rating>()
			{
				new Rating() { Id = 1, BeerId = 1, UserId = 3, Value = 5 },
				new Rating() { Id = 2, BeerId = 1, UserId = 2, Value = 2 },
				new Rating() { Id = 3, BeerId = 2, UserId = 3, Value = 1 },
				new Rating() { Id = 4, BeerId = 2, UserId = 2, Value = 3 },
				new Rating() { Id = 5, BeerId = 3, UserId = 3, Value = 5 },
				new Rating() { Id = 6, BeerId = 3, UserId = 2, Value = 5 }
			};
			modelBuilder.Entity<Rating>().HasData(ratings);
		}
	}
}
