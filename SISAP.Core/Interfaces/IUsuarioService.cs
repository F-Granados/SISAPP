using SISAP.Core.Entities;
using System.Collections.Generic;

namespace SISAP.Core.Interfaces
{
    public interface IUsuarioService
    {
        IEnumerable<Usuario> GetAll();
        Usuario SingIn(string user, string password);
    }
}
