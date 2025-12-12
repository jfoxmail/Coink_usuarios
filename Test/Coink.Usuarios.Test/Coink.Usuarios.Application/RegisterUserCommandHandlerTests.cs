using Coink.Usuarios.Application.Interfaces;
using Coink.Usuarios.Application.UseCases.Command;
using Coink.Usuarios.Domain.Entities;
using Moq;

namespace Coink.Usuarios.Test.Coink.Usuarios.Application
{
    public class RegisterUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsUserId()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(r => r.RegistrarAsync(It.IsAny<Usuario>()))
                    .ReturnsAsync(123);

            var mockParametros = new Mock<IParametrosRepository>();
            mockParametros.Setup(p => p.PaisExiste(It.IsAny<int>())).ReturnsAsync(true);
            mockParametros.Setup(p => p.DepartamentoExiste(It.IsAny<int>())).ReturnsAsync(true);
            mockParametros.Setup(p => p.MunicipioExiste(It.IsAny<int>())).ReturnsAsync(true);
            mockParametros.Setup(p => p.MunicipioPerteneceAlDepartamento(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
            mockParametros.Setup(p => p.DepartamentoPerteneceAlPais(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            var handler = new RegisterUserCommandHandler(mockRepo.Object, mockParametros.Object);

            var command = new RegisterUserCommand(
                "Juan",
                "123456789",
                1,
                1,
                1,
                "Calle Falsa 123"
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(123, result);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            var mockParametros = new Mock<IParametrosRepository>();
            mockParametros.Setup(p => p.PaisExiste(It.IsAny<int>())).ReturnsAsync(false);

            var handler = new RegisterUserCommandHandler(mockRepo.Object, mockParametros.Object);

            var command = new RegisterUserCommand(
                "Juan",
                "123456789",
                99,
                1,
                1,
                "Calle Falsa 123"
            );

            // Act & Assert
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
                () => handler.Handle(command, CancellationToken.None)
            );
        }
    }
}
