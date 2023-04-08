using AspNetCoreDemo.Models;
using AspNetCoreDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDemo.Exceptions;
using Microsoft.AspNetCore.Http;
using AspNetCoreDemo.Helpers;

namespace AspNetCoreDemo.Controllers
{
    public class BeersController : Controller
    {
        private readonly IBeersService beersService;
        private readonly AuthManager authManager;

        public BeersController(IBeersService beersService, AuthManager authManager)
        {
            this.beersService = beersService;
            this.authManager = authManager;
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

        [HttpGet]
        public IActionResult Create()
        {
            //var beer = new Beer();

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(Beer beer)
        {            
            if (!this.ModelState.IsValid)
            {
                return this.View(beer);
            }

            try
            {
                // Warning: We bypass authentication and authorization just for this demo
                var user = this.authManager.TryGetUser("admin");
                var createdBeer = this.beersService.Create(beer, user);

                return this.RedirectToAction("Details", "Beers", new { id = createdBeer.Id });
            }
            catch (DuplicateEntityException ex)
            {
                this.ModelState.AddModelError("Name", ex.Message);

                return this.View(beer);
            }
        }
    }
}
