
using Business.Handlers.Patients.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Patients.Queries.GetPatientQuery;
using Entities.Concrete;
using static Business.Handlers.Patients.Queries.GetPatientsQuery;
using static Business.Handlers.Patients.Commands.CreatePatientCommand;
using Business.Handlers.Patients.Commands;
using Business.Constants;
using static Business.Handlers.Patients.Commands.UpdatePatientCommand;
using static Business.Handlers.Patients.Commands.DeletePatientCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PatientHandlerTests
    {
        Mock<IPatientRepository> _patientRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _patientRepository = new Mock<IPatientRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Patient_GetQuery_Success()
        {
            //Arrange
            var query = new GetPatientQuery();

            _patientRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Patient, bool>>>())).ReturnsAsync(new Patient()
//propertyler buraya yazılacak
//{																		
//PatientId = 1,
//PatientName = "Test"
//}
);

            var handler = new GetPatientQueryHandler(_patientRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PatientId.Should().Be(1);

        }

        [Test]
        public async Task Patient_GetQueries_Success()
        {
            //Arrange
            var query = new GetPatientsQuery();

            _patientRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Patient, bool>>>()))
                        .ReturnsAsync(new List<Patient> { new Patient() { /*TODO:propertyler buraya yazılacak PatientId = 1, PatientName = "test"*/ } });

            var handler = new GetPatientsQueryHandler(_patientRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Patient>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Patient_CreateCommand_Success()
        {
            Patient rt = null;
            //Arrange
            var command = new CreatePatientCommand();
            //propertyler buraya yazılacak
            //command.PatientName = "deneme";

            _patientRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Patient, bool>>>()))
                        .ReturnsAsync(rt);

            _patientRepository.Setup(x => x.Add(It.IsAny<Patient>())).Returns(new Patient());

            var handler = new CreatePatientCommandHandler(_patientRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _patientRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Patient_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePatientCommand();
            //propertyler buraya yazılacak 
            //command.PatientName = "test";

            _patientRepository.Setup(x => x.Query())
                                           .Returns(new List<Patient> { new Patient() { /*TODO:propertyler buraya yazılacak PatientId = 1, PatientName = "test"*/ } }.AsQueryable());

            _patientRepository.Setup(x => x.Add(It.IsAny<Patient>())).Returns(new Patient());

            var handler = new CreatePatientCommandHandler(_patientRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Patient_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePatientCommand();
            //command.PatientName = "test";

            _patientRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Patient, bool>>>()))
                        .ReturnsAsync(new Patient() { /*TODO:propertyler buraya yazılacak PatientId = 1, PatientName = "deneme"*/ });

            _patientRepository.Setup(x => x.Update(It.IsAny<Patient>())).Returns(new Patient());

            var handler = new UpdatePatientCommandHandler(_patientRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _patientRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Patient_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePatientCommand();

            _patientRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Patient, bool>>>()))
                        .ReturnsAsync(new Patient() { /*TODO:propertyler buraya yazılacak PatientId = 1, PatientName = "deneme"*/});

            _patientRepository.Setup(x => x.Delete(It.IsAny<Patient>()));

            var handler = new DeletePatientCommandHandler(_patientRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _patientRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

