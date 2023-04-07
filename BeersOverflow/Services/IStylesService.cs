using System.Collections.Generic;

using AspNetCoreDemo.Models;

namespace AspNetCoreDemo.Services
{
	public interface IStylesService
	{
		List<Style> GetAll();
		Style GetById(int id);
	}
}
