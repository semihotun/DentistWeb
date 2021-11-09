using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Utilities.Pagedlist;
using Core.Entities.Concrete;
using Business.Handlers.Logs.Models;
using Core.Utilities.Filter;
using System.Linq.Dynamic.Core;
using Core.Aspects.Autofac.Transaction;

namespace Business.Handlers.Logs.Queries
{
    public class GetLogDtoPagedListQuery : IRequest<IDataResult<IPagedList<LogDto>>>
    {
        public PagedListFilterModel pagedListFilterModel { get; set; }

        public class GetLogDtoPagedListQueryHandler : IRequestHandler<GetLogDtoPagedListQuery, IDataResult<IPagedList<LogDto>>>
        {
            private readonly ILogRepository _logRepository;
            private readonly IMediator _mediator;

            public GetLogDtoPagedListQueryHandler(ILogRepository logRepository, IMediator mediator)
            {
                _logRepository = logRepository;
                _mediator = mediator;
            }
   
            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IPagedList<LogDto>>> Handle(GetLogDtoPagedListQuery request, CancellationToken cancellationToken)
            {
                var query = _logRepository.Query();
                var data = await query.ToTableSettings(request.pagedListFilterModel);

                var result = data.Select(x =>
                  {
                      var jsonMessage = JsonConvert.DeserializeObject<LogDto>(x.MessageTemplate);
                      dynamic msg = JsonConvert.DeserializeObject(x.MessageTemplate);
                      var valueList = msg.Parameters[0];
                      var exceptionMessage = msg.ExceptionMessage;
                      valueList = valueList.Value.ToString();

                      var model = new LogDto();
                      model.Id = x.Id;
                      model.Level = x.Level;
                      model.TimeStamp = x.TimeStamp;
                      model.Type = msg.Parameters[0].Type;
                      model.User = jsonMessage.User;
                      model.Value = valueList;
                      model.ExceptionMessage = exceptionMessage;
                      return model;
                  });

                return new SuccessDataResult<IPagedList<LogDto>>(result);
            }

        }
    }
}
