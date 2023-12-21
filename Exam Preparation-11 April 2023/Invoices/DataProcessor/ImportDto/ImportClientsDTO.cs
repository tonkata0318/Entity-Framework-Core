using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType("Client")]
    public class ImportClientsDTO
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("NumberVat")]

        public string NumberVat { get; set; }

        [XmlArray("Addresses")]
        public ImportAddressesDTO[] Addresses { get; set; }
    }
}
