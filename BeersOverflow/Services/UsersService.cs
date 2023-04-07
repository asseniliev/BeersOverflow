using System.Collections.Generic;

using AspNetCoreDemo.Models;
using AspNetCoreDemo.Repositories;

namespace AspNetCoreDemo.Services
{
	public class UsersService : IUsersService
	{
		private readonly IUsersRepository repository;

		public UsersService(IUsersRepository repository)
		{
			this.repository = repository;
		}

		public List<User> GetAll()
		{
			return this.repository.GetAll();
		}

		public User GetById(int id)
		{
			return this.repository.GetById(id);
		}

		public User GetByUsername(string username)
		{
			return this.repository.GetByUsername(username);
		}
	}
}
