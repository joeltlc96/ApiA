namespace ApiA.DTO
{
    public class ActorDTO
    {
        public int IdActor { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Apellido { get; set; }

        public DateOnly? FechaNacimiento { get; set; }

        public string? Nacionalidad { get; set; }

        public string? GeneroBiografia { get; set; }

        public string? Premios { get; set; }

        public int? NumeroPeliculas { get; set; }

    }
}
