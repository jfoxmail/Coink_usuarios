using Coink.Usuarios.Application.Common;
using Coink.Usuarios.Application.UseCases.Command;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Usuarios.Test.Coink.Usuarios.Api
{
    public class UsuariosControllerTests
    {
        [Fact]
        public async Task Registrar_ValidCommand_ReturnsOkWithData()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(123);

            var controller = new UsuariosController(mockMediator.Object);

            var command = new RegisterUserCommand(
                "Juan",
                "123456789",
                1,
                1,
                1,
                "Calle Falsa 123"
            );

            // Act
            var result = await controller.Registrar(command) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            dynamic response = result.Value;
            Assert.Equal(123, (int)response.Data);
        }

        [Fact]
        public async Task Registrar_InvalidCommand_ReturnsBadRequestWithErrors()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new FluentValidation.ValidationException(new[]
                        {
                    new FluentValidation.Results.ValidationFailure("PaisId", "El país no existe")
                        }));

            var controller = new UsuariosController(mockMediator.Object);

            var command = new RegisterUserCommand(
                "Juan",
                "123456789",
                99, // País inexistente
                1,
                1,
                "Calle Falsa 123"
            );

            // Act
            var result = await controller.Registrar(command) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);

            // Convertimos a ApiResponse<object>
            var response = result.Value as ApiResponse<object>;
            Assert.NotNull(response);

            // Cast de Errors a Dictionary<string, string[]>
            var errors = response.Errors as Dictionary<string, string[]>;
            Assert.NotNull(errors);
            Assert.True(errors.ContainsKey("PaisId"));
            Assert.Equal("El país no existe", errors["PaisId"][0]);
        }
    }
}
