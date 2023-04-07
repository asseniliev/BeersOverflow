namespace AspNetCoreDemo.Models
{
	public class BeerQueryParameters
	{
		public string Name { get; set; }
		public string Style { get; set; }
		public double? MinAbv { get; set; }
		public double? MaxAbv { get; set; }
		public string SortBy { get; set; }
		public string SortOrder { get; set; }
	}
}
