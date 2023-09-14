using BusinessObjects;
using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            var list = new List<Product>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    list = context.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static Product FindProductById(int proId)
        {
           Product p = new Product();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    p = context.Products.SingleOrDefault(x => x.ProductId == proId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public static void SaveProduct (Product p)
        {    
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Products.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateProduct(Product p)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteProduct(Product p)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var productRemove = context.Products.SingleOrDefault(prm => prm.ProductId == p.ProductId);
                    context.Remove(productRemove);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
