
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.DoctorTypes.Queries
{
    public class GetDoctorTypeQuery : IRequest<IDataResult<DoctorType>>
    {
        public int Id { get; set; }

        public class GetDoctorTypeQueryHandler : IRequestHandler<GetDoctorTypeQuery, IDataResult<DoctorType>>
        {
            private readonly IDoctorTypeRepository _doctorTypeRepository;
            private readonly IMediator _mediator;

            public GetDoctorTypeQueryHandler(IDoctorTypeRepository doctorTypeRepository, IMediator mediator)
            {
                _doctorTypeRepository = doctorTypeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<DoctorType>> Handle(GetDoctorTypeQuery request, CancellationToken cancellationToken)
            {
                var doctorType = await _doctorTypeRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<DoctorType>(doctorType);
            }
        }
    }
}
