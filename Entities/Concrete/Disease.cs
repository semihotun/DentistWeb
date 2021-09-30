using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Disease : IEntity
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public double Amount { get; set; }
    }
}
