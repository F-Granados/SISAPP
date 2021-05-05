using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Core.Entities
{
    public class Lectura
    {
        public int LecturaId { get; set; }
        public int CategoriaId { get; set; }
        public int ConsumoServicioId { get; set; }
        public DateTime Año { get; set; }
        public DateTime Mes { get; set; }
        public DateTime LecturaAnterior { get; set; }
        public DateTime LecturaActual { get; set; }
        public string Monto { get; set; }


    }
}
