using Coink.Usuarios.Application.Common;
using Coink.Usuarios.Application.UseCases.Command;
using Coink.Usuarios.Application.UseCases.Query;
using Coink.Usuarios.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegisterUserCommand command)
    {
        try
        {
            var userId = await _mediator.Send(command);
            return Ok(ApiResponse<int>.Success(userId));
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => string.IsNullOrEmpty(g.Key) ? "General" : g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            return BadRequest(ApiResponse<object>.Fail(errors, 400));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.Fail(new { Message = ex.Message }, 500));
        }
    }

    [HttpGet("consultar")]
    public async Task<IActionResult> Consultar([FromQuery] int id)
    {
        try
        {
            var userId = await _mediator.Send(new ConsultUserQuery(id));
            return Ok(ApiResponse<UsuarioDto>.Success(userId));
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => string.IsNullOrEmpty(g.Key) ? "General" : g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            return BadRequest(ApiResponse<object>.Fail(errors, 400));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.Fail(new { Message = ex.Message }, 500));
        }
    }

    [HttpGet("listar")]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var userId = await _mediator.Send(new ListUserQuery());
            return Ok(ApiResponse<List<UsuarioDto>>.Success(userId));
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => string.IsNullOrEmpty(g.Key) ? "General" : g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            return BadRequest(ApiResponse<object>.Fail(errors, 400));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.Fail(new { Message = ex.Message }, 500));
        }
    }
}
