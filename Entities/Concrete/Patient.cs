using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Patient :IEntity
    {
        public int Id { get; set; }
        public int IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public int Telephone { get; set; }
    }
}
