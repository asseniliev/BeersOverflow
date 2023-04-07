using System.Collections.Generic;

using AspNetCoreDemo.Models;

namespace AspNetCoreDemo.Repositories
{
	public interface IUsersRepository
	{
		List<User> GetAll();
		User GetById(int id);
		User GetByUsername(string username);
	}
}
