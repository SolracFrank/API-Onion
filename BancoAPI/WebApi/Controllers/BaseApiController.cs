using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/v(version:apiVersion)/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediatR;
        protected IMediator Mediator => _mediatR ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
