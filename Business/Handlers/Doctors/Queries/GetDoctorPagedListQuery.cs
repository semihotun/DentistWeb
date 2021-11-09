using Business.BusinessAspects;
using Business.Handlers.Logs.Queries;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Filter;
using Core.Utilities.Pagedlist;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Business.Handlers.Doctors.Queries
{
    public class GetDoctorPagedListQuery : IRequest<IDataResult<IPagedList<Doctor>>>
    {
        public PagedListFilterModel pagedListFilterModel { get; set; }

        public class GetDoctorPagedListQueryHandler : IRequestHandler<GetDoctorPagedListQuery, IDataResult<IPagedList<Doctor>>>
        {
            private readonly IMediator _mediator;
            private readonly IDoctorRepository _doctorRepository;

            public GetDoctorPagedListQueryHandler(IMediator mediator, IDoctorRepository doctorRepository)
            {
                _mediator = mediator;
                _doctorRepository = doctorRepository;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            //[CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IPagedList<Doctor>>> Handle(GetDoctorPagedListQuery request, CancellationToken cancellationToken)
            {
                var query =await _doctorRepository.Query().Where(x=>x.Deleted==false).ToTableSettings(request.pagedListFilterModel);        
        
                return new SuccessDataResult<IPagedList<Doctor>>(query);
            }

        }
    }
}
