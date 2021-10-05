
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
using Business.Handlers.Patients.ValidationRules;

namespace Business.Handlers.Patients.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePatientCommand : IRequest<IResult>
    {

        public int IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public int Telephone { get; set; }


        public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, IResult>
        {
            private readonly IPatientRepository _patientRepository;
            private readonly IMediator _mediator;
            public CreatePatientCommandHandler(IPatientRepository patientRepository, IMediator mediator)
            {
                _patientRepository = patientRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePatientValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
           [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
            {
                var isTherePatientRecord = _patientRepository.Query().Any(u => u.IdentificationNumber == request.IdentificationNumber);

                if (isTherePatientRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPatient = new Patient
                {
                    IdentificationNumber = request.IdentificationNumber,
                    Name = request.Name,
                    Surname = request.Surname,
                    Adress = request.Adress,
                    Telephone = request.Telephone,

                };

                _patientRepository.Add(addedPatient);
                await _patientRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}