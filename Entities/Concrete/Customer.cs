using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Customer : IEntity
    {
        public string CustomerId { get; set; }   //Northwind veritabanında string olarak tutuluyor. Mecbur string tanımlıyoruz

        public string ContactName { get; set; }

        public string CompanyName { get; set; }

        public string City { get; set; }
    }
}
