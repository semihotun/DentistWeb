
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
using Business.Handlers.PatientOperations.ValidationRules;

namespace Business.Handlers.PatientOperations.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePatientOperationCommand : IRequest<IResult>
    {

        public int PatientId { get; set; }
        public int DiseaseId { get; set; }


        public class CreatePatientOperationCommandHandler : IRequestHandler<CreatePatientOperationCommand, IResult>
        {
            private readonly IPatientOperationRepository _patientOperationRepository;
            private readonly IMediator _mediator;
            public CreatePatientOperationCommandHandler(IPatientOperationRepository patientOperationRepository, IMediator mediator)
            {
                _patientOperationRepository = patientOperationRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePatientOperationValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
                     [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePatientOperationCommand request, CancellationToken cancellationToken)
            {    
                var addedPatientOperation = new PatientOperation
                {
                    PatientId = request.PatientId,
                    DiseaseId = request.DiseaseId,
                };

                _patientOperationRepository.Add(addedPatientOperation);
                await _patientOperationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}