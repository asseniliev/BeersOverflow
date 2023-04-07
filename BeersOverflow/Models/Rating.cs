using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreDemo.Models
{
	public class Rating
	{
		public int Id { get; set; }

		public int? BeerId { get; set; }
		public Beer Beer { get; set; }

		public int? UserId { get; set; }
		public User User { get; set; }

		[Range(1, 5)]
		public int Value { get; set; }
	}
}
