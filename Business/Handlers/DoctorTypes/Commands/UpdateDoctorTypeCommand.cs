
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
using Business.Handlers.DoctorTypes.ValidationRules;


namespace Business.Handlers.DoctorTypes.Commands
{


    public class UpdateDoctorTypeCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateDoctorTypeCommandHandler : IRequestHandler<UpdateDoctorTypeCommand, IResult>
        {
            private readonly IDoctorTypeRepository _doctorTypeRepository;
            private readonly IMediator _mediator;

            public UpdateDoctorTypeCommandHandler(IDoctorTypeRepository doctorTypeRepository, IMediator mediator)
            {
                _doctorTypeRepository = doctorTypeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDoctorTypeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDoctorTypeCommand request, CancellationToken cancellationToken)
            {
                var isThereDoctorTypeRecord = await _doctorTypeRepository.GetAsync(u => u.Id == request.Id);


                isThereDoctorTypeRecord.Name = request.Name;


                _doctorTypeRepository.Update(isThereDoctorTypeRecord);
                await _doctorTypeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

