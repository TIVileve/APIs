using System;
using Vileve.Domain.Core.Commands;
using Vileve.Domain.Core.Events;

namespace Vileve.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        public DomainNotification(string key, string value, Command command)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
            Version = 1;
            Command = command;
        }

        public Guid DomainNotificationId { get; }
        public string Key { get; }
        public string Value { get; }
        public int Version { get; }
        public Command Command { get; }
    }
}