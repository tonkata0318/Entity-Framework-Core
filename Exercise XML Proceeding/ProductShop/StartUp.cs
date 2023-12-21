using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            //1.
            ProductShopContext context= new ProductShopContext();
            //string inputXml = File.ReadAllText("../../../Datasets/users.xml");
            //Console.WriteLine(ImportUsers(context,inputXml));
            //2.
            //string inputXml = File.ReadAllText("../../../Datasets/products.xml");
            //Console.WriteLine(ImportProducts(context, inputXml));
            //3.
            //string inputXml = File.ReadAllText("../../../Datasets/categories.xml");
            //Console.WriteLine(ImportCategories(context, inputXml));
            //4.
            //string inputXml = File.ReadAllText("../../../Datasets/categories-products.xml");
            //Console.WriteLine(ImportCategoryProducts(context, inputXml));
            //5.
            //Console.WriteLine(GetProductsInRange(context));
            //6.
            //Console.WriteLine(GetSoldProducts(context));
            //7.
            //Console.WriteLine(GetCategoriesByProductsCount(context));
            //8.-not finished
            //Console.WriteLine(GetUsersWithProducts(context));
        }
        private static Mapper GetMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<ProductShopProfile>());
            return new Mapper(cfg);
        }
        //public static string ImportUsers(ProductShopContext context, string inputXml)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportUsersDTO[]), new XmlRootAttribute("Users"));
        //    using var reader = new StringReader(inputXml);
        //    ImportUsersDTO[] importUsersDTOs = (ImportUsersDTO[])xmlSerializer.Deserialize(reader);
        //    var mapper=GetMapper();
        //    User[] users = mapper.Map<User[]>(importUsersDTOs);
        //    context.AddRange(users);
        //    context.SaveChanges();
        //    return $"Successfully imported {users.Length}";
        //}
        //public static string ImportProducts(ProductShopContext context, string inputXml)
        //{
        //    XmlSerializer xmlSerializer=new XmlSerializer(typeof(ImportProductsDTO[]),new XmlRootAttribute("Products"));
        //    using var reader = new StringReader(inputXml);
        //    ImportProductsDTO[] importProductsDTOs = (ImportProductsDTO[])xmlSerializer.Deserialize(reader);
        //    var mapper = GetMapper();

        //    //var buyerIds=context.Users
        //    //    .Select(u => u.Id)
        //    //    .ToArray();
        //    Product[] products = mapper.Map<Product[]>(importProductsDTOs
        //      /*  .Where(p=>buyerIds.Contains(p.BuyerId))*/);

        //    context.AddRange(products);
        //    context.SaveChanges();
        //    return $"Successfully imported {products.Length}";
        //}
        //public static string ImportCategories(ProductShopContext context, string inputXml)
        //{
        //    XmlSerializer xmlSerializer=new XmlSerializer(typeof(ImportCategoriesDTO[]),new XmlRootAttribute("Categories"));
        //    using var reader=new StringReader(inputXml);
        //    ImportCategoriesDTO[] importCategoriesDTOs = (ImportCategoriesDTO[])xmlSerializer.Deserialize(reader);
        //    var mapper=GetMapper();
        //    Category[] categories = mapper.Map<Category[]>(importCategoriesDTOs.Where(c => c.Name != null)).ToArray();
        //    context.AddRange(categories);
        //    context.SaveChanges();
        //     return $"Successfully imported {categories.Length}";
        //}
        //public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCategoryProductsDTO[]), new XmlRootAttribute("CategoryProducts"));
        //    using var reader=new StringReader(inputXml);
        //    ImportCategoryProductsDTO[] importCategoryProductsDTOs = (ImportCategoryProductsDTO[])xmlSerializer.Deserialize(reader);
        //    var mapper=GetMapper();
        //    var productIds = context.Products
        //        .Select(p => p.Id)
        //        .ToArray();
        //    var categoriesIds=context.Categories
        //        .Select(c => c.Id)
        //        .ToArray();

        //    CategoryProduct[] categoryProducts = mapper.Map<CategoryProduct[]>(importCategoryProductsDTOs.Where(pc=>categoriesIds.Contains(pc.CategoryID)&& productIds.Contains(pc.ProductId))).ToArray();
        //    context.AddRange(categoryProducts);
        //    context.SaveChanges();
        //    return $"Successfully imported {categoryProducts.Length}";
        //}
        //public static string GetProductsInRange(ProductShopContext context)
        //{
        //    var mapper=GetMapper();
        //    ExportGetProductsInRangeDTO[] productsInRange = context.Products
        //        .Select(p=> new ExportGetProductsInRangeDTO()
        //        {
        //            Name= p.Name,
        //            Price= p.Price,
        //            BuyerFullName=p.Buyer.FirstName+" "+p.Buyer.LastName
        //        })
        //        .Where(p => p.Price >= 500 && p.Price <= 1000)
        //        .OrderBy(p => p.Price)
        //        .Take(10)
        //        .ToArray();

        //    return SerializeToXml<ExportGetProductsInRangeDTO[]>(productsInRange, "Products");
        //}
        //public static string GetSoldProducts(ProductShopContext context)
        //{
        //    var mapper = GetMapper();
        //    ExportGetSoldProductsDTO[] productsSold = context.Users
        //        .Where(u => u.ProductsSold.Any())
        //        .Select(u => new ExportGetSoldProductsDTO()
        //        {
        //            FirstName = u.FirstName,
        //            LastName = u.LastName,
        //            SoldProducts = u.ProductsSold.Select(ps => new ExportProductDTO
        //            {
        //                Name = ps.Name,
        //                Price = ps.Price,
        //            }).ToArray(),
        //        }).OrderBy(u => u.LastName).ThenBy(u => u.FirstName).Take(5).ToArray();

        //    return SerializeToXml<ExportGetSoldProductsDTO[]>(productsSold, "Users");
        //}
        //public static string GetCategoriesByProductsCount(ProductShopContext context)
        //{
        //    var mapper=GetMapper();
        //    ExportGetCategoriesByProductsCountDTO[] categories=context.Categories
        //        .Select(c=> new ExportGetCategoriesByProductsCountDTO()
        //        {
        //            Name= c.Name,
        //            Count=c.CategoryProducts.Count,
        //            AveragePrice=c.CategoryProducts.Average(cp=>cp.Product.Price),
        //            TotalRevenue=c.CategoryProducts.Sum(cp => cp.Product.Price)
        //        }).OrderByDescending(c=>c.Count).ThenBy(c=>c.TotalRevenue).ToArray();

        //    return SerializeToXml<ExportGetCategoriesByProductsCountDTO[]>(categories, "Categories");
        //}
        //public static string GetUsersWithProducts(ProductShopContext context)
        //{
        //    var mapper=GetMapper();
        //    ExportGetUsersWithProductsDTO userswithProducts = context.Users
        //        .Select(u => new ExportGetUsersWithProductsDTO()
        //        { 
        //            Count=context.Users.Count(),
        //            exportUser=new ExportUserDTO
        //            {
        //                FirstName=u.FirstName,
        //                LastName=u.LastName,
        //                Age=u.Age,
        //                SoldProducts=new Ex
        //            }
        //        });

        //}
        private static string SerializeToXml<T>(T dto, string xmlRootAttribute)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttribute));

            StringBuilder stringBuilder = new StringBuilder();

            using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
            {
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add(string.Empty, string.Empty);

                try
                {
                    xmlSerializer.Serialize(stringWriter, dto, xmlSerializerNamespaces);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return stringBuilder.ToString();
        }
    }
}