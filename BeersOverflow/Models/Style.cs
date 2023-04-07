using System.Collections.Generic;

namespace AspNetCoreDemo.Models
{
	public class Style
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Beer> Beers { get; set; }
	}
}
