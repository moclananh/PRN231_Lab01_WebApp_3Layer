using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using BusinessObjects.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;

namespace WebClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public ProductsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:44300/api/products";
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(stringData, options);
            return View(listProducts);
        }



       // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Make a request to your API to get the product by id
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:44300/api/Products/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Product product = JsonSerializer.Deserialize<Product>(productData, options);

                    // Make a request to your API to get the list of categories
                    HttpResponseMessage categoriesResponse = await client.GetAsync("https://localhost:44300/api/Category");

                    if (categoriesResponse.IsSuccessStatusCode)
                    {
                        string categoriesData = await categoriesResponse.Content.ReadAsStringAsync();
                        List<Category> categories = JsonSerializer.Deserialize<List<Category>>(categoriesData, options);

                        // Pass the list of categories and the product to the view
                        ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
                        return View(product);
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or errors, e.g., log the error
            }

            return NotFound();
        }
       
        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            // Make a request to your API to get the list of categories
            HttpResponseMessage categoriesResponse = await client.GetAsync("https://localhost:44300/api/Category");

            if (categoriesResponse.IsSuccessStatusCode)
            {
                string categoriesData = await categoriesResponse.Content.ReadAsStringAsync();
                List<Category> categories = JsonSerializer.Deserialize<List<Category>>(categoriesData, options);

                // Pass the list of categories and the product to the view
                ViewData["Category"] = new SelectList(categories, "CategoryId", "CategoryName");
              
            }

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId,UnitsInStock,UnitPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ProductApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Handle successful creation (e.g., redirect to the product list)
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle errors
                    ModelState.AddModelError(string.Empty, "Error creating the product.");
                }
            }
            return View(product);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Make a request to your API to get the product by id
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:44300/api/Products/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Product product = JsonSerializer.Deserialize<Product>(productData, options);

                    // Make a request to your API to get the list of categories
                    HttpResponseMessage categoriesResponse = await client.GetAsync("https://localhost:44300/api/Category");

                    if (categoriesResponse.IsSuccessStatusCode)
                    {
                        string categoriesData = await categoriesResponse.Content.ReadAsStringAsync();
                        List<Category> categories = JsonSerializer.Deserialize<List<Category>>(categoriesData, options);

                        // Pass the list of categories and the product to the view
                        ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
                        return View(product);
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or errors, e.g., log the error
            }

            return NotFound();
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,CategoryId,UnitsInStock,UnitPrice")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{ProductApiUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Handle successful update (e.g., redirect to the product list)
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle errors
                    ModelState.AddModelError(string.Empty, "Error updating the product.");
                }
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Make a request to your API to get the product by id
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:44300/api/Products/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Product product = JsonSerializer.Deserialize<Product>(productData, options);

                    // Make a request to your API to get the list of categories
                    HttpResponseMessage categoriesResponse = await client.GetAsync("https://localhost:44300/api/Category");

                    if (categoriesResponse.IsSuccessStatusCode)
                    {
                        string categoriesData = await categoriesResponse.Content.ReadAsStringAsync();
                        List<Category> categories = JsonSerializer.Deserialize<List<Category>>(categoriesData, options);

                        // Pass the list of categories and the product to the view
                        ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
                        return View(product);
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or errors, e.g., log the error
            }

            return NotFound();
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{ProductApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Handle successful deletion (e.g., redirect to the product list)
                return RedirectToAction("Index");
            }
            else
            {
                // Handle errors
                return NotFound();
            }
        }

/*        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }*/
    }
}
