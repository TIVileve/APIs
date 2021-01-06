using System;

namespace Vileve.Application.ViewModels.v1.Property
{
    public class PropertyViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool IsRequired { get; set; }
    }
}