using AspNetCoreDemo.Models;

using System.Collections.Generic;

namespace AspNetCoreDemo.Services
{
	public interface IBeersService
	{
		List<Beer> GetAll();
		List<Beer> FilterBy(BeerQueryParameters filterParameters);
		Beer GetById(int id);
		Beer Create(Beer beer, User user);
		Beer Update(int id, Beer beer, User user);
		void Delete(int id, User user);
	}
}
