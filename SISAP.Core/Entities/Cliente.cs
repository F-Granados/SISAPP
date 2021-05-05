
namespace SISAP.Core.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string UsuarioCreacion { get; set; }
        public string CodigoCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public int Telefono { get; set; }
        public int Estado { get; set; }
    }
}
