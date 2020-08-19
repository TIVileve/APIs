using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VilevePay.Application.EventSourcedNormalizers.Property;
using VilevePay.Application.ViewModels.v1.Property;

namespace VilevePay.Application.Interfaces
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