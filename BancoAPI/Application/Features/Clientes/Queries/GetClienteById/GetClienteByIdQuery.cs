using Application.Dtos;
using Application.Dtos.Extensions;
using Application.Interface;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clientes.Queries.GetClienteById
{
    public class GetClienteByIdQuery : IRequest<Response<ClienteDto>>
    {
        public int Id { get; set; }
    }
    public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, Response<ClienteDto>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;

        public GetClienteByIdQueryHandler(IRepositoryAsync<Cliente> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }
        public async Task<Response<ClienteDto>> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _repositoryAsync.GetByIdAsync(request.Id);
            
            if(cliente == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado {request.Id}");
            }
            else
            {
                var clienteDto = cliente.ToClienteDto();
                return new Response<ClienteDto> { Data = clienteDto };
            }
        }
    }
}
