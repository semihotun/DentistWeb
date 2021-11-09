using Core.Utilities.Filter;
using System;

namespace Core.Entities.Dtos
{
    public class LogDto : IEntity
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }

        [JsonFilterNameAttribute("MessageTemplate")]
        public string ExceptionMessage { get; set; }
        [JsonFilterNameAttribute("MessageTemplate")]
        public string User { get; set; }
        [JsonFilterNameAttribute("MessageTemplate")]
        public string Value { get; set; }
        [JsonFilterNameAttribute("MessageTemplate")]
        public string Type { get; set; }

    }
}
