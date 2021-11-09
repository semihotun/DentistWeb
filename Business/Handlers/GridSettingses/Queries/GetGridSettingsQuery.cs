
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using System.Linq;
using Entities.Dtos;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Business.Handlers.GridSettingses.Queries
{
    public class GetGridSettingsQuery : IRequest<IDataResult<GridSettingsDTO>>
    {
        public string Path { get; set; }

        public class GetGridSettingsQueryHandler : IRequestHandler<GetGridSettingsQuery, IDataResult<GridSettingsDTO>>
        {
            private readonly IGridSettingsRepository _gridSettingsRepository;
            private readonly IMediator _mediator;

            public GetGridSettingsQueryHandler(IGridSettingsRepository gridSettingsRepository, IMediator mediator)
            {
                _gridSettingsRepository = gridSettingsRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<GridSettingsDTO>> Handle(GetGridSettingsQuery request, CancellationToken cancellationToken)
            {
                var query = await (from grs in _gridSettingsRepository.Query()
                                   where grs.Path == request.Path
                                   select new GridSettingsDTO
                                   {
                                       Id = grs.Id,
                                       Path = grs.Path,
                                       PropertyInfo = grs.PropertyInfo
                                   }).FirstOrDefaultAsync();


                return new SuccessDataResult<GridSettingsDTO>(query);
            }
        }
    }
}
