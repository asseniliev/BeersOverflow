using System.Collections.Generic;

namespace AspNetCoreDemo.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool IsAdmin { get; set; }
		// A many-to-many relationship between Beer and User.
		// One beer can be rated by many users and one user can rate many beers.
		public List<Rating> Ratings { get; set; }
	}
}
