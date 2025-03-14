namespace UserManagementApi.Domain.Models
{
    public class User
    {
        public string? Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public DateTime FechaUltimoAcceso { get; set; }
    }
}