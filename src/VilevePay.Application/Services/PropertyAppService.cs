using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using VilevePay.Application.EventSourcedNormalizers.Property;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Property;
using VilevePay.Domain.Commands.Property;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;
using VilevePay.Infra.Data.Repository.EventSourcing;

namespace VilevePay.Application.Services
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