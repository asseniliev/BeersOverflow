using System.Collections.Generic;
using System.Linq;

using AspNetCoreDemo.Data;
using AspNetCoreDemo.Exceptions;
using AspNetCoreDemo.Models;

using Microsoft.EntityFrameworkCore;

namespace AspNetCoreDemo.Repositories
{
	public class StylesRepository : IStylesRepository
	{
		private readonly ApplicationContext context;

		public StylesRepository(ApplicationContext context)
		{
			this.context = context;
		}

		public List<Style> GetAll()
		{
			return this.GetStyles().ToList();
		}

		public Style GetById(int id)
		{
			Style style = this.GetStyles().Where(style => style.Id == id).FirstOrDefault();

			return style ?? throw new EntityNotFoundException();
		}

		private IQueryable<Style> GetStyles()
		{
			return this.context.Styles
						.Include(style => style.Beers)
							.ThenInclude(beer => beer.Ratings)
								.ThenInclude(rating => rating.User);
		}
	}
}
