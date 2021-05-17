﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISAP.Core.Enum
{
    public enum EstadoServicioMaestro
    {
        [Description("Activo")]
        Activo = 1,
        [Description("Cortada")]
        Cortada = 2,
        [Description("Clausurada")]
        Clausurada  = 3,
        [Description("Levantada")]
        Levantada = 4
    }


    public enum Servicios
    {

        [Description("Agua")]
        Agua = 1,

        [Description("Desague")]
        Desague = 2,

        [Description("Agua y Desague")]
        AguaDesague = 3
    }
    public enum CategoriaCliente
    {
        [Description("Social")]
        Social = 1,
        [Description("Domestico")]
        Domestico = 2,
        [Description("Comercial")]
        Comercial = 3,
        [Description("Industrial")]
        Industrial = 4,
        [Description("Estatal")]
        Estatal = 5,
    }
}