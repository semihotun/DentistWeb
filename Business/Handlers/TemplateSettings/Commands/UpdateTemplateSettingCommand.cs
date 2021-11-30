using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using Entities.Concrete;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.TemplateSettings.Commands
{
    public class UpdateTemplateSettingCommand : IRequest<IResult>
    {
        public TemplateSetting templateSetting { get; set; }

        public class UpdateTemplateSettingCommandHandler: IRequestHandler<UpdateTemplateSettingCommand,IResult>
        {
            private readonly IMediator _mediator;
            public UpdateTemplateSettingCommandHandler(IMediator mediator)
            {
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            public async Task<IResult> Handle(UpdateTemplateSettingCommand request, CancellationToken cancellationToken)
            {
                var path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "templateSetting.json").ToString();
                System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(request.templateSetting));
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

