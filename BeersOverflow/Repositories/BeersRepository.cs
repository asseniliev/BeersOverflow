using System;
using System.Collections.Generic;
using System.Linq;

using AspNetCoreDemo.Data;
using AspNetCoreDemo.Exceptions;
using AspNetCoreDemo.Models;

using Microsoft.EntityFrameworkCore;

namespace AspNetCoreDemo.Repositories
{
	public class BeersRepository : IBeersRepository
	{
		private readonly ApplicationContext context;

		public BeersRepository(ApplicationContext context)
		{
			this.context = context;
		}

		public List<Beer> GetAll()
		{
			return this.GetBeers().ToList();
		}

		public Beer GetById(int id)
		{
			Beer beer = this.GetBeers().Where(b => b.Id == id).FirstOrDefault();

			return beer ?? throw new EntityNotFoundException();
		}

		public Beer GetByName(string name)
		{
			Beer beer = this.GetBeers().Where(b => b.Name == name).FirstOrDefault();

			return beer ?? throw new EntityNotFoundException();
		}

		public List<Beer> FilterBy(BeerQueryParameters filterParameters)
		{
			IQueryable<Beer> result = this.GetBeers();
			result = FilterByName(result, filterParameters.Name);
			result = FilterByStyle(result, filterParameters.Style);
			result = FilterByMinAbv(result, filterParameters.MinAbv);
			result = FilterByMaxAbv(result, filterParameters.MaxAbv);
			result = SortBy(result, filterParameters.SortBy);
			result = Order(result, filterParameters.SortOrder);

			return result.ToList();
		}

		public Beer Create(Beer beer)
		{
			this.context.Beers.Add(beer);
			this.context.SaveChanges();

			return beer;
		}

		public Beer Update(int id, Beer beer)
		{
			Beer beerToUpdate = this.GetById(id);
			beerToUpdate.Abv = beer.Abv;
			beerToUpdate.StyleId = beer.StyleId;
			this.context.Update(beerToUpdate);
			this.context.SaveChanges();

			return beerToUpdate;
		}

		public void Delete(int id)
		{
			Beer existingBeer = this.GetById(id);
			this.context.Beers.Remove(existingBeer);
			this.context.SaveChanges();
		}

		private static IQueryable<Beer> FilterByStyle(IQueryable<Beer> beers, string style)
		{
			if (!string.IsNullOrEmpty(style))
			{
				return beers.Where(beer => beer.Style.Name.Contains(style));
			}
			else
			{
				return beers;
			}
		}

		private static IQueryable<Beer> FilterByName(IQueryable<Beer> beers, string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return beers.Where(beer => beer.Name.Contains(name));
			}
			else
			{
				return beers;
			}
		}

		private static IQueryable<Beer> FilterByMinAbv(IQueryable<Beer> beers, double? minAbv)
		{
			if (minAbv.HasValue)
			{
				return beers.Where(beer => beer.Abv >= minAbv);
			}
			else
			{
				return beers;
			}
		}

		private static IQueryable<Beer> FilterByMaxAbv(IQueryable<Beer> beers, double? maxAbv)
		{
			if (maxAbv.HasValue)
			{
				return beers.Where(beer => beer.Abv <= maxAbv);
			}
			else
			{
				return beers;
			}
		}

		private static IQueryable<Beer> SortBy(IQueryable<Beer> beers, string sortCriteria)
		{
			switch (sortCriteria)
			{
				case "name":
					return beers.OrderBy(beer => beer.Name);
				case "abv":
					return beers.OrderBy(beer => beer.Abv);
				case "style":
					return beers.OrderBy(beer => beer.Style.Name);
				default:
					return beers;
			}
		}

		private static IQueryable<Beer> Order(IQueryable<Beer> beers, string sortOrder)
		{
			switch (sortOrder)
			{
				case "desc":
					return beers.Reverse();
				default:
					return beers;
			}
		}

		private IQueryable<Beer> GetBeers()
		{
			return this.context.Beers
					.Include(beer => beer.Style)
					.Include(beer => beer.CreatedBy)
					.Include(beer => beer.Ratings)
						.ThenInclude(rating => rating.User);
		}
	}
}
