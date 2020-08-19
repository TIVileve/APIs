using System;
using VilevePay.Domain.Core.Commands;
using Type = VilevePay.Domain.Enums.Type;

namespace VilevePay.Domain.Commands.Property
{
    public abstract class PropertyCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public Type Type { get; protected set; }
        public bool IsRequired { get; protected set; }
    }
}