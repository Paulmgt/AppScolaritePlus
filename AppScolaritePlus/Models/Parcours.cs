namespace AppScolaritePlus.Models
{
    public class Parcours
    {
        public int Id { get; set; }

        public string Intitule { get; set; }

        public string? Logo { get; set; }

        public string? Resume { get; set; }

        public string? Infos { get; set; }

        public ICollection<Modules>? Modules { get; set; } = new List<Modules>();

        public ICollection<SessionsFormation>? SessionsFormation { get; set; } = new List<SessionsFormation>();

    }
}
