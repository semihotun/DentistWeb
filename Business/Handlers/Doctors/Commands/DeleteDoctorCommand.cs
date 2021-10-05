
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Doctors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDoctorCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, IResult>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly IMediator _mediator;

            public DeleteDoctorCommandHandler(IDoctorRepository doctorRepository, IMediator mediator)
            {
                _doctorRepository = doctorRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
            {
                var doctorToDelete = _doctorRepository.Get(p => p.Id == request.Id);
                doctorToDelete.Deleted = true;
                _doctorRepository.Update(doctorToDelete);
                await _doctorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

