using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class AppointmentTime : IEntity
    {
        public int Id { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }
    }
}
