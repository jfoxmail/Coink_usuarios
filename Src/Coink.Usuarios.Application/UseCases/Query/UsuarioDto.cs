namespace Coink.Usuarios.Domain.Entities
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public int PaisId { get; set; }
        public int DepartamentoId { get; set; }
        public int MunicipioId { get; set; }
        public string Direccion { get; set; }

    }
}
