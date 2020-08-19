using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VilevePay.Domain.Commands.Property;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Events.Property;
using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;

namespace VilevePay.Domain.CommandHandlers
{
    public class PropertyCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewPropertyCommand, object>,
        IRequestHandler<UpdatePropertyCommand, bool>,
        IRequestHandler<RemovePropertyCommand, bool>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMediatorHandler _bus;

        public PropertyCommandHandler(
            IPropertyRepository propertyRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _propertyRepository = propertyRepository;
            _bus = bus;
        }

        public async Task<object> Handle(RegisterNewPropertyCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var property = new Property(Guid.NewGuid(), message.Name, message.Type, message.IsRequired);

            _propertyRepository.Add(property);

            if (Commit())
            {
                await _bus.RaiseEvent(new PropertyRegisteredEvent(property.Id, property.Name, property.Type, property.IsRequired));
            }

            return await Task.FromResult(property);
        }

        public Task<bool> Handle(UpdatePropertyCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var property = _propertyRepository.GetById(message.Id).Update(message.Name, message.Type, message.IsRequired);

            _propertyRepository.Update(property);

            if (Commit())
            {
                _bus.RaiseEvent(new PropertyUpdatedEvent(property.Id, property.Name, property.Type, property.IsRequired));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemovePropertyCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _propertyRepository.Remove(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new PropertyRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _propertyRepository.Dispose();
        }
    }
}