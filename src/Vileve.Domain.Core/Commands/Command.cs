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
        }

        public DateTime Timestamp { get; }
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}