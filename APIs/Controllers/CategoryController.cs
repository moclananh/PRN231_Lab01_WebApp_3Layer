using BusinessObjects.Model;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories.Respositories;
using System.Collections.Generic;
using System;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository repository = new CategoryRepository();

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategory()
        {
            return repository.GetCategories();
        }

        
    }
}
