using System.Text.Json.Serialization;

namespace ApiA.DTO
{
    public class PeliculaDTO
    {
        [JsonPropertyName("idPelicula")]
        public int IdPelicula { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; } = null!;

        [JsonPropertyName("genero")]
        public string? Genero { get; set; }

        [JsonPropertyName("director")]
        public string? Director { get; set; }

        [JsonPropertyName("anioEstreno")]
        public int? AnioEstreno { get; set; }

        [JsonPropertyName("duracion")]
        public int? Duracion { get; set; }

        [JsonPropertyName("sinopsis")]
        public string? Sinopsis { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }



    }
}
