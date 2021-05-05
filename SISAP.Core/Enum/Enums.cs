using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Core.Enum
{
    public enum EstadoMedidor
    {
        [Description("Activo")]
        Pendiente = 1,
        [Description("Cortada")]
        AprobadaRV = 2,
        [Description("Clausurada")]
        Aprobada = 3,
        [Description("Levantada")]
        Levantada = 4
    }

}
