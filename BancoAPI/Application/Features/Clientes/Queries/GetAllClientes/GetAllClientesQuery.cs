using Application.Dtos;
using Application.Dtos.Extensions;
using Application.Interface;
using Application.Specifications;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Clientes.Queries.GetAllClientes
{
    public class GetAllClientesQuery : IRequest<PageResponse<List<ClienteDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }

    public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, PageResponse<List<ClienteDto>>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;

        public GetAllClientesQueryHandler(IRepositoryAsync<Cliente> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<PageResponse<List<ClienteDto>>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _repositoryAsync.ListAsync(new PagedClienteSpecification(
                request.PageSize,
                request.PageNumber,
                request.Nombre,
                request.Apellido
                ));
           
            var clienteDto = clientes.ToClienteDto();

            return new PageResponse<List<ClienteDto>>(clienteDto.ToList(), request.PageNumber, request.PageSize);
        }
    }
}
