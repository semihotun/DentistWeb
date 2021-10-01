
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Patients.ValidationRules;


namespace Business.Handlers.Patients.Commands
{


    public class UpdatePatientCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public int Telephone { get; set; }

        public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, IResult>
        {
            private readonly IPatientRepository _patientRepository;
            private readonly IMediator _mediator;

            public UpdatePatientCommandHandler(IPatientRepository patientRepository, IMediator mediator)
            {
                _patientRepository = patientRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePatientValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
            {
                var isTherePatientRecord = await _patientRepository.GetAsync(u => u.Id == request.Id);


                isTherePatientRecord.IdentificationNumber = request.IdentificationNumber;
                isTherePatientRecord.Name = request.Name;
                isTherePatientRecord.Surname = request.Surname;
                isTherePatientRecord.Adress = request.Adress;
                isTherePatientRecord.Telephone = request.Telephone;


                _patientRepository.Update(isTherePatientRecord);
                await _patientRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

