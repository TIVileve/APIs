using System;
using Vileve.Domain.Core.Commands;
using Type = Vileve.Domain.Enums.Type;

namespace Vileve.Domain.Commands.Property
{
    public abstract class PropertyCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public Type Type { get; protected set; }
        public bool IsRequired { get; protected set; }
    }
}