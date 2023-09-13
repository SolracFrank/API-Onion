using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediatR;
        protected IMediator Mediator => _mediatR ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
