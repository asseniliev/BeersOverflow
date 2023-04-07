using System;

namespace AspNetCoreDemo.Exceptions
{
	public class UnauthorizedOperationException : ApplicationException
	{
		public UnauthorizedOperationException(string message)
			: base(message)
		{
		}
	}
}
