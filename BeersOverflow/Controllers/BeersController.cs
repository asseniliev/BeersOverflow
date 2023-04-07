using AspNetCoreDemo.Models;
using AspNetCoreDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Beer beer = this.beersService.GetById(id);
            return View(beer);
        }
    }
}
