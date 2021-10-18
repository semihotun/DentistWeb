
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
using Business.Handlers.PatientOperations.ValidationRules;


namespace Business.Handlers.PatientOperations.Commands
{


    public class UpdatePatientOperationCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DiseaseId { get; set; }

        public class UpdatePatientOperationCommandHandler : IRequestHandler<UpdatePatientOperationCommand, IResult>
        {
            private readonly IPatientOperationRepository _patientOperationRepository;
            private readonly IMediator _mediator;

            public UpdatePatientOperationCommandHandler(IPatientOperationRepository patientOperationRepository, IMediator mediator)
            {
                _patientOperationRepository = patientOperationRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePatientOperationValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
                     [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePatientOperationCommand request, CancellationToken cancellationToken)
            {
                var isTherePatientOperationRecord = await _patientOperationRepository.GetAsync(u => u.Id == request.Id);


                isTherePatientOperationRecord.PatientId = request.PatientId;
                isTherePatientOperationRecord.DiseaseId = request.DiseaseId;


                _patientOperationRepository.Update(isTherePatientOperationRecord);
                await _patientOperationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

