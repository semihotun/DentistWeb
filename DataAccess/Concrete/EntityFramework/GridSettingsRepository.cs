
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Collections.Generic;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Core.Utilities.Pagedlist;

namespace DataAccess.Concrete.EntityFramework
{
    public class GridSettingsRepository : EfEntityRepositoryBase<GridSettings, ProjectDbContext>, IGridSettingsRepository
    {
        public GridSettingsRepository(ProjectDbContext context) : base(context)
        {
        }

      

    }
}
