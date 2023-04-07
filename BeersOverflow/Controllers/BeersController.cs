using AspNetCoreDemo.Models;
using AspNetCoreDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDemo.Exceptions;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreDemo.Controllers
{
    public class BeersController : Controller
    {
        private readonly IBeersService beersService;

        public BeersController(IBeersService beersService)
        {
            this.beersService = beersService;
        }

        public IActionResult Index()
        {
            List<Beer> beers = this.beersService.GetAll();
            return View(beers);
        }

        public IActionResult Details(int id)
        {
            try
            {
                Beer beer = this.beersService.GetById(id);
                return View(beer);
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = $"Beer with id {id} does not exist.";

                return this.View("Error");
            }

        }
    }
}
