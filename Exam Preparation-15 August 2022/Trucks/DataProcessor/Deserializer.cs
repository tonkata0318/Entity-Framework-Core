namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Trucks.Helpers;
    using Data;
    using Newtonsoft.Json;
    using Trucks.Data.Models;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            var despatchersDTO = XmlSerializationHelper.Deserialize<ImportDispatcherDTO[]>(xmlString, "Despatchers");

            StringBuilder sb=new StringBuilder();
            List<Despatcher> despatchers = new List<Despatcher>();
            foreach (var despatcher in despatchersDTO)
            {
                int counter = 1;
                if (!IsValid(despatcher))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (despatcher.Position==null||despatcher.Position==string.Empty)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Despatcher despatchertoAdd = new Despatcher()
                {
                    Name= despatcher.Name,
                    Position=despatcher.Position,
                };
                foreach (var truck in despatcher.Trucks)
                {
                    if (!IsValid(truck))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (truck.RegistrationNumber == null || truck.RegistrationNumber == string.Empty)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    else
                    {
                        despatchertoAdd.Trucks.Add(new Truck()
                        {
                            DespatcherId=counter,
                            RegistrationNumber= truck.RegistrationNumber,
                            VinNumber= truck.VinNumber,
                            TankCapacity= truck.TankCapacity,
                            CargoCapacity= truck.CargoCapacity,
                            CategoryType=(CategoryType)truck.CategoryType,
                            MakeType=(MakeType)truck.MakeType
                        });
                    }
                }
                counter++;
                despatchers.Add(despatchertoAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedDespatcher,despatchertoAdd.Name,despatchertoAdd.Trucks.Count));
            }
            context.Despatchers.AddRange(despatchers);
            context.SaveChanges();
            return sb.ToString();
        }
        public static string ImportClient(TrucksContext context, string jsonString)
        {
            StringBuilder sb=new StringBuilder();
            var clientsDTOs = JsonConvert.DeserializeObject<ImportClientsDTO[]>(jsonString);

            List<Client> clients = new List<Client>();
            var truckIdsExisting = context.Trucks
                .Select(t => t.Id)
                .ToArray();
            foreach (var client in clientsDTOs)
            {
                if (!IsValid(client))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (client.Type=="usual")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var clienttoAdd = new Client()
                { 
                    Name= client.Name,
                    Nationality= client.Nationality,
                    Type= client.Type,
                };
                foreach (var id in client.TrucksIds.Distinct())
                {
                    if (!truckIdsExisting.Contains(id))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    ClientTruck clientTruck = new ClientTruck()
                    {
                        Client=clienttoAdd,
                        TruckId= id,
                    };
                    clienttoAdd.ClientsTrucks.Add(clientTruck);
                }
                clients.Add(clienttoAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedClient,clienttoAdd.Name,clienttoAdd.ClientsTrucks.Count));
            }
            context.Clients.AddRange(clients);
            context.SaveChanges();
            return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}