using Application.Interface;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clientes.Commands.UpdateClienteCommand
{
    public class UpdateClienteCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
    }
    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;

        public UpdateClienteCommandHandler(IRepositoryAsync<Cliente> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repositoryAsync.GetByIdAsync(request.Id);
            if(cliente == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                ClienteToRequest(cliente, request); 
                await _repositoryAsync.UpdateAsync(cliente);
            }
            return new Response<int>(cliente.Id);
        }
        void ClienteToRequest(Cliente cliente, UpdateClienteCommand request)
        {
            cliente.Nombre = request.Nombre;
            cliente.Telefono = request.Telefono;
            cliente.Email = request.Email;
            cliente.Apellido = request.Apellido;
            cliente.Direccion = request.Direccion;
            cliente.FechaNacimiento = request.FechaNacimiento;
        }
    }
}
