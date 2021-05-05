using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISAP.Core.Entities;

namespace SISAP.Core.Interfaces
{
    public interface ICiclosService
    {
        IEnumerable<Ciclos> GetAll();
        Ciclos RegistrarCiclos(DateTime Año, DateTime Mes, DateTime LecturaInicial, DateTime LecturaFinal, 
        DateTime EmisionIncial, DateTime EmisionFinal, DateTime DistribucionInicial, DateTime DistribucionFinal, DateTime CorteInicial, DateTime CorteFinal, DateTime Facturacion);
    }
}
