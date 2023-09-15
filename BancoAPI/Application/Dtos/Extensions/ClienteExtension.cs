using Domain.Entities;

namespace Application.Dtos.Extensions
{
    public static class ClienteExtension
    {
        public static ClienteDto ToClienteDto (this Cliente cliente)
        {
            return new ClienteDto
            {
                Id = cliente.Id,
                Apellido = cliente.Apellido,
                Direccion = cliente.Direccion,
                Edad = cliente.Edad,
                Email = cliente.Email,
                FechaNacimiento = cliente.FechaNacimiento,
                Nombre = cliente.Nombre,
                Telefono = cliente.Telefono
            };
        }
        public static IEnumerable<ClienteDto> ToClienteDto(this IEnumerable<Cliente> cliente)
        {
            return cliente.Select(cliente => cliente.ToClienteDto());
        }
    }
   
}
