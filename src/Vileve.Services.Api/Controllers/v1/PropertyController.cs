using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vileve.Application.EventSourcedNormalizers.Property;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Property;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;

namespace Vileve.Services.Api.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/properties")]
    [Produces("application/json")]
    public class PropertyController : ApiController
    {
        private readonly IPropertyAppService _propertyAppService;
        private readonly DomainNotificationHandler _notifications;

        public PropertyController(
            IPropertyAppService propertyAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _propertyAppService = propertyAppService;
            _notifications = (DomainNotificationHandler)notifications;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult Get()
        {
            var response = _propertyAppService.GetAll();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PropertyViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult Get(Guid id)
        {
            var response = _propertyAppService.GetById(id);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PropertyViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] RegisterNewPropertyViewModel property)
        {
            var response = await _propertyAppService.Register(property);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult Put(Guid id, [FromBody] UpdatePropertyViewModel property)
        {
            property.Id = id;
            _propertyAppService.Update(property);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult Delete(Guid id)
        {
            _propertyAppService.Remove(id);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("history/{id:guid}")]
        [ProducesResponseType(typeof(IList<PropertyHistoryData>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult History(Guid id)
        {
            var response = _propertyAppService.GetAllHistory(id);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}