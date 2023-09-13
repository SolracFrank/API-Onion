using Application.Interface;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clientes.Commands.CreateClienteCommand
{
    public class CreateClienteCommand : IRequest<Response<int>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

    }
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;

        public CreateClienteCommandHandler(IRepositoryAsync<Cliente> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente();
            ClienteToRequest(cliente, request);

            var data = await _repositoryAsync.AddAsync(cliente);

            return new Response<int>(data.Id);        
        }

        void ClienteToRequest (Cliente cliente, CreateClienteCommand request) {
            cliente.Nombre = request.Nombre;
            cliente.Telefono = request.Telefono;
            cliente.Email = request.Email;
            cliente.Apellido = request.Apellido;
            cliente.Direccion = request.Direccion;
            cliente.FechaNacimiento = request.FechaNacimiento;
            cliente.Created = DateTime.UtcNow;
            cliente.CreatedBy = "default";
        }
    }
}
