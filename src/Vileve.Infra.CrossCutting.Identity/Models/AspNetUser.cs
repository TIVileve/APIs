using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Vileve.Domain.Interfaces;

namespace Vileve.Infra.CrossCutting.Identity.Models
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => GetName();

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public string Token => GetToken();
        public string ConsultorId => GetConsultorId();

        private string GetName()
        {
            return _accessor.HttpContext.User.Identity.Name ??
                   _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        private string GetToken()
        {
            return _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
        }

        private string GetConsultorId()
        {
            return _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "ConsultorId")?.Value;
        }
    }
}