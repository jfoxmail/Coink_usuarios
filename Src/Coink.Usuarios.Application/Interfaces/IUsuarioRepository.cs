using Coink.Usuarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Usuarios.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> RegistrarAsync(Usuario usuario);
    }
}
