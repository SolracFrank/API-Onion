using Application.Features.Clientes.Commands.CreateClienteCommand;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : BaseApiController
    {

        //Post AQUI
        [HttpPost]
        public async Task<IActionResult> Post(CreateClienteCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
