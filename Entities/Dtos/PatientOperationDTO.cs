using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class PatientOperationDTO : IDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }//hasta
        public int DiseaseId { get; set; }//hastalık
        public string DiseaseName { get; set; }
    }
}
