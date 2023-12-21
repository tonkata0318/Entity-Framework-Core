namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using AutoMapper;
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";

        private static Mapper GetMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<InvoicesProfile>());
            return new Mapper(cfg);
        }
        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            XmlSerializer xmlSerializer=new XmlSerializer(typeof(ImportClientsDTO[]),new XmlRootAttribute("Clients"));
            using var reader=new StringReader(xmlString);
            ImportClientsDTO[] importClientsDTOs = (ImportClientsDTO[])xmlSerializer.Deserialize(reader);
            var mapper = GetMapper();
            Client[] clients = mapper.Map<Client[]>(importClientsDTOs).Where(c=>c.Name);
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {


            throw new NotImplementedException();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    } 
}
