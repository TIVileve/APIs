using System;
using FluentValidation.Results;
using Vileve.Domain.Core.Events;

namespace Vileve.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        protected Command()
        {
            Timestamp = DateTime.Now;
            RequestId = Guid.NewGuid();
        }

        public DateTime Timestamp { get; }
        public Guid RequestId { get; }
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}