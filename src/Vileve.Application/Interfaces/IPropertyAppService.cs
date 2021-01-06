using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vileve.Application.EventSourcedNormalizers.Property;
using Vileve.Application.ViewModels.v1.Property;

namespace Vileve.Application.Interfaces
{
    public interface IPropertyAppService : IDisposable
    {
        IEnumerable<PropertyViewModel> GetAll();
        PropertyViewModel GetById(Guid id);
        Task<object> Register(RegisterNewPropertyViewModel registerNewPropertyViewModel);
        void Update(UpdatePropertyViewModel updatePropertyViewModel);
        void Remove(Guid id);
        IList<PropertyHistoryData> GetAllHistory(Guid id);
    }
}