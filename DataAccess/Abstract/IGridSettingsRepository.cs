
using System;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IGridSettingsRepository : IEntityRepository<GridSettings>
    {

    }
}