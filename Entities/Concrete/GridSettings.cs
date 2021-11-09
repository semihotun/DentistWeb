using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class GridSettings:IEntity
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string PropertyInfo { get; set; }
    }
}
