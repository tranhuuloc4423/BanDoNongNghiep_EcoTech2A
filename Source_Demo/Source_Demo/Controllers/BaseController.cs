using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Source_Demo.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private string _accessToken = string.Empty;
        private string _userId = string.Empty;

        protected IMapper mapper => _mapper ?? (_mapper = HttpContext.RequestServices.GetService<IMapper>());
        protected IHttpContextAccessor httpContextAccessor => _httpContextAccessor ?? (_httpContextAccessor = HttpContext.RequestServices.GetService<IHttpContextAccessor>());
        protected string accessToken => _accessToken ?? (_accessToken = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value);
        protected string userId => _userId ?? (_userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
    }
}
