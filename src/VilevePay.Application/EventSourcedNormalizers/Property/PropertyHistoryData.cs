using System;

namespace VilevePay.Application.EventSourcedNormalizers.Property
{
    public class PropertyHistoryData
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public bool? IsRequired { get; set; }
        public string Action { get; set; }
        public string Timestamp { get; set; }
        public string Who { get; set; }
    }
}