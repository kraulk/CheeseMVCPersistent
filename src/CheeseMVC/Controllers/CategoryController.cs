using System;
using System.Linq;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CheeseMVC.Controllers {
    public class CategoryController : Controller {
        
        // first we add the context as a field
        // we want to interact WITH the context not change it directly so we use read-only
        
        // this field will be given a value during the dependency injection (Controller constructor call)
        private readonly CheeseDbContext context;
        
         /*
          the controller constructor will instantiate the controller with the context made available
          
          remember when a request is routed to this controller it will be instantiated
          a new instance of the Category[any]Controller class is made
         
         by passing the context as an argument into this constructor it means that the Controller instance will be able to use the context for any methods within it.
         
         the route handlers in this controller that "control" the "model" providing CRUD functionality (Create Read Update Delete - interactions with the database)
         
         just like passing an argument into a function makes that that variable available for use within the function.
         
         remember constructors are just a special type of class method. meaning its just a special function.
         
         this is high level overview of the dependendancy injection mechanism. if what i said above doesnt make sense yet let it sink in before looking at the following link for deeper detail
         https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/dependency-injection
         
          */
        public CategoryController(CheeseDbContext dbContext) {
            context = dbContext;
        }
        
        [HttpGet]
        public IActionResult Index() => View(context.Categories.ToList());

        // get
        [HttpGet]
        public IActionResult Add() => View(new AddCheeseCategoryViewModel());
        
        // post
        [HttpPost]
        public IActionResult Add(AddCheeseCategoryViewModel newCategoryContainer) {
            if (!ModelState.IsValid) return View("Add");
            
            context.Categories.Add(new CheeseCategory {
                Name = newCategoryContainer.Name
            });
            context.SaveChanges();

            return Redirect("/Category");
        }

    }
}