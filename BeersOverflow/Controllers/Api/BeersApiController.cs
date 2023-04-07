using System.Collections.Generic;

using AspNetCoreDemo.Exceptions;
using AspNetCoreDemo.Helpers;
using AspNetCoreDemo.Models;
using AspNetCoreDemo.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemo.Controllers.Api
{
	[ApiController]
	[Route("api/beers")]
	public class BeersApiController : ControllerBase
	{
		private readonly IBeersService beersService;
		private readonly ModelMapper modelMapper;
		private readonly AuthManager authManager;

		public BeersApiController(IBeersService beersService, ModelMapper modelMapper, AuthManager authManager)
		{
			this.beersService = beersService;
			this.modelMapper = modelMapper;
			this.authManager = authManager;
		}

		[HttpGet("")]
		public IActionResult GetBeers([FromQuery] BeerQueryParameters filterParameters)
		{
			List<Beer> result = this.beersService.FilterBy(filterParameters);

			return this.StatusCode(StatusCodes.Status200OK, result);
		}

		[HttpGet("{id}")]
		public IActionResult GetBeerById(int id)
		{
			try
			{
				Beer beer = this.beersService.GetById(id);

				return this.StatusCode(StatusCodes.Status200OK, beer);
			}
			catch (EntityNotFoundException e)
			{
				return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
			}
		}

		[HttpPost("")]
		public IActionResult CreateBeer([FromHeader] string username, [FromBody] BeerDto dto)
		{
			try
			{
				User user = this.authManager.TryGetUser(username);
				Beer beer = this.modelMapper.Map(dto);
				Beer createdBeer = this.beersService.Create(beer, user);

				return this.StatusCode(StatusCodes.Status201Created, createdBeer);
			}
			catch (UnauthorizedOperationException e)
			{
				return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
			}
			catch (DuplicateEntityException e)
			{
				return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
			}
		}

		[HttpPut("{id}")]
		public IActionResult UpdateBeer(int id, [FromHeader] string username, [FromBody] BeerDto dto)
		{
			try
			{
				User user = this.authManager.TryGetUser(username);
				Beer beer = this.modelMapper.Map(dto);

				Beer updatedBeer = this.beersService.Update(id, beer, user);

				return this.StatusCode(StatusCodes.Status200OK, updatedBeer);
			}
			catch (UnauthorizedOperationException e)
			{
				return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
			}
			catch (EntityNotFoundException e)
			{
				return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
			}
			catch (DuplicateEntityException e)
			{
				return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteBeer(int id, [FromHeader] string username)
		{
			try
			{
				User user = this.authManager.TryGetUser(username);
				this.beersService.Delete(id, user);

				return this.StatusCode(StatusCodes.Status200OK);
			}
			catch (UnauthorizedOperationException e)
			{
				return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
			}
			catch (EntityNotFoundException e)
			{
				return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
			}
		}
	}
}
