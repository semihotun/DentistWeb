
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.DoctorTypes.ValidationRules;

namespace Business.Handlers.DoctorTypes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDoctorTypeCommand : IRequest<IResult>
    {

        public string Name { get; set; }


        public class CreateDoctorTypeCommandHandler : IRequestHandler<CreateDoctorTypeCommand, IResult>
        {
            private readonly IDoctorTypeRepository _doctorTypeRepository;
            private readonly IMediator _mediator;
            public CreateDoctorTypeCommandHandler(IDoctorTypeRepository doctorTypeRepository, IMediator mediator)
            {
                _doctorTypeRepository = doctorTypeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDoctorTypeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
           [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDoctorTypeCommand request, CancellationToken cancellationToken)
            {
                var isThereDoctorTypeRecord = _doctorTypeRepository.Query().Any(u => u.Name == request.Name);

                if (isThereDoctorTypeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDoctorType = new DoctorType
                {
                    Name = request.Name,

                };

                _doctorTypeRepository.Add(addedDoctorType);
                await _doctorTypeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}