using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SISAP.Core.Entities;
using SISAP.Core.Interfaces;

namespace SISAP.Infrastructure.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ClienteServicee" en el código y en el archivo de configuración a la vez.
    public class ClienteService : _BaseContext, IClienteService
    {
    public IEnumerable<Cliente> GetAll()

        {
            using (var dbContext = GetSISAPDBContext())

            {
                return dbContext.Clientes.OrderBy(o => o.ClienteId).ToList();
            }
        }
        public Cliente ObtenerCliente( string Codigo, string Nombre, string Apellido)
        {
            using (var dbContext = GetSISAPDBContext())
            {
                return dbContext.Clientes.FirstOrDefault(u => u.CodigoCliente == Codigo && u.Nombre == Nombre && u.Apellido == Apellido);
            }
        }
    }
}
