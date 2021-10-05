
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Doctors.Queries
{
    public class GetDoctorQuery : IRequest<IDataResult<Doctor>>
    {
        public int Id { get; set; }

        public class GetDoctorQueryHandler : IRequestHandler<GetDoctorQuery, IDataResult<Doctor>>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly IMediator _mediator;

            public GetDoctorQueryHandler(IDoctorRepository doctorRepository, IMediator mediator)
            {
                _doctorRepository = doctorRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Doctor>> Handle(GetDoctorQuery request, CancellationToken cancellationToken)
            {
                var doctor = await _doctorRepository.GetAsync(p => p.Id == request.Id && p.Deleted != true);
                return new SuccessDataResult<Doctor>(doctor);
            }
        }
    }
}
