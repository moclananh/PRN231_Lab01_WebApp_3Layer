using BusinessObjects;
using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class CategoryDAO
    {
       
        public static List<Category> GetCategories()
        {
            var listCat =  new List<Category>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listCat = context.Categories.ToList();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCat;
        }
    }
}
