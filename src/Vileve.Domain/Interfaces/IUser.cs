using System.Collections.Generic;
using System.Security.Claims;

namespace Vileve.Domain.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        string Token { get; }
        string ConsultorId { get; }
    }
}