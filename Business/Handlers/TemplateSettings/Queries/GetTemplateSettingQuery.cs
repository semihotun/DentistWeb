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
    public class GetTemplateSettingQuery : IRequest<IDataResult<TemplateSetting>>
    {
        public class GetTemplateSettingQueryHandler : IRequestHandler<GetTemplateSettingQuery, IDataResult<TemplateSetting>>
        {
            private readonly IMediator _mediator;

            public GetTemplateSettingQueryHandler( IMediator mediator)
            {
                _mediator = mediator;
            }

            [CacheAspect(10)]
            public async Task<IDataResult<TemplateSetting>> Handle(GetTemplateSettingQuery request, CancellationToken cancellationToken)
            {
                var path =Path.Combine(System.IO.Directory.GetCurrentDirectory() ,"templateSetting.json").ToString();
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    var items = JsonConvert.DeserializeObject<TemplateSetting>(json);
                    return new SuccessDataResult<TemplateSetting>(items);
                }
            }
        }
    }
}
