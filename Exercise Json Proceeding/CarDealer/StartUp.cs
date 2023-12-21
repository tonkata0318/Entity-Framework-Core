using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs;
using CarDealer.Models;
using Castle.Core.Resource;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context=new CarDealerContext();
            //1.
            //string userJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //2.
            //string userJson = File.ReadAllText("../../../Datasets/parts.json");
            //3.
            //string userJson = File.ReadAllText("../../../Datasets/cars.json");
            //Console.WriteLine(ImportCars(context, userJson));
            //4.
            //string userJson = File.ReadAllText("../../../Datasets/customers.json");
            //Console.WriteLine(ImportCustomers(context, userJson));
            //5.
            //string userJson = File.ReadAllText("../../../Datasets/sales.json");
            //Console.WriteLine(ImportSales(context, userJson));
            //6.
            //Console.WriteLine(GetOrderedCustomers(context));
            //7.
            //Console.WriteLine(GetCarsFromMakeToyota(context));
            //8.
            //Console.WriteLine(GetLocalSuppliers(context));
            //9.
            //Console.WriteLine(GetCarsWithTheirListOfParts(context));
            //10.
            //Console.WriteLine(GetCarsWithTheirListOfParts(context));
        }
        //public static string ImportSuppliers(CarDealerContext context, string inputJson)
        //{
        //    var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);
        //    context.Suppliers.AddRange(suppliers);
        //    context.SaveChanges();
        //    return $"Successfully imported {suppliers.Length}.";
        //}
        //public static string ImportParts(CarDealerContext context, string inputJson)
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());
        //    IMapper mapper = new Mapper(config);

        //    PartsDTO[] partsDTOs = JsonConvert.DeserializeObject<PartsDTO[]>(inputJson);
        //    Part[] parts = mapper.Map<Part[]>(partsDTOs);

        //    int[] supplierIds = context.Suppliers
        //        .Select(x => x.Id)
        //        .ToArray();

        //    Part[] partsWithvalidSuppliers = parts
        //        .Where(p => supplierIds.Contains(p.SupplierId)).ToArray();

        //    context.Parts.AddRange(partsWithvalidSuppliers);
        //    context.SaveChanges();

        //    return $"Successfully imported {partsWithvalidSuppliers.Count()}.";
        //}
        //public static string ImportCars(CarDealerContext context, string inputJson)
        //{
        //    var cars = JsonConvert.DeserializeObject<Car[]>(inputJson);
        //    context.Cars.AddRange(cars);
        //    context.SaveChanges();
        //    return $"Successfully imported {cars.Length}.";
        //}
        //public static string ImportCustomers(CarDealerContext context, string inputJson)
        //{
        //    var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);
        //    context.Customers.AddRange(customers);
        //    context.SaveChanges();
        //    return $"Successfully imported {customers.Length}.";
        //}
        //public static string ImportSales(CarDealerContext context, string inputJson)
        //{
        //    var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);
        //    context.Sales.AddRange(sales);
        //    context.SaveChanges();
        //    return $"Successfully imported {sales.Length}.";
        //}
        //public static string GetOrderedCustomers(CarDealerContext context)
        //{
        //    var customers = context.Customers
        //        .Select(c => new
        //        {
        //            Name=c.Name,
        //            BirthDate=c.BirthDate,
        //            IsYoungDriver =c.IsYoungDriver,
        //        }).OrderBy(c=>c.BirthDate).ThenBy(c=>c.IsYoungDriver).ToArray();

        //    var json=JsonConvert.SerializeObject(customers,Formatting.Indented);

        //    return json;
        //}
        //public static string GetCarsFromMakeToyota(CarDealerContext context)
        //{
        //    var carsFromToyota = context.Cars
        //        .Where(c => c.Make == "Toyota")
        //        .Select(c => new
        //        {
        //            Id = c.Id,
        //            Make = c.Make,
        //            Model = c.Model,
        //            TraveledDistance = c.TraveledDistance
        //        }).OrderBy(c=>c.Model).ThenByDescending(c=>c.TraveledDistance);

        //    var json = JsonConvert.SerializeObject(carsFromToyota, Formatting.Indented);

        //    return json;
        //}
        //public static string GetLocalSuppliers(CarDealerContext context)
        //{
        //    var suplliers = context.Suppliers
        //        .Where(s => s.IsImporter == false)
        //        .Select(s => new
        //        {
        //            Id = s.Id,
        //            Name = s.Name,
        //            PartsCount = s.Parts.Count,
        //        });

        //    var json = JsonConvert.SerializeObject(suplliers, Formatting.Indented);

        //    return json;
        //}
        //public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        //{
        //    var data = context.Cars
        //        .Select(c => new
        //        {
        //            c.Make,
        //            c.Model,
        //            c.TraveledDistance,
        //            parts=c.PartsCars.Select(cp => new
        //            {
        //                cp.Part.Name,
        //                Price=$"{cp.Part.Price}:f2"
        //            })
        //        }).ToArray();

        //    var json = JsonConvert.SerializeObject(data, Formatting.Indented);

        //    return json;
        //public static string GetTotalSalesByCustomer(CarDealerContext context)
        //{ 
        //    var customers=context.Customers
        //        .Where(c=>c.Sales.Any(s=>s.CarId!=null))
        //        .Select(c=>new
        //        {
        //            FullName=c.Name,
        //            boughtCars=c.Sales.Count(),
        //            spentMoney=c.Sales.Sum(s=>s.Car.PartsCars.Sum(pc=>pc.P))
        //        })
        //}

    }
}
