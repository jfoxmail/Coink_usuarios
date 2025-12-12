using AutoMapper;
using Coink.Usuarios.Application.Interfaces;
using Coink.Usuarios.Application.UseCases.Query;
using Coink.Usuarios.Domain.Entities;
using MediatR;

public class ListUserQueryHandler : IRequestHandler<ListUserQuery, List<UsuarioDto>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;


    public ListUserQueryHandler(
        IUsuarioRepository usuarioRepository,
        IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;

    }

    public async Task<List<UsuarioDto>> Handle(ListUserQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<UsuarioDto>>(await _usuarioRepository.ListarAsync());
    }
}
