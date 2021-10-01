
using Business.Handlers.Diseases.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Diseases.Queries.GetDiseaseQuery;
using Entities.Concrete;
using static Business.Handlers.Diseases.Queries.GetDiseasesQuery;
using static Business.Handlers.Diseases.Commands.CreateDiseaseCommand;
using Business.Handlers.Diseases.Commands;
using Business.Constants;
using static Business.Handlers.Diseases.Commands.UpdateDiseaseCommand;
using static Business.Handlers.Diseases.Commands.DeleteDiseaseCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DiseaseHandlerTests
    {
        Mock<IDiseaseRepository> _diseaseRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _diseaseRepository = new Mock<IDiseaseRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Disease_GetQuery_Success()
        {
            //Arrange
            var query = new GetDiseaseQuery();

            _diseaseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Disease, bool>>>())).ReturnsAsync(new Disease()
//propertyler buraya yazılacak
//{																		
//DiseaseId = 1,
//DiseaseName = "Test"
//}
);

            var handler = new GetDiseaseQueryHandler(_diseaseRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DiseaseId.Should().Be(1);

        }

        [Test]
        public async Task Disease_GetQueries_Success()
        {
            //Arrange
            var query = new GetDiseasesQuery();

            _diseaseRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Disease, bool>>>()))
                        .ReturnsAsync(new List<Disease> { new Disease() { /*TODO:propertyler buraya yazılacak DiseaseId = 1, DiseaseName = "test"*/ } });

            var handler = new GetDiseasesQueryHandler(_diseaseRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Disease>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Disease_CreateCommand_Success()
        {
            Disease rt = null;
            //Arrange
            var command = new CreateDiseaseCommand();
            //propertyler buraya yazılacak
            //command.DiseaseName = "deneme";

            _diseaseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Disease, bool>>>()))
                        .ReturnsAsync(rt);

            _diseaseRepository.Setup(x => x.Add(It.IsAny<Disease>())).Returns(new Disease());

            var handler = new CreateDiseaseCommandHandler(_diseaseRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _diseaseRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Disease_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDiseaseCommand();
            //propertyler buraya yazılacak 
            //command.DiseaseName = "test";

            _diseaseRepository.Setup(x => x.Query())
                                           .Returns(new List<Disease> { new Disease() { /*TODO:propertyler buraya yazılacak DiseaseId = 1, DiseaseName = "test"*/ } }.AsQueryable());

            _diseaseRepository.Setup(x => x.Add(It.IsAny<Disease>())).Returns(new Disease());

            var handler = new CreateDiseaseCommandHandler(_diseaseRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Disease_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDiseaseCommand();
            //command.DiseaseName = "test";

            _diseaseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Disease, bool>>>()))
                        .ReturnsAsync(new Disease() { /*TODO:propertyler buraya yazılacak DiseaseId = 1, DiseaseName = "deneme"*/ });

            _diseaseRepository.Setup(x => x.Update(It.IsAny<Disease>())).Returns(new Disease());

            var handler = new UpdateDiseaseCommandHandler(_diseaseRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _diseaseRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Disease_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDiseaseCommand();

            _diseaseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Disease, bool>>>()))
                        .ReturnsAsync(new Disease() { /*TODO:propertyler buraya yazılacak DiseaseId = 1, DiseaseName = "deneme"*/});

            _diseaseRepository.Setup(x => x.Delete(It.IsAny<Disease>()));

            var handler = new DeleteDiseaseCommandHandler(_diseaseRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _diseaseRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

