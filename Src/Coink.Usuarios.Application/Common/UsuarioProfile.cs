using AutoMapper;
using Coink.Usuarios.Domain.Entities;

namespace Coink.Usuarios.Application.Common
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
