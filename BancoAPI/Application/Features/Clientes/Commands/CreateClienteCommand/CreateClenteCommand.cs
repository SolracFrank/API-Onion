using Application.Wrappers;
using MediatR;

namespace Application.Features.Clientes.Commands.CreateClienteCommand
{
    public class CreateClenteCommand : IRequest<Response<int>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

    }
    public class CreateClienteCommandHandler : IRequestHandler<CreateClenteCommand, Response<int>>
    {
        public async Task<Response<int>> Handle(CreateClenteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
