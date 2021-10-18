
using Business.Handlers.PatientOperations.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.PatientOperations.Queries.GetPatientOperationQuery;
using Entities.Concrete;
using static Business.Handlers.PatientOperations.Queries.GetPatientOperationsQuery;
using static Business.Handlers.PatientOperations.Commands.CreatePatientOperationCommand;
using Business.Handlers.PatientOperations.Commands;
using Business.Constants;
using static Business.Handlers.PatientOperations.Commands.UpdatePatientOperationCommand;
using static Business.Handlers.PatientOperations.Commands.DeletePatientOperationCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PatientOperationHandlerTests
    {
        Mock<IPatientOperationRepository> _patientOperationRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _patientOperationRepository = new Mock<IPatientOperationRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task PatientOperation_GetQuery_Success()
        {
            //Arrange
            var query = new GetPatientOperationQuery();

            _patientOperationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PatientOperation, bool>>>())).ReturnsAsync(new PatientOperation()
//propertyler buraya yazılacak
//{																		
//PatientOperationId = 1,
//PatientOperationName = "Test"
//}
);

            var handler = new GetPatientOperationQueryHandler(_patientOperationRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PatientOperationId.Should().Be(1);

        }

        [Test]
        public async Task PatientOperation_GetQueries_Success()
        {
            //Arrange
            var query = new GetPatientOperationsQuery();

            _patientOperationRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<PatientOperation, bool>>>()))
                        .ReturnsAsync(new List<PatientOperation> { new PatientOperation() { /*TODO:propertyler buraya yazılacak PatientOperationId = 1, PatientOperationName = "test"*/ } });

            var handler = new GetPatientOperationsQueryHandler(_patientOperationRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<PatientOperation>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task PatientOperation_CreateCommand_Success()
        {
            PatientOperation rt = null;
            //Arrange
            var command = new CreatePatientOperationCommand();
            //propertyler buraya yazılacak
            //command.PatientOperationName = "deneme";

            _patientOperationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PatientOperation, bool>>>()))
                        .ReturnsAsync(rt);

            _patientOperationRepository.Setup(x => x.Add(It.IsAny<PatientOperation>())).Returns(new PatientOperation());

            var handler = new CreatePatientOperationCommandHandler(_patientOperationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _patientOperationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task PatientOperation_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePatientOperationCommand();
            //propertyler buraya yazılacak 
            //command.PatientOperationName = "test";

            _patientOperationRepository.Setup(x => x.Query())
                                           .Returns(new List<PatientOperation> { new PatientOperation() { /*TODO:propertyler buraya yazılacak PatientOperationId = 1, PatientOperationName = "test"*/ } }.AsQueryable());

            _patientOperationRepository.Setup(x => x.Add(It.IsAny<PatientOperation>())).Returns(new PatientOperation());

            var handler = new CreatePatientOperationCommandHandler(_patientOperationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task PatientOperation_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePatientOperationCommand();
            //command.PatientOperationName = "test";

            _patientOperationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PatientOperation, bool>>>()))
                        .ReturnsAsync(new PatientOperation() { /*TODO:propertyler buraya yazılacak PatientOperationId = 1, PatientOperationName = "deneme"*/ });

            _patientOperationRepository.Setup(x => x.Update(It.IsAny<PatientOperation>())).Returns(new PatientOperation());

            var handler = new UpdatePatientOperationCommandHandler(_patientOperationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _patientOperationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task PatientOperation_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePatientOperationCommand();

            _patientOperationRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<PatientOperation, bool>>>()))
                        .ReturnsAsync(new PatientOperation() { /*TODO:propertyler buraya yazılacak PatientOperationId = 1, PatientOperationName = "deneme"*/});

            _patientOperationRepository.Setup(x => x.Delete(It.IsAny<PatientOperation>()));

            var handler = new DeletePatientOperationCommandHandler(_patientOperationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _patientOperationRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

