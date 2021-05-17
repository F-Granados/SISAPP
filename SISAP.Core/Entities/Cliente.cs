
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int UrbanizacionId { get; set; }
        public int ManzanaId { get; set; }
        public int ServicioId { get; set; }
        public int CategoriaId { get; set; }
        public string NumeroMedidor { get; set; }
        public int EstadoServicioId { get; set; }

        [NotMapped]
        public string UrbanizacionNombre { get; set; }

        [NotMapped]
        public string ManzanaNombre { get; set; }
        
        [NotMapped]
        public int Annio { get; set; }
        
        [NotMapped]
        public int Mes { get; set; }
        
        [NotMapped]
        public decimal CantidadLectura { get; set; }
        
        [NotMapped]
        public decimal Consumo { get; set; }
        
        [NotMapped]
        public decimal Promedio { get; set; }
        
        [NotMapped]
        public string Alerta { get; set; }

        public virtual EstadoServicio EstadoServicio { get; set; }


        [NotMapped]
        public string NombreCompleto
        {
            get { return string.Format("{0}, {1}", this.Apellido, this.Nombre); }
        }

        [NotMapped]
        public string DireccionStr
        {
            get { return string.Format("{0}, {1}, {2}", "Urb. "+this.UrbanizacionNombre, "Mz." + this.ManzanaNombre, "Lt. " + this.Direccion); }
        }


    }
}
