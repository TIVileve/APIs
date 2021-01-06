using System;
using System.Text.Json.Serialization;

namespace Vileve.Application.ViewModels.v1.Property
{
    public class UpdatePropertyViewModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool IsRequired { get; set; }
    }
}