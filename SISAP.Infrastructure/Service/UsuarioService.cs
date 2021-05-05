using SISAP.Core.Entities;
using SISAP.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SISAP.Infrastructure.Service
{
    public class UsuarioService : _BaseContext, IUsuarioService
    {
        public IEnumerable<Usuario> GetAll()
        {
            using (var dbContext = GetSISAPDBContext())
            {
                return dbContext.Usuarios.OrderBy(o => o.UsuarioId).ToList();
            }
        }
        public Usuario SingIn(string user, string password)
        {
            using (var dbContext = GetSISAPDBContext())
            {
                return dbContext.Usuarios.FirstOrDefault(u => u.usuario == user && u.Password == password);
            }
        }

    }
}
