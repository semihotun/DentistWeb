using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Doctor : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Telephone { get; set; }
        public int DoctorTypeId { get; set; }
        public DateTime StartDateOfWork { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    } 
}
