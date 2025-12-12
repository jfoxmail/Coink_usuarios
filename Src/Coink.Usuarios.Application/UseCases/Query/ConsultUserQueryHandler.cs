using AutoMapper;
using Coink.Usuarios.Application.Interfaces;
using Coink.Usuarios.Application.UseCases.Query;
using Coink.Usuarios.Domain.Entities;
using MediatR;

public class ConsultUserQueryHandler : IRequestHandler<ConsultUserQuery, UsuarioDto>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;


    public ConsultUserQueryHandler(
        IUsuarioRepository usuarioRepository,
        IParametrosRepository parametrosRepository,
        IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;

    }

    public async Task<UsuarioDto> Handle(ConsultUserQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<UsuarioDto>(await _usuarioRepository.ConsultarAsync(request.Id));
    }
}
