using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreDemo.Models
{
	public class BeerDto
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
		[MaxLength(25, ErrorMessage = "The {0} field must be less than {1} characters.")]
		[MinLength(1, ErrorMessage = "The {0} field must be at least {1} character.")]
		public string Name { get; set; }

		[Range(0.1, 35.0, ErrorMessage = "The {0} field must be between {1}% and {2}%.")]
		public double Abv { get; set; }

		[Required(ErrorMessage = "The {0} field is required")]
		[Range(1, int.MaxValue, ErrorMessage = "The {0} field must be in the range from {1} to {2}.")]
		public int StyleId { get; set; }
	}
}
