using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UserManagementApi.Domain.Models;

namespace UserManagementApi.Infrastructure.Persistence.MongoDb
{
    public class MongoDbUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [BsonElement("apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [BsonElement("cedula")]
        public string Cedula { get; set; } = string.Empty;

        [BsonElement("correoElectronico")]
        public string CorreoElectronico { get; set; } = string.Empty;

        [BsonElement("fechaUltimoAcceso")]
        public DateTime FechaUltimoAcceso { get; set; }

        public static MongoDbUser FromDomain(User user)
        {
            return new MongoDbUser
            {
                Id = string.IsNullOrEmpty(user.Id) ? null : user.Id,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Cedula = user.Cedula,
                CorreoElectronico = user.CorreoElectronico,
                FechaUltimoAcceso = user.FechaUltimoAcceso
            };
        }

        public User ToDomain()
        {
            return new User
            {
                Id = Id,
                Nombre = Nombre,
                Apellidos = Apellidos,
                Cedula = Cedula,
                CorreoElectronico = CorreoElectronico,
                FechaUltimoAcceso = FechaUltimoAcceso
            };
        }
    }
}