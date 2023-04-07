using AspNetCoreDemo.Models;

using System.Collections.Generic;

namespace AspNetCoreDemo.Repositories
{
	public interface IBeersRepository
	{
		List<Beer> GetAll();
		Beer GetById(int id);
		Beer GetByName(string name);
		List<Beer> FilterBy(BeerQueryParameters filterParameters);
		Beer Create(Beer beer);
		Beer Update(int id, Beer beer);
		void Delete(int id);
	}
}
