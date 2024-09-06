using System.Text.Json.Serialization;

namespace ApiA.DTO
{
    public class ActorDTO
    {
        [JsonPropertyName("idActor")]
        public int IdActor { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("apellido")]
        public string Apellido { get; set; }

        [JsonPropertyName("fechaNacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [JsonPropertyName("nacionalidad")]
        public string? Nacionalidad { get; set; }

        [JsonPropertyName("generoBografia")]
        public string? GeneroBiografia { get; set; }

        [JsonPropertyName("premios")]
        public string? Premios { get; set; }

        [JsonPropertyName("numeroPeliculas")]
        public int? NumeroPeliculas { get; set; }


    }
}
