using Application.Parameters;

namespace Application.Features.Clientes.Queries.GetAllClientes
{
    public class GetAllClientesParameters : RequestParameters
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
