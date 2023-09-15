using Domain.Common;

namespace Domain.Entities
{
    public class Cliente : AuditableBaseEntity
    {
        private int _edad;
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public  DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public int Edad
        {
            get 
            {
                _edad = DateTime.Now.Year - FechaNacimiento.Year;

                if (DateTime.Now.DayOfYear < FechaNacimiento.DayOfYear)
                    _edad = _edad - 1;

                return _edad;
            }
        }
    }
}
