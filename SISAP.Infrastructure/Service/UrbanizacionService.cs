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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "UrbanizacionService" en el código y en el archivo de configuración a la vez.
    public class UrbanizacionService : _BaseContext, IUrbanizacionService
    {
        public IEnumerable<Urbanizacion> GetAll()
        {
            using (var dbContext = GetSISAPDBContext())
            {
                return dbContext.Urbanizacions.OrderBy(o => o.UrbanizacionId).ToList();
            }
        }
        public Urbanizacion RegistroUrbanizacion(string Codigo, string urbanizaciones)
        {
            using (var dbContext = GetSISAPDBContext())
            {
                return dbContext.Urbanizacions.FirstOrDefault(u => u.TipoUrbanizacion == Codigo && u.Urbanizaciones == urbanizaciones);
            }
        }
    }
    
    }

