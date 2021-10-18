using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class PatientOperation : IEntity
    {
        public int Id { get; set; }
        public int PatientId { get; set; }//hasta
        public int DiseaseId { get; set; }//hastalık
    }
}
