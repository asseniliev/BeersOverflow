using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreDemo.Models
{
	public class Beer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Abv { get; set; }

		[Display(Name = "Style")]
		// Foreign key
		public int StyleId { get; set; }
		// Navigation property
		public Style Style { get; set; }

		// Foreign key
		public int CreatedById { get; set; }
		// Navigation property
		public User CreatedBy { get; set; }

		// A many-to-many relationship between Beer and User.
		// One beer can be rated by many users and one user can rate many beers.
		public List<Rating> Ratings { get; set; }

		public override bool Equals(object obj)
		{
			Beer other = obj as Beer;
			if(obj != null)
			{
				return this.Id == other.Id
						&& this.Name == other.Name
						&& this.Abv == other.Abv
						&& this.StyleId == other.StyleId
						&& this.CreatedById == other.CreatedById;
			}
			else
			{
				return false;
			}
		}
	}
}
