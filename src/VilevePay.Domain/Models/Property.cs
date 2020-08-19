using System;
using VilevePay.Domain.Core.Models;
using Type = VilevePay.Domain.Enums.Type;

namespace VilevePay.Domain.Models
{
    public class Property : Entity
    {
        public Property(Guid id, string name, Type type, bool isRequired)
        {
            Id = id;
            Name = name;
            Type = type;
            IsRequired = isRequired;
        }

        // Empty constructor for EF
        protected Property()
        {
        }

        public string Name { get; private set; }
        public Type Type { get; private set; }
        public bool IsRequired { get; private set; }

        public Property Update(string name, Type type, bool isRequired)
        {
            Name = name;
            Type = type;
            IsRequired = isRequired;

            return this;
        }
    }
}