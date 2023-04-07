using AspNetCoreDemo.Exceptions;
using AspNetCoreDemo.Models;
using AspNetCoreDemo.Services;

namespace AspNetCoreDemo.Helpers
{
	public class AuthManager
	{
		private readonly IUsersService usersService;

		public AuthManager(IUsersService usersService)
		{
			this.usersService = usersService;
		}

		public User TryGetUser(string username)
		{
			try
			{
				return this.usersService.GetByUsername(username);
			}
			catch (EntityNotFoundException)
			{
				throw new UnauthorizedOperationException("Not Found");
			}
		}
	}
}
