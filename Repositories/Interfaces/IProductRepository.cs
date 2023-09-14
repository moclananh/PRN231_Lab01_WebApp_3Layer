using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        void SaveProduct(Product product);
        Product GetProductById(int id);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        List<Category> GetCategories();
        List<Product> GetProducts();
    }
}
