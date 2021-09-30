using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Appointment: IEntity
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int DiseaseId { get; set; }
    }
}
