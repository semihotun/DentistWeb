
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
using Business.Handlers.Doctors.ValidationRules;

namespace Business.Handlers.Doctors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDoctorCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Telephone { get; set; }
        public int DoctorTypeId { get; set; }
        public System.DateTime StartDateOfWork { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }


        public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, IResult>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly IMediator _mediator;
            public CreateDoctorCommandHandler(IDoctorRepository doctorRepository, IMediator mediator)
            {
                _doctorRepository = doctorRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDoctorValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
            {
                var isThereDoctorRecord = _doctorRepository.Query().Any(u => u.Name == request.Name);

                if (isThereDoctorRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDoctor = new Doctor
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Adress = request.Adress,
                    Telephone = request.Telephone,
                    DoctorTypeId = request.DoctorTypeId,
                    StartDateOfWork = request.StartDateOfWork,
                    Active = request.Active,
                    Deleted = request.Deleted,

                };

                _doctorRepository.Add(addedDoctor);
                await _doctorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}