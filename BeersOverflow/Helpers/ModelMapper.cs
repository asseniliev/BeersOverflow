using AspNetCoreDemo.Models;

namespace AspNetCoreDemo.Helpers
{
	public class ModelMapper
	{
		public Beer Map(BeerDto dto)
		{
			return new Beer
			{
				Name = dto.Name,
				Abv = dto.Abv,
				StyleId = dto.StyleId
			};
		}
	}
}
