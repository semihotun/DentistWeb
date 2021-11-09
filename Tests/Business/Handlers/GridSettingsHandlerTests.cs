
using Business.Handlers.GridSettingses.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.GridSettingses.Queries.GetGridSettingsQuery;
using Entities.Concrete;
using static Business.Handlers.GridSettingses.Commands.CreateGridSettingsCommand;
using Business.Handlers.GridSettingses.Commands;
using Business.Constants;
using static Business.Handlers.GridSettingses.Commands.UpdateGridSettingsCommand;
using static Business.Handlers.GridSettingses.Commands.DeleteGridSettingsCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class GridSettingsHandlerTests
    {
        Mock<IGridSettingsRepository> _gridSettingsRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _gridSettingsRepository = new Mock<IGridSettingsRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task GridSettings_GetQuery_Success()
        {
            //Arrange
            var query = new GetGridSettingsQuery();

            _gridSettingsRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GridSettings, bool>>>())).ReturnsAsync(new GridSettings()
//propertyler buraya yazılacak
//{																		
//GridSettingsId = 1,
//GridSettingsName = "Test"
//}
);

            var handler = new GetGridSettingsQueryHandler(_gridSettingsRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.GridSettingsId.Should().Be(1);

        }

        [Test]
        public async Task GridSettings_GetQueries_Success()
        {
            ////Arrange
            //var query = new GetGridSettingsesQuery();

            //_gridSettingsRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<GridSettings, bool>>>()))
            //            .ReturnsAsync(new List<GridSettings> { new GridSettings() { /*TODO:propertyler buraya yazılacak GridSettingsId = 1, GridSettingsName = "test"*/ } });

            //var handler = new GetGridSettingsesQueryHandler(_gridSettingsRepository.Object, _mediator.Object);

            ////Act
            //var x = await handler.Handle(query, new System.Threading.CancellationToken());

            ////Asset
            //x.Success.Should().BeTrue();
            //((List<GridSettings>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task GridSettings_CreateCommand_Success()
        {
            GridSettings rt = null;
            //Arrange
            var command = new CreateGridSettingsCommand();
            //propertyler buraya yazılacak
            //command.GridSettingsName = "deneme";

            _gridSettingsRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GridSettings, bool>>>()))
                        .ReturnsAsync(rt);

            _gridSettingsRepository.Setup(x => x.Add(It.IsAny<GridSettings>())).Returns(new GridSettings());

            var handler = new CreateGridSettingsCommandHandler(_gridSettingsRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _gridSettingsRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task GridSettings_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateGridSettingsCommand();
            //propertyler buraya yazılacak 
            //command.GridSettingsName = "test";

            _gridSettingsRepository.Setup(x => x.Query())
                                           .Returns(new List<GridSettings> { new GridSettings() { /*TODO:propertyler buraya yazılacak GridSettingsId = 1, GridSettingsName = "test"*/ } }.AsQueryable());

            _gridSettingsRepository.Setup(x => x.Add(It.IsAny<GridSettings>())).Returns(new GridSettings());

            var handler = new CreateGridSettingsCommandHandler(_gridSettingsRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task GridSettings_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateGridSettingsCommand();
            //command.GridSettingsName = "test";

            _gridSettingsRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GridSettings, bool>>>()))
                        .ReturnsAsync(new GridSettings() { /*TODO:propertyler buraya yazılacak GridSettingsId = 1, GridSettingsName = "deneme"*/ });

            _gridSettingsRepository.Setup(x => x.Update(It.IsAny<GridSettings>())).Returns(new GridSettings());

            var handler = new UpdateGridSettingsCommandHandler(_gridSettingsRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _gridSettingsRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task GridSettings_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteGridSettingsCommand();

            _gridSettingsRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<GridSettings, bool>>>()))
                        .ReturnsAsync(new GridSettings() { /*TODO:propertyler buraya yazılacak GridSettingsId = 1, GridSettingsName = "deneme"*/});

            _gridSettingsRepository.Setup(x => x.Delete(It.IsAny<GridSettings>()));

            var handler = new DeleteGridSettingsCommandHandler(_gridSettingsRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _gridSettingsRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

