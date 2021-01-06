using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Vileve.Application.EventSourcedNormalizers.Property;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Property;
using Vileve.Domain.Commands.Property;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Infra.Data.Repository.EventSourcing;

namespace Vileve.Application.Services
{
    public class PropertyAppService : IPropertyAppService
    {
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public PropertyAppService(
            IMapper mapper,
            IPropertyRepository propertyRepository,
            IEventStoreRepository eventStoreRepository,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
        {
            _mapper = mapper;
            _propertyRepository = propertyRepository;
            _eventStoreRepository = eventStoreRepository;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public IEnumerable<PropertyViewModel> GetAll()
        {
            return _propertyRepository.GetAll().ProjectTo<PropertyViewModel>(_mapper.ConfigurationProvider);
        }

        public PropertyViewModel GetById(Guid id)
        {
            return _mapper.Map<PropertyViewModel>(_propertyRepository.GetById(id));
        }

        public async Task<object> Register(RegisterNewPropertyViewModel registerNewPropertyViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewPropertyCommand>(registerNewPropertyViewModel);
            var registerResponse = await _bus.SendCommand(registerCommand);

            return _notifications.HasNotifications() ? registerResponse : _mapper.Map<PropertyViewModel>((Property)registerResponse);
        }

        public void Update(UpdatePropertyViewModel updatePropertyViewModel)
        {
            var updateCommand = _mapper.Map<UpdatePropertyCommand>(updatePropertyViewModel);
            _bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemovePropertyCommand(id);
            _bus.SendCommand(removeCommand);
        }

        public IList<PropertyHistoryData> GetAllHistory(Guid id)
        {
            return PropertyHistory.ToJavaScriptPropertyHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}