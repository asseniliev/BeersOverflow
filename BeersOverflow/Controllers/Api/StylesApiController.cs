using System.Collections.Generic;

using AspNetCoreDemo.Exceptions;
using AspNetCoreDemo.Models;
using AspNetCoreDemo.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemo.Controllers.Api
{
	[ApiController]
	[Route("api/styles")]
	public class StylesApiController : ControllerBase
	{
		private readonly IStylesService stylesService;

		public StylesApiController(IStylesService stylesService)
		{
			this.stylesService = stylesService;
		}

		[HttpGet("")]
		public IActionResult Get()
		{
			List<Style> styles = this.stylesService.GetAll();
			
			return this.StatusCode(StatusCodes.Status200OK, styles);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			try
			{
				Style style = this.stylesService.GetById(id);
				
				return this.StatusCode(StatusCodes.Status200OK, style);
			}
			catch (EntityNotFoundException e)
			{
				return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
			}
		}
	}
}