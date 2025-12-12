using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Usuarios.Application.Interfaces
{
    public interface IParametrosRepository
    {
        Task<bool> PaisExiste(int id);
        Task<bool> DepartamentoExiste(int id);
        Task<bool> MunicipioExiste(int id);

        Task<bool> MunicipioPerteneceAlDepartamento(int municipioId, int departamentoId);
        Task<bool> DepartamentoPerteneceAlPais(int departamentoId, int paisId);
    }
}
