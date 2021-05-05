using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Core.Entities
{
    public class Ciclos
    {
        public int CiclosId { get; set; }
        public DateTime Año { get; set; }
        public DateTime Mes { get; set; }
        public DateTime LecturaInicial { get; set; }
        public DateTime LecturaFinal { get; set; }
        public DateTime EmisionInicial { get; set; }
        public DateTime EmisionFinal { get; set; }
        public DateTime DistribucionInicial { get; set; }
        public DateTime DistribucionFinal { get; set; }
        public DateTime CorteInicial { get; set; }
        public DateTime CorteFinal { get; set; }
        public DateTime Facturacion { get; set; }
    }
}
