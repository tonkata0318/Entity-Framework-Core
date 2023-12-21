using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();
            //1.
            //string userJson = File.ReadAllText("../../../Datasets/users.json");
            //2. 
            //string userJson = File.ReadAllText("../../../Datasets/products.json");
            //3.
            //string userJson = File.ReadAllText("../../../Datasets/categories.json");
            //4.
            //string userJson = File.ReadAllText("../../../Datasets/categories-products.json");
            //5.
            //Console.WriteLine(GetProductsInRange(context));
            //6.
            //Console.WriteLine(GetSoldProducts(context));
            //7.
            //Console.WriteLine(GetCategoriesByProductsCount(context));
            //8.
            Console.WriteLine(GetUsersWithProducts(context));
        }
        //public static string ImportUsers(ProductShopContext context, string inputJson)
        //{
        //    var users = JsonConvert.DeserializeObject<User[]>(inputJson);

        //    context.Users.AddRange(users);
        //    context.SaveChanges();
        //    return $"Successfully imported {users.Length}";
        //}
        //public static string ImportProducts(ProductShopContext context, string inputJson)
        //{
        //    var products = JsonConvert.DeserializeObject<Product[]>(inputJson);
        //    context.Products.AddRange(products);
        //    context.SaveChanges();
        //    return $"Successfully imported {products.Length}";
        //}
        //public static string ImportCategories(ProductShopContext context, string inputJson)
        //{
        //    var categories = JsonConvert.DeserializeObject<Category[]>(inputJson);
        //    var vaidCategories = categories?.Where(c => c.Name is not null).ToArray();

        //    if (vaidCategories != null)
        //    {
        //        context.Categories.AddRange(vaidCategories);
        //        context.SaveChanges();
        //        return $"Successfully imported {vaidCategories.Length}";
        //    }
        //    return $"Successfully imported 0";
        //}
        //public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        //{
        //    var categorieproducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);
        //    context.CategoriesProducts.AddRange(categorieproducts);
        //    context.SaveChanges();
        //    return $"Successfully imported {categorieproducts.Length}";
        //}
        //public static string GetProductsInRange(ProductShopContext context)
        //{
        //    var data = context.Products
        //        .Where(p => p.Price >= 500 && p.Price <= 1000)
        //        .Select(p => new
        //        {
        //            name=p.Name,
        //            price=p.Price,
        //            seller = $"{p.Seller.FirstName} {p.Seller.LastName}",
        //        })
        //        .OrderBy(p=>p.price)
        //        .ToArray();

        //    var json=JsonConvert.SerializeObject(data,Formatting.Indented);
        //    return json;
        //}
        //public static string GetSoldProducts(ProductShopContext context)
        //{
        //    var data = context.Users
        //        .Where(u => u.ProductsSold.Count >= 1&&u.ProductsSold.Any(pc=>pc.Buyer.Id!=null))
        //        .Select(u => new
        //        {
        //            firstName = u.FirstName,
        //            lastName = u.LastName,
        //            soldProducts = u.ProductsSold.Select(sp => new
        //            {
        //                name=sp.Name,
        //                price=sp.Price,
        //                buyerFirstName=sp.Buyer.FirstName,
        //                buyerLastName=sp.Buyer.LastName
        //            })
        //        })
        //        .OrderBy(u=>u.lastName).ThenBy(u=>u.firstName)
        //        .ToArray();

        //    var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        //    return json;
        //}
        //public static string GetCategoriesByProductsCount(ProductShopContext context)
        //{
        //    var data = context.Categories
        //        .Select(c => new
        //        {
        //            category = c.Name,
        //            productsCount = c.CategoriesProducts.Count(),
        //            averagePrice=$"{c.CategoriesProducts.Average(c=>c.Product.Price):f2}",
        //            totalRevenue=$"{c.CategoriesProducts.Sum(cp=>cp.Product.Price):f2}"
        //        }).OrderByDescending(c=>c.productsCount);
        //    var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        //    return json;
        //}
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersWithProduct = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = u.ProductsSold.
                        Where(p => p.BuyerId != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price
                        }).ToArray()
                }).OrderByDescending(u=>u.soldProducts.Count())
                .ToArray();
            return "";
        }
    }
}